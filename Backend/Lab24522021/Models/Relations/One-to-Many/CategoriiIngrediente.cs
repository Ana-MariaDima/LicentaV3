using Licenta.Models.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Licenta.Models.Relations.One_to_Many
{
    public class CategoriiIngrediente: BaseEntity
        {
            [Required]
            public string Nume_categoriie_ingredient { get; set; }
            public string Descriere_categorie_ingredient { get; set; }
            public ICollection<SubCategoriiIngrediente> SubCategoriiIngrediente{ get; set; }


        }
    }

