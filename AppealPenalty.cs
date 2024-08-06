using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWAD_Assignment2_GrpC
{
    internal class AppealPenalty
    {
        private string appealReason;
        private DateTime appealDate;
        private bool appealStatus;
        private bool appealDecision;
        private Penalty chosenPenalty;

        //Properties
        public string AppealReason
        {
            get { return appealReason; }
            set { appealReason = value; }
        }

        public DateTime AppealDate
        {
            get { return appealDate; }
            set { appealDate = value; }
        }

        public bool AppealStatus
        {
            get { return appealStatus; }
            set { appealStatus = value; }
        }

        public bool AppealDecision
        {
            get { return appealDecision; }
            set { appealDecision = value; }
        }

        public Penalty ChosenPenalty
        {
            get { return chosenPenalty; }
            set { chosenPenalty = value; }
        }

        //Constructor
        public AppealPenalty(string appealReason, DateTime appealDate, bool appealStatus, bool appealDecision, Penalty chosenPenalty)
        {
            this.appealReason = appealReason;
            this.appealDate = appealDate;
            this.appealStatus = appealStatus;
            this.appealDecision = appealDecision;
            this.chosenPenalty = chosenPenalty;
        }
    }
}
