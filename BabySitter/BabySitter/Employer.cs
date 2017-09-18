using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BabySitter
{
    public class Employer : AdultPerson
    {
        public string Address;
        public EmergencyContact emergencyContact;
        public List<Child> Children;

        public Employer()
        {
            Children = new List<Child>();
        }
    }
}
