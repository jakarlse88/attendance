using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<ClassSession> GetByIdAsync(int id)
        {
            if (id <= 0)
            {
                throw new Exception("Bad Id");
            }

            var result =
                await _repositoryWrapper
                    .ClassSession
                    .GetByIdAsync(id);

            if (result == null)
            {
                throw new Exception($"ClassSession with Id {id} could not be found.");
            }

            return result;
        }

        public Task<ClassSession> EditAsync(int id, ClassSessionDto dto)
        {
            throw new NotImplementedException();
        }

        public async Task<ClassSession> CreateAsync(ClassSessionDto dto)
        {
            if (dto == null)
            {
                throw new ArgumentException("ClassSession DTO cannot be null.");
            }
            
            var entity = new ClassSession
            {
                Date = dto.Date,
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

            var enumerable = studentIds as int[] ?? studentIds.ToArray();
            
            if (enumerable.Any())
            {
                foreach (var id in enumerable)
                {
                    var student = await _repositoryWrapper.Student.GetByIdAsync(id);

                    if (student == null)
                    {
                        throw new Exception("No Student with the supplied Id was found.");
                    }

                    students.Add(student);
                }
            }

            return students;
        }

        private async Task<StudentClass> GetStudentClassByIdAsync(int id)
        {
            var result =  
                await _repositoryWrapper.StudentClass.GetByIdAsync(id);

            if (result == null)
            {
                throw new Exception("No StudentClass with the supplied Id was found.");
            }

            return result;
        }
    }
}