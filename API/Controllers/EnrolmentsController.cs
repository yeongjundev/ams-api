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
    public class EnrolmentsController : ControllerBase
    {
        private readonly ILogger<EnrolmentsController> _logger;

        private readonly IMapper _mapper;

        private readonly IUnitOfWork _uow;

        public EnrolmentsController(ILogger<EnrolmentsController> logger, IMapper mapper, IUnitOfWork uow)
        {
            _logger = logger;
            _mapper = mapper;
            _uow = uow;
        }

        [HttpPost("students/{studentId}/lessons/{lessonId}")]
        public async Task<IActionResult> PostEnrolment([FromRoute] Guid studentId, [FromRoute] Guid lessonId)
        {
            try
            {
                var getStudent = _uow.StudentRepository.RetrieveById(studentId);
                var getLesson = _uow.LessonRepository.RetrieveById(lessonId);
                Task.WaitAll(getStudent.AsTask(), getLesson.AsTask());

                var student = getStudent.Result;
                var lesson = getLesson.Result;
                if (student == null || lesson == null)
                {
                    return NotFound();
                }

                var enrolment = await _uow.EnrolmentRepository.RetrieveById(studentId, lessonId);

                var result = enrolment != null;

                if (enrolment != null)
                {
                    return BadRequest();
                }

                var newEnrolment = new Enrolment()
                {
                    StudentId = studentId,
                    Student = student,
                    LessonId = lessonId,
                    Lesson = lesson
                };
                _uow.EnrolmentRepository.Create(newEnrolment);

                _uow.Complete(false);
                return StatusCode(StatusCodes.Status201Created, _mapper.Map<EnrolmentDTO>(newEnrolment));
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in action `PostEnrolment()`. {e.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpDelete("students/{studentId}/lessons/{lessonId}")]
        public async Task<IActionResult> DeleteEnrolment([FromRoute] Guid studentId, [FromRoute] Guid lessonId)
        {
            try
            {
                var getStudent = _uow.StudentRepository.RetrieveById(studentId);
                var getLesson = _uow.LessonRepository.RetrieveById(lessonId);
                Task.WaitAll(getStudent.AsTask(), getLesson.AsTask());

                var student = getStudent.Result;
                var lesson = getLesson.Result;
                if (student == null || lesson == null)
                {
                    return NotFound();
                }

                var enrolment = await _uow.EnrolmentRepository.RetrieveById(studentId, lessonId);
                if (enrolment == null)
                {
                    return BadRequest();
                }

                _uow.EnrolmentRepository.Delete(enrolment);

                _uow.Complete(false);
                return Ok();
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in action `DeleteEnrolment()`. {e.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}