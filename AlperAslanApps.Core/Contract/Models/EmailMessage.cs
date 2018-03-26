namespace AlperAslanApps.Core.Contract.Models
{
    public class EmailMessage
    {
        public string FromAddress { get; set; }
        public string ToAddress { get; set; }
        public string Subject { get; set; }
        public string PlainTextContent { get; set; }
        public string HtmlContent { get; set; }
    }
}
