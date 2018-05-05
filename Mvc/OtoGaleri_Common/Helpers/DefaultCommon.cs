using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtoGaleri_Common.Helpers
{
    public class DefaultCommon : ICommon
    {
        public string GetCurrentUserName()
        {
            return "System";
        }
    }
}
