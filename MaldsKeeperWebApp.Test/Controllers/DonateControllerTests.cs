using FakeItEasy;
using FluentAssertions;
using MaldsKeeperWebApp.Controllers;
using Microsoft.AspNetCore.Mvc;
using Stripe;
using Stripe.Checkout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaldsKeeperWebApp.Test.Controllers
{
    public class DonateControllerTests
    {
        private DonateController _donateController;
        public DonateControllerTests()
        {
            _donateController = new DonateController();
        }
        [Fact]
        public void DonateController_IndexGet_ReturnSuccess()
        {
            //Arrange


            //Act
            var result = _donateController.Index();

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<ViewResult>();
        }

        [Fact]
        public void DonateController_SuccessGet_ReturnSuccess()
        {
            //Arrange


            //Act
            var result = _donateController.Success();

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<ViewResult>();
        }

        [Fact]
        public void DonateController_CancelGet_ReturnSuccess()
        {
            //Arrange


            //Act
            var result = _donateController.Cancel();

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<ViewResult>();
        }
    }
}
