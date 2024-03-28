using System.ComponentModel.DataAnnotations;

namespace InternshipForm.Models
{
    public class ContactUs
    {
        public int Id { get; set; }
        [Display (Name ="Subject") ]
        public string Subject { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
    }
    
    public enum Categories
    {
        AccountManagement,
        InternshipListing,
            ApplicationProcess,
            TechnicalSupport,
            PaymentAndBilling,
            ProfileAssistance,
            TrainingAndResource


    }
}
