using MaldsKeeperWebApp.Interfaces;
using MaldsKeeperWebApp.Models;
using MaldsKeeperWebApp.Repository;
using MaldsKeeperWebApp.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MaldsKeeperWebApp.Controllers
{
    public class RecordController : Controller
    {
        private readonly IRecordRepository _recordRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<AppUser> _userManager;
        private readonly IUserRepository _userRepository;

        public RecordController(
            IRecordRepository recordRepository, 
            IHttpContextAccessor httpContextAccessor, 
            UserManager<AppUser> userManager, 
            IUserRepository userRepository)
        {
            _recordRepository = recordRepository;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
            _userRepository = userRepository;
        }
        public async Task<IActionResult> Index()
        {
            var currentUserRecords = await _recordRepository.GetRecordsByUserIdAsync(
                _userRepository.GetUserIdByEmail(_userRepository.GetCurrentUserEmail()));

            List<IndexRecordViewModel> recordsVM = new List<IndexRecordViewModel>();
            foreach (Record record in currentUserRecords)
            {
                recordsVM.Add(new IndexRecordViewModel()
                {
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
    }
}
