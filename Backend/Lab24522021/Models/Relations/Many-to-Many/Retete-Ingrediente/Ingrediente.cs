using Licenta.Models.Base;
using Licenta.Models.Relations.One_to_Many;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Licenta.Models.Relations.Many_to_Many
{
    public class Ingrediente: BaseEntity
    {
        [Required]
        public string Nume_ingredient { set; get; }
       // public int Categorie_ingredient { set; get; }

       // ingredient-subcategorieIngredient
        public SubCategoriiIngrediente SubCategorieIngredient { get; set; }
        public Guid IdSubCategorieIngredient  { get; set; }

        //for many-to-many
       // public ICollection<RetetaIngrediente> RetetaIngredient { get; set; }

    }
}
