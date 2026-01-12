# Aumovio AI IT Knowledge Base Web Application

## Overview

This project is a simple web-based IT Knowledge Base Assistant built with **C# and ASP.NET Core (.NET 6+)**. The application loads IT support documentation, retrieves the most relevant content based on a user question, and uses a Large Language Model (LLM) API to generate accurate, context-aware answers.

The solution demonstrates a lightweight **Retrieval-Augmented Generation (RAG)** approach suitable for internal IT support scenarios.

---

## How to Run the Application

### Prerequisites

* .NET SDK 6 or later
* An OpenAI API key (paid account)

### Setup Steps

1. Clone the repository:

   ```bash
   git clone https://github.com/ulookt/aumovio-ai-it-knowledge-base-web.git
   cd aumovio-ai-it-knowledge-base-web
   ```

2. Create or update `appsettings.Development.json` (not committed to Git):

   ```json
   {
     "OpenAI": {
       "ApiKey": "YOUR_OPENAI_API_KEY"
     }
   }
   ```

3. Run the application:

   ```bash
   dotnet run
   ```

4. Open a browser and navigate to:

   ```
   http://localhost:5202
   ```

---

## Architecture and Design Choices

### High-Level Architecture

```
User Question
   ↓
Razor Page (UI)
   ↓
Retrieval Services
   ↓
LLM (OpenAI API)
   ↓
Generated Answer
```

### Key Components

* **Razor Pages UI**

  * Simple and minimal web interface
  * Suitable for small internal tools

* **KnowledgeBaseService**

  * Loads IT documentation from a local text file

* **WebKnowledgeSourceService**

  * Optionally loads IT documentation directly from a website
  * Falls back to the local file if the website is unavailable

* **TextRetrievalService**

  * Splits content into fixed-size chunks (~800 characters)
  * Selects the most relevant chunk using keyword matching

* **OpenAiService**

  * Sends the selected chunk and user question to the LLM

* **AnswerCacheService**

  * Caches previously generated answers in memory
  * Reduces API usage and improves performance

* **Dependency Injection**

  * All services are registered via ASP.NET Core DI
  * Improves testability and separation of concerns

---

## Prompt Design

### Prompt Structure

The prompt sent to the LLM consists of:

* A **system instruction** defining the assistant as an IT support agent
* The **retrieved document chunk** as context
* The **user question**

### Why This Design

* Ensures answers are grounded in official IT documentation
* Reduces hallucinations by restricting the model to provided context
* Keeps prompts small to reduce token usage and cost

### Example System Instruction

> "You are an IT support assistant. Answer only using the provided context. If the answer is not contained in the context, advise the user to contact IT support."

---

## Assumptions and Limitations

* Uses simple keyword-based retrieval (no embeddings)
* Only the most relevant single chunk is sent to the LLM
* Website content is parsed using basic HTML stripping
* No persistent storage for cache or logs
* Designed for demo and small-scale internal usage

---

## Improvement Ideas

* Use vector embeddings for semantic search
* Support multiple retrieved chunks
* Add structured logging and monitoring
* Improve HTML parsing for web-based sources
* Add authentication and role-based access
* Introduce persistent caching (e.g., Redis)

---

## Summary

This application fulfills all required objectives of the assignment and implements several optional bonus features, including web-based data sources, in-memory caching, and prompt grounding. It demonstrates clean architecture, secure configuration handling, and production-style design principles suitable for an intern-level project.
