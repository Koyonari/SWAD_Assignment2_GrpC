namespace SWAD_Assignment2_GrpC
{
    public class BookingLocation
    {
        // Properties
        public string PickupLocation { get; set; }

        public string ReturnLocation { get; set; }

        public bool Delivery { get; set; }

        public bool ArrangedReturnLocation { get; set; }

        public float AdditionalPayment { get; set; }

        public BookingLocation() { }

        // Constructor
        public BookingLocation(string pickupLocation, string returnLocation, bool delivery, bool arrangedReturnLocation, float additionalPayment)
        {
            PickupLocation = pickupLocation;
            ReturnLocation = returnLocation;
            Delivery = delivery;
            ArrangedReturnLocation = arrangedReturnLocation;
            AdditionalPayment = additionalPayment;
        }

        // Aaron's Methods
        // -----------------------------------------------------------------------------------------------
        // Method to add the pickup location details to BookingLocation
        public void AddPickupLocationDetails(string pickupLocation, bool delivery, float additionalPayment)
        {
            this.PickupLocation = pickupLocation;
            this.Delivery = delivery;
            this.AdditionalPayment = additionalPayment;
        }
        // -----------------------------------------------------------------------------------------------
        // End of Aaron's methods

        // Start of Yong Shyan's methods
        // -----------------------------------------------------------------------------------------------
        public float CheckForAdditionalFee()
        {
            return AdditionalPayment;
        }
        // -----------------------------------------------------------------------------------------------
        // End of Yong Shyan's methods
    }
}
