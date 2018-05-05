using OtoGaleri_BusinessLayer.Abstract;
using OtoGaleri_Entities.Tablolar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OtoGaleri_BusinessLayer.Result;
using OtoGaleri_Entities.Messages;

namespace OtoGaleri_BusinessLayer
{
    public class ArabalarManager : ManegerBase<Arabalar>
    {
        //public BusinessLayerResult<Arabalar> UpdateProfileK(Arabalar data)
        //{
        //   Arabalar db_user = Find(x => x.Marka == data.Marka || x.Model == data.Model);
        //    BusinessLayerResult<Arabalar> res = new BusinessLayerResult<Arabalar>();
        //    if (db_user != null && db_user.Id != data.Id)
        //    {
        //        if (db_user.Marka == data.Marka)
        //        {
        //           res.AddError(ErrorMessageCode.UserNameAlreadyExists, "Kullanıcı adı kayıtlı.");
        //        }
        //        if (db_user.Model == data.Model)
        //        {
        //            res.AddError(ErrorMessageCode.EmailAlreadyExists, "E-Posta adresi kayıtlı.");

        //        }
        //        return res;


        //    }
        //    res.Result = data;
        //    res.Result.Aciklama = data.Aciklama;
        //    res.Result.ArabayiEkleyen = data.ArabayiEkleyen;
        //    res.Result.Cekis = data.Cekis;
        //    res.Result.Durum = data.Durum;
        //    res.Result.EklenmeTarihi = data.EklenmeTarihi;
        //    res.Result.Fiyat = data.Fiyat;
        //    res.Result.Garanti = data.Garanti;
        //    res.Result.IlanTarihi = data.IlanTarihi;
        //    res.Result.Kasatipi = data.Kasatipi;
        //    res.Result.Marka = data.Marka;
        //    res.Result.Model = data.Model;
        //    res.Result.MotorGucu = data.MotorGucu;
        //    res.Result.MotorHacmi = data.MotorHacmi;
        //    res.Result.Renk = data.Renk;
        //    res.Result.Resim1 = data.Resim1;

        //    if (string.IsNullOrEmpty(data.Resim1) == false)
        //    {
        //        res.Result.Resim1 = data.Resim1;
        //    }
        //    if (base.Update(res.Result) == 0)
        //    {
        //        res.AddError(ErrorMessageCode.ProfilCouldNotUpdate, "Profil Güncellenemedi");
        //    }
        //    if (base.Insert(res.Result) == 0)
        //    {
        //        res.AddError(ErrorMessageCode.UserCouldNotInserted, "Kullanıcı Eklenemedi..");
        //    }
        //    return res;
        //}
        public BusinessLayerResult<Arabalar> UpdateProfileK(Arabalar arabalar)
        {
            Arabalar db_user = Find(x => x.Marka == arabalar.Marka || x.Model == arabalar.Model);
            BusinessLayerResult<Arabalar> res = new BusinessLayerResult<Arabalar>();
           
            res.Result = Find(x => x.Id == arabalar.Id);
            res.Result.Aciklama = arabalar.Aciklama;
            res.Result.ArabayiEkleyen = arabalar.ArabayiEkleyen;
            res.Result.Cekis = arabalar.Cekis;
            res.Result.Durum = arabalar.Durum;
            res.Result.EklenmeTarihi = arabalar.EklenmeTarihi;
            res.Result.Fiyat = arabalar.Fiyat;
            res.Result.Garanti = arabalar.Garanti;
            res.Result.IlanTarihi = arabalar.IlanTarihi;
            res.Result.Kasatipi = arabalar.Kasatipi;
            res.Result.Marka = arabalar.Marka;
            res.Result.Model = arabalar.Model;
            res.Result.MotorGucu = arabalar.MotorGucu;
            res.Result.MotorHacmi = arabalar.MotorHacmi;
            res.Result.Renk = arabalar.Renk;
          //  res.Result.Resim1 = arabalar.Resim1;
           // res.Result.Resim2 = arabalar.Resim2;
         //   res.Result.Resim3 = arabalar.Resim3;
            res.Result.Vites = arabalar.Vites;
            res.Result.Yakit = arabalar.Yakit;
            res.Result.Yil = arabalar.Yil;

            if (string.IsNullOrEmpty(arabalar.Resim1) == false)
            {
                res.Result.Resim1 = arabalar.Resim1;
            }
            if (base.Update(res.Result) == 0)
            {
                res.AddError(ErrorMessageCode.ProfilCouldNotUpdate, "Profil Güncellenemedi");
            }

            return res;

        }
    }
}
