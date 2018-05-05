using OtoGaleri_BusinessLayer.Abstract;
using OtoGaleri_BusinessLayer.Result;
using OtoGaleri_Entities.Messages;
using OtoGaleri_Entities.Tablolar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtoGaleri_BusinessLayer
{
   public class HizmetlilerManager : ManegerBase<Hizmetliler>
    {
        public new BusinessLayerResult<Hizmetliler> Insert(Hizmetliler data)
        {//base class tan gelen  virtual methodu  new ile ezdik  çünkü new ile yeni bir geri dönüş ekledik  baseclass ta int ti burda farklı...!!!!

            Hizmetliler user = Find(x => x.Tc == data.Tc);
            BusinessLayerResult<Hizmetliler> layerResult = new BusinessLayerResult<Hizmetliler>();
            layerResult.Result = data;
            if (user != null)
            {
                if (user.Tc == data.Tc)
                {
                    layerResult.AddError(ErrorMessageCode.UserNameAlreadyExists, "Böyle Tc kayıtlı..");
                }
               
            }
            else
            {
                
                if (base.Insert(layerResult.Result) == 0)
                {
                    layerResult.AddError(ErrorMessageCode.UserCouldNotInserted, "Kullanıcı Eklenemedi..");
                }

            }
            return layerResult;
        }
        public new BusinessLayerResult<Hizmetliler> Update(Hizmetliler data)
        {
            Hizmetliler db_user = Find(x => x.Tc == data.Tc);
            BusinessLayerResult<Hizmetliler> res = new BusinessLayerResult<Hizmetliler>();
            res.Result = data;
            if (db_user != null && db_user.Id != data.Id)
            {
                if (db_user.Tc == data.Tc)
                {
                    res.AddError(ErrorMessageCode.UserNameAlreadyExists, "Kullanıcı adı kayıtlı.");
                }
             
                return res;


            }
            res.Result = Find(x => x.Id == data.Id);
          //  res.Result.e = data.Eposta;
            res.Result.Adi = data.Adi;
            res.Result.Soyadi = data.Soyadi;
           // res.Result.EklenmeTarihi = data.e;
            res.Result.EkleyenPersonel = data.EkleyenPersonel;
            res.Result.Görevi = data.Görevi;
            res.Result.Telefon = data.Telefon;
            res.Result.Tc = data.Tc;
            res.Result.Ucret = data.Ucret;
            res.Result.UcretPeriyodu = data.UcretPeriyodu;
            res.Result.Acres = data.Acres;




            if (base.Update(res.Result) == 0)
            {
                res.AddError(ErrorMessageCode.UserCouldNotUpdated, "Kullanıcı Güncellenemedi");
            }
            return res;
        }
    }
}
