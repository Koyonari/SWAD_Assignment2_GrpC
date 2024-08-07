using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWAD_Assignment2_GrpC
{
    public class Penalty
    {
        private int penaltyId;
        private float penaltyAmount;
        private string penaltyReason;
        private string penaltyDescription;
        private DateTime expirationDate;
        private Booking chosenBooking;

        // Properties
        public int PenaltyId
        {
            get { return penaltyId; }
            set { penaltyId = value; }
        }

        public float PenaltyAmount
        {
            get { return penaltyAmount; }
            set { penaltyAmount = value; }
        }

        public string PenaltyReason
        {
            get { return penaltyReason; }
            set { penaltyReason = value; }
        }

        public string PenaltyDescription
        {
            get { return penaltyDescription; }
            set { penaltyDescription = value; }
        }

        public DateTime ExpirationDate
        {
            get { return expirationDate; }
            set { expirationDate = value; }
        }

        public Booking ChosenBooking
        {
            get { return chosenBooking; }
            set { chosenBooking = value; }
        }

        // Constructor
        public Penalty(int penaltyId, float penaltyAmount, string penaltyReason, string penaltyDescription, DateTime expirationDate, Booking chosenBooking)
        {
            this.penaltyId = penaltyId;
            this.penaltyAmount = penaltyAmount;
            this.penaltyReason = penaltyReason;
            this.penaltyDescription = penaltyDescription;
            this.expirationDate = expirationDate;
            this.chosenBooking = chosenBooking;
        }


        // Casey's Methods
        // -----------------------------------------------------------------------------------------------
        public void ApplyPenalty()
        {
            // Logic to apply the penalty
            Console.WriteLine($"Penalty of ${penaltyAmount} applied for reason: {penaltyReason}");
        }

        public bool IsExpired()
        {
            return DateTime.Now > expirationDate;
        }

        public override string ToString()
        {
            return $"Penalty ID: {penaltyId}, Amount: ${penaltyAmount}, Reason: {penaltyReason}, Expires: {expirationDate.ToShortDateString()}";
        }
        // -----------------------------------------------------------------------------------------------
        // End of Casey's methods
    }
}
