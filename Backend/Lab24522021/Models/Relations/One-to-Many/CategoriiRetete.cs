﻿using Licenta.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Licenta.Models.Relations.One_to_Many
{
    public class CategoriiRetete : BaseEntity
    {
        public string Nume_Categorie_Retete { set; get; }
        public ICollection<Retete> Retete { get; set; }
       
    }
}
