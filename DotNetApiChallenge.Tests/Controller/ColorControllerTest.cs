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

namespace DotNetApiChallenge.Tests.Controller
{
    public  class ColorControllerTest
    {
        private readonly IColorRepository _colorRepository;
        private readonly IMapper _mapper;

        public ColorControllerTest()
        {
            _colorRepository = A.Fake<IColorRepository>();
            _mapper = A.Fake<IMapper>();
        }

        [Fact]
        public void ColorController_GetColors_ReturnOK()
        {
            //Arrange
            var colors = A.Fake<ICollection<ColorDto>>();
            var colorList = A.Fake<List<ColorDto>>();
            A.CallTo(() => _mapper.Map<List<ColorDto>>(colors)).Returns(colorList);
            var controller = new ColorController(_colorRepository, _mapper);

            //Act
            var result = controller.GetColors();

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(OkObjectResult));
        }

        [Fact]
        public void ColorController_GetColorWithId_ReturnOK()
        {
            //Arrange
            var colorRepo = A.Fake<IColorRepository>();
            var colorDto = A.Fake<ColorDto>();
            var color = A.Fake<Color>();
            var id = 1;
            A.CallTo(() => colorRepo.ColorExists(id)).Returns(true);
            A.CallTo(() => colorRepo.GetColor(id)).Returns(color);
            A.CallTo(() => _mapper.Map<ColorDto>(color)).Returns(colorDto);
            var controller = new ColorController(colorRepo, _mapper);

            //Act
            var result = controller.GetColor(id);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(OkObjectResult));
        }
        [Fact]
        public void ColorController_CreateColor_ReturnOK()
        {
            //Arrange
            var colorMap = A.Fake<Color>();
            var color = A.Fake<Color>();
            var colorCreate = A.Fake<ColorDto>();
            var colors = A.Fake<ICollection<ColorDto>>();

            A.CallTo(() => _mapper.Map<Color>(colorCreate)).Returns(color);
            A.CallTo(() => _colorRepository.CreateColor(color)).Returns(true);
            var controller = new ColorController(_colorRepository, _mapper);

            //Act
            var result = controller.CreateColor(new ColorDto { Id = 1, Name = "TestColor" });

            //Assert
            result.Should().NotBeNull();
        }
    }
}
