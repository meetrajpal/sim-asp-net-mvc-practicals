using System;
using System.Collections.Generic;
using System.Linq;
using Test2.Models.Interfaces;

namespace Test2.Models.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly static List<T> _store = new List<T>();
        private static int _nextId = 1;

        public IEnumerable<T> GetAll()
        {
            try
            {
                return _store.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to retrieve records.", ex);
            }
        }

        public T GetById(int id)
        {
            try
            {
                var prop = typeof(T).GetProperty("Id");

                if (prop == null)
                {
                    throw new InvalidOperationException($"{typeof(T).Name} does not have an Id property.");
                }

                return _store.FirstOrDefault(e => (int)prop.GetValue(e) == id);
            }
            catch (InvalidOperationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to retrieve record with id {id}.", ex);
            }
        }

        public void Add(T entity)
        {
            try
            {
                if (entity == null)
                {
                    throw new ArgumentNullException(nameof(entity), "Entity cannot be null.");
                }

                var prop = typeof(T).GetProperty("Id");

                if (prop == null)
                {
                    throw new InvalidOperationException($"{typeof(T).Name} does not have an Id property.");
                }

                prop.SetValue(entity, _nextId++);
                _store.Add(entity);
            }
            catch (ArgumentNullException)
            {
                throw;
            }
            catch (InvalidOperationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to add record.", ex);
            }
        }

        public void Update(T entity)
        {
            try
            {
                if (entity == null)
                {
                    throw new ArgumentNullException(nameof(entity), "Entity cannot be null.");
                }

                var prop = typeof(T).GetProperty("Id");

                if (prop == null)
                {
                    throw new InvalidOperationException($"{typeof(T).Name} does not have an Id property.");
                }

                int id = (int)prop.GetValue(entity);
                var existing = GetById(id);

                if (existing == null)
                {
                    throw new KeyNotFoundException($"{typeof(T).Name} with id {id} not found.");
                }

                _store.Remove(existing);
                _store.Add(entity);
            }
            catch (ArgumentNullException)
            {
                throw;
            }
            catch (KeyNotFoundException)
            {
                throw;
            }
            catch (InvalidOperationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to update record.", ex);
            }
        }

        public void Delete(int id)
        {
            try
            {
                var entity = GetById(id);

                if (entity == null)
                {
                    throw new KeyNotFoundException($"{typeof(T).Name} with id {id} not found.");
                }

                _store.Remove(entity);
            }
            catch (KeyNotFoundException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to delete record with id {id}.", ex);
            }
        }
    }
}