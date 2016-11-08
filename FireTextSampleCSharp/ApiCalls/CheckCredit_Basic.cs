using FireTextSampleCSharp.Acks;
using System;
using System.Net.Http;
using System.Text;

namespace FireTextSampleCSharp.ApiCalls
{
    public class CheckCredit_Basic
    {
        /// <summary>
        /// Gets your current credit with FireText. You can build on this sample using the docs - https://www.firetext.co.uk/docs#credit
        /// </summary>
        /// <param name="baseUrl">The firetext base api url</param>
        /// <param name="apiKey">Your api key</param>
        public async void CheckCredit(string baseUrl, string apiKey)
        {
            string apiFunction = "credit";

            using (var client = new HttpClient())
            {
                try
                {
                    // Build the query
                    StringBuilder qb = new StringBuilder();
                    qb.Append($"?apiKey={apiKey}");

                    string fullUrl = baseUrl + apiFunction + qb.ToString();

                    // Call the api
                    var response = await client.GetAsync(fullUrl);
                    response.EnsureSuccessStatusCode(); // Throw if not success
                    var stringResponse = await response.Content.ReadAsStringAsync();

                    // Format the response
                    FTCreditResponse ftResponse = new FTCreditResponse(stringResponse);

                    // Get the number of credits.
                    var numberOfCredits = ftResponse.GetCreditsLeft();
                }
                catch (HttpRequestException e)
                {
                    Console.WriteLine($"Request exception: {e.Message}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Exception: {ex.Message}");
                }
            }
        }
    }
}
