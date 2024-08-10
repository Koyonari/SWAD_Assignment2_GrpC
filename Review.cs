namespace SWAD_Assignment2_GrpC
{
    public class Review
    {
        //Properties
        public int Id { get; set; }
        public int ReviewRating { get; set; }
        public string ReviewFeedback { get; set; }
        public Booking ChosenBooking { get; set; }

        //Constructor
        public Review(int id, int reviewRating, string reviewFeedback, Booking chosenBooking)
        {
            Id = id;
            ReviewRating = reviewRating;
            ReviewFeedback = reviewFeedback;
            ChosenBooking = chosenBooking;
        }
    }
}
