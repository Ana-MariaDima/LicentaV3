using Laborator54522021.Models;
using Licenta.Models.Base;
using Licenta.Models.Relations.One_to_Many;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Licenta.Models.Relations.Many_to_Many
{
    public class Aprecieri : BaseEntity
    {

        //for many-to-many
        [Required]
        public Guid IdReteta { get; set; }
        public Retete Reteta { get; set; }
        [Required]
        public Guid IdUser { get; set; }
        public  User User { get; set; }

        public bool Star { get; set; }
        public Nullable<int> Review { get; set; }
        
    }
}
