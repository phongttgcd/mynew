﻿using COMP1640_WebDev.Models;
using COMP1640_WebDev.Repositories;
using COMP1640_WebDev.Repositories.Interfaces;
using COMP1640_WebDev.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;

namespace COMP1640_WebDev.Controllers
{

	[Authorize(Roles = "Student")]
<<<<<<< HEAD
<<<<<<< HEAD
<<<<<<< HEAD
	public class StudentController(IWebHostEnvironment hostEnvironment, IMagazineRepository magazineRepository, IAcademicYearRepository academicYearRepository, UserManager<User> userManager, IContributionRepository contributionRepository, IUserRepository userRepository) : Controller
	{
		private readonly IContributionRepository _contributionRepository = contributionRepository;
		private readonly IAcademicYearRepository _academicYearRepository = academicYearRepository;
		private readonly IMagazineRepository _magazineRepository = magazineRepository;
		private readonly UserManager<User> _userManager = userManager;
		private readonly IUserRepository _userRepository = userRepository;
		private readonly IWebHostEnvironment _hostEnvironment = hostEnvironment;
		public async Task<IActionResult> Index()
=======

	public class StudentController : Controller
	{
=======

	public class StudentController : Controller
	{
>>>>>>> parent of a982cff (Refactor Marketing Manager and Student Controller)
=======

	public class StudentController : Controller
	{
>>>>>>> parent of a982cff (Refactor Marketing Manager and Student Controller)
		private readonly IContributionRepository _contributionRepository;
		private readonly IAcademicYearRepository _academicYearRepository;
		private readonly IFacultyRepository _facultyRepository;
		private readonly IMagazineRepository _magazineRepository;
		private readonly IWebHostEnvironment _hostEnvironment;
        private readonly UserManager<User> _userManager;
		private readonly IUserRepository _userRepository;

		public StudentController(IMagazineRepository magazineRepository,IWebHostEnvironment hostEnvironment, IAcademicYearRepository academicYearRepository, UserManager<User> userManager,  IContributionRepository contributionRepository, IUserRepository userRepository, IFacultyRepository facultyRepository)
		{
			_hostEnvironment = hostEnvironment;
			_contributionRepository = contributionRepository;
			_academicYearRepository = academicYearRepository;
			_userRepository = userRepository;
            _facultyRepository = facultyRepository;
            _userManager = userManager;
			_magazineRepository = magazineRepository;
		}

		[HttpGet]
		public async Task<IActionResult> IndexAsync()
>>>>>>> parent of a982cff (Refactor Marketing Manager and Student Controller)
		{
			List<MagazineTableView> magazines;
			var user = await _userManager.GetUserAsync(User);
			var userFacultyId = user!.FacultyId;
			magazines = _magazineRepository.GetAllMagazinesByFaculty(userFacultyId!);
			return View(magazines);
		}

<<<<<<< HEAD
<<<<<<< HEAD
<<<<<<< HEAD
		[HttpGet]
		public async Task<IActionResult> Details(string id)
=======
=======
>>>>>>> parent of a982cff (Refactor Marketing Manager and Student Controller)
=======
>>>>>>> parent of a982cff (Refactor Marketing Manager and Student Controller)
        /*[HttpGet]
        public async Task<IActionResult> AddCommentAsync()
        {

        }*/

        // Method to handle the submission of a new comment
        [HttpPost]
        public async Task<IActionResult> AddComment(CreateContribute data, List<IFormFile> files)
>>>>>>> parent of a982cff (Refactor Marketing Manager and Student Controller)
		{
			var magazineInDb = await _magazineRepository.GetMagazineByID(id);
			var contributions = await _contributionRepository.GetContributionsAccept();

<<<<<<< HEAD
			string imageBase64Data = Convert.ToBase64String(magazineInDb.CoverImage!);
			string image = string.Format("data:image/jpg;base64, {0}", imageBase64Data);
			@ViewBag.AcademicYearId = magazineInDb.AcademicYearId;
=======
			Contribution contri = new();
			var user = await _userRepository.GetUser(userId);
			var academicYear = await _academicYearRepository.GetAcademicYear(data.AcademicYearId);

           
			using (var memoryStream = new MemoryStream())
			{
				await files[0].CopyToAsync(memoryStream);
				contri.AcademicYearId = data.AcademicYearId;
                contri.Title = data.Title;
                contri.Document = data.Document;
                contri.UserId = userId;
                contri.Image =  memoryStream.ToArray();
                contri.IsEnabled = true;
			};
			if (DateTime.Now > academicYear.ClosureDate)
			{
				contri.IsEnabled = false;
>>>>>>> parent of a982cff (Refactor Marketing Manager and Student Controller)

			@ViewBag.Magazine = magazineInDb;
			@ViewBag.Image = image;

			for (int i = 0; i < contributions.Count(); i++)
			{
				string imageString = Convert.ToBase64String(contributions[i].Image);
				contributions[i].ImageString = string.Format("data:image/jpg;base64, {0}", imageString);
			}
			@ViewBag.Contributions = contributions;


			return View();
<<<<<<< HEAD
<<<<<<< HEAD
<<<<<<< HEAD
		}

		[HttpPost]
		public async Task<IActionResult> Details(ContributeViewModel data)
		{
			var userId = _userManager.GetUserId(User);
			Contribution contri = new();
			var user = await _userRepository.GetUser(userId!);
			var academicYear = await _academicYearRepository.GetAcademicYear(data.AcademicYearId);
			var magazineInDb = await _magazineRepository.GetMagazineByID(data.Id);
			var contributions = await _contributionRepository.GetContributionsAccept();

			string imageBase64Data = Convert.ToBase64String(magazineInDb.CoverImage!);
			string image = string.Format("data:image/jpg;base64, {0}", imageBase64Data);
			@ViewBag.AcademicYearId = magazineInDb.AcademicYearId;

			@ViewBag.Magazine = magazineInDb;
			@ViewBag.Image = image;
			@ViewBag.Contributions = contributions;
			if (DateTime.Now >= academicYear.ClosureDate)
			{
				TempData["AlertMessage"] = "Qua han roi khong tao contribution duoc";

			}
			else
			{
				using (var memoryStream = new MemoryStream())
				{
					await data.FormFile!.CopyToAsync(memoryStream);
					contri.Image = memoryStream.ToArray();
				}
				contri.AcademicYearId = data.AcademicYearId;
				contri.Title = data.Title;
				contri.UserId = userId;
				contri.IsEnabled = true;
				contri.ImageString = ".";

                if (data.FormFileWord != null)
				{
					Guid myUUID = Guid.NewGuid();
					var fileName = myUUID.ToString() + Path.GetFileName(data.FormFileWord.FileName);
					var filePath = Path.Combine(_hostEnvironment.WebRootPath, "document", fileName);
					using (var fileStream = new FileStream(filePath, FileMode.Create))
					{
						await data.FormFileWord.CopyToAsync(fileStream);
					}
					contri.Document = Path.Combine("document", fileName);
				}

				await _contributionRepository.CreateContribution(contri, data.FormFile);




				return View();
			}
			return View();


		}


		//[HttpPost]
		//public async Task<IActionResult> AddComment(ContributeViewModel data, List<IFormFile> files)
		//{
		//    var userId = _userManager.GetUserId(User);
		//    Contribution contri = new();
		//    var user = await _userRepository.GetUser(userId!);
		//    var academicYear = await _academicYearRepository.GetAcademicYear(data.AcademicYearId);
		//    using (var memoryStream = new MemoryStream())
		//    {
		//        await files[0].CopyToAsync(memoryStream);
		//        contri.AcademicYearId = data.AcademicYearId;
		//        contri.Title = data.Title;
		//        contri.Document = data.Document;
		//        contri.UserId = userId;
		//        contri.Image = memoryStream.ToArray();
		//        contri.IsEnabled = true;
		//    };
		//    if (DateTime.Now > academicYear.ClosureDate)
		//    {
		//        contri.IsEnabled = false;
		//    }
		//    await _contributionRepository.CreateContribution(contri);
		//    return View();
		//}

=======
        }
>>>>>>> parent of a982cff (Refactor Marketing Manager and Student Controller)
=======
        }
>>>>>>> parent of a982cff (Refactor Marketing Manager and Student Controller)
=======
        }
>>>>>>> parent of a982cff (Refactor Marketing Manager and Student Controller)
		[HttpGet]
		public async Task<IActionResult> EditComment(string id)
		{
			if (id == null)
			{
				return NotFound();
			}
			var comment = await _contributionRepository.GetContribution(id);
			if (comment == null)
			{
				return NotFound();
			}
			return View(comment);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> EditComment(string id, Contribution updateContribution)
		{
			if (ModelState.IsValid)
			{
				await _contributionRepository.UpdateContribution(id, updateContribution);
				TempData["AlertMessage"] = "Updated successfully!!!";
				return RedirectToAction("Index");
			}
			return View(updateContribution);
		}

	}
<<<<<<< HEAD
<<<<<<< HEAD
<<<<<<< HEAD
=======


>>>>>>> parent of a982cff (Refactor Marketing Manager and Student Controller)
=======


>>>>>>> parent of a982cff (Refactor Marketing Manager and Student Controller)
}
=======


}
>>>>>>> parent of a982cff (Refactor Marketing Manager and Student Controller)
