using Licenta.Models.Base;
using Licenta.Models.Relations.Many_to_Many;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Licenta.Models.Relations.One_to_Many
{
    public class CategoriiIngrediente: BaseEntity
    {
        public string Nume_categoriie_ingredient { get; set; }
        public string Descriere_categorie_ingredient { get; set; }
   

        public ICollection<Ingrediente> Ingrediente { get; set; }

      
    }
}
