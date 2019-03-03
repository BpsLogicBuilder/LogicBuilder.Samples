namespace CheckMySymptoms.Flow.Requests
{
    public class CommandButtonRequest
    {
        public string NewSelection { get; set; }
        public string SymtomText { get; set; }
        public bool Cancel { get; set; }
    }
}
