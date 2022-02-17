using Licenta.Models.Base;
using Licenta.Models.Relations.Many_to_Many;
using System;

namespace Licenta.Models.Relations.One_to_Many
{

    public class RetetaIngrediente : BaseEntity
    {

        //for many-to-many
        public Guid IdReteta { get; set; }
        public Retete Reteta { get; set; }
        public Guid IdIngredient { get; set; }
        public Ingrediente Ingredient { get; set; }

        //for one-to-many
        public Guid IdUnitate { get; set; }
        public Unitati Unitate  { get; set; }


        public string Cantitate_Ingredient { get; set; }
    
    }
}