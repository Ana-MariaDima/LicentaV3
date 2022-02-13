using AutoMapper;
using Licenta.Models.DTOs;
using Licenta.Models.Relations.Many_to_Many;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Licenta.Utilities
{
    public class MapperProfiles:Profile
    {
        public MapperProfiles()
        {
            ///Mutam datelle din baza de date intr-un dto sau invers 
            CreateMap<Ingrediente, IngredienteDTO>();
            CreateMap<IngredienteDTO,Ingrediente >();
        }
    }
}
