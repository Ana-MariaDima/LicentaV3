using Licenta.Data;
using Licenta.Models.Relations.Many_to_Many;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Licenta.Utilities.Seeders
{
    public class IngredienteSteeders
    {
        public readonly Context _context;

        public IngredienteSteeders(Context context)
        {
            _context = context;
        }
        public  void SeedInitialIngredients()
        {
            if (!_context.Ingredient.Any())
            {
                var ingredient = new Ingrediente
                {
                   /* Nume_ingredient = "Vin rosu",
                    Categorie_ingredient = 1*/

                };
                _context.Add(ingredient);
                _context.SaveChanges();

            }

        }

    }
}
