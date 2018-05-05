using OtoGaleri_BusinessLayer.Abstract;
using OtoGaleri_BusinessLayer.Result;
using OtoGaleri_DataAccessLayer.Entity_Framework;
using OtoGaleri_Entities.Messages;
using OtoGaleri_Entities.Tablolar;
using OtoGaleri_Entities.ValueObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtoGaleri_BusinessLayer
{
   public class PersonelManager : ManegerBase<Personeller>
    {
        private Repository<Personeller> repo_user = new Repository<Personeller>();
        public BusinessLayerResult<Personeller> LoginPersonel(LoginViewModel data)
        {//bu bölümü ortak123 alanı için yapacaz çünkü sadece kullanıcı degil yönetici ve personelde giriş yapacak

            BusinessLayerResult<Personeller> layerResult = new BusinessLayerResult<Personeller>();
            layerResult.Result = Find(x => x.KullaniciAdi == data.UserName && x.Sifre == data.Password);

            if (layerResult.Result != null)
            {
                if (!layerResult.Result.IsActive)
                {
                    layerResult.AddError(ErrorMessageCode.UserIsNotActive, "Kullanıcı aktifleştirilmedi...");
                    layerResult.AddError(ErrorMessageCode.CheckYourEmail, "Lütfen E-Posta adresininiz kontrol edin.");
                }

            }
            else
            {
                layerResult.AddError(ErrorMessageCode.UsernameOrPassWrong, "Kullanıcı adı veya şifre uyuşmuyor..");
            }
            return layerResult;
        }

        public BusinessLayerResult<Personeller> GetUserById(int id)
        {
            BusinessLayerResult<Personeller> res = new BusinessLayerResult<Personeller>();
            res.Result = repo_user.Find(x => x.Id == id);
            if (res.Result == null)
            {
                res.AddError(ErrorMessageCode.UserNotFound, "Kullanıcı Bulunamadı");
            }
            return res;
        }
        
        public BusinessLayerResult<Personeller> UpdateProfileP(Personeller data)
        {
            Personeller db_user = Find(x => x.Adi == data.Adi || x.Eposta == data.Eposta);
            BusinessLayerResult<Personeller> res = new BusinessLayerResult<Personeller>();
            if (db_user != null && db_user.Id != data.Id)
            {
                if (db_user.KullaniciAdi == data.KullaniciAdi)
                {
                    res.AddError(ErrorMessageCode.UserNameAlreadyExists, "Kullanıcı adı kayıtlı.");
                }
                if (db_user.Eposta == data.Eposta)
                {
                    res.AddError(ErrorMessageCode.EmailAlreadyExists, "E-Posta adresi kayıtlı.");

                }
                return res;


            }
            res.Result = Find(x => x.Id == data.Id);
            res.Result.Eposta = data.Eposta;
            res.Result.Adi = data.Adi;
            res.Result.Soyadi = data.Soyadi;
            res.Result.Sifre = data.Sifre;
            res.Result.KullaniciAdi = data.KullaniciAdi;
            res.Result.Tc = data.Tc;
            res.Result.DogumTarih = data.DogumTarih;
            res.Result.Telefon = data.Telefon;
          //  res.Result.KimKayitEtti = data.KimKayitEtti;
            res.Result.Adres = data.Adres;


            if (string.IsNullOrEmpty(data.Resim) == false)
            {
                res.Result.Resim = data.Resim;
            }
            if (base.Update(res.Result) == 0)
            {
                res.AddError(ErrorMessageCode.ProfilCouldNotUpdate, "Profil Güncellenemedi");
            }
            return res;
        }

        public BusinessLayerResult<Personeller> RemoveUserByIdP(int id)
        {
            BusinessLayerResult<Personeller> res = new BusinessLayerResult<Personeller>();
            Personeller user = Find(x => x.Id == id);
            if (user != null)
            {
                if (Delete(user) == 0)
                {
                    res.AddError(ErrorMessageCode.UserCouldNotRemove, "Kullanıcı silinemedi.");
                    return res;
                }
            }
            else
            {
                res.AddError(ErrorMessageCode.UserCouldNotFind, "Kullanıcı Bulunamadı.");
            }
            return res;
        }
        public new BusinessLayerResult<Personeller> Insert(Personeller data)
        {//base class tan gelen  virtual methodu  new ile ezdik  çünkü new ile yeni bir geri dönüş ekledik  baseclass ta int ti burda farklı...!!!!

            Personeller user = Find(x => x.KullaniciAdi == data.KullaniciAdi || x.Sifre == data.Sifre);
            BusinessLayerResult<Personeller> layerResult = new BusinessLayerResult<Personeller>();
            
          
            layerResult.Result = data;
            if (user != null)
            {
                if (user.KullaniciAdi == data.KullaniciAdi)
                {
                    layerResult.AddError(ErrorMessageCode.UserNameAlreadyExists, "Kullanıcı adı kayıtlı..");
                }
                if (user.Sifre == data.Sifre)
                {
                    layerResult.AddError(ErrorMessageCode.EmailAlreadyExists, "E-posta adresi kayıtlı.. ");
                }
            }
            else
            {
                layerResult.Result.Resim = "resim.jpg";
                layerResult.Result.AktiflikGuid = Guid.NewGuid();
               
                if (base.Insert(layerResult.Result) == 0)
                {
                    layerResult.AddError(ErrorMessageCode.UserCouldNotInserted, "Kullanıcı Eklenemedi..");
                }

            }
            return layerResult;
        }
        public new BusinessLayerResult<Personeller> Update(Personeller data)
        {
            Personeller db_user = Find(x => x.KullaniciAdi == data.KullaniciAdi || x.Sifre == data.Sifre);
            BusinessLayerResult<Personeller> res = new BusinessLayerResult<Personeller>();
            res.Result = data;
            if (db_user != null && db_user.Id != data.Id)
            {
                if (db_user.KullaniciAdi == data.KullaniciAdi)
                {
                    res.AddError(ErrorMessageCode.UserNameAlreadyExists, "Kullanıcı adı kayıtlı.");
                }
                if (db_user.Sifre == data.Sifre)
                {
                    res.AddError(ErrorMessageCode.EmailAlreadyExists, "E-Posta adresi kayıtlı.");

                }
                return res;


            }
            res.Result = Find(x => x.Id == data.Id);
            res.Result.Eposta = data.Eposta;
            res.Result.Adi = data.Adi;
            res.Result.Soyadi = data.Soyadi;
            res.Result.Sifre = data.Sifre;
            res.Result.KullaniciAdi = data.KullaniciAdi;
            res.Result.IsActive = data.IsActive;
            res.Result.Telefon = data.Telefon;
            res.Result.Tc = data.Tc;
            res.Result.DogumTarih = data.DogumTarih;
            res.Result.Adres = data.Adres;



            if (base.Update(res.Result) == 0)
            {
                res.AddError(ErrorMessageCode.UserCouldNotUpdated, "Kullanıcı Güncellenemedi");
            }
            return res;
        }
    }
}
