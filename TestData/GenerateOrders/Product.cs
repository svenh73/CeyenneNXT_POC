//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GenerateOrders
{
    using System;
    using System.Collections.Generic;
    
    public partial class Product
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Product()
        {
            this.OrderLine = new HashSet<OrderLine>();
        }
    
        public int ProductID { get; set; }
        public string VendorItemNumber { get; set; }
        public int BrandID { get; set; }
        public int SourceVendorID { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime CreationTime { get; set; }
        public Nullable<System.DateTime> LastModificationTime { get; set; }
        public Nullable<int> LastModifiedBy { get; set; }
        public bool IsBlocked { get; set; }
        public int ProductTypeID { get; set; }
        public bool IsConfigurable { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderLine> OrderLine { get; set; }
    }
}
