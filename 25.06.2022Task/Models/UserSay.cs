using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;

namespace _25._06._2022Task.Models
{
    public class UserSay
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Raiting { get; set; }
        public string Position { get; set; }
        public string Description { get;set; }
        public string ImgUrl { get; set; }
        [NotMapped]
        public IFormFile Photo { get; set; }
    }
}
