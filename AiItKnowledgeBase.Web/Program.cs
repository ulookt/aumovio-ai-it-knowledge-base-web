using AiItKnowledgeBase.Web.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddSingleton<KnowledgeBaseService>();
builder.Services.AddSingleton<TextRetrievalService>();
builder.Services.AddSingleton<OpenAiService>();
builder.Services.AddSingleton<AnswerCacheService>();
builder.Services.AddSingleton<WebKnowledgeSourceService>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorPages()
   .WithStaticAssets();

app.Run();
