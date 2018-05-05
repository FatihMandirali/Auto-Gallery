using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OtoGaleri.ViewModels
{
    public class InfoViewModel : NotifyWievModelBase<string>
    {
        public InfoViewModel()
        {
            Title = "Bilgilendirme...";
        }
    }
}