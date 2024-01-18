using System.ComponentModel.DataAnnotations;

namespace InternshipForm.Models
{
    public class OfficalUse
    {
        public int Id { get; set; }
        [Display (Name ="Internship For : ")]
        public string Internship_For { get; set; }
        [Display (Name ="Intern Id : ")]
        public int Intern_Id { get; set; }
        [Display (Name =" Duration of Internship:")]
        public string Duration_of_Internship { get; set; }
        [Display (Name =" Authorized Signature: ")]
        public string Authorized_Signature { get; set; }
    }
}
