using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System;

namespace SidPortfolio.Models
{
    public class ContactUsModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [JsonIgnore]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Description { get; set; }
        [JsonIgnore]
        public bool ActiveStatus { get; set; }
        [JsonIgnore]
        public DateTime CreationDateTime { get; set; }
        [JsonIgnore]
        public DateTime? LastUpdateDateTime { get; set; }
    }
}
