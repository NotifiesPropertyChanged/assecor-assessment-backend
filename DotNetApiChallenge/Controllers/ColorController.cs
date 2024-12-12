//DotNetApiChallenge
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using DotNetApiChallenge.Dto;
using DotNetApiChallenge.Contracts;
using DotNetApiChallenge.Models;

namespace DotNetApiChallenge.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ColorController : Controller
    {
        private readonly IColorRepository _colorRepository;
        private readonly IMapper _mapper;

        public ColorController(IColorRepository colorRepository, IMapper mapper)
        {
            _colorRepository = colorRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Color>))]
        public IActionResult GetColors()
        {
            var countries = _mapper.Map<List<ColorDto>>(_colorRepository.GetColors());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(countries);
        }

        [HttpGet("{colorId}")]
        [ProducesResponseType(200, Type = typeof(Color))]
        [ProducesResponseType(400)]
        public IActionResult GetColor(int colorId)
        {
            if (!_colorRepository.ColorExists(colorId))
                return NotFound();

            var color = _mapper.Map<ColorDto>(_colorRepository.GetColor(colorId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(color);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateColor([FromBody] ColorDto colorCreate)
        {
            if (colorCreate == null)
                return BadRequest(ModelState);

            var color = _colorRepository.GetColors()
                .Where(c => c.Name.Trim().ToUpper() == colorCreate.Name.TrimEnd().ToUpper())
                .FirstOrDefault();

            if (color != null)
            {
                ModelState.AddModelError("", "Color already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var colorMap = _mapper.Map<Color>(colorCreate);

            if (!_colorRepository.CreateColor(colorMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }

        [HttpPut("{colorId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateCategory(int colorId, [FromBody] ColorDto updatedColor)
        {
            if (updatedColor == null)
                return BadRequest(ModelState);

            if (colorId != updatedColor.Id)
                return BadRequest(ModelState);

            if (!_colorRepository.ColorExists(colorId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var colorMap = _mapper.Map<Color>(updatedColor);

            if (!_colorRepository.UpdateColor(colorMap))
            {
                ModelState.AddModelError("", "Something went wrong updating category");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{colorId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteColor(int colorId)
        {
            if (!_colorRepository.ColorExists(colorId))
            {
                return NotFound();
            }

            var colorToDelete = _colorRepository.GetColor(colorId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_colorRepository.DeleteColor(colorToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting category");
            }

            return NoContent();
        }
    }
}

