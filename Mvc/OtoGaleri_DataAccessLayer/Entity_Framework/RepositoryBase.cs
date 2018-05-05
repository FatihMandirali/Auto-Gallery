using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtoGaleri_DataAccessLayer.Entity_Framework
{
    public class RepositoryBase
    {

        protected static DatabaseContext context;//protected özelligi diger classtaki contextler burdan gelecek 
        private static object _lockSync = new object();
        protected RepositoryBase()
        {
            CreateContext();
            //CLASSIN NEW LENMEMESİ İÇİN PROTECTED KULLANDIK
        }
        public static void CreateContext()
        {
            if (context == null)
            {
                lock (_lockSync)
                {
                    if (context == null)
                        context = new DatabaseContext();
                }//bu işlem ile çoklu zamanlı uygulamalarda bile contextin 1 kere yenilenmesini gerçekleştiriyoruz.

            }

        }
    }
}
