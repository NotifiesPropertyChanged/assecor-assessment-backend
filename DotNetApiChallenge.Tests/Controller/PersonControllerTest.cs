using AutoMapper;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using DotNetApiChallenge.Dto;
using DotNetApiChallenge.Contracts;
using DotNetApiChallenge.Models;
using DotNetApiChallenge.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions.Execution;

namespace DotNetApiChallenge.Tests.Controller
{
    public class PersonControllerTest
    {
        private readonly IColorRepository _colorRepository;
        private readonly IPersonRepository _personRepository;
        private readonly IMapper _mapper;

        public PersonControllerTest()
        {
            _colorRepository = A.Fake<IColorRepository>();
            _personRepository = A.Fake<IPersonRepository>();
            _mapper = A.Fake<IMapper>();
        }

        [Fact]
        public void PersonController_GetPersons_ReturnOK()
        {
            //Arrange
            var persons = A.Fake<ICollection<Person>>();
            var personList = A.Fake<List<PersonDto>>();
            A.CallTo(() => _mapper.Map<List<PersonDto>>(persons)).Returns(personList);
            var controller = new PersonController(_personRepository,_colorRepository, _mapper);

            //Act
            var result = controller.GetPersons();

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(OkObjectResult));
        }

        [Fact]
        public void PersonController_GetPersonWithId_ReturnOK()
        {
            //Arrange
            var personRepo = A.Fake<IPersonRepository>();
            var personDto = A.Fake<PersonDto>();
            var person = A.Fake<Person>();
            var id = 1;
            A.CallTo(() => personRepo.PersonExists(id)).Returns(true);
            A.CallTo( () => personRepo.GetPerson(id)).Returns(person);
            A.CallTo( () => _mapper.Map<PersonDto>(person)).Returns(personDto);           
            var controller = new PersonController(personRepo, _colorRepository, _mapper);

            //Act
            var result = controller.GetPerson(id);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(OkObjectResult));
        }

        [Fact]
        public void PersonController_GetPersonsByColor_ReturnOK()
        {
            //Arrange
            var personRepo = A.Fake<IPersonRepository>();
            var colorRepo = A.Fake<IColorRepository>();
            var personDtos = A.Fake<List<PersonDto>>();
            var persons = A.Fake<ICollection<Person>>();
            var colorId = 1;

            A.CallTo(() => colorRepo.ColorExists(colorId)).Returns(true);
            A.CallTo(() => personRepo.GetPersonsByColor(colorId)).Returns(persons);
            A.CallTo(() => _mapper.Map<List<PersonDto>>(persons)).Returns(personDtos);
            
            var controller = new PersonController(personRepo, colorRepo, _mapper);

            //Act
            var result = controller.GetPersonsByColor(colorId);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(OkObjectResult));
        }

        [Fact]
        public void PersonController_CreatePerson_ReturnOK()
        {
            //Arrange
            var personRepo = A.Fake<IPersonRepository>();
            var colorRepo = A.Fake<IColorRepository>();
            var personMap = A.Fake<Person>();
            var person = A.Fake<Person>();
            var personCreate = A.Fake<PersonDto>();
            var persons = A.Fake<ICollection<PersonDto>>();
            var colorId = 1;
            var color = A.Fake<Color>();
            A.CallTo(() => _mapper.Map<Person>(personCreate)).Returns(person);
            A.CallTo(() => personRepo.CreatePerson(person)).Returns(true);
            A.CallTo(() => colorRepo.GetColor(colorId)).Returns(color);
            var controller = new PersonController(personRepo, colorRepo, _mapper);

            //Act
            var result = controller.CreatePerson(1, new PersonDto { Id = 1, Name = "TestPerson" });

            //Assert
            result.Should().NotBeNull();
        }

    }
}
