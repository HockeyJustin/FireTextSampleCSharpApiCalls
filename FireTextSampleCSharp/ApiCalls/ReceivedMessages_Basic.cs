using FireTextSampleCSharp.Acks;
using System;
using System.Net.Http;
using System.Text;

namespace FireTextSampleCSharp.ApiCalls
{
    public class ReceivedMessages_Basic
    {
        /// <summary>
        /// Gets peoples responses to your texts. You can build on this sample using the docs - https://www.firetext.co.uk/docs#receivesms
        /// NOTE: To receive messages, you must set up a number with FireText.
        /// </summary>
        /// <param name="baseUrl">The firetext base api url</param>
        /// <param name="apiKey">Your api key</param>
        public async void ReceivedMessages(string baseUrl, string apiKey)
        {
            string apiFunction = "receivedmessages";
            string from = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
            string to = DateTime.Now.AddDays(1).ToString("yyyy-MM-dd");
            int resultsPerPage = 100;
            int pageNumber = 1;

            using (var client = new HttpClient())
            {
                try
                {
                    // Build the query
                    StringBuilder qb = new StringBuilder();
                    qb.Append($"?apiKey={apiKey}");
                    qb.Append($"&from={from}");
                    qb.Append($"&to={to}");
                    qb.Append($"&pp={resultsPerPage}");
                    qb.Append($"&page={pageNumber}");

                    string fullUrl = baseUrl + apiFunction + qb.ToString();

                    // Call the api
                    var response = await client.GetAsync(fullUrl);
                    response.EnsureSuccessStatusCode(); // Throw if not success
                    var stringResponse = await response.Content.ReadAsStringAsync();

                    // Format the response
                    FTReceivedMessagesResponse ftResponse = new FTReceivedMessagesResponse(stringResponse);

                    // Here are the message responses.
                    var messages = ftResponse.ReceivedMessages;
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
