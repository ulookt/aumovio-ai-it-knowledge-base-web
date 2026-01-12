Aumovio AI IT Knowledge Base Web Application

This project is a simple web-based IT support assistant built with C# and ASP.NET Core (.NET 6+).It loads IT documentation, retrieves the most relevant information based on a user question, and uses an OpenAI-compatible LLM API to generate context-aware answers.

The application demonstrates a lightweight Retrieval-Augmented Generation (RAG) approach for internal IT support use cases.

Prerequisites
* .NET SDK 6 or later
* OpenAI API key (or compatible provider)

How to run the application:

1. Clone the repository:

git clone https://github.com/ulookt/aumovio-ai-it-knowledge-base-web.git
cd aumovio-ai-it-knowledge-base-web/AiItKnowledgeBase.Web

2. Configure the API keyOpen the file:
In the appsettings.json file
Replace the placeholder value: "ApiKey": "YOUR_OPENAI_API_KEY_HERE"
The API key is required only in this file.Do not add the key to appsettings.Development.json.

Note: 
- The API key is required only in this file.Do not add the key to appsettings.Development.json.
- Only OpenAI API key is supported for this web application.

3. Run the applicationStart the web application by running:
dotnet run

4. Open the applicationOpen a browser and navigate to: http://localhost:5202
What the application does:
1. Loads IT support content from a local text file (KnowledgeBase.txt)
2. Optionally loads IT documentation from a website
3. Splits the content into fixed-size chunks
4. Selects the most relevant chunk using keyword-based matching
5. Sends the selected content and user question to an OpenAI-compatible API
6. Displays a detailed, user-friendly answer
7. Caches previous answers in memory to reduce API usage

Architecture overview:
1. The user enters a question through the web interface
2. Documentation is retrieved from the configured source
3. Relevant content is selected and sent to the LLM API
4. The generated answer is returned to the user

Assumptions and limitations:
* Uses keyword-based retrieval (no embeddings)
* Only one documentation chunk is used per question
* In-memory caching only
* Designed for demo and small-scale internal use
Improvement ideas:

* Use vector embeddings for semantic search
* Retrieve multiple relevant chunks
* Add persistent caching or logging
* Improve website parsing
* Add authentication and access control
