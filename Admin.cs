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

        public Admin(int adminId) : base(adminId, "", "", "", 0, "", DateTime.Now)
        {
            AdminId = adminId;
        }

    }
}
