using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWAD_Assignment2_GrpC
{
    internal class User
    {
        private int id;
        private string address;
        private string email;
        private string username;
        private int contactNumber;
        private string name;
        private DateTime dateJoined;

        // Properties
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public string Address
        {
            get { return address; }
            set { address = value; }
        }

        public string Email
        {
            get { return email; }
            set { email = value; }
        }

        public string Username
        {
            get { return username; }
            set { username = value; }
        }

        public int ContactNumber
        {
            get { return contactNumber; }
            set { contactNumber = value; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public DateTime DateJoined
        {
            get { return dateJoined; }
            set { dateJoined = value; }
        }

        // Constructor
        public User(int id, string address, string email, string username, int contactNumber, string name, DateTime dateJoined)
        {
            this.id = id;
            this.address = address;
            this.email = email;
            this.username = username;
            this.contactNumber = contactNumber;
            this.name = name;
            this.dateJoined = dateJoined;
        }
    }
}
