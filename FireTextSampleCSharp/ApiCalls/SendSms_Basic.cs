using FireTextSampleCSharp.Acks;
using System;
using System.Net.Http;
using System.Text;

namespace FireTextSampleCSharp.ApiCalls
{

    public class SendSms_Basic
    {
        /// <summary>
        /// Sends an SMS Text Message via FireText. 
        /// This does not include all possible parameters.
        /// You can build on this sample using the docs - https://www.firetext.co.uk/docs#sendingsms-xml
        /// </summary>
        /// <param name="baseUrl">The firetext base api url</param>
        /// <param name="apiKey">Your api key</param>
        public async void SendSms(string baseUrl, string apiKey)
        {
            string apiFunction = "sendsms";

            string testMessage = "This is a test";
            string from = "<ADDVALUEHERE>"; // Could be company name e.g. "My Company" or a number. Must be FireText purchased number to receive responses.
            string toNumbersCommaSeparated = "<ADDVALUEHERE>"; // e.g. "07811111222" or "07811111222, 08561111222"
            string scheduleTimeFormatted = DateTime.Now.AddMinutes(2).ToString("yyyy-MM-dd HH:mm:ss");
            string referenceNo = "Test0001";

            using (var client = new HttpClient())
            {
                try
                {
                    // Build the query
                    StringBuilder qb = new StringBuilder();
                    qb.Append($"?apiKey={apiKey}");
                    qb.Append($"&message={testMessage.Replace(" ", "+")}");
                    qb.Append($"&from={from}");
                    qb.Append($"&to={toNumbersCommaSeparated}");
                    qb.Append($"&schedule={scheduleTimeFormatted}");
                    qb.Append($"&reference={referenceNo}");

                    string fullUrl = baseUrl + apiFunction + qb.ToString();

                    // Call the api
                    var response = await client.GetAsync(fullUrl);
                    response.EnsureSuccessStatusCode(); // Throw if not success
                    var stringResponse = await response.Content.ReadAsStringAsync();

                    // Format the response
                    FTSendSmsResponse ftResponse = new FTSendSmsResponse(stringResponse);

                    var numberMessagesQueued = ftResponse.GetNumberOfMessagesQueued();


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
