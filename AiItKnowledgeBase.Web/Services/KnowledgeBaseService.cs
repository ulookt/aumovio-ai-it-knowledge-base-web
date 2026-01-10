using System.Text;

namespace AiItKnowledgeBase.Web.Services
{
    public class KnowledgeBaseService
    {
        private readonly string _filePath;

        public KnowledgeBaseService(IWebHostEnvironment env)
        {
            _filePath = Path.Combine(env.ContentRootPath, "Data", "KnowledgeBase.txt");
        }

        public string LoadContent()
        {
            if (!File.Exists(_filePath))
                throw new FileNotFoundException("Knowledge base file not found.");

            return File.ReadAllText(_filePath, Encoding.UTF8);
        }
    }
}
