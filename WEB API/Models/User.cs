using System.ComponentModel.DataAnnotations;

namespace WEB_API.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Last_name { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }
    }  
}