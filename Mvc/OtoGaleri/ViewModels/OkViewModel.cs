using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OtoGaleri.ViewModels
{
    public class OkViewModel : NotifyWievModelBase<string>
    {
        public OkViewModel()
        {
            Title = "İşlem başarılı..";
        }
    }
}