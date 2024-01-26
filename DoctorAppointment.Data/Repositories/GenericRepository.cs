using MyDoctorAppointment.Data.Configuration;
using MyDoctorAppointment.Data.Interfaces;
using MyDoctorAppointment.Domain.Entities;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Xml;
using System.Xml.Serialization;

namespace MyDoctorAppointment.Data.Repositories
{
    public abstract class GenericRepository<TSource> : IGenericRepository<TSource> where TSource : Auditable
    {
        public abstract string Path { get; set; }

        public abstract int LastId { get; set; }

        public IEnumerable<TSource> GetAllXml()
        {
            var xml = File.ReadAllText(Path);
            if (xml.ToString().Length == 0)
            {
                return new List<TSource>();
            }
            else
            {
                XmlSerializer xmlDeserializer = new XmlSerializer(typeof(List<TSource>));
                using (StreamReader sr = new StreamReader(Path))
                {
                    List<TSource>? all = xmlDeserializer.Deserialize(sr) as List<TSource>;
                    return all;
                }
            }
        }
        public IEnumerable<TSource> GetAllJson()
        {
            if (!File.Exists(Path))
            {
                File.WriteAllText(Path, "[]");
            }

            var json = File.ReadAllText(Path);

            if (string.IsNullOrWhiteSpace(json))
            {
                File.WriteAllText(Path, "[]");
                json = "[]";
            }

            return JsonConvert.DeserializeObject<List<TSource>>(json)!;
        }

        public TSource CreateXml(TSource source)
        {
            source.Id = ++LastId;
            source.CreatedAt = DateTime.Now;

            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<TSource>));
            var all = (List<TSource>)GetAllXml();
            all.Add(source);
            using (StreamWriter sw = new StreamWriter(Path))
            {
                xmlSerializer.Serialize(sw, all);
            }
            SaveLastId();

            return source;
        }
        public TSource CreateJson(TSource source)
        {
            source.Id = ++LastId;
            source.CreatedAt = DateTime.Now;

            File.WriteAllText(Path, JsonConvert.SerializeObject(GetAllJson().Append(source), Newtonsoft.Json.Formatting.Indented));
            SaveLastId();

            return source;
        }

        public TSource? GetByIdXml(int id)
        {
            return GetAllXml().FirstOrDefault(x => x.Id == id);
        }
        public TSource? GetByIdJson(int id)
        {
            return GetAllJson().FirstOrDefault(x => x.Id == id);
        }

        public bool DeleteXml(int id)
        {
            if (GetByIdXml(id) is null)
                return false;

            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<TSource>));
            var all = (List<TSource>)GetAllXml();
            List<TSource> updAll = all.Where(x => x.Id != id).ToList();
            using (StreamWriter sw = new StreamWriter(Path))
            {
                xmlSerializer.Serialize(sw, updAll);
            }
            return true;
        }

        public bool DeleteJson(int id)
        {
            if (GetByIdJson(id) is null)
                return false;

            File.WriteAllText(Path, JsonConvert.SerializeObject(GetAllJson().Where(x => x.Id != id), Newtonsoft.Json.Formatting.Indented));

            return true;
        }

        public TSource UpdateXml(int id, TSource source)
        {
            source.UpdatedAt = DateTime.Now;
            source.Id = id;

            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<TSource>));
            var all = (List<TSource>)GetAllXml();
            List<TSource> updAll = all.Select(x => x.Id == id ? source : x).ToList();
            using (StreamWriter sw = new StreamWriter(Path))
            {
                xmlSerializer.Serialize(sw, updAll);
            }
            return source;
        }

        public TSource UpdateJson(int id, TSource source)
        {
            source.UpdatedAt = DateTime.Now;
            source.Id = id;

            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<TSource>));
            var all = (List<TSource>)GetAllJson();
            List<TSource> updAll = all.Select(x => x.Id == id ? source : x).ToList();
            using (StreamWriter sw = new StreamWriter(Path))
            {
                xmlSerializer.Serialize(sw, updAll);
            }
            return source;
        }

        public abstract void ShowInfo(TSource source);

        protected abstract void SaveLastId();

        protected dynamic ReadFromAppSettings() => JsonConvert.DeserializeObject<dynamic>(File.ReadAllText(Constants.AppSettingsPath))!;
    }
}
