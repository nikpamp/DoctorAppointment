using MyDoctorAppointment.Data.Repositories;
using MyDoctorAppointment.Domain.Entities;
using MyDoctorAppointment.Domain.Enums;
using MyDoctorAppointment.Service.Interfaces;
using MyDoctorAppointment.Service.Services;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Xml;

namespace MyDoctorAppointment
{
    public class DoctorAppointment
    {
        private readonly IDoctorService _doctorService;
        public string serType;

        public DoctorAppointment()
        {
            string Initialize()
            {
                while (true)
                {
                    Console.WriteLine("Enter type of serialization ('xml' or 'json')");
                    string? serType = Console.ReadLine();

                    if (serType == "xml" || serType == "json")
                    {
                        this.serType = serType;
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Please enter 'xml' or 'json'.");
                    }
                }
                return serType;
            }
            _doctorService = new DoctorService(Initialize());
        }

        public void Menu()
        {
            while (true)
            {
                Console.WriteLine("Please enter a number to proceed\n1 - view all doctors\n2 - add new doctor" +
                    "\n3 - view any doctor\n4 - delete doctor\n5 - update doctor's info\n6 - stop");
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
                        ViewAnyDoctor();
                        return;

                    case 4:
                        DeleteDoctor();
                        return;

                    case 5:
                        UpdateDoctor();
                        return;

                    case 6:
                        Console.WriteLine("See you later))");
                        return;

                    default:
                        Console.WriteLine("Please enter a number from 1 to 6!");
                        break;
                }
            }

            void ViewAllDoctors()
            {
                var docs = _doctorService.GetAll();
                if (docs.Count() == 0)
                {
                    Console.WriteLine("There are no doctors...");
                }
                else
                {
                    Console.WriteLine("Current doctors list: ");
                    foreach (var doc in docs)
                    {
                        Console.WriteLine(doc.Name);
                    }
                }
            }

            void AddNewDoctor()
            {
                Console.WriteLine("Adding doctor: ");
                Console.WriteLine("Enter doctor's name");
                string? docName = Console.ReadLine();
                Console.WriteLine("Enter doctor's surname");
                string? docSurname = Console.ReadLine();
                Console.WriteLine("Enter doctor's experience");
                byte docExperience = Convert.ToByte(Console.ReadLine());
                Console.WriteLine("Enter doctor's type");
                Console.WriteLine("Please select a doctor specialty\n1 - Dentist\n2 - Dermatologist\n3 - FamilyDoctor\n4 - Paramedic");
                int docTypeNumber = Convert.ToInt32(Console.ReadLine());
                
                var newDoctor = new Doctor
                {
                    Name = docName,
                    Surname = docSurname,
                    Experience = docExperience,
                };

                while (true)
                {
                    switch (docTypeNumber)
                    {
                        case 1:
                            newDoctor.DoctorType = DoctorTypes.Dentist;
                            _doctorService.Create(newDoctor);
                            return;
                        case 2:
                            newDoctor.DoctorType = DoctorTypes.Dermatologist;
                            _doctorService.Create(newDoctor);
                            return;
                        case 3:
                            newDoctor.DoctorType = DoctorTypes.FamilyDoctor;
                            _doctorService.Create(newDoctor);
                            return;
                        case 4:
                            newDoctor.DoctorType = DoctorTypes.Paramedic;
                            _doctorService.Create(newDoctor);
                            return;
                        default:
                            Console.WriteLine($"Please enter a number from 1 to {Enum.GetNames(typeof(DoctorTypes)).Length}!");
                            continue;
                    }
                }
            }

            void ViewAnyDoctor()
            {
                Console.WriteLine("Enter doctor's Id");
                int docId = Convert.ToInt32(Console.ReadLine());
                var doc = _doctorService.Get(docId);
                if (doc == null)
                {
                    Console.WriteLine("There is no doctor with such ID.");
                }
                else
                {
                    var viewDoc = new DoctorRepository(serType);
                    viewDoc.ShowInfo(doc);
                }
            }

            void DeleteDoctor()
            {
                Console.WriteLine("Enter doctor's Id");
                int docId = Convert.ToInt32(Console.ReadLine());
                var doc = _doctorService.Get(docId);
                if (doc == null)
                {
                    Console.WriteLine("There is no doctor with such ID.");
                }
                else
                {
                    _doctorService.Delete(docId);
                    Console.WriteLine($"The doctor {doc.Name} was deleted.");
                }
            }

            void UpdateDoctor()
            {
                Console.WriteLine("Enter doctor's Id");
                int docId = Convert.ToInt32(Console.ReadLine());
                var doc = _doctorService.Get(docId);
                _doctorService.Update(docId, doc);
                Console.WriteLine($"The doctor {doc.Name} was updated.");
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