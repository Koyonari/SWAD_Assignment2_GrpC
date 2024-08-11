using System;

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

        public void DisplayInformation()
        {
            Console.WriteLine($"Reason: {AppealReason}");
            Console.WriteLine($"Amount: ${Penalty.PenaltyAmount}");
            Console.WriteLine($"Status: {AppealStatus}");
            Console.WriteLine($"Description: {AppealDescription}");
        }

        public void UpdateStatus(string newStatus)
        {
            AppealStatus = newStatus;
        }

        public string GetSummary()
        {
            return $"Appeal by {Appellant.Name} - Reason: {AppealReason}, Amount: ${Penalty.PenaltyAmount}";
        }
    }
}