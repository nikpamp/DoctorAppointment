using MyDoctorAppointment.Data.Interfaces;
using MyDoctorAppointment.Data.Repositories;
using MyDoctorAppointment.Domain.Entities;
using MyDoctorAppointment.Service.Interfaces;

namespace MyDoctorAppointment.Service.Services
{
    public class DoctorService : IDoctorService
    {
        private readonly IDoctorRepository _doctorRepository;
        public string serType;

        public DoctorService(string serType)
        {
            _doctorRepository = new DoctorRepository(serType);
            this.serType = serType;
        }

        public Doctor Create(Doctor doctor)
        {
            if (serType == "xml")
            {
                return _doctorRepository.CreateXml(doctor);
            }
            else
            {
                return _doctorRepository.CreateJson(doctor);
            }
        }

        public bool Delete(int id)
        {
            if (serType == "xml")
            {
                return _doctorRepository.DeleteXml(id);
            }
            else
            {
                return _doctorRepository.DeleteJson(id);
            }
        }

        public Doctor? Get(int id)
        {
            if (serType == "xml")
            {
                return _doctorRepository.GetByIdXml(id);
            }
            else
            {
                return _doctorRepository.GetByIdJson(id);
            }
        }

        public IEnumerable<Doctor> GetAll()
        {
            if (serType == "xml")
            {
                return _doctorRepository.GetAllXml();
            }
            else
            {
                return _doctorRepository.GetAllJson();
            }
        }

        public Doctor Update(int id, Doctor doctor)
        {
            if (serType == "xml")
            {
                return _doctorRepository.UpdateXml(id, doctor);
            }
            else
            {
                return _doctorRepository.UpdateJson(id, doctor);
            }
        }
    }
}
