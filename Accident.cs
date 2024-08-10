namespace SWAD_Assignment2_GrpC
{
    public class Accident
    {
        //Properties
        public string Location { get; set; }
        public DateTime Time { get; set; }
        public string Type { get; set; }
        public Booking ChosenBooking { get; set; }

        //Constructor
        public Accident(string location, DateTime time, string type, Booking chosenBooking)
        {
            Location = location;
            Time = time;
            Type = type;
            ChosenBooking = chosenBooking;
        }
    }
}
