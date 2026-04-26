using System;
using System.Collections.Generic;
using Test2.Models.Entities;
using Test2.Models.Repositories;

namespace Test2.Models.Services
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
            if (id <= 0)
            {
                throw new ArgumentException("Id must be greater than zero.");
            }

            var student = _studentRepo.GetById(id);

            if (student == null)
            {
                throw new KeyNotFoundException($"Student with id {id} not found.");
            }

            return student;
        }

        public void Create(Student student)
        {
            if (student == null)
            {
                throw new ArgumentNullException(nameof(student), "Student cannot be null.");
            }

            if (string.IsNullOrWhiteSpace(student.FullName))
            {
                throw new ArgumentException("Student name is required.");
            }

            _studentRepo.Add(student);
        }

        public void Update(Student student)
        {
            if (student == null)
            {
                throw new ArgumentNullException(nameof(student), "Student cannot be null.");
            }

            if (string.IsNullOrWhiteSpace(student.FullName))
            {
                throw new ArgumentException("Student name is required.");
            }

            _studentRepo.Update(student);
        }

        public void Delete(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Id must be greater than zero.");
            }

            _studentRepo.Delete(id);
        }
    }
}