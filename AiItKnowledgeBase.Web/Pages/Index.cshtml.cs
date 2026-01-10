using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using AiItKnowledgeBase.Web.Services;

public class IndexModel : PageModel
{
    private readonly KnowledgeBaseService _kbService;
    private readonly TextRetrievalService _retrievalService;

    public IndexModel(
        KnowledgeBaseService kbService,
        TextRetrievalService retrievalService)
    {
        _kbService = kbService;
        _retrievalService = retrievalService;
    }

    [BindProperty]
    public string? UserQuestion { get; set; }

    public string? Answer { get; set; }
    public string? ErrorMessage { get; set; }

    public void OnPost()
    {
        if (string.IsNullOrWhiteSpace(UserQuestion))
        {
            ErrorMessage = "Please enter a question.";
            return;
        }

        try
        {
            var content = _kbService.LoadContent();
            var chunks = _retrievalService.SplitIntoChunks(content);
            var bestChunk = _retrievalService.FindBestChunk(chunks, UserQuestion);

            Answer = bestChunk;
        }
        catch (Exception ex)
        {
            ErrorMessage = ex.Message;
        }
    }
}
