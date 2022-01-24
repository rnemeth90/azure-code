﻿using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using WebApiWithAuth.Models;

namespace WebApiWithAuth.Services
{
    public class CourseService
    {
        public IWebHostEnvironment _env;


        private string storage_account_connection_string = "DefaultEndpointsProtocol=https;AccountName=appstore445577;AccountKey=5jk/QJO99okHBa0M/uZCjX0JNMTrbl2HG9kFfmAFp1ce15wSife+o0iXROEkSu9C17AqnawUchFKAq9a8nqyeg==;EndpointSuffix=core.windows.net";


        public IEnumerable<Course> GetCourses()
        {

            BlobServiceClient _blobServiceClient = new BlobServiceClient(storage_account_connection_string);
            BlobContainerClient _containerClient = _blobServiceClient.GetBlobContainerClient("data");
            BlobClient _blobClient = _containerClient.GetBlobClient("Courses.json");


            var response = _blobClient.Download();
            var l_reader = new StreamReader(response.Value.Content);
            return JsonSerializer.Deserialize<Course[]>(l_reader.ReadToEnd());

        }

        public Course GetCourse(string p_course)
        {

            IEnumerable<Course> courses = this.GetCourses();
            Course course = courses.FirstOrDefault(m => m.CourseID == p_course);
            return course;
        }


        public void AddCourse(Course course)
        {
            Course[] courses;
            BlobServiceClient _blobServiceClient = new BlobServiceClient(storage_account_connection_string);
            BlobContainerClient _containerClient = _blobServiceClient.GetBlobContainerClient("data");
            BlobClient _blobClient = _containerClient.GetBlobClient("data.json");


            var response = _blobClient.Download();
            var l_reader = new StreamReader(response.Value.Content);

            courses = JsonSerializer.Deserialize<Course[]>(l_reader.ReadToEnd());

            List<Course> courselist = courses.ToList<Course>();

            courselist.Add(course);

            //Writing the new list of courses

            var output = JsonSerializer.Serialize(courselist, new JsonSerializerOptions
            {
                WriteIndented = true
            });
            var content = Encoding.UTF8.GetBytes(output);
            using (var ms = new MemoryStream(content))
                _blobClient.Upload(ms, overwrite: true);

        }
    }
}