using System.Collections;
using System.Globalization;
using System.Text;

namespace BadAdvisor.Mvc.Services
{
    public interface ISanitizerService
    {
        string SanitizeBadWords(string phrase);
    }

    public class SanitizerService : ISanitizerService
    {
        private static readonly string[] StopWords = { "fuck", "shit" };

        public string SanitizeBadWords(string phrase)
        {
            foreach (var stopWord in StopWords)
            {
                phrase = phrase.Replace(stopWord, "***", StringComparison.InvariantCultureIgnoreCase);
            }

            return phrase;
        }
    }
}
