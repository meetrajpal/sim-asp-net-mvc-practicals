using System;
using System.Collections.Generic;
using System.Linq;
using Test2.Models.Data;
using Test2.Models.Entities;
using Test2.Models.Interfaces;

namespace Test2.Models.Repositories
{
    public class DesignationRepository : IRepository<Designation>
    {
        private readonly AppDbContext _context;

        public DesignationRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Designation> GetAll()
        {
            try
            {
                return _context.Designations.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to retrieve designations.", ex);
            }
        }

        public Designation GetById(int id)
        {
            try
            {
                return _context.Designations.Find(id);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to retrieve designation with id {id}.", ex);
            }
        }

        public void Add(Designation entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity), "Designation cannot be null.");

                _context.Designations.Add(entity);
                _context.SaveChanges();
            }
            catch (ArgumentNullException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to add designation.", ex);
            }
        }

        public void Update(Designation entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity), "Designation cannot be null.");

                var existing = _context.Designations.Find(entity.Id);

                if (existing == null)
                    throw new KeyNotFoundException($"Designation with id {entity.Id} not found.");

                existing.DesignationName = entity.DesignationName;

                _context.SaveChanges();
            }
            catch (ArgumentNullException)
            {
                throw;
            }
            catch (KeyNotFoundException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to update designation.", ex);
            }
        }

        public void Delete(int id)
        {
            try
            {
                var entity = _context.Designations.Find(id);

                if (entity == null)
                    throw new KeyNotFoundException($"Designation with id {id} not found.");

                _context.Designations.Remove(entity);
                _context.SaveChanges();
            }
            catch (KeyNotFoundException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to delete designation with id {id}.", ex);
            }
        }
    }
}