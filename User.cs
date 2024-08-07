namespace SWAD_Assignment2_GrpC
{
    public class User
    {
        public int Id { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public int ContactNumber { get; set; }
        public string Name { get; set; }
        public DateTime DateJoined { get; set; }

        public User(int id, string address, string email, string username, int contactNumber, string name, DateTime dateJoined)
        {
            Id = id;
            Address = address;
            Email = email;
            Username = username;
            ContactNumber = contactNumber;
            Name = name;
            DateJoined = dateJoined;
        }
    }
}
