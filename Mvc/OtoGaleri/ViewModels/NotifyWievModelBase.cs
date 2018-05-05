using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OtoGaleri.ViewModels
{
    public class NotifyWievModelBase<T>
    {
        public List<T> Items { get; set; }
        public string Header { get; set; }
        public string Title { get; set; }
        public bool IsRedirecting { get; set; }
        public string RedirectingUrl { get; set; }
        public int RedirectingTimeout { get; set; }

        public NotifyWievModelBase()
        {
            Header = "Yönlendiriliyorsunuz....";
            Title = "Geçersiz İşlem...";
            IsRedirecting = true;
            RedirectingUrl = "/Home/Index";
            RedirectingTimeout = 3000;
            Items = new List<T>();//bunu kullanıeken homecontroller da ıtems.add diyerek çok daha fazla ıtem ekleyebiliriz.
        }
    }
}