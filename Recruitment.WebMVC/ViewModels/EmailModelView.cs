namespace Recruitment.WebMVC
{
    public class EmailModelView
    {
        public int CandidateId { get; set; }

        public string To { get; set; }

        public string Bcc { get; set; }

        public string Subject { get; set; }

        public string Body { get; set; }
    }
}