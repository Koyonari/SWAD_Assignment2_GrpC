namespace SWAD_Assignment2_GrpC
{
    public class Penalty
    {
        // Properties
        public int PenaltyId { get; set; }
        public float PenaltyAmount { get; set; }
        public string PenaltyReason { get;set; }
        public string PenaltyDescription { get; set; }
        public DateTime ExpirationDate { get; set; }
        public Booking ChosenBooking { get; set; }

        // Constructor
        public Penalty(int penaltyId, float penaltyAmount, string penaltyReason, string penaltyDescription, DateTime expirationDate, Booking chosenBooking)
        {
            PenaltyId = penaltyId;
            PenaltyAmount = penaltyAmount;
            PenaltyReason = penaltyReason;
            PenaltyDescription = penaltyDescription;
            ExpirationDate = expirationDate;
            ChosenBooking = chosenBooking;
        }


        // Casey's Methods
        // -----------------------------------------------------------------------------------------------
        public void ApplyPenalty()
        {
            // Logic to apply the penalty
            Console.WriteLine($"Penalty of ${PenaltyAmount} applied for reason: {PenaltyReason}");
        }

        public bool IsExpired()
        {
            return DateTime.Now > ExpirationDate;
        }

        public override string ToString()
        {
            return $"Penalty ID: {PenaltyId}, Amount: ${PenaltyAmount}, Reason: {PenaltyReason}, Expires: {ExpirationDate.ToShortDateString()}";
        }
        // -----------------------------------------------------------------------------------------------
        // End of Casey's methods
    }
}
