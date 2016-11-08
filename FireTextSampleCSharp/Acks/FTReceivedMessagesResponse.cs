using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;

namespace FireTextSampleCSharp.Acks
{
    public class FTReceivedMessagesResponse : FTResponseBase
    {
        public List<ReceivedMessageRow> ReceivedMessages { get; set; } = new List<ReceivedMessageRow>();

        public FTReceivedMessagesResponse(string receivedMessagesResponse) : base(receivedMessagesResponse)
        {
            // Remove the url encoding.
            this.ResponseMessage = System.Net.WebUtility.UrlDecode(this.ResponseMessage);

            // This is slightly ugly, but safer that splitting on newlines (which a user could put in a text).
            var messageRows = this.ResponseMessage.Split(new string[] { "messageID" }, StringSplitOptions.None);

            this.ResponseMessage = messageRows[0];

            if (messageRows != null && messageRows.Any())
            {
                foreach (var row in messageRows.Skip(1))
                {
                    var fullRow = "messageID" + row;

                    // For .NET CORE use: var query = Microsoft.AspNetCore.WebUtilities.QueryHelpers.ParseQuery(fullRow);
                    NameValueCollection query =  HttpUtility.ParseQueryString(fullRow);

                    string id = query["messageID"];
                    string sentTo = query["sentTo"];
                    string keyword = query["keyword"];
                    string receivedFrom = query["receivedFrom"];
                    string receivedOn = query["receivedOn"];
                    DateTime receivedOnDate = new DateTime();
                    if (!String.IsNullOrWhiteSpace(receivedOn))
                    {
                        DateTime outValue;
                        if (DateTime.TryParse(receivedOn, out outValue))
                        {
                            receivedOnDate = outValue;
                        }
                    }
                    string message = query["message"];

                    ReceivedMessageRow rowToStore = new ReceivedMessageRow(id, sentTo, keyword, receivedFrom, receivedOnDate, message);
                    ReceivedMessages.Add(rowToStore);
                }
            }
        }
    }



    public class ReceivedMessageRow
    {
        public ReceivedMessageRow(string messageId, string sentTo, string keyword, string receivedFrom, DateTime receivedOn, string message)
        {
            MessageId = messageId;
            SentTo = sentTo;
            Keyword = keyword;
            ReceivedFrom = receivedFrom;
            ReceivedOn = receivedOn;
            Message = message;
        }

        public string MessageId { get; set; }
        public string SentTo { get; set; }
        public string Keyword { get; set; }
        public string ReceivedFrom { get; set; }
        public DateTime ReceivedOn { get; set; }
        public string Message { get; set; }
    }

}
