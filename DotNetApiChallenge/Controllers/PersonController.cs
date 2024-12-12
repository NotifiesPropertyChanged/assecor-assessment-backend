using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using DotNetApiChallenge.Dto;
using DotNetApiChallenge.Contracts;
using DotNetApiChallenge.Models;

namespace DotNetApiChallenge.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : Controller
    {
        private readonly IPersonRepository _personRepository;
        private readonly IColorRepository _colorRepository;
        private readonly IMapper _mapper;

        public PersonController(IPersonRepository personRepository,
            IColorRepository colorRepository,
            IMapper mapper)
        {
            _personRepository = personRepository;
            _colorRepository = colorRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Person>))]
        public IActionResult GetPersons()
        {
            var persons = _mapper.Map<List<PersonDto>>(_personRepository.GetPersons());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(persons);
        }

        [HttpGet("{personId}")]
        [ProducesResponseType(200, Type = typeof(Person))]
        [ProducesResponseType(400)]
        public IActionResult GetPerson(int personId)
        {
            if (!_personRepository.PersonExists(personId))
                return NotFound();

            var person = _mapper.Map<PersonDto>(_personRepository.GetPerson(personId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(person);
        }

        [HttpGet("color/{id}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Person>))]
        [ProducesResponseType(400)]
        public IActionResult GetPersonsByColor(int id)
        {
            if (!_colorRepository.ColorExists(id))
            {
                return NotFound();
            }

            var persons = _mapper.Map<List<PersonDto>>(_personRepository.GetPersonsByColor(id));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(persons);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreatePerson([FromQuery] int colorId, [FromBody] PersonDto personCreate)
        {
            if (personCreate == null)
                return BadRequest(ModelState);

            var persons = _personRepository.GetPersons()
                .Where(c => c.Lastname.Trim().ToUpper() == personCreate.Lastname.TrimEnd().ToUpper())
                .FirstOrDefault();

            if (persons != null)
            {
                ModelState.AddModelError("", "Person already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var personMap = _mapper.Map<Person>(personCreate);

            personMap.Color = _colorRepository.GetColor(colorId);

            if (!_personRepository.CreatePerson(personMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }

        [HttpPut("{personId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdatePerson(int personId, [FromBody] PersonDto updatedPerson)
        {
            if (updatedPerson == null)
                return BadRequest(ModelState);

            if (personId != updatedPerson.Id)
                return BadRequest(ModelState);

            if (!_personRepository.PersonExists(personId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var personMap = _mapper.Map<Person>(updatedPerson);

            if (!_personRepository.UpdatePerson(personMap))
            {
                ModelState.AddModelError("", "Something went wrong while updating person");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{personId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeletePerson(int personId)
        {
            if (!_personRepository.PersonExists(personId))
            {
                return NotFound();
            }

            var personToDelete = _personRepository.GetPerson(personId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_personRepository.DeletePerson(personToDelete))
            {
                ModelState.AddModelError("", $"Something went wrong deleting Person with ID: {personId}");
            }

            return NoContent();
        }
    }
}

