using System.Text.RegularExpressions;

namespace Crawler
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            string websiteUrl = args[0];
            HttpClient httpClient = new HttpClient();
            HttpResponseMessage response = await httpClient.GetAsync(websiteUrl);
            if (response.IsSuccessStatusCode)
            {
                var htmlContent = await response.Content.ReadAsStringAsync();
                var regex = new Regex("[a-z]+[a-z0-9-]*@[a-z-]+\\.[a-z]+", RegexOptions.IgnoreCase);

                var emailAdress = regex.Matches(htmlContent);

                foreach(var x in emailAdress)
                {
                    Console.WriteLine(x.ToString());   
                }
            }
            Console.WriteLine("");
        }
    }
}