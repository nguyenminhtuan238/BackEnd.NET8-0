using System.ComponentModel.DataAnnotations;

namespace ProjectCV.Server.Models
{
    public class Note
    {
        public int Id { get; set; }
        [StringLength(255)]
        public string ?Content { get; set; }
        
        public DateTime DateTime { get; set; }
        public int ?TypeId { get; set; }
        public Types ?Types { get; set; }
    }
}
