using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SWAD_Assignment2_GrpC
{
    internal class Accident
    {
        private string location;
        private DateTime time;
        private string type;
        private Booking chosenBooking;

        //Properties
        public string Location
        {
            get { return location; }
            set { location = value; }
        }

        public DateTime Time
        {
            get { return time; }
            set { time = value; }
        }

        public string Type
        {
            get { return type; }
            set { type = value; }
        }

        public Booking ChosenBooking
        {
            get { return chosenBooking; }
            set { chosenBooking = value; }
        }

        //Constructor
        public Accident(string location, DateTime time, string type, Booking chosenBooking)
        {
            this.location = location;
            this.time = time;
            this.type = type;
            this.chosenBooking = chosenBooking;
        }
    }
}
