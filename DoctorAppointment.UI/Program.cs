using MyDoctorAppointment.Domain.Entities;
using MyDoctorAppointment.Domain.Enums;
using MyDoctorAppointment.Service.Interfaces;
using MyDoctorAppointment.Service.Services;

namespace MyDoctorAppointment
{
    public enum MenuItems
    {
        ViewAllDoctors = 1,

        AddDoctor,

        ViewAllPatients,

        AddPatient,

        AddAppointment
    }
    public class DoctorAppointment
    {
        private readonly IDoctorService _doctorService;

        public DoctorAppointment()
        {
            _doctorService = new DoctorService();
        }

        public void Menu()
        {
            var docs = _doctorService.GetAll();
            while (true)
            {
                Console.WriteLine("Please enter a number to proceed\n1 - view all doctors\n2 - add new doctor" +
                    "\n3 - view all patients\n4 - add new patient\n5 - set an appointment\n6 - stop");
                int action = Convert.ToInt32(Console.ReadLine());

                switch (action)
                {
                    case 1:
                        ViewAllDoctors();
                        return;

                    case 2:
                        AddNewDoctor();
                        return;

                    case 3:
                        Console.WriteLine(3);
                        return;

                    case 4:
                        Console.WriteLine(4);
                        return;

                    case 5:
                        Console.WriteLine(5);
                        return;

                    case 6:
                        Console.WriteLine("See you later☺");
                        return;

                    default:
                        Console.WriteLine("Please enter a number from 1 to 6!");
                        break;
                }
            }

            void ViewAllDoctors()
            {
                Console.WriteLine("Current doctors list: ");
                foreach (var doc in docs)
                {
                    Console.WriteLine(doc.Name);
                }
            }

            void AddNewDoctor()
            {
                Console.WriteLine("Adding doctor: ");
                Console.WriteLine("Enter doctor name");
                string? docName = Console.ReadLine();
                Console.WriteLine("Enter doctor surname");
                string? docSurname = Console.ReadLine();
                Console.WriteLine("Enter doctor experience");
                byte docExperience = Convert.ToByte(Console.ReadLine());
                Console.WriteLine("Enter doctor type");
                Console.WriteLine("Please select a doctor specialty\n1 - Dentist\n2 - Dermatologist\n3 - FamilyDoctor\n4 - Paramedic");
                int docTypeNumber = Convert.ToInt32(Console.ReadLine());
                DoctorTypes docType = DoctorTypes.Dentist;

                var newDoctor = new Doctor
                {
                    Name = docName,
                    Surname = docSurname,
                    Experience = docExperience,
                    DoctorType = docType
                };

                _doctorService.Create(newDoctor);

                while (true)
                {
                    switch (docTypeNumber)
                    {
                        case 1:
                            docType = DoctorTypes.Dentist;
                            return;
                        case 2:
                            docType = DoctorTypes.Dermatologist;
                            return;
                        case 3:
                            docType = DoctorTypes.FamilyDoctor;
                            return;
                        case 4:
                            docType = DoctorTypes.Paramedic;
                            return;
                        default:
                            Console.WriteLine($"Please enter a number from 1 to {Enum.GetNames(typeof(DoctorTypes)).Length}!");
                            break;
                    }
                }
            }
        }
    }

    public static class Program
    {
        public static void Main()
        {
            var doctorAppointment = new DoctorAppointment();
            doctorAppointment.Menu();
        }
    }
}