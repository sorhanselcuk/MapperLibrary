using MapperLibrary.Abstract;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace MapperLibrary.ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<KisiSourceObject> sourceObjects = new List<KisiSourceObject>
            {
                new KisiSourceObject{ Adi = "Şuayip Orhan", Soyadi = "Selçuk", KimlikNumarasi = 12345678901},
                new KisiSourceObject{ Adi = "Ali", Soyadi = "Sevgi", KimlikNumarasi = 12345678902},
                new KisiSourceObject{ Adi = "Murat", Soyadi = "Akkurt", KimlikNumarasi = 12345678902},
            };
            var targetObjects = Mapper.ConvertDatas(new KisiToPersonMapConfigurator(), sourceObjects);
        }
    }
    public class KisiSourceObject
    {
        public long KimlikNumarasi { get; set; }
        public string Adi { get; set; }
        public string Soyadi { get; set; }
    }
    public class PersonTargetObject
    {
        public string NationalIdentityNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
    public class KisiToPersonMapConfigurator : MapAbstractConfigurator<KisiSourceObject, PersonTargetObject>
    {
        public KisiToPersonMapConfigurator()
        {
            Register(source => source.Adi, target => target.FirstName);
            Register(source => source.KimlikNumarasi, target => target.NationalIdentityNumber);
            Register(source => source.Soyadi, target => target.LastName);

        }
        public IDictionary<PropertyInfo, PropertyInfo> GetConfigurations()
        {
            var sourceTypeFields = typeof(KisiSourceObject).GetProperties();
            var targetTypeFields = typeof(PersonTargetObject).GetProperties();

            return new Dictionary<PropertyInfo, PropertyInfo>
            {
                { 
                    sourceTypeFields.First(x => x.Name == "KimlikNumarasi"), 
                    targetTypeFields.First(x => x.Name == "NationalIdentityNumber") 
                },
                { 
                    sourceTypeFields.First(x => x.Name == "Adi"),
                    targetTypeFields.First(x => x.Name == "FirstName") 
                },
                { 
                    sourceTypeFields.First(x => x.Name == "Soyadi"), 
                    targetTypeFields.First(x => x.Name == "LastName") 
                }
            };
        }
    }
}
