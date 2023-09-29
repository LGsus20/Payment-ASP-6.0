using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebApp.Models
{
    public class Payment
    {
        [Key]
        public int PaymentID { get; set; }
        [Required]
        public string PaymentName { get; set; }
        [DisplayName("Payment Name")]
        public int PaymentAmount { get; set; }
        [DisplayName("Payment Amount")]
        public DateTime PaymentDateTIme { get; set; } = DateTime.Now;

    }
}
