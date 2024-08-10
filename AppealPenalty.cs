namespace SWAD_Assignment2_GrpC
{
    public class AppealPenalty
    {
        public int Id { get; set; }
        public string AppealReason { get; set; }
        public User Appellant { get; set; }
        public Penalty Penalty { get; set; }
        public DateTime AppealDate { get; set; }
        public string AppealStatus { get; set; }
        public string AppealDescription { get; set; }

        public AppealPenalty(int id, string appealReason, User appellant, Penalty penalty, DateTime appealDate, string appealStatus, string appealDescription)
        {
            Id = id;
            AppealReason = appealReason;
            Appellant = appellant;
            Penalty = penalty;
            AppealDate = appealDate;
            AppealStatus = appealStatus;
            AppealDescription = appealDescription;
        }

        // Casey's Methods
        // -----------------------------------------------------------------------------------------------
        public void DisplayInformation()
        {
            Console.WriteLine($"Appeal Information:\nReason: {AppealReason}\nAmount: ${Penalty.PenaltyAmount}\nStatus: {AppealStatus}\nDescription: {AppealDescription}");
        }

        public void UpdateStatus(string newStatus)
        {
            AppealStatus = newStatus;
            Console.WriteLine($"Appeal status updated to: {AppealStatus}");
        }

        public string GetSummary()
        {
            return $"Appeal by {Appellant.Name} - Reason: {AppealReason}, Amount: ${Penalty.PenaltyAmount}";
        }
        // -----------------------------------------------------------------------------------------------
        // End of Casey's methods
    }
}
