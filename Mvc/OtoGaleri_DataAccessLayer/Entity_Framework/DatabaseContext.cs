using OtoGaleri_Entities.Tablolar;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtoGaleri_DataAccessLayer.Entity_Framework
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Yoneticiler> Yoneticiler { get; set; }
        public DbSet<Personeller> Personeller { get; set; }
        public DbSet<Kullanicilar> Kullanicilar { get; set; }
        public DbSet<Arabalar> Arabalar { get; set; }
        public DbSet<IkinciEl> IkinciElArabalar { get; set; }
        public DbSet<YeniArabalar> YeniArabalar { get; set; }
        public DbSet<KiralikArabalar> KiralikArabalar { get; set; }
        public DbSet<KirayaVerilmisArabalar> KirayaVerilmisArabalar { get; set; }
        public DbSet<KiralikBekleyen> KiralikBekleyenArabalar { get; set; }
        public DbSet<IkinciElSatilmisArabalar> IkinciElSatilmisArabalar { get; set; }
        public DbSet<IkinciElSatilmayanArabalar> IkinciElSatilmayanArabalar { get; set; }
        public DbSet<SifirSatilanAraba> SifirSatilanArabalar { get; set; }
        public DbSet<SifirSatilmayanArabalar> SifirSatilmayanArabalar { get; set; }
        public DbSet<PersonelMevkii> PersonelMevkii { get; set; }
        public DbSet<Hizmetliler> Hizmetliler { get; set; }
        public DbSet<TestSurusu> TestSurusu { get; set; }
        public DbSet<FaturaTur> FaturaTur { get; set; }
        public DbSet<Faturalar> Faturalar { get; set; }
        public DbSet<DigerMasrafTurleri> DigerMasrafTurleri { get; set; }
        public DbSet<DigerMasraflar> DigerMasraflar { get; set; }
        public DbSet<CalismaGunleri> CalismaGunleri { get; set; } //ilerde hata ile karşılaşırsam hizmetlilerden son geldigi için olabilir
        public DbSet<SifirBegenilenArabalar> SifirBegenilenArabalar { get; set; }
        public DbSet<Gelirler> Gelirler { get; set; }
        public DbSet<CalisanUcretleri> CalisanUcretleri { get; set; }
        public DbSet<CalisanUcretleriControl> CalisanUcretleriControl { get; set; }
        public DbSet<Mesajlasma> Mesajlasma { get; set; }
       
        public DatabaseContext()
        {
          Database.SetInitializer(new MyInitializer());
        }
    }
}
