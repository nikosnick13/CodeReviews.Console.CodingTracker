namespace Coding_Tracker.nikosnick13
{
    internal class CodingSession
    {
        public int Id { get; set; }

        public TimeSpan StartTime { get; set; }

        public TimeSpan EndTime { get; set; }

        public TimeSpan Duration
        {
            get
            {
                return EndTime - StartTime;   
            }
        }

        // public string? Date { get; set; }
       
        // public string? Duration { get; set; }
    }
}