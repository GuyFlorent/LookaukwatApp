﻿using System;
using System.Collections.Generic;
using System.Text;

namespace LookaukwatApp.Models
{
    public class ApplicationUser
    {
        public string FirstName { get; set; }
        public virtual string Email { get; set; }
        public virtual string PhoneNumber { get; set; }

    }
}
