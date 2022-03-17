using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.SqlClient;
using System.Text;
using Sqlapp.Interfaces;

namespace Sqlapp.Services
{
    public class CourseService : ICourseService
    {
        private static string _sqlConnectionString;

        public CourseService(string sqlConnectionString)
        {
            _sqlConnectionString = sqlConnectionString;
        }

        public IEnumerable<ICourse> GetCourses()
        {
            List<ICourse> _lst = new List<ICourse>();
            string _statement = "SELECT CourseID,CourseName,rating from Course";
            var _connection = new SqlConnection(_sqlConnectionString);
            _connection.Open();
            var _sqlcommand = new SqlCommand(_statement, _connection);
            using (SqlDataReader _reader = _sqlcommand.ExecuteReader())
            {
                while (_reader.Read())
                {
                    ICourse _course = Factory.CreateCourse();
                    _course.CourseID = _reader.GetInt32(0);
                    _course.CourseName = _reader.GetString(1);
                    _course.Rating = _reader.GetDecimal(2);
                    _lst.Add(_course);
                }
            }
            _connection.Close();
            return _lst;
        }

        public void UpdateCourse(ICourse p_course)
        {
            StringBuilder _statement = new StringBuilder("UPDATE Course SET Rating=");
            _statement.Append(p_course.Rating);
            _statement.Append(" WHERE CourseID=");
            _statement.Append(p_course.CourseID);

            SqlConnection _connection = new SqlConnection(_sqlConnectionString);
            _connection.Open();
            SqlCommand _sqlcommand = new SqlCommand(_statement.ToString(), _connection);
            _sqlcommand.ExecuteNonQuery();

        }

        public ICourse GetCourse(string id)
        {
            IEnumerable<ICourse> _courses = this.GetCourses();
            ICourse _course = _courses.FirstOrDefault(m => m.CourseID == Int32.Parse(id));
            return _course;
        }

    }
}

    

