using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWAD_Assignment2_GrpC
{
    internal class BookingLocation
    {
        private string pickupLocation;
        private string returnLocation;
        private bool delivery;
        private bool arrangedReturnLocation;
        private float additionalPayment;

        // Properties
        public string PickupLocation
        {
            get { return pickupLocation; }
            set { pickupLocation = value; }
        }

        public string ReturnLocation
        {
            get { return returnLocation; }
            set { returnLocation = value; }
        }

        public bool Delivery
        {
            get { return delivery; }
            set { delivery = value; }
        }

        public bool ArrangedReturnLocation
        {
            get { return arrangedReturnLocation; }
            set { arrangedReturnLocation = value; }
        }

        public float AdditionalPayment
        {
            get { return additionalPayment; }
            set { additionalPayment = value; }
        }

        // Constructor
        public BookingLocation(string pickupLocation, string returnLocation, bool delivery, bool arrangedReturnLocation, float additionalPayment)
        {
            this.pickupLocation = pickupLocation;
            this.returnLocation = returnLocation;
            this.delivery = delivery;
            this.arrangedReturnLocation = arrangedReturnLocation;
            this.additionalPayment = additionalPayment;
        }
    }
}
