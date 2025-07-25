using CRM_Interface.Dtos;
using CRM_Interface.Interfaces;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace CRM_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroqAiController : ControllerBase
    {
        private readonly IGroqAIService _aiService;

        public GroqAiController(IGroqAIService aiService)
        {
            _aiService = aiService;
        }


        //[HttpGet("ai/stream")]
        //public async Task StreamAI([FromQuery] string prompt)
        //{
        //    Response.Headers.Add("Content-Type", "text/event-stream");

        //    await foreach (var chunk in _aiService.GetGroqStreamAsync(prompt))
        //    {
        //        var json = JsonDocument.Parse(chunk);
        //        var delta = json.RootElement.GetProperty("choices")[0].GetProperty("delta");

        //        if (delta.TryGetProperty("content", out var content))
        //        {
        //            await Response.WriteAsync($"data: {content.GetString()}\n\n");
        //            await Response.Body.FlushAsync();
        //        }
        //    }

        //    // ابعت Done عشان الـ Frontend يعرف يقفل
        //    await Response.WriteAsync("data: [DONE]\n\n");
        //    await Response.Body.FlushAsync();
        //}

        [HttpGet("ai/stream")]
        public async Task StreamAI([FromQuery] string prompt)
        {
            HttpContext.Features.Get<IHttpResponseBodyFeature>()?.DisableBuffering();

            Response.Headers.Add("Content-Type", "text/event-stream");
            Response.Headers.Add("Cache-Control", "no-cache");
            Response.Headers.Add("X-Accel-Buffering", "no");
            Response.Headers.Add("Connection", "keep-alive");

            await Response.Body.FlushAsync(); // عشان يبدأ الـ Stream

            await foreach (var chunk in _aiService.GetGroqStreamAsync(prompt))
            {
                var json = JsonDocument.Parse(chunk);
                var delta = json.RootElement.GetProperty("choices")[0].GetProperty("delta");

                if (delta.TryGetProperty("content", out var content))
                {
                    await Response.WriteAsync($"data: {content.GetString()}\n\n");
                    await Response.Body.FlushAsync();
                }
            }

            await Response.WriteAsync("data: [DONE]\n\n");
            await Response.Body.FlushAsync();
        }

    }
}
