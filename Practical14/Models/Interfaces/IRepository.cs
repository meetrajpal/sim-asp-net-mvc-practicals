using Practical14.Models.ViewModels;
using System.Collections.Generic;

namespace Practical14.Models.Interfaces
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T GetById(int id);
        void Add(T entity);
        void Update(T entity);
        void Delete(int id);
        IEnumerable<T> Search(string keyword);
        PagedResult<T> GetPaged(int page, int pageSize);
    }
}
