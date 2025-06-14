//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace POS_API.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class tblStockMain
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tblStockMain()
        {
            this.tblStockDetails = new HashSet<tblStockDetail>();
        }
        [Key]
        public int intStid { get; set; }
        public Nullable<int> intCompanyId { get; set; }
        public Nullable<int> intPartyId { get; set; }
        public Nullable<int> intVrno { get; set; }
        public Nullable<int> intVrnoA { get; set; }
        public Nullable<System.DateTime> dtVrDate { get; set; }
        public string varRemarks { get; set; }
        public Nullable<int> intTransporterId { get; set; }
        public string varVrType { get; set; }
        public Nullable<decimal> dcDiscount { get; set; }
        public Nullable<decimal> dcDiscountAmount { get; set; }
        public Nullable<decimal> dcExpense { get; set; }
        public Nullable<decimal> dcAdditionalCharges { get; set; }
        public Nullable<decimal> dcNetAmount { get; set; }
        public Nullable<System.DateTime> dtCreationDate { get; set; }
        public Nullable<System.DateTime> dtUpdationDate { get; set; }
        public Nullable<int> intCreatedBy { get; set; }
        public Nullable<int> intUpdatedBy { get; set; }
        public Nullable<decimal> dcTotalAmount { get; set; }
    
        public virtual tblCompany tblCompany { get; set; }
        public virtual tblParty tblParty { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblStockDetail> tblStockDetails { get; set; }
        public virtual tblTransporter tblTransporter { get; set; }
    }
}
