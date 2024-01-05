using System.ComponentModel.DataAnnotations.Schema;

namespace InternshipForm.Models
{
    public class District
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ProvienceId { get; set; }


    }
}
