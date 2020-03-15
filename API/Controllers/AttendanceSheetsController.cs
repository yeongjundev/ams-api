using System;
using System.Collections.Generic;
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
using Services.AttendanceManager;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AttendanceSheetsController : ControllerBase
    {
        private readonly ILogger<AttendanceSheetsController> _logger;

        private readonly IMapper _mapper;

        private readonly IUnitOfWork _uow;

        private readonly IAttendanceManager _attendanceManager;

        public AttendanceSheetsController(
            ILogger<AttendanceSheetsController> logger,
            IMapper mapper,
            IUnitOfWork uow,
            IAttendanceManager attendanceManager
        )
        {
            _logger = logger;
            _mapper = mapper;
            _uow = uow;
            _attendanceManager = attendanceManager;
        }

        [HttpPost]
        public async Task<IActionResult> PostAttendanceSheet([FromBody] PostAttendanceSheetDTO body)
        {
            try
            {
                var lesson = await _uow.LessonRepository.RetrieveById(body.LessonId);
                if (lesson == null)
                {
                    return BadRequest();
                }

                var newAttendanceSheet = _mapper.Map<AttendanceSheet>(body);
                newAttendanceSheet = await _attendanceManager.CreateAttendanceSheet(newAttendanceSheet);

                _uow.Complete(false);
                return CreatedAtRoute("GetAttendanceSheet", new { id = newAttendanceSheet.Id }, _mapper.Map<AttendanceSheetDTO>(newAttendanceSheet));
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in action `PostAttendanceSheet()`. {e.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("{id}", Name = "GetAttendanceSheet")]
        public async Task<IActionResult> GetAttendanceSheet([FromRoute] Guid id)
        {
            try
            {
                var attendanceSheet = _uow.AttendanceSheetRepository.RetrieveById(id);
                if (await attendanceSheet == null)
                {
                    return NotFound();
                }
                return Ok(_mapper.Map<AttendanceSheetDTO>(attendanceSheet.Result));
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in action `GetAttendanceSheet()`. {e.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAttendanceSheets(
            [FromQuery] SearchingOption searchingOption,
            [FromQuery] OrderingOption orderingOption,
            [FromQuery] PaginationOption paginationOption
        )
        {
            try
            {
                var attendanceSheets = await _uow.AttendanceSheetRepository.RetrieveAttendanceSheets(
                    searchingOption,
                    orderingOption,
                    paginationOption
                );
                return Ok(_mapper.Map<PagedAttendanceSheetDTO>(attendanceSheets));
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in action `GetAttendanceSheets()`. {e.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAttendanceSheet([FromRoute] Guid id, [FromBody] PutAttendanceSheetDTO body)
        {
            try
            {
                var attendanceSheet = await _uow.AttendanceSheetRepository.RetrieveById(id);
                if (attendanceSheet == null)
                {
                    return NotFound();
                }
                attendanceSheet = _mapper.Map(body, attendanceSheet);
                _uow.AttendanceSheetRepository.Update(attendanceSheet);

                _uow.Complete(true);
                return Ok(_mapper.Map<AttendanceSheetDTO>(attendanceSheet));
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in action `PutAttendanceSheet()`. {e.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAttendanceSheet([FromRoute] Guid id)
        {
            try
            {
                var attendanceSheet = await _uow.AttendanceSheetRepository.RetrieveById(id);
                if (attendanceSheet == null)
                {
                    return NotFound();
                }
                _uow.AttendanceSheetRepository.Delete(attendanceSheet);

                _uow.Complete(false);
                return Ok();
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in action `DeleteAttendanceSheet()`. {e.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("{id}/attendances/")]
        public async Task<IActionResult> GetStudentsInAttendanceSheet([FromRoute] Guid id)
        {
            try
            {
                var attendances = await _uow.AttendanceRepository.RetrieveAttendanceSheetAttendances(id);
                return Ok(_mapper.Map<List<AttendanceDTO>>(attendances));
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in action `GetStudentsInAttendanceSheet()`. {e.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost("{attendanceSheetId}/attendances/students/{studentId}")]
        public async Task<IActionResult> PostAttendance(
            [FromRoute] Guid attendanceSheetId,
            [FromRoute] Guid studentId
        )
        {
            try
            {
                var attendanceSheet = _uow.AttendanceSheetRepository.RetrieveById(attendanceSheetId);
                var student = _uow.StudentRepository.RetrieveById(studentId);
                Task.WaitAll(attendanceSheet.AsTask(), student.AsTask());
                if (attendanceSheet == null || student == null)
                {
                    return BadRequest();
                }

                var attendance = await _uow.AttendanceRepository
                    .RetrieveById(studentId, attendanceSheet.Result.LessonId, attendanceSheetId);
                if (attendance != null)
                {
                    return BadRequest();
                }

                attendance = new Attendance
                {
                    AttendanceSheetId = attendanceSheetId,
                    LessonId = attendanceSheet.Result.LessonId,
                    StudentId = studentId
                };
                attendanceSheet.Result.Attendances.Add(attendance);
                _uow.AttendanceRepository.Create(attendance);

                _uow.Complete(false);
                return StatusCode(StatusCodes.Status201Created, _mapper.Map<AttendanceSheetDTO>(attendanceSheet.Result));
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in action `PostAttendance()`. {e.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPut("{attendanceSheetId}/attendances/students/{studentId}")]
        public async Task<IActionResult> DeleteAttendance(
            [FromRoute] Guid attendanceSheetId,
            [FromRoute] Guid studentId,
            [FromBody] PutAttendanceDTO body
        )
        {
            try
            {
                var attendanceSheet = _uow.AttendanceSheetRepository.RetrieveById(attendanceSheetId);
                var student = _uow.StudentRepository.RetrieveById(studentId);
                Task.WaitAll(attendanceSheet.AsTask(), student.AsTask());
                if (attendanceSheet == null || student == null)
                {
                    return NotFound();
                }

                var attendance = _uow.AttendanceRepository.RetrieveById(
                    studentId,
                    attendanceSheet.Result.LessonId,
                    attendanceSheetId
                );
                if (await attendance == null)
                {
                    return NotFound();
                }

                attendance.Result.AttendanceType = (AttendanceType)Enum.Parse(typeof(AttendanceType), body.AttendanceType, true);
                _uow.AttendanceRepository.Delete(attendance.Result);

                _uow.Complete(false);
                return Ok();
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in action `DeleteAttendance()`. {e.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpDelete("{attendanceSheetId}/attendances/students/{studentId}")]
        public async Task<IActionResult> DeleteAttendance(
            [FromRoute] Guid attendanceSheetId,
            [FromRoute] Guid studentId
        )
        {
            try
            {
                var attendanceSheet = _uow.AttendanceSheetRepository.RetrieveById(attendanceSheetId);
                var student = _uow.StudentRepository.RetrieveById(studentId);
                Task.WaitAll(attendanceSheet.AsTask(), student.AsTask());
                if (attendanceSheet == null || student == null)
                {
                    return NotFound();
                }

                var attendance = _uow.AttendanceRepository.RetrieveById(
                    studentId,
                    attendanceSheet.Result.LessonId,
                    attendanceSheetId
                );
                if (await attendance == null)
                {
                    return NotFound();
                }
                _uow.AttendanceRepository.Delete(attendance.Result);

                _uow.Complete(false);
                return Ok();
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in action `DeleteAttendance()`. {e.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}