using System.Collections.Generic;
using System.Linq;
using Test1.Models.Interfaces;

namespace Test1.Models.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly static List<T> _store = new List<T>();
        private static int _nextId = 1;

        public IEnumerable<T> GetAll()
        {
            return _store.ToList();
        }

        public T GetById(int id)
        {
            var prop = typeof(T).GetProperty("Id");
            return _store.FirstOrDefault(e => (int)prop.GetValue(e) == id);
        }

        public void Add(T entity)
        {
            var prop = typeof(T).GetProperty("Id");
            prop?.SetValue(entity, _nextId++);

            _store.Add(entity);
        }

        public void Update(T entity)
        {
            var prop = typeof(T).GetProperty("Id");
            int id = (int)prop.GetValue(entity);

            var existing = GetById(id);
            if (existing != null)
            {
                _store.Remove(existing);
                _store.Add(entity);
            }
        }

        public void Delete(int id)
        {
            var entity = GetById(id);
            if (entity != null)
                _store.Remove(entity);
        }
    }
}