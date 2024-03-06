using FluentAssertions;
using MaldsKeeperWebApp.Data;
using MaldsKeeperWebApp.Interfaces;
using MaldsKeeperWebApp.Models;
using MaldsKeeperWebApp.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Record = MaldsKeeperWebApp.Models.Record;

namespace MaldsKeeperWebApp.Test.Repository
{
    public class RecordRepositoryTests
    {
        private ApplicationDbContext _context;
        private HttpContextAccessor _contextAccessor;
        private IUserRepository _userRepository;
        private IRecordRepository _recordRepository;
        public RecordRepositoryTests()
        {
            _context = GetDbContext().Result;
            _context.Records.AsNoTracking();
            _contextAccessor = new HttpContextAccessor();
            _userRepository = new UserRepository(_context, _contextAccessor);
            _recordRepository = new RecordRepository(_context, _userRepository);
        }
        private async Task<ApplicationDbContext> GetDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var databaseContext = new ApplicationDbContext(options);
            databaseContext.Database.EnsureCreated();

            if (await databaseContext.Records.CountAsync() <= 0)
            {
                for (int i = 0; i < 10; i++)
                {
                    databaseContext.Records.Add(
                        new Record()
                        {
                            Id = i,
                            Title = "Test title",
                            Login = "tester@test.com",
                            Password = "GhGyinKnNB6vgh54",
                            Description = "Test description",
                            AddedTime = new DateTime(2022, 10, 15, 12, 30, 0),
                            AppUserId = "test_id",
                            AppUser = new AppUser()
                            {
                                Id = "test_id",
                                Key = "test_key",
                                AddedTime = new DateTime(2022, 10, 15, 12, 30, 0)
                            }
                        });
                    await databaseContext.SaveChangesAsync();
                }
            }
            return databaseContext;
        }
        [Fact]
        public void RecordRepository_Create_ReturnsBool()
        {
            //Arrange
            var record = new Record()
            {
                Id = 1000,
                Title = "Test title",
                Login = "tester@test.com",
                Password = "GhGyinKnNB6vgh54",
                Description = "Test description",
                AddedTime = new DateTime(2022, 10, 15, 12, 30, 0),
                AppUserId = "test_id1",
                AppUser = new AppUser()
                {
                    Id = "test_id",
                    Key = "test_key",
                    AddedTime = new DateTime(2022, 10, 15, 12, 30, 0)
                }
            };

            //Act
            var result = _recordRepository.Create(record);

            //Assert
            result.Should().BeTrue();
        }

        [Fact]
        public async void RecordRepository_Edit_ReturnsBool()
        {
            //Arrange
            var record = new Record()
            {
                Id = 1,
                Title = "Test title5",
                Login = "tester5@test.com",
                Password = "GhGyinKnNB6vgh54",
                Description = "Test description5",
                AddedTime = new DateTime(2022, 10, 15, 12, 30, 0),
                AppUserId = "test_id5",
                AppUser = new AppUser()
                {
                    Id = "test_id",
                    Key = "test_key5",
                    AddedTime = new DateTime(2022, 10, 15, 12, 30, 0)
                }
            };
            var dbContext = await GetDbContext();
            var recordRepository = new RecordRepository(dbContext, _userRepository);


            //Act
            var result = _recordRepository.Edit(record);

            //Assert
            result.Should().BeTrue();
        }
    }
}
