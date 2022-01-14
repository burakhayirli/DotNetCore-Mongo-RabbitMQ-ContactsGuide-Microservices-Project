using AutoMapper;
using Contact.Api.Controllers;
using Contact.Api.ValidationRules.FluentValidation;
using Contact.Domain.Dtos;
using Contact.Domain.Entities;
using Contact.Repository;
using ContactGuide.Shared.Utilities.Results;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

namespace Contact.Api.xUnitTests
{
    public class PersonsControllerTests
    {
        private Mock<IPersonRepository> _repositoryStub;
        private Mock<IMapper> _mapperStub;

        public PersonsControllerTests()
        {
            _repositoryStub = new Mock<IPersonRepository>();
            _mapperStub = new Mock<IMapper>();
        }

        [Fact]
        public async Task WhenNotExistsPersonIdGiven_GetAsync_ReturnsBadRequestObjectResult()
        {
            // Arrange
            _repositoryStub.Setup(repo => repo.GetAsync(It.IsAny<string>()).Result)
                .Returns(new DataResult<Person>(null, false));
            var controller = new PersonsController(_repositoryStub.Object, _mapperStub.Object);

            //Act
            var result = await controller.Get(ObjectId.GenerateNewId().ToString());

            //Assert
            result.Should().BeOfType<BadRequestObjectResult>();
        }

        [Fact]
        public async Task WhenExistsPersonIdGiven_GetAsync_ReturnsOkObjectResult()
        {
            // Arrange
            _repositoryStub.Setup(repo => repo.GetAsync(It.IsAny<string>()).Result)
                .Returns(new DataResult<Person>(new Person { Id = It.IsAny<string>() }, true));
            var controller = new PersonsController(_repositoryStub.Object, _mapperStub.Object);

            //Act
            var result = await controller.Get(ObjectId.GenerateNewId().ToString());

            //Assert
            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async Task WhenReturnsOneOrMorePersonData_GetAllAsync_ReturnsFilledCollection()
        {
            // Arrange
            var personList = new List<Person>
            {
                new Person { Id = It.IsAny<string>()},
                new Person { Id = It.IsAny<string>()},
                new Person { Id = It.IsAny<string>()},
                new Person { Id = It.IsAny<string>()}
            };

            var dataResult = new DataResult<IEnumerable<Person>>(personList, true);
            
            _repositoryStub.Setup(repo => repo.GetAllAsync().Result)
                    .Returns(dataResult);
            var controller = new PersonsController(_repositoryStub.Object, _mapperStub.Object);

            //Act
            var result = await controller.Get();

            //Assert
            var okResult = result as OkObjectResult;
            Assert.NotNull(okResult);
            var datas = okResult.Value as IEnumerable<Person>;
            datas.Count().Should().BeGreaterThan(0);
        }

        [Theory]
        [InlineData("Burak", "HAYIRLI", "RISE TECHNOLOGY", 32.3212, 34.1211)]
        public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnErrors(string name, string surname, string company, double latitude, double longitude)
        {
            //Arrange
            CreatePersonDto person = new CreatePersonDto
            {
                Name = name,
                Surname = surname,
                Company = company,
                Latitude = latitude,
                Longitude = longitude
            };

            //Act
            CreatePersonDtoValidator validator = new CreatePersonDtoValidator();
            var result = validator.Validate(person);

            //Assert
            result.Errors.Count.Should().Be(0);
        }

        [Theory]
        [InlineData("Burak","HAYIRLI","RISE TECH",-200,200)]
        [InlineData("Bu","HA","RISE",32.3212,34.1211)]
        [InlineData("Burak","HA","RISE TECH",32.3212,34.1211)]
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(string name, string surname, string company,double latitude,double longitude)
        {
            //Arrange
            CreatePersonDto person = new CreatePersonDto
            {
                Name = name,
                Surname=surname,
                Company=company,
                Latitude=latitude,
                Longitude=longitude
            };

            //Act
            CreatePersonDtoValidator validator = new CreatePersonDtoValidator();
            var result = validator.Validate(person);

            //Assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }
    }
}
