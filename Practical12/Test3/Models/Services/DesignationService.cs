using System;
using System.Collections.Generic;
using Test3.Models.Entities;
using Test3.Models.Iterfaces;
using Test3.Models.Repositories;
using Test3.Models.ViewModels;

namespace Test3.Models.Services
{
    public class DesignationService
    {
        private readonly IRepository<Designation> _designationRepo;

        public DesignationService(IRepository<Designation> repository)
        {
            _designationRepo = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public IEnumerable<Designation> GetAll()
        {
            return _designationRepo.GetAll();
        }

        public Designation GetById(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Id must be greater than zero.");

            var designation = _designationRepo.GetById(id);

            if (designation == null)
                throw new KeyNotFoundException($"Designation with id {id} not found.");

            return designation;
        }

        public void Create(Designation designation)
        {
            if (designation == null)
                throw new ArgumentNullException(nameof(designation));

            if (string.IsNullOrWhiteSpace(designation.DesignationName))
                throw new ArgumentException("Designation name is required.");

            _designationRepo.Add(designation);
        }

        public void Update(Designation designation)
        {
            if (designation == null)
                throw new ArgumentNullException(nameof(designation));

            if (string.IsNullOrWhiteSpace(designation.DesignationName))
                throw new ArgumentException("Designation name is required.");

            _designationRepo.Update(designation);
        }

        public void Delete(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Id must be greater than zero.");

            _designationRepo.Delete(id);
        }

        public IEnumerable<DesignationCount> GetCountByDesignation()
        {
            if (_designationRepo is DesignationRepository designationRepository)
                return designationRepository.GetCountByDesignation();
            return new List<DesignationCount>();
        }

        public IEnumerable<DesignationCount> GetDesignationsWithMultipleEmployees()
        {
            if (_designationRepo is DesignationRepository designationRepository)
                return designationRepository.GetDesignationsWithMultipleEmployees();
            return new List<DesignationCount>();
        }
    }
}