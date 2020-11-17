using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataServiceLib
{
    public class DataService : IDataService
    {


        public IList<Category> GetCategories()
        {
            using var ctx = new NorthwindContext();
            return ctx.Categories.ToList();
        }

        public Category GetCategory(int id)
        {
            using var ctx = new NorthwindContext();
            return ctx.Categories.Find(id);
        }

        public Category CreateCategory(string name, string description)
        {
            using var ctx = new NorthwindContext();
            var category = new Category
            {
                Id = ctx.Categories.Max(x => x.Id) + 1,
                Name = name,
                Description = description
            };

            ctx.Categories.Add(category);

            ctx.SaveChanges();

            return category;
        }

        public bool DeleteCategory(int id)
        {
            using var ctx = new NorthwindContext();
            var category = ctx.Categories.Find(id);
            if (category == null)
            {
                return false;
            }

            ctx.Categories.Remove(category);

            ctx.SaveChanges();

            return true;

        }
    }
}
