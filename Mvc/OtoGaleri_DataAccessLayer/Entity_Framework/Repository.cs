using OtoGaleri_Common.Helpers;
using OtoGaleri_Core.DataAccess;
using OtoGaleri_Entities.Tablolar;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OtoGaleri_DataAccessLayer.Entity_Framework
{
    public class Repository<T> : RepositoryBase, IDataAccess<T> where T : class
    {
        //   private DatabaseContext context; 

        private DbSet<T> _objectSet;

        public Repository()
        {
            //  context = RepositoryBase.CreateContext();//böylece tek databasecontext kullanıyoruz her defasında farklılarını kullanmıyoz...
            _objectSet = context.Set<T>();
        }
        public List<T> List()
        {
            return _objectSet.ToList();
        }
        public IQueryable<T> ListQueryable()
        {
            return _objectSet.AsQueryable<T>();
        }

        public List<T> List(Expression<Func<T, bool>> where)
        {
            return _objectSet.Where(where).ToList();
            
        }
        public int Insert(T obj)
        {
            _objectSet.Add(obj);
            if (obj is Ortak123)
            {
                Ortak123 o = obj as Ortak123;
                DateTime now = DateTime.Now;

                o.KayitTarih = now;
               // o.KimKayitEtti = App.Common.GetCurrentUserName();//işlem yapa kullanıcı adı yazılmalı...
                o.IsActive = false;//bunu ekledim hata çıkarsa burası olabilir sebebi...
                o.AktiflikGuid = new Guid();
                o.KimKayitEtti = "system";
            }
            if(obj is Arabalar)
            {
                Arabalar a = obj as Arabalar;
                DateTime now = DateTime.Now;

               // a.ArabayiEkleyen= App.Common.GetCurrentUserName();
                a.EklenmeTarihi = now;
                a.IlanTarihi = now;
               a.Resim1 = "resim.jpg";
            }
            if (obj is Hizmetliler)
            {
                Hizmetliler a = obj as Hizmetliler;
                DateTime now = DateTime.Now;

              //  a.EkleyenPersonel = App.Common.GetCurrentUserName();
                a.EklenmeTarihi = now;
                
            }
            return Save();
        }
        public int Update(T obj)
        {
            if (obj is Ortak123)
            {
                Ortak123 o = obj as Ortak123;
            //    o.KimKayitEtti = App.Common.GetCurrentUserName();//işlem yapa kullanıcı adı yazılmalı...

            }
            if (obj is Arabalar)
            {
                Arabalar a = obj as Arabalar;
                DateTime now = DateTime.Now;

             //   a.ArabayiEkleyen = App.Common.GetCurrentUserName();
                a.EklenmeTarihi = now;
                a.IlanTarihi = now;
               // a.Resim1 = "resim.jpg";  bunu yorum satırından kaldır.. güncel
            }
            if (obj is Hizmetliler)
            {
                Hizmetliler a = obj as Hizmetliler;
                DateTime now = DateTime.Now;

             //   a.EkleyenPersonel = App.Common.GetCurrentUserName();
                a.EklenmeTarihi = now;

            }
            return Save();
        }

        public int Delete(T obj)
        {
            _objectSet.Remove(obj);

            return Save();
        }
        public int Save()
        {
            return context.SaveChanges();
        }
        public T Find(Expression<Func<T, bool>> where)
        {
            return _objectSet.FirstOrDefault(where);
        }

      

    }
}
