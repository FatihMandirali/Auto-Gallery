﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtoGaleri_Entities.Messages
{
    public class ErrorMessageObj
    {
        public ErrorMessageCode Code { get; set; }
        public string Message { get; set; }
    }
}
