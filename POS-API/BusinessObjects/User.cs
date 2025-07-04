﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace POS_API.BusinessObjects
{
    public class User
    {
        public int intUserId { get; set; }
        public string? varName { get; set; }
        public string? varEmail { get; set; }
        public string? varPassword { get; set; }
        public string? varAddress { get; set; }
        public string? varCnic { get; set; }
        public string? varContactNo { get; set; }
        public string? varAuthProvider { get; set; }
        public string? varExternalId { get; set; }
        public Nullable<bool> isAdmin { get; set; }
        public string? varPhoto { get; set; }
        public DateTime? dtCreationDate { get; set; }
        public DateTime? dtUpdationDate { get; set; }
        public int? intCreatedBy { get; set; }
        public int? intUpdatedBy { get; set; }
        public int? intCompanyId { get; set; }

    }
}