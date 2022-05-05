using Licenta.Models.Base;
using Licenta.Models.Relations.Many_to_Many;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Laborator54522021.Models
{
    public class User: BaseEntity
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }

        public string Username { get; set; }

        public string Sex { get; set; }

        public int Varsta { get; set; }
        //to add
        public bool IsActive { get; set; }

        [JsonIgnore]
        public string PasswordHash { get; set; }

        public Role Role { get; set; }

        //for many-to-many
        public ICollection<Aprecieri> Apreciere { get; set; }
    }
}
