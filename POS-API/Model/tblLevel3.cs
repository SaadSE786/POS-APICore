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

    public partial class tblLevel3
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tblLevel3()
        {
            this.tblParties = new HashSet<tblParty>();
        }
        [Key]
        public int intLevel3Id { get; set; }
        public Nullable<int> intLevel2Id { get; set; }
        public string varLevel3Name { get; set; }
        public Nullable<System.DateTime> dtCreationDate { get; set; }
        public Nullable<System.DateTime> dtUpdationDate { get; set; }
        public Nullable<int> intCreatedBy { get; set; }
        public Nullable<int> intUpdatedBy { get; set; }
    
        public virtual tblLevel2 tblLevel2 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblParty> tblParties { get; set; }
    }
}
