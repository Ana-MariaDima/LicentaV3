using Licenta.Data;
using Licenta.Models.Relations.One_to_Many;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Licenta.Utilities.Seeders
{
    public class CategorieIngredienteSeeder
    {

        public readonly Context _context;

        public CategorieIngredienteSeeder(Context context)
        {
            _context = context;
        }
        public void SeedInitialCategIngredients()
        {
            if (!_context.CategorieIngredient.Any())
            {
                var CategIngredient = new SubCategoriiIngrediente
                {
                    Nume_Subcategorie_ingredient = "Vinuri"

                };
                _context.Add(CategIngredient);
                _context.SaveChanges();

            }

        }

    }
}
