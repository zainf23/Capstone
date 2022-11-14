namespace Lab.Pages.DataClasses
{
    public class TeamMeeting
    {
        public int teamMeetingID { get; set; }

        public int projectID { get; set; }

        public string meetingTitle { get; set; }

        public string meetingDate { get; set; }

        public string meetingTime { get; set; }

        public string meetingLocation { get; set; }

        public int attended { get; set; }

        public string meetingSummary { get; set; }

        public string meetingPlan { get; set; }

    }
}
