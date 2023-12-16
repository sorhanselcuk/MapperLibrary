# Mapper Library Kullanımı

## Örnek Kullanım

### Kaynak Class
```csharp
  public class KisiSourceObject
  {
      public long KimlikNumarasi { get; set; }
      public string Adi { get; set; }
      public string Soyadi { get; set; }
  }
```

### Hedef Class
```csharp
  public class PersonTargetObject
  {
      public string NationalIdentityNumber { get; set; }
      public string FirstName { get; set; }
      public string LastName { get; set; }
  }
```

Propertylerin eşleşebilmesi için MapAbstractConfigurator sınıfından türetilmiş bir sınıf üzerinden yapılandırmalar sağlanır.

```csharp
  public class KisiToPersonMapConfigurator : MapAbstractConfigurator<KisiSourceObject, PersonTargetObject>
  {
      public KisiToPersonMapConfigurator()
      {
          Register(source => source.Adi, target => target.FirstName);
          Register(source => source.KimlikNumarasi, target => target.NationalIdentityNumber);
          Register(source => source.Soyadi, target => target.LastName);
      }
  }
```

Yapılandırmaların sağlanmasından sonra Mapper sınıfının ConvertDatas yöntemine MapperAbstarctConfigurator sınıfından türetilmiş Configurator sınıfı ve kaynak sınıflar verilerek dönüştürme işlemi gerçekleştirilir.
```csharp
  List<KisiSourceObject> sourceObjects = new List<KisiSourceObject>
  {
      new KisiSourceObject{ Adi = "Test isim 1", Soyadi = "test soyisim 1", KimlikNumarasi = 1},
      new KisiSourceObject{ Adi = "Test isim 2", Soyadi = "test soyisim 2", KimlikNumarasi = 2},
      new KisiSourceObject{ Adi = "Test isim 3", Soyadi = "test soyisim 3", KimlikNumarasi = 3}
  };
  IEnumerable<PersonTargetObject> convertedDatas = Mapper.ConvertDatas(new KisiToPersonMapConfigurator(), sourceObjects);
```
