using System.Collections.Generic;

namespace Test1.Models.Interfaces
{
    internal interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();

        T GetById(int id);

        void Add(T entity);

        void Update(T entity);

        void Delete(int id);
    }
}
