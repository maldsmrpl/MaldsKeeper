using MaldsKeeperWebApp.Interfaces;
using MaldsKeeperWebApp.Models;
using MaldsKeeperWebApp.Repository;
using MaldsKeeperWebApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace MaldsKeeperWebApp.Controllers
{
    
    public class RecordController : Controller
    {
        private readonly IRecordRepository _recordRepository;
        private readonly UserManager<AppUser> _userManager;
        private readonly IUserRepository _userRepository;

        public RecordController(
            IRecordRepository recordRepository, 
            UserManager<AppUser> userManager, 
            IUserRepository userRepository)
        {
            _recordRepository = recordRepository;
            _userManager = userManager;
            _userRepository = userRepository;
        }
        public async Task<IActionResult> Index()
        {
            if (!User.Identity.IsAuthenticated) return RedirectToAction("Index", "Home");
            var currentUserRecords = await _recordRepository.GetRecordsByUserIdAsync(
                _userRepository.GetUserIdByEmail(_userRepository.GetCurrentUserEmail()));

            List<IndexRecordViewModel> recordsVM = new List<IndexRecordViewModel>();
            foreach (Record record in currentUserRecords)
            {
                recordsVM.Add(new IndexRecordViewModel()
                {
                    Id = record.Id,
                    Title = record.Title,
                    Login = record.Login,
                    Password = record.Password,
                    Description = record.Description,
                    AddedTime = record.AddedTime,
                    EditedTime = record.EditedTime
                });
            }

            return View(recordsVM);
        }
        public IActionResult Create()
        {
            var userEmail = HttpContext.User.Identity.Name;
            if (userEmail == null) return RedirectToAction("Index", "Home");

            CreateRecordViewModel createVM = new CreateRecordViewModel() { 
                AppUserId = _userRepository.GetUserIdByEmail(userEmail) };
            return View(createVM);
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateRecordViewModel recordVM)
        {
            if (ModelState.IsValid)
            {
                AppUser currentUser = _userRepository.GetUserById(recordVM.AppUserId);

                var record = new Record()
                {
                    Title = recordVM.Title,
                    Login = recordVM.Login,
                    Password = recordVM.Password,
                    Description = recordVM.Description,
                    AddedTime = DateTime.Now,
                    EditedTime = null,
                    AppUserId = recordVM.AppUserId,
                    AppUser = currentUser
                };
                _recordRepository.Create(record);
                return RedirectToAction("Index");
            }
            return View(recordVM);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var record = await _recordRepository.GetRecordByIdAsync(id);
            if (record == null) return NotFound();

            var editVM = new EditRecordViewModel
            {
                Id = id,
                Title = record.Title,
                Login = record.Login,
                Password = record.Password,
                Description = record.Description
            };
            return View(editVM);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditRecordViewModel recordVM)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to edit club");
                return View("Edit", recordVM);
            }

            var userRecord = await _recordRepository.GetRecordByIdAsync(id);

            if (userRecord == null)
            {
                return View("Error");
            }

            var record = new Record
            {
                AppUserId = userRecord.AppUserId,
                AddedTime = userRecord.AddedTime,
                Id = id,
                Title = recordVM.Title,
                Login = recordVM.Login,
                Password = recordVM.Password,
                Description = recordVM.Description,
                EditedTime = DateTime.Now,
            };

            _recordRepository.Edit(record);

            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var recordDetails = await _recordRepository.GetRecordByIdAsync(id);
            if (recordDetails == null) return View("Error");
            return View(recordDetails);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteRecord(int id)
        {
            var recordDetails = await _recordRepository.GetRecordByIdAsync(id);

            if (recordDetails == null)
            {
                return View("Error");
            }

            _recordRepository.Delete(recordDetails);
            return RedirectToAction("Index");
        }
    }
}
