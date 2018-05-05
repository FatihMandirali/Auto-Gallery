using OtoGaleri_BusinessLayer.Abstract;
using OtoGaleri_BusinessLayer.Result;
using OtoGaleri_Common.Helpers;
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
    public class KullaniciManager :ManegerBase<Kullanicilar>
    {
        private Repository<Kullanicilar> repo_user = new Repository<Kullanicilar>();
        public BusinessLayerResult<Kullanicilar> RegisterUser(RegisterViewModel data)
        {
            Kullanicilar k = Find(x => x.KullaniciAdi == data.UserName || x.Sifre == data.Password);
            BusinessLayerResult<Kullanicilar> layerResult = new BusinessLayerResult<Kullanicilar>();

            if (k != null)
            {
                if (k.KullaniciAdi == data.UserName)
                {
                    layerResult.AddError(ErrorMessageCode.UserNameAlreadyExists, "Kullanıcı adı kayıtlı..");
                }
                if (k.Sifre == data.Password)
                {
                    layerResult.AddError(ErrorMessageCode.EmailAlreadyExists, "E-posta adresi kayıtlı.. ");
                }
            }
            else
            {
                int dbResult = base.Insert(new Kullanicilar()
                {
                    KullaniciAdi = data.UserName,
                    Eposta = data.EPosta,
                    Sifre = data.Password,
                    AktiflikGuid = Guid.NewGuid(),
                    Resim = "resim.jpg",
                    IsActive = false,
                    Tc=data.Tc,
                    DogumTarih=data.DogumTarihi,
                    Telefon=data.Telefon,
                    Adres=data.Adres,
                    Adi=data.Adi,
                    Soyadi=data.Soyad

                });
                if (dbResult > 0)
                {
                    layerResult.Result = Find(x => x.KullaniciAdi == data.UserName && x.Sifre == data.Password);
                    string siteUri = ConfigHelper.Get<string>("SiteRootUri");
                    string activateUri = $"{siteUri}/Home/UserActivate/{layerResult.Result.AktiflikGuid}";
                    string body = $"merhaba{layerResult.Result.Eposta};Hesabınızı aktifleştirmek için <a href = '{activateUri}' target = '_blank' > tıklayınız.</ a > ";
                    MailHelper.SendMail(body, layerResult.Result.Eposta, "FM OtoGaleri Hesap Aktifleştirme", true);
                }
            }
            return layerResult;


        }

        public BusinessLayerResult<Kullanicilar> LoginUser(LoginViewModel data)
        {//bu bölümü ortak123 alanı için yapacaz çünkü sadece kullanıcı degil yönetici ve personelde giriş yapacak
            
            BusinessLayerResult<Kullanicilar> layerResult = new BusinessLayerResult<Kullanicilar>(); 
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

        public BusinessLayerResult<Kullanicilar> ActivateUser(Guid activateId)
        {
            BusinessLayerResult<Kullanicilar> res = new BusinessLayerResult<Kullanicilar>();
            res.Result = Find(x => x.AktiflikGuid == activateId);
            if (res.Result != null)
            {
                if (res.Result.IsActive)
                {
                    res.AddError(ErrorMessageCode.UserAlreadyActive, "Kullanıcı zaten aktif");
                    return res;
                }
                res.Result.IsActive = true;
                Update(res.Result);
            }
            else
            {
                res.AddError(ErrorMessageCode.ActivateIdDoesntExists, "aktifleştirilecek kullanıcı bulunamadı");
            }
            return res;
        }

        public BusinessLayerResult<Kullanicilar> GetUserById(int id)
        {
            BusinessLayerResult<Kullanicilar> res = new BusinessLayerResult<Kullanicilar>();
            res.Result = repo_user.Find(x => x.Id == id);
            if (res.Result == null)
            {
                res.AddError(ErrorMessageCode.UserNotFound, "Kullanıcı Bulunamadı");
            }
            return res;
        }

        public BusinessLayerResult<Kullanicilar> UpdateProfileK(Kullanicilar data)
        {
            Kullanicilar db_user = Find(x => x.Adi == data.Adi || x.Eposta == data.Eposta);
            BusinessLayerResult<Kullanicilar> res = new BusinessLayerResult<Kullanicilar>();
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
           // res.Result.KimKayitEtti = data.KimKayitEtti;
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

        public BusinessLayerResult<Kullanicilar> RemoveUserByIdK(int id )
        {
            BusinessLayerResult<Kullanicilar> res = new BusinessLayerResult<Kullanicilar>();
            Kullanicilar user = Find(x => x.Id == id);
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
        public new BusinessLayerResult<Kullanicilar> Insert(Kullanicilar data)
        {//base class tan gelen  virtual methodu  new ile ezdik  çünkü new ile yeni bir geri dönüş ekledik  baseclass ta int ti burda farklı...!!!!

            Kullanicilar user = Find(x => x.KullaniciAdi == data.KullaniciAdi || x.Sifre == data.Sifre);
            BusinessLayerResult<Kullanicilar> layerResult = new BusinessLayerResult<Kullanicilar>();
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
        public new BusinessLayerResult<Kullanicilar> Update(Kullanicilar data)
        {
            Kullanicilar db_user = Find(x => x.KullaniciAdi == data.KullaniciAdi || x.Sifre == data.Sifre);
            BusinessLayerResult<Kullanicilar> res = new BusinessLayerResult<Kullanicilar>();
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
