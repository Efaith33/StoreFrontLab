using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations; 

namespace StoreFront2.DATA.EF//.StoreFrontMetadata
{
    [MetadataType(typeof(ProductMetadata))]
   public partial class Product
   { }
        
    

    public class ProductMetadata
    {
       [StringLength(50)]
       [Display(Name = "Product Name")]
       [Required]
       public string Name { get; set; }

        [StringLength(100)]
        public string Description { get; set; }
        public int CategoryID { get; set; }

        [DisplayFormat(DataFormatString = "{0:c}")]
        public decimal Price { get; set; }

        [Required]
        public int ProductStatusID { get; set; }

        [Required]
        public Nullable<int> Quanity { get; set; }

    }

}
