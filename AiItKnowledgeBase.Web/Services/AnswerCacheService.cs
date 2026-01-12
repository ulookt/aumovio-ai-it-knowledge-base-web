using System.Collections.Concurrent;

namespace AiItKnowledgeBase.Web.Services
{
    public class AnswerCacheService
    {
        private readonly ConcurrentDictionary<string, string> _cache = new();

        public bool TryGet(string question, out string answer)
        {
            return _cache.TryGetValue(question.ToLower(), out answer!);
        }

        public void Store(string question, string answer)
        {
            _cache[question.ToLower()] = answer;
        }
    }
}
