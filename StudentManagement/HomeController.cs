using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StudentManagement.Models;

namespace StudentManagement.Controllers
{
    public class StudentsController : Controller
    {
        private readonly Student22bitv04Context _context;
        private readonly IWebHostEnvironment _env;

        public StudentsController(Student22bitv04Context context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        // GET: Students/Create
        public IActionResult Create()
        {
            var vm = new StudentViewModel
            {
                Departments = _context.Departments.ToList()
            };
            return View(vm);
        }

        // POST: Students/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(StudentViewModel vm)
        {
            if (ModelState.IsValid)
            {
                if (vm.PhotoUpload != null)
                {
                    var ext = Path.GetExtension(vm.PhotoUpload.FileName).ToLower();
                    if (ext != ".jpg" && ext != ".jpeg" && ext != ".png")
                    {
                        ModelState.AddModelError("PhotoUpload", "Only .jpg, .jpeg, .png allowed.");
                        vm.Departments = _context.Departments.ToList();
                        return View(vm);
                    }

                    if (vm.PhotoUpload.Length > 2 * 1024 * 1024)
                    {
                        ModelState.AddModelError("PhotoUpload", "Max file size is 2MB.");
                        vm.Departments = _context.Departments.ToList();
                        return View(vm);
                    }

                    var fileName = Guid.NewGuid().ToString() + ext;
                    var filePath = Path.Combine(_env.WebRootPath, "images", "students");
                    Directory.CreateDirectory(filePath);
                    var fullPath = Path.Combine(filePath, fileName);

                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        await vm.PhotoUpload.CopyToAsync(stream);
                    }

                    vm.Student.PhotoPath = "/images/students/" + fileName;
                }

                _context.Add(vm.Student);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            vm.Departments = _context.Departments.ToList();
            return View(vm);
        }
    }
}
