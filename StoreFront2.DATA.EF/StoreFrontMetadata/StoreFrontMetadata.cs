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
        [StringLength(50, ErrorMessage = "* The value must be 50 characters or less")]
        [Display(Name = "Product Name")]
        [Required]
        public string Name { get; set; }

        [UIHint("MultilineText")]
        [DisplayFormat(NullDisplayText = "[-N/A-]")]
        public string Description { get; set; }

        public int CategoryID { get; set; }

        [DisplayFormat(DataFormatString = "{0:c}")]
        public decimal Price { get; set; }

        [Required]
        [Display(Name = "Product StatusID")]
        public int ProductStatusID { get; set; }

        [DisplayFormat(NullDisplayText = "[-N/A-]")]
        public Nullable<int> Quanity { get; set; }

        [StringLength(133, ErrorMessage = "* Value must be 133 characters or less.")]
        [Display(Name = "Image")]
        public string ProductImage { get; set; }
    }

    [MetadataType(typeof(CategoryMetadata))]
    public partial class Category
    { }

    
    public class CategoryMetadata
    {
        [Required]
        [StringLength(50, ErrorMessage = "* Value must be 50 characters or less.")]
        [Display(Name = "Category")]
        public string CategoryName { get; set; }
    }

    [MetadataType(typeof(ProductStatusMetadata))]
    public partial class Product_Status
    { }

    public class ProductStatusMetadata
    {
        [Required]
        [StringLength(50, ErrorMessage = "* Value must be 50 characters or less.")]
        [Display(Name = "Product Status")]
        public string ProductStatusName { get; set; }
    }

    [MetadataType(typeof(DepartmentMetadata))]
    public partial class Department
    { }

    public class DepartmentMetadata
    {
        [Required]
        [StringLength(50, ErrorMessage = "* Value must be 50 characters or less.")]
        [Display(Name = "Department")]
        public string DepartmentName { get; set; }
    }

    [MetadataType(typeof(EmployeesMetadata))]
    public partial class Employees
    { }

    public class EmployeesMetadata
    {
        [Required]
        [StringLength(25, ErrorMessage = "* Value must be 25 characters or less.")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(25, ErrorMessage = "* Value must be 25 characters or less.")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [StringLength(25, ErrorMessage = "* Value must be 25 characters or less.")]
        [DisplayFormat(NullDisplayText = "[-N/A-]")]
        public string City { get; set; }

        [StringLength(2, ErrorMessage = "* Value must be 2 characters or less.")]
        public string State { get; set; }

        [DisplayFormat(NullDisplayText = "[-N/A-]")]
        public int DirectReportID { get; set; }

        [StringLength(50, ErrorMessage = "* Value must be 50 characters or less.")]
        [DisplayFormat(NullDisplayText = "[-N/A-]")]
        public string ContactEmail { get; set; }

        public int DepartmentID { get; set; }

    }
}
