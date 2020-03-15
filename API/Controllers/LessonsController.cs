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
    public class LessonsController : ControllerBase
    {
        private readonly ILogger<LessonsController> _logger;

        private readonly IMapper _mapper;

        private readonly IUnitOfWork _uow;

        public LessonsController(ILogger<LessonsController> logger, IMapper mapper, IUnitOfWork uow)
        {
            _logger = logger;
            _mapper = mapper;
            _uow = uow;
        }

        [HttpPost]
        public IActionResult PostLesson([FromBody] PostLessonDTO body)
        {
            try
            {
                var newLesson = _mapper.Map<Lesson>(body);
                _uow.LessonRepository.Create(newLesson);

                _uow.Complete(false);
                return CreatedAtRoute("GetLesson", new { id = newLesson.Id }, _mapper.Map<LessonDTO>(newLesson));
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in action `PostLesson()`. {e.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("{id}", Name = "GetLesson")]
        public async Task<IActionResult> GetLesson([FromRoute] Guid id)
        {
            try
            {
                var lesson = _uow.LessonRepository.RetrieveById(id);
                if (await lesson == null)
                {
                    return NotFound();
                }
                return Ok(_mapper.Map<LessonDTO>(lesson));
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in action `GetLesson()`. {e.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetLessons(
            [FromQuery] SearchingOption searchingOption,
            [FromQuery] OrderingOption orderingOption,
            [FromQuery] PaginationOption paginationOption
        )
        {
            try
            {
                var lessons = _uow.LessonRepository.RetrieveLessons(
                    searchingOption,
                    orderingOption,
                    paginationOption
                );
                return Ok(_mapper.Map<PagedLessonDTO>(await lessons));
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in action `GetLessons()`. {e.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPut("{lessonId}")]
        public async Task<IActionResult> PutLesson([FromRoute] Guid lessonId, [FromBody] PutLessonDTO body)
        {
            try
            {
                var lesson = await _uow.LessonRepository.RetrieveById(lessonId);
                if (lesson == null)
                {
                    return NotFound();
                }
                lesson = _mapper.Map(body, lesson);
                _uow.LessonRepository.Update(lesson);

                _uow.Complete(true);
                return Ok(_mapper.Map<LessonDTO>(lesson));
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in action `PutLesson()`. {e.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpDelete("{lessonId}")]
        public async Task<IActionResult> DeleteLesson([FromRoute] Guid lessonId)
        {
            try
            {
                var lesson = await _uow.LessonRepository.RetrieveById(lessonId);
                if (lesson == null)
                {
                    return NotFound();
                }
                _uow.LessonRepository.Delete(lesson);

                _uow.Complete(false);
                return Ok();
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in action `DeleteLesson()`. {e.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("{lessonId}/enrolments/students/")]
        public async Task<IActionResult> GetEnrolledStudents(
            [FromRoute] Guid lessonId,
            [FromQuery] SearchingOption searchingOption,
            [FromQuery] OrderingOption orderingOption,
            [FromQuery] PaginationOption paginationOption
        )
        {
            try
            {
                var enrolledStudents = _uow.StudentRepository.RetrieveEnrolledStudents(
                    lessonId,
                    searchingOption,
                    orderingOption,
                    paginationOption
                );
                return Ok(_mapper.Map<PagedStudentDTO>(await enrolledStudents));
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in action `GetEnrolledStudents()`. {e.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}