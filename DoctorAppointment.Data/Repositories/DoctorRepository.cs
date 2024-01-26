using MyDoctorAppointment.Data.Configuration;
using MyDoctorAppointment.Data.Interfaces;
using MyDoctorAppointment.Domain.Entities;

namespace MyDoctorAppointment.Data.Repositories
{
    public class DoctorRepository : GenericRepository<Doctor>, IDoctorRepository
    {
        public override string Path { get; set; }

        public override int LastId { get; set; }

        public DoctorRepository(string serType)
        {
            dynamic result = ReadFromAppSettings();

            if (serType == "xml")
            {
                Path = result.Database.Doctors.PathXml;
            }
            else if (serType == "json")
            {
                Path = result.Database.Doctors.PathJson;
            }
            LastId = result.Database.Doctors.LastId;
        }

        public override void ShowInfo(Doctor doctor)
        {
            Console.WriteLine($"Doctor: {doctor.Name} {doctor.Surname}\nPhone Number: {doctor.Phone}\nEmail: {doctor.Email}" +
                $"Doctor Type: {doctor.DoctorType}\nExperience: {doctor.Experience}\nSalary: {doctor.Salary}");
        }

        protected override void SaveLastId()
        {
            dynamic result = ReadFromAppSettings();
            result.Database.Doctors.LastId = LastId;

            File.WriteAllText(Constants.AppSettingsPath, result.ToString());
        }
    }
}