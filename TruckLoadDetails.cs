using System.ComponentModel.DataAnnotations;

namespace WinFormsApp1
{
    public class TruckLoadDetails
    {
        [ExcelColumnOrder(0)]
        [Required]
        [Display(Name = "Factory Id")]
        public string? FactoryId { get; set; }

        [ExcelColumnOrder(1)]
        [Required]
        [Display(Name = "Cluster Id")]
        public string? ClusterId { get; set; }

        [ExcelColumnOrder(2)]
        [Required]
        [Display(Name = "Cascade Id")]
        public string? CascadeId { get; set; }

        [ExcelColumnOrder(3)]
        [Display(Name = "SlNo")]
        public int SlNo { get; set; }

        [ExcelColumnOrder(4)]
        [Display(Name = "2nd Weight Ref No.")]
        public int SecondWeightRefNo { get; set; }

        [ExcelColumnOrder(5)]
        [Required]
        [Display(Name = "In")]
        public DateTime? In { get; set; }

        [ExcelColumnOrder(6)]
        [Required]
        [Display(Name = "Out")]
        public DateTime? Out { get; set; }

        [ExcelColumnOrder(7)]
        [Display(Name = "Challan No")]
        public string? ChallanNo { get; set; }

        [ExcelColumnOrder(8)]
        [Display(Name = "Vehicle No")]
        public string? VehicleNo { get; set; }

        [ExcelColumnOrder(9)]
        [Required]
        [Display(Name = "Material ID")]
        public string? MaterialID { get; set; }

        [ExcelColumnOrder(10)]
        [Required]
        [Display(Name = "Material")]
        public string? Material { get; set; }

        [ExcelColumnOrder(10)]
        [Required]
        [Display(Name = "Material Given")]
        public string? MaterialActualName { get; set; }

        [ExcelColumnOrder(11)]
        [Required]
        [MaxLength(100)]
        [Display(Name = "Supplier/Customer")]
        public string? SupplierOrCustomer { get; set; }

        [ExcelColumnOrder(12)]
        [Display(Name = "Destination")]
        public string? Destination { get; set; }

        [ExcelColumnOrder(13)]
        [MaxLength(100)]
        [Display(Name = "Remark")]
        public string? Remark { get; set; }

        [ExcelColumnOrder(14)]
        [MaxLength(200)]
        [Display(Name = "Carrier")]
        public string? Carrier { get; set; }

        [ExcelColumnOrder(15)]
        [Required]
        [Display(Name = "1st Reading Weight")]
        public int FirstReadingWeight { get; set; }

        [ExcelColumnOrder(16)]
        [Required]
        [Display(Name = "2nd Reading Weight")]
        public int SecondReadingWeight { get; set; }

        [ExcelColumnOrder(17)]
        [Required]
        [Display(Name = "Net Weight")]
        public int NetWeight { get; set; }

        [ExcelColumnOrder(18)]
        [Display(Name = "inmach")]
        public string? InMachine { get; set; }

        [ExcelColumnOrder(19)]
        [Display(Name = "outmach")]
        public string? OutMachine { get; set; }
    }
    /// <summary>
    /// Order provided will be the column number in Excel
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class ExcelColumnOrderAttribute : Attribute
    {
        private int Order { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ExcelColumnOrderAttribute"/> class with the specified order.
        /// </summary>
        /// <param name="order">The <c>order</c> value determines the position of the column in an Excel sheet. For example, a property with <c>Order = 1</c> will appear before a property with <c>Order = 2</c>.</param>
        /// <remarks>
        /// The <c>order</c> value determines the position of the column in an Excel sheet. 
        /// For example, a property with <c>Order = 1</c> will appear before a property with <c>Order = 2</c>.
        /// </remarks>
        public ExcelColumnOrderAttribute(int order)
        {
            Order = order;
        }

        /// <summary>
        /// Returns the mentioned order of property used in definition
        /// </summary>
        public int GetOrder() => Order;
    }
}
