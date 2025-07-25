using CRM_Interface.Interfaces;
using System.Net.Http.Json;

namespace CRM_Infrastraction.Services
{
    public class GroqAIService : IGroqAIService
    {
        private readonly HttpClient _httpClient;

        public GroqAIService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async IAsyncEnumerable<string> GetGroqStreamAsync(string prompt)
        {
            var request = new
            {
                model = "llama3-8b-8192",
                messages = new[]
                {
                    new { role = "system", content = "انت مساعد ذكي ومرح، ترد على الأسئلة باللهجة المصرية بشكل طبيعي من غير مبالغة أو كلام عشوائي." },
                    new { role = "user", content = prompt }
                },
                stream = true
            };

            var response = await _httpClient.PostAsJsonAsync("https://api.groq.com/openai/v1/chat/completions", request);

            response.EnsureSuccessStatusCode();

            using var stream = await response.Content.ReadAsStreamAsync();
            using var reader = new StreamReader(stream);

            while (!reader.EndOfStream)
            {
                var line = await reader.ReadLineAsync();

                if (string.IsNullOrWhiteSpace(line) || !line.StartsWith("data:"))
                    continue;

                var jsonData = line.Substring(5).Trim();
                if (jsonData == "[DONE]") yield break;

                yield return jsonData;
            }
        }
    }
}
