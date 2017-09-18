using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace BabySitter
{
    public class BabySitterApp
    {
        int employeeId = 0;
        int emergencyContactId = 0;
        int childId = 1;
        private string inputErrorMessage;
        private List<EmergencyContact> emergencyContacts = new List<EmergencyContact>();
        private List<Employer> employers = new List<Employer>();
        
        enum select
        {
            add,
            remove,
            details
        }

        public void frontView()
        {
            var input = (select)TakeUserInput("1. Add Employee\n2. Remove Employee\n3. Employee Details", inputErrorMessage);
            input -= 1;
            switch (input)
            {
                case select.add:
                    addEmployee();
                    break;
                case select.remove:
                    removeEmployee();
                    break;
                case select.details:
                    employeeDetails();
                    break;

            }
        }

        private int TakeUserInput(string inputPrompt, string errorMessage)
        {
            Console.WriteLine(inputPrompt);
            var input = Console.ReadLine();
            try
            {
                return Convert.ToInt32(input);
            }
            catch (Exception ex)
            {
                Console.WriteLine(errorMessage);
                return TakeUserInput(inputPrompt, errorMessage);
            }
        }

        private string TakeUserInputString(string inputPrompt, string errorMessage)
        {
            Console.WriteLine(inputPrompt);
            var input = Console.ReadLine();
            return input;
        }
        public void addEmployee()
        {
            Console.WriteLine("Enter Employee details:");
            Console.WriteLine("========================\n");
            var name = TakeUserInputString("Input Employee Name: ", inputErrorMessage);
            var phoneNumber = TakeUserInputString("Input Employee Phone Number: ", inputErrorMessage);
            var address = TakeUserInputString("Input Employee Address: ", inputErrorMessage);
            Employer employer = new Employer()
            {
                Id = employeeId + 1,
                Name = name,
                PhoneNumber = phoneNumber,
                Address = address,
                emergencyContact = addEmergencyContact()
            };
            employeeId++;

            addChildren(employer);
            
            
            
           
        }
        public EmergencyContact addEmergencyContact()
        {
            Console.WriteLine("Enter Emergency Contact");
            Console.WriteLine("========================\n");
            var name = TakeUserInputString("Input Emergency Contact Name: ", inputErrorMessage);
            var phoneNumber = TakeUserInputString("Input Emergency Phone Number: ", inputErrorMessage);
            var relationshipWithClient = TakeUserInputString("Input Emergency Contact Address: ", inputErrorMessage);
            EmergencyContact emergencyContact = new EmergencyContact()
            {
                Id = emergencyContactId + 1,
                Name = name,
                PhoneNumber = phoneNumber,
                RelationshipWithClient = relationshipWithClient
            };
            emergencyContactId++;
            emergencyContacts.Add(emergencyContact);
            return emergencyContact;
        }

        public void addChildren(Employer employer)
        {
            Console.WriteLine("Enter Children Details: ");
            Console.WriteLine("=======================\n");
            var name = TakeUserInputString("Input Children Name: ", inputErrorMessage);
            var age = TakeUserInput("Input Children Age: ", inputErrorMessage);
            var healthInformation = TakeUserInputString("Input Children Health Information: ", inputErrorMessage);
            var interest = TakeUserInputString("Input Child Interest: ", inputErrorMessage);
            Child child = new Child()
            {
                Id = childId,
                Name = name,
                Age = age,
                HealthInformation = healthInformation,
                Interests = interest
            };

            employer.Children.Add(child);

            var input = TakeUserInput("Do you want to Add more children?\n1. Yes\n2. No", inputErrorMessage);
            switch (input)
            {
                case 1:
                    childId += 1;
                    addChildren(employer);
                    break;
                case 2:
                    employers.Add(employer);
                    frontView();
                    break;

            }

        }
        public void removeEmployee()
        {
            if(employers.Count == 0)
            {
                Console.WriteLine("Your employee list is empty. Press 1 to add new employee...");
                frontView();
            }
            else
            {
                showEmployeeToRemove();
                var input = TakeUserInput("Select which employee you one to remove!", inputErrorMessage);
                employers.RemoveAt(input - 1);
                var press = TakeUserInput("Do you want to remove more employee?\n1. Yes\n2. No", inputErrorMessage);
                switch (press)
                {
                    case 1:
                        removeEmployee();
                        break;
                    case 2:
                        frontView();
                        break;

                }
            }

        }
        public void showEmployeeToRemove()
        {
            Console.WriteLine("\nEmployee Details:");
            Console.WriteLine("===================\n");
            Console.WriteLine("Id\t\tName\t\tPhone\t\tAddress");
            var count = 1;
            foreach (Employer employee in employers)
            {
                Console.WriteLine("{0}\t\t{1}\t\t{2}\t\t{3}", count, employee.Name, employee.PhoneNumber, employee.Address);
                count++;
            }
           
        }

        public void employeeDetails()
        {
            var count = 1;
            if(employers.Count == 0)
            {
                Console.WriteLine("Your Employee list is empty.Press 1 to add new employee...");
                frontView();
            }
            else
            {
                
                foreach (Employer employee in employers)
                {
                    Console.WriteLine("===============================================================================");
                    Console.WriteLine("{0}. Employee Details:", count);
                    Console.WriteLine("===============================================================================");
                    Console.WriteLine("Name\t\tPhone\t\tAddress");
                    Console.WriteLine("----\t\t-----\t\t-------");
                    Console.WriteLine("{0}\t\t{1}\t\t{2}", employee.Name, employee.PhoneNumber, employee.Address);
                    emergencyContactDetails(employee.Id);
                    count++;
                }
                
                frontView();
            }
            
        }
        public void emergencyContactDetails(int employeeId)
        {
            Console.WriteLine("\nEmergency Contact Details:");
            Console.WriteLine("===============================================================================");
            Console.WriteLine("Name\t\tPhone\t\tRelation");
            Console.WriteLine("----\t\t-----\t\t--------");
            foreach (EmergencyContact emergencyContact in emergencyContacts)
            {
                if(emergencyContact.Id == employeeId)
                {
                    Console.WriteLine("{0}\t\t{1}\t\t{2}", emergencyContact.Name, emergencyContact.PhoneNumber, emergencyContact.RelationshipWithClient);
                    childrenDetails(employeeId);
                }
            }

        }
        public void childrenDetails(int employeeId)
        {
            Console.WriteLine("\nChildren Details:");
            Console.WriteLine("===============================================================================");
            Console.WriteLine("Name\t\tAge\t\tHealth\t\tInterest");
            Console.WriteLine("----\t\t---\t\t------\t\t--------");
            foreach (Employer employee in employers)
            {
                if(employee.Id == employeeId)
                {
                    foreach (Child child in employee.Children)
                    {
                        Console.WriteLine("{0}\t\t{1}\t\t{2}\t\t{3}", child.Name, child.Age, child.HealthInformation, child.Interests);
                    }
                }
            }
            Console.WriteLine("===============================================================================");
            Console.WriteLine("===============================================================================\n");

        }
    }
}
