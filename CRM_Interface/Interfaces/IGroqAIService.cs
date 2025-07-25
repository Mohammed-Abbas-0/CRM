namespace CRM_Interface.Interfaces
{
    public interface IGroqAIService
    {
        IAsyncEnumerable<string> GetGroqStreamAsync(string prompt);

    }
}
