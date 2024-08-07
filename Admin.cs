using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWAD_Assignment2_GrpC
{
    public class Admin : User
    {
        public int AdminId { get; set; }

        // Casey's Methods
        // -----------------------------------------------------------------------------------------------
        public Admin(int adminId) : base(adminId, "", "", "", 0, "", DateTime.Now)
        {
            AdminId = adminId;
        }

        public void DisplayAppealsList(List<AppealPenalty> appeals)
        {
            Console.WriteLine("List of available Penalty Appeals:");
            for (int i = 0; i < appeals.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {appeals[i].GetSummary()}");
            }
        }

        public AppealPenalty SelectAppeal(List<AppealPenalty> appeals)
        {
            Console.Write("Enter the number of the appeal to review: ");
            int selection = int.Parse(Console.ReadLine()) - 1;
            return appeals[selection];
        }

        public bool ConfirmRenterInformation(CarRenter renter)
        {
            Console.Write("Is the renter information correct? (y/n): ");
            return Console.ReadLine().ToLower() == "y";
        }

        public bool DecideOnAppeal()
        {
            Console.Write("Accept the appeal? (y/n): ");
            return Console.ReadLine().ToLower() == "y";
        }

        public bool ConfirmDecision()
        {
            Console.Write("Confirm your decision? (y/n): ");
            return Console.ReadLine().ToLower() == "y";
        }

        public void SendAppealStatusEmail(CarRenter renter, string status)
        {
            Console.WriteLine($"Email sent to {renter.Email} - Appeal status: {status}");
        }

        public bool ConfirmCarOwnerInformation(CarOwner owner)
        {
            Console.Write("Is the car owner information correct? (y/n): ");
            return Console.ReadLine().ToLower() == "y";
        }

        public void SendAppealStatusEmail(User user, string status)
        {
            Console.WriteLine($"Email sent to {user.Email} - Appeal status: {status}");
        }
        // -----------------------------------------------------------------------------------------------
        // End of Casey's methods
    }
}
