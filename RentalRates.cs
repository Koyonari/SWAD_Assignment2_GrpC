namespace SWAD_Assignment2_GrpC
{
    public class RentalRates
    {
        public decimal DailyRate { get; set; }
        public bool IsValid()
        {
            return DailyRate >= 0;
        }
    }
}
