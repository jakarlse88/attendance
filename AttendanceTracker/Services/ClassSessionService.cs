using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using AttendanceTracker.Models;
using AttendanceTracker.Models.DTO;
using AttendanceTracker.Repositories;

namespace AttendanceTracker.Services
{
    [SuppressMessage("ReSharper", "InvertIf")]
    public class ClassSessionService : IClassSessionService
    {
        private readonly IRepositoryWrapper _repositoryWrapper;

        public ClassSessionService(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        /// <summary>
        /// Asynchronously gets a ClassSession entity by Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
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

        /// <summary>
        /// Asynchronously updates the details of an existing ClassSession entity.
        /// </summary>
        /// <param name="id">Id of the ClassSession entity to be updated.</param>
        /// <param name="dto">DTO containing data to be applied to extant entity.</param>
        /// <returns>The updated entity.</returns>
        /// <exception cref="Exception"></exception>
        public async Task<ClassSession> UpdateAsync(int id, ClassSessionDto dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto), 
                    "You tried to call the ClassSessionService.UpdateAsync() method without passing it any data (the DTO was null). Please supply a valid ClassSessionDTO.");
            
            var entity =
                await GetClassSessionByIdAsync(id);

            entity.Date = dto.Date ?? entity.Date;
            entity.StartTime = dto.StartTime ?? entity.StartTime;
            entity.EndTime = dto.EndTime ?? entity.EndTime;
            
            if (dto.StudentClassId != null)
                entity.StudentClass = await GetStudentClassByIdAsync((int) dto.StudentClassId);

            if (dto.StudentIds.Any())
                entity.Students = await MapStudentIdsToStudents(dto.StudentIds);

            await _repositoryWrapper.SaveAsync();

            return entity;
        }

        /// <summary>
        /// Asynchronously creates a new ClassSession entity and saves it
        /// to the context. 
        /// </summary>
        /// <param name="dto"></param>
        /// <returns>The created ClassSession entity.</returns>
        /// <exception cref="ArgumentException"></exception>
        public async Task<ClassSession> CreateAsync(ClassSessionDto dto)
        {
            if (dto == null)
            {
                throw new ArgumentException("ClassSession DTO cannot be null.");
            }

            var entity = new ClassSession
            {
                Date = dto.Date ?? new DateTime(),
                StartTime = dto.StartTime ?? new DateTime(),
                EndTime = dto.EndTime ?? new DateTime(),
                StudentClass = await GetStudentClassByIdAsync(dto.StudentClassId.GetValueOrDefault()),
                Students = await MapStudentIdsToStudents(dto.StudentIds)
            };

            _repositoryWrapper.ClassSession.Add(entity);

            await _repositoryWrapper.SaveAsync();

            return entity;
        }

        /// <summary>
        /// Asynchronously maps a collection of student Ids to a collection of
        /// Student entities. An exception will be thrown if any given id
        /// does not correspond to an existing Student entity.
        /// </summary>
        /// <param name="studentIds"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
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
                        throw new ArgumentOutOfRangeException(
                            nameof(id),
                            $"No Student matching the supplied Id was found. The Id you supplied was [{id}].");
                    }

                    students.Add(student);
                }
            }

            return students;
        }

        /// <summary>
        /// Asynchronously retrieves a StudentClass entity by id.
        /// An ArgumentException will be thrown if a StudentClass with
        /// the supplied id cannot be found.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        private async Task<StudentClass> GetStudentClassByIdAsync(int id)
        {
            var result =
                await _repositoryWrapper.StudentClass.GetByIdAsync(id);

            if (result == null)
            {
                throw new ArgumentException($"No StudentClass matching the supplied Id was found. The Id you supplied was [{id}].");
            }

            return result;
        }

        /// <summary>
        /// Asynchronously retrieves a ClassSession entity by id.
        /// An ArgumentException will be thrown if a ClassSession with
        /// the supplied id cannot be found.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>A ClassSession entity.</returns>
        /// <exception cref="ArgumentException"></exception>
        private async Task<ClassSession> GetClassSessionByIdAsync(int id)
        {
            var result =
                await _repositoryWrapper.ClassSession.GetByIdAsync(id);

            if (result == null)
                throw new ArgumentOutOfRangeException(nameof(id),
                    $"No ClassSession matching the supplied Id was found. The Id you supplied was [{id}].");

            return result;
        }
    }
}