using OtoGaleri_Entities.Tablolar;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtoGaleri_DataAccessLayer.Entity_Framework
{
    public class MyInitializer : CreateDatabaseIfNotExists<DatabaseContext>//eger veritabanı daha önce yoksa otomatik olarak bunları eklicek
    {
        protected override void Seed(DatabaseContext context)
        {
            Yoneticiler yonetici = new Yoneticiler()
            {
                Adi = "Fatih",
                Soyadi = "Mandıralı",
                Tc = "22271040738",
                Eposta = "fatihmandiralii@gmail.com",
                Resim = "resim.jpg",
                DogumTarih = "06/03/1997",
                Telefon = "05393551932",
                KayitTarih = DateTime.Now,
                KimKayitEtti = "system",
                KullaniciAdi = "fm",
                Sifre = "fm",
                Adres = "Sakarya",
                IsActive = true

            };
            context.Yoneticiler.Add(yonetici);


            CalisanUcretleriControl calisankontrol = new CalisanUcretleriControl();
            calisankontrol.OdemeYapildimi = false;
            context.CalisanUcretleriControl.Add(calisankontrol);
            context.SaveChanges();


            ///* string[] yakit = {Convert.ToString(OtoGaleri_Entities.ArabalarEnums.Yakit.Benzin),
            //    Convert.ToString(OtoGaleri_Entities.ArabalarEnums.Yakit.LPG),
            // Convert.ToString(OtoGaleri_Entities.ArabalarEnums.Yakit.Dizel)};
            // int[] sayilar = { 1, 2, 0, 1, 2, 2, 1, 0, 1, 2 };*/
            // for(int k = 0; k < 10; k++)
            // {
            //     Arabalar arabalar = new Arabalar()
            //     {
            //         IlanTarihi = FakeData.DateTimeData.GetDatetime(DateTime.Now.AddYears(-1), DateTime.Now),
            //         Marka = FakeData.PlaceData.GetStreetName(),
            //         Model=FakeData.PlaceData.GetCity(),
            //         Yil=FakeData.NumberData.GetNumber(),
            //         Yakit=

            //     };

            // }

        }



    }
}
