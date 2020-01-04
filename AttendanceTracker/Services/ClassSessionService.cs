using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AttendanceTracker.Models;
using AttendanceTracker.Models.DTO;
using AttendanceTracker.Repositories;

namespace AttendanceTracker.Services
{
    public class ClassSessionService : IClassSessionService
    {
        private readonly IRepositoryWrapper _repositoryWrapper;

        public ClassSessionService(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        public async Task<ClassSession> Create(ClassSessionDto dto)
        {
            if (dto == null)
            {
                throw new ArgumentException("ClassSession DTO cannot be null.");
            }
            
            var entity = new ClassSession
            {
                Date = dto.Date,
                StartTime = dto.StartTime,
                EndTime = dto.EndTime,
                StudentClass = await GetStudentClassByIdAsync(dto.StudentClassId), 
                Students = await MapStudentIdsToStudents(dto.StudentIds)
            };
            
            _repositoryWrapper.ClassSession.Add(entity);
         
            await _repositoryWrapper.SaveAsync();

            return entity;
        }

        private async Task<HashSet<Student>> MapStudentIdsToStudents(IEnumerable<int> studentIds)
        {
            var students = new HashSet<Student>();

            foreach (var id in studentIds)
            {
                var student = await _repositoryWrapper.Student.SearchByIdAsync(id);

                if (student == null)
                {
                    throw new Exception("No Student with the supplied Id was found.");
                }

                students.Add(student);
            }

            return students;
        }

        private async Task<StudentClass> GetStudentClassByIdAsync(int id)
        {
            var result =  
                await _repositoryWrapper.StudentClass.SearchByIdAsync(id);

            if (result == null)
            {
                throw new Exception("No StudentClass with the supplied Id was found.");
            }

            return result;
        }
    }
}