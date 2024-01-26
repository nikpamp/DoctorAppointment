using MyDoctorAppointment.Domain.Entities;

namespace MyDoctorAppointment.Data.Interfaces
{
    public interface IGenericRepository<TSource> where TSource : Auditable
    {
        TSource CreateXml(TSource source);
        TSource CreateJson(TSource source);

        TSource? GetByIdXml(int id);
        TSource? GetByIdJson(int id);

        TSource UpdateXml(int id, TSource source);
        TSource UpdateJson(int id, TSource source);

        IEnumerable<TSource> GetAllXml();
        IEnumerable<TSource> GetAllJson();

        bool DeleteXml(int id);
        bool DeleteJson(int id);
    }
}
