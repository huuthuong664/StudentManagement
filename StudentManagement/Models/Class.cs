using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace StudentManagement.Models
{
    public class StudentViewModel
    {
        public Student Student { get; set; }
        public IFormFile PhotoUpload { get; set; }
        public List<Department> Departments { get; set; }
    }
}
