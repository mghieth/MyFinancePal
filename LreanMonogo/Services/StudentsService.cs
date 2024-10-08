﻿using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Linq;
using MyFinancePal.Models;
using MyFinancePal.Resource;

namespace MyFinancePal.Services
{
    public class StudentsService: IStudentService
    {
        private readonly IMongoCollection<Student> _studentsCollection;
        private readonly IMongoCollection<Course> _coursesCollection;
        public StudentsService(IOptions<BookStoreDatabaseSettings> bookStoreDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                bookStoreDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                bookStoreDatabaseSettings.Value.DatabaseName);

            _studentsCollection = mongoDatabase.GetCollection<Student>(
                bookStoreDatabaseSettings.Value.StudentsCollectionName);

            _coursesCollection = mongoDatabase.GetCollection<Course>(
                bookStoreDatabaseSettings.Value.CoursesCollectionName);
        }


        public async Task<List<StudentResource>> GetAsync()
        {
            var students =await _studentsCollection.Find(_ => true).ToListAsync();

            var resourceStudent = new List<StudentResource>();


            foreach (var student in students)
            {
                resourceStudent.Add(Resource(student));
            }

            return resourceStudent;
        }
           
           

        public async Task<StudentResource?> GetAsync(string id) => Resource(await _studentsCollection.Find(x => x.Id == id).FirstOrDefaultAsync());


        public async Task CreateAsync(Student newStudent)
        {
            if (newStudent.Courses != null)
            {
                foreach (var course in newStudent.Courses)
                {
                    var courseName = await _coursesCollection.Find(c => c.CourseName == course.CourseName).FirstOrDefaultAsync();
                    if(courseName != null) continue;
                    await _coursesCollection.InsertOneAsync(course);
                }
            }

            await _studentsCollection.InsertOneAsync(newStudent);
        }
        
        public async Task UpdateAsync(string id, Student updatedStudent) =>
            await _studentsCollection.ReplaceOneAsync(x => x.Id == id, updatedStudent);

        public async Task RemoveAsync(string id) =>
            await _studentsCollection.DeleteOneAsync(x => x.Id == id);

        public StudentResource Resource(Student student)
        {
            var courseName = student.Courses?.Select(x => x.CourseName).ToList();

            return new StudentResource
            {   Id = student.Id,
                Courses = courseName,
                BookName = student.Book?.BookName,
                Birthday = student.Birthday,
                StudentName = student.StudentName
            };
        }
    }
}
