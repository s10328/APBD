using System.Text.RegularExpressions;

namespace Crawler
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            string websiteUrl = args[0];

            if (websiteUrl == null)
            {
                throw new ArgumentNullException("Musisz podać adres strony jako argument!");
            }
            if (!Uri.IsWellFormedUriString(websiteUrl, UriKind.Absolute))
            {
                throw new ArgumentException("To nie jest prawidłowy adres url!");
            }

            HttpClient httpClient = new HttpClient();
            HttpResponseMessage response = await httpClient.GetAsync(websiteUrl);
            httpClient.Dispose();

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Błąd w czasie pobierania strony.");
            }

            if (response.IsSuccessStatusCode)
            {
                var htmlContent = await response.Content.ReadAsStringAsync();
                var regex = new Regex("[a-z]+[a-z0-9-]*@[a-z-]+\\.[a-z]+", RegexOptions.IgnoreCase);
                Match matcher = regex.Match(htmlContent);
                HashSet<string> unique = new HashSet<string>();
                while(matcher.Success)
                {
                    unique.Add(matcher.Groups[0].Value);
                    matcher = matcher.NextMatch();
                }

                if (!unique.Any())
                {
                    Console.WriteLine("Nie znaleziono żadnych adresów email.");
                }
                else
                {
                    Console.WriteLine(String.Join(", ", unique));
                }
            }

            Console.WriteLine("");

        }

    }
}
