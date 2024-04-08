using Microsoft.CodeAnalysis;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace InternshipForm.Models
{
    public class CreateInternship
    {
        [Key]
        public int Id { get; set; }
        [Display(Name ="Title Of Internship")]
        public string TitleOfInternship { get; set; }
        [Display(Name = "Name of the Company")]
        public string NameOfCompany { get; set; }
        public string Address { get; set; }
        [Display(Name = "Type Of Internship")]
        public int SelectedType { get; set; }
        
        [Display(Name = "Offered Salary")]
        public int OfferedSalary { get; set; }
        public string Location { get; set; }

       
        public  string Level { get; set; }
        public int Duration { get; set; }
        [Display(Name = "Job Description")]
        public string JobDescription { get; set; }
        [Display(Name = "Responsbility of Candidate")]
        public string Responsiblity { get; set; }
        public List<AppliedInternships> AppliedInternships { get; set; }
        public string Status { get; set; }

    }

    public enum TypeOfInternship
    {
        Fulltime=1,
         PartTime=2
    }

    public enum LocationType
    {
        Onsite=1,
        Remote=2
    }
    public static class EnumHelper
    {
        public static string GetDisplayName(Enum value)
        {
            return value.GetType()
                        .GetMember(value.ToString())
                        .First()
                        .GetCustomAttribute<DisplayAttribute>()
                        ?.GetName() ?? value.ToString();
        }
    }

}
