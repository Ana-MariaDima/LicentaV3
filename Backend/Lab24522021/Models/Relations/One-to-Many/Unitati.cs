using Licenta.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Licenta.Models.Relations.One_to_Many
{
    public class Unitati : BaseEntity
    {
        public string Nume_unitate { get; set; }
        public ICollection<RetetaIngrediente> ReteteIngrediente { get; set; }

    }
}
