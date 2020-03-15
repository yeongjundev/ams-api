using System;
using System.Threading.Tasks;
using AutoMapper;
using Core.DTOs.Deserializers;
using Core.DTOs.Serializers;
using Core.Helpers;
using Core.Models;
using DataAccess.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentsController : ControllerBase
    {
        private readonly ILogger<StudentsController> _logger;

        private readonly IMapper _mapper;

        private readonly IUnitOfWork _uow;

        public StudentsController(ILogger<StudentsController> logger, IMapper mapper, IUnitOfWork uow)
        {
            _logger = logger;
            _mapper = mapper;
            _uow = uow;
        }

        [HttpPost]
        public IActionResult PostStudent([FromBody] PostStudentDTO body)
        {
            try
            {
                var newStudent = _mapper.Map<Student>(body);
                _uow.StudentRepository.Create(newStudent);

                _uow.Complete(true);
                return CreatedAtRoute("GetStudent", new { id = newStudent.Id }, _mapper.Map<StudentDTO>(newStudent));
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in action `PostStudent()`. {e.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("{id}", Name = "GetStudent")]
        public async Task<IActionResult> GetStudent([FromRoute] Guid id)
        {
            try
            {
                var student = _uow.StudentRepository.RetrieveById(id);
                if (await student == null)
                {
                    return NotFound();
                }
                return Ok(_mapper.Map<StudentDTO>(student));
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in action `GetStudent()`. {e.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetStudents(
            [FromQuery] OrderingOption orderingOption,
            [FromQuery] PaginationOption paginationOption
        )
        {
            try
            {
                var students = _uow.StudentRepository.RetrieveStudents(
                    orderingOption,
                    paginationOption
                );
                return Ok(_mapper.Map<PagedResultDTO<Student>>(await students));
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in action `GetStudents()`. {e.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}