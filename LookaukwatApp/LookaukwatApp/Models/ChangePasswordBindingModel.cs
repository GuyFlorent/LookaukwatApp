﻿using System;
using System.Collections.Generic;
using System.Text;

namespace LookaukwatApp.Models
{
     public class ChangePasswordBindingModel
    {
        public string OldPassword { get; set; }

        public string NewPassword { get; set; }

        public string ConfirmPassword { get; set; }
    }
}
