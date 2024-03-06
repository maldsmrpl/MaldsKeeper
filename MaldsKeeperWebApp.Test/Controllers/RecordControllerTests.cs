using FakeItEasy;
using FluentAssertions;
using MaldsKeeperWebApp.Controllers;
using MaldsKeeperWebApp.Interfaces;
using MaldsKeeperWebApp.Models;
using MaldsKeeperWebApp.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Record = MaldsKeeperWebApp.Models.Record;

namespace MaldsKeeperWebApp.Test.Controllers
{
    public class RecordControllerTests
    {
        private RecordController _recordController;
        private IRecordRepository _recordRepository;
        private IUserRepository _userRepository;
        private UserManager<AppUser> _userManager;
        private HttpContext _httpContext;
        public RecordControllerTests()
        {
            _recordRepository = A.Fake<IRecordRepository>();
            _userRepository = A.Fake<IUserRepository>();
            _userManager = A.Fake<UserManager<AppUser>>();
            _httpContext = A.Fake<HttpContext>();

            //SUT
            _recordController = new RecordController(_recordRepository, _userManager, _userRepository);
        }

        [Fact]
        public void RecordController_IndexGet_ReturnSuccess()
        {
            // Arrange
            var records = A.Fake<IEnumerable<Record>>();
            A.CallTo(() => _recordRepository.GetRecordsByUserIdAsync(_userRepository.GetUserIdByEmail(_userRepository.GetCurrentUserEmail()))).Returns(records);
            
            //Act
            var result = _recordController.Index();

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<Task<IActionResult>>();
        }

        [Fact]
        public void RecordController_EditGet_ReturnSuccess()
        {
            //Arrange
            var id = 1;
            var record = A.Fake<Record>();
            A.CallTo(() => _recordRepository.GetRecordByIdAsync(id)).Returns(record);
            //Act
            var result = _recordController.Edit(id);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<Task<IActionResult>>();
        }

        [Fact]
        public void RecordController_EditPost_ReturnSuccess()
        {
            //Arrange
            var id = 1;
            EditRecordViewModel viewModel = A.Fake<EditRecordViewModel>();
            A.CallTo(() => _recordRepository.GetRecordByIdAsync(id));

            //Act
            var result = _recordController.Edit(id, viewModel);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<Task<IActionResult>>();
        }

        [Fact]
        public void RecordController_DeleteGet_ReturnSuccess()
        {
            //Arrange
            var id = 1;
            Record recordDetails = A.Fake<Record>();
            A.CallTo(() => _recordRepository.GetRecordByIdAsync(id)).Returns(recordDetails);

            //Act
            var result = _recordController.Delete(id);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<Task<IActionResult>>();
        }

        [Fact]
        public void RecordController_DeletePost_ReturnSuccess()
        {
            //Arrange
            var id = 1;
            Record recordDetails = A.Fake<Record>();
            A.CallTo(() => _recordRepository.GetRecordByIdAsync(id)).Returns(recordDetails);

            //Act
            var result = _recordController.DeleteRecord(id);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<Task<IActionResult>>();
        }
    }
}