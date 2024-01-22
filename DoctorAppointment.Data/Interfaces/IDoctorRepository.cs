using MyDoctorAppointment.Domain.Entities;

namespace MyDoctorAppointment.Data.Interfaces
{
    public interface IDoctorRepository : IGenericRepository<Doctor>
    {
        void TakeAVacation(DateTime start, DateTime end)
        {
            Console.WriteLine($"The doctor will be on vacation from {start.Date} to {end.Date}");
        }
    }
}