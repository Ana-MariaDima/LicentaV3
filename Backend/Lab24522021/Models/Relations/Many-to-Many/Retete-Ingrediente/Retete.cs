using Licenta.Models.Base;
using Licenta.Models.Relations.Many_to_Many;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Licenta.Models.Relations.One_to_Many
{
    public class Retete: BaseEntity
    {
        [Required]
        public string Nume_reteta { set; get; }
        public string Descriere_reteta { set; get; }
        public string Instructiuni_reteta { set; get; }
        public string Poza_reteta { set; get; }
        public float Rating_retea { set; get; }



        public CategoriiRetete CategorieReteta { get; set; }
        public Guid IdCategorieReteta { get; set; }
        public TipuriRetete TipReteta { get; set; }
        public Guid IdTipReteta { get; set; }

        public Pahare Pahar { get; set; }
        public Guid IdPahar { get; set; }

        //for many-to-many
        public ICollection<RetetaIngrediente> RetetaIngredient { get; set; }
        public ICollection <Aprecieri> Apreciere { get; set; }

    }
}
