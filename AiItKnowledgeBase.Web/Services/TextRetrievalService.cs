namespace AiItKnowledgeBase.Web.Services
{
    public class TextRetrievalService
    {
        public List<string> SplitIntoChunks(string text, int chunkSize = 800)
        {
            var chunks = new List<string>();

            for (int i = 0; i < text.Length; i += chunkSize)
            {
                chunks.Add(text.Substring(i, Math.Min(chunkSize, text.Length - i)));
            }

            return chunks;
        }

        public string FindBestChunk(List<string> chunks, string question)
        {
            var keywords = question
                .ToLower()
                .Split(' ', StringSplitOptions.RemoveEmptyEntries);

            return chunks
                .OrderByDescending(chunk =>
                    keywords.Count(k => chunk.ToLower().Contains(k)))
                .FirstOrDefault() ?? string.Empty;
        }
    }
}
