using System.IO;
using Inbound.Parsers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables()
    .Build();

builder.Configuration.AddConfiguration(configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();
app.MapGet("/", () =>
{
    const string content = @"
        <html>
            <head>
                <title>SendGrid Incoming Parse</title>
            </head>
            <body>
                <h1>You have successfully launched the server!</h1>
                Check out <a href=""https://github.com/KoditkarVedant/sendgrid-inbound"">the documentation</a> 
                on how to use this software to utilize the SendGrid Inbound Parse webhook.
            </body>
        </html>
    ";
    const string contentType = "text/html";
    return Results.Content(content, contentType);
});

app.MapPost("/inbound", async (HttpContext context) =>
{
    var inboundParser = await InboundWebhookParser.Create(context.Request.Body);
    var inboundEmail = inboundParser.Parse();
    return Results.Ok();
});

await app.RunAsync();

public partial class Program
{
}