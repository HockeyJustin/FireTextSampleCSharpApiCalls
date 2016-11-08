using FireTextSampleCSharp.Acks;
using System;
using System.Threading.Tasks;

namespace FireTextSampleCSharp
{
    class Program
    {
        /// <summary>
        /// Sample application for sending SMS messages via the FireText API.
        /// The code in this project should port to .NET Core without any problems.
        /// 
        /// NOTE 0: You will need to set up a https://www.firetext.co.uk/ account for this to work
        /// NOTE 1: Change the API key for your own, which can be set at https://app.firetext.co.uk/settings/manage/api/ 
        /// NOTE 2: Each class also has parameters for which you will need to change the values.
        /// NOTE 3: You can easily extend these samples using the documentation here - https://www.firetext.co.uk/docs
        /// NOTE 4: Most of this code will port to .NET Core, with the exception of url parsing in FTReceivedMessagesResponse.cs
        /// NOTE 5: Simply review debug this application to understand the code. Good code is self documenting right?
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            string baseUrl = "https://www.firetext.co.uk/api/";
            string apiKey = "<YOUR_API_KEY_HERE>";

            // Run the sample methods.
            Task.Run(() => new FireText().RunMethods(baseUrl, apiKey));
            Console.ReadLine();
        }
    }

    public class FireText
    {
        public async void RunMethods(string baseUrl, string apiKey)
        {
            // Send a basic sms message from C#
            var sendBasic = new ApiCalls.SendSms_Basic();
            await Task.Run(() => sendBasic.SendSms(baseUrl, apiKey));

            // Check your firetext credit
            var creditCheck = new ApiCalls.CheckCredit_Basic();
            await Task.Run(() => creditCheck.CheckCredit(baseUrl, apiKey));

            // If you have set up a number and people have replied, check the responses
            var receiveBasic = new ApiCalls.ReceivedMessages_Basic();
            await Task.Run(() => receiveBasic.ReceivedMessages(baseUrl, apiKey));
        }
    }
}
