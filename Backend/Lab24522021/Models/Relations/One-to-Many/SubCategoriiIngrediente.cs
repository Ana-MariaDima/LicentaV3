using Licenta.Models.Base;
using Licenta.Models.Relations.Many_to_Many;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Licenta.Models.Relations.One_to_Many
{
    public class SubCategoriiIngrediente: BaseEntity
    {
        public string Nume_Subcategoriie_ingredient { get; set; }
        public string Descriere_subcategorie_ingredient { get; set; }

        public CategoriiIngrediente CategorieIngredient { get; set; }
        public Guid IdCategorieIngredient { get; set; }
        public ICollection<Ingrediente> Ingrediente { get; set; }

      
    }
}
