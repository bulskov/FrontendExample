using System.Collections.Generic;

namespace DataServiceLib
{
    public interface IDataService
    {
        IList<Category> GetCategories();
        Category GetCategory(int id);

        Category CreateCategory(string name, string description);

        bool DeleteCategory(int id);
    }
}