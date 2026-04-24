using System.Collections.Generic;
using Test1.Models.Entities;
using Test1.Models.Repositories;

namespace Test1.Models.Services
{
    public class StudentService
    {
        private readonly Repository<Student> _studentRepo;

        public StudentService(Repository<Student> repository)
        {
            _studentRepo = repository;
        }

        public IEnumerable<Student> GetAll()
        {
            return _studentRepo.GetAll();
        }

        public Student GetById(int id)
        {
            return _studentRepo.GetById(id);
        }

        public void Create(Student student)
        {

            _studentRepo.Add(student);
        }

        public void Update(Student student)
        {
            _studentRepo.Update(student);
        }

        public void Delete(int id)
        {
            _studentRepo.Delete(id);
        }
    }
}