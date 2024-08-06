using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWAD_Assignment2_GrpC
{
    internal class Review
    {
        private int id;
        private int reviewRating;
        private string reviewFeedback;
        private Booking chosenBooking;

        //Properties
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public int ReviewRating
        {
            get { return reviewRating; }
            set { reviewRating = value; }
        }

        public string ReviewFeedback
        {
            get { return reviewFeedback; }
            set { reviewFeedback = value; }
        }

        public Booking ChosenBooking
        {
            get { return chosenBooking; }
            set { chosenBooking = value; }
        }

        //Constructor
        public Review(int id, int reviewRating, string reviewFeedback, Booking chosenBooking)
        {
            this.id = id;
            this.reviewRating = reviewRating;
            this.reviewFeedback = reviewFeedback;
            this.chosenBooking = chosenBooking;
        }
    }
}
