using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using AiItKnowledgeBase.Web.Services;

public class IndexModel : PageModel
{
    private readonly KnowledgeBaseService _kbService;
    private readonly TextRetrievalService _retrievalService;
    private readonly OpenAiService _openAiService;
    private readonly AnswerCacheService _answerCacheService;
    private readonly WebKnowledgeSourceService _webKnowledgeService;

    public IndexModel(
        KnowledgeBaseService kbService,
        TextRetrievalService retrievalService,
        OpenAiService openAiService,
        AnswerCacheService answerCacheService,
        WebKnowledgeSourceService webKnowledgeService)
    {
        _kbService = kbService;
        _retrievalService = retrievalService;
        _openAiService = openAiService;
        _answerCacheService = answerCacheService;
        _webKnowledgeService = webKnowledgeService;
    }

    [BindProperty]
    public string? UserQuestion { get; set; }

    public string? Answer { get; set; }
    public string? ErrorMessage { get; set; }

    public async Task OnPostAsync()
    {
        if (string.IsNullOrWhiteSpace(UserQuestion))
        {
            ErrorMessage = "Please enter a question.";
            return;
        }

        if (_answerCacheService.TryGet(UserQuestion, out var cachedAnswer))
        {
            Answer = cachedAnswer;
            return;
        }

        try
        {
            string content;

            try
            {
                content = await _webKnowledgeService.LoadFromUrlAsync(
                    "https://www.ibm.com/docs/en/om-jvm/5.4.0?topic=troubleshooting-checklist"
                );
            }
            catch
            {
                content = _kbService.LoadContent();
            }
            var chunks = _retrievalService.SplitIntoChunks(content);
            var bestChunk = _retrievalService.FindBestChunk(chunks, UserQuestion);

            Answer = await _openAiService.GetAnswerAsync(UserQuestion, bestChunk);

            _answerCacheService.Store(UserQuestion, Answer);
        }
        catch (Exception ex)
        {
            ErrorMessage = ex.Message;
        }
    }
}
