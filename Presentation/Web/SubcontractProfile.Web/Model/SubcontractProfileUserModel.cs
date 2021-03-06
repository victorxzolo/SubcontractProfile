﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SubcontractProfile.Web.Model
{
    public class SubcontractProfileUserModel
    {
        public System.Guid UserId { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(100)]
        public string Username { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(100)]
        public string SubModuleName { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(100)]
        public string SsoFirstName { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(100)]
        public string SsoLastName { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(100)]
        public string StaffName { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(10)]
        public string StaffRole { get; set; }

        public System.DateTime? CreateDate { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(100)]
        public string CreateBy { get; set; }
    }
}
