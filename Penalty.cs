using System;

namespace SWAD_Assignment2_GrpC
{
    public class Penalty
    {
        public int Id { get; set; }
        public float PenaltyAmount { get; set; }
        public string PenaltyReason { get; set; }
        public string PenaltyDescription { get; set; }
        public DateTime ExpirationDate { get; set; }
        public Booking ChosenBooking { get; set; }

        public Penalty(int id, float penaltyAmount, string penaltyReason, string penaltyDescription, DateTime expirationDate, Booking chosenBooking)
        {
            Id = id;
            PenaltyAmount = penaltyAmount;
            PenaltyReason = penaltyReason;
            PenaltyDescription = penaltyDescription;
            ExpirationDate = expirationDate;
            ChosenBooking = chosenBooking;
        }
    }
}