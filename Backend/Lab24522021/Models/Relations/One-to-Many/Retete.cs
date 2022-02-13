using Licenta.Models.Base;
using Licenta.Models.Relations.Many_to_Many;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Licenta.Models.Relations.One_to_Many
{
    public class Retete: BaseEntity
    {
        public string Nume_reteta { set; get; }
        public string Descriere_reteta { set; get; }
       // public int Id_categorie_reteta { set; get; }
        public string Link_reteta { set; get; }
        public string Poza_reteta { set; get; }
        public int durata_preparare { set; get; }
        public int durata_gatire { set; get; }
        public int durata_totala { set; get; }
        public float Scor_retea { set; get; }



        public CategoriiRetete CategorieReteta { get; set; }
        public Guid IdCategorieReteta { get; set; }

        //for many-to-many
        public ICollection<RetetaIngrediente> RetetaIngredient { get; set; }

    }
}
