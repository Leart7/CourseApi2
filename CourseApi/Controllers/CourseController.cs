using AutoMapper;
using CourseApi.Constants;
using CourseApi.DTOs;
using CourseApi.Models;
using CourseApi.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CourseApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly ICourseRepository _courseRepository;
        private readonly IMapper _mapper;

        public CourseController(ICourseRepository courseRepository, IMapper mapper)
        {
            _courseRepository = courseRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetCourses([FromQuery] string? name, [FromQuery] string? category, [FromQuery] string? status, [FromQuery] int? pageNumber)
        {
            var result = await _courseRepository.GetCourses(name, category, status, pageNumber);

            var courses = result.Courses;   
            var totalCourses = result.TotalCourses;
            var totalPages = result.TotalPages;

            return Ok(new { Courses = _mapper.Map<List<CourseDto>>(courses), totalCourses, totalPages });
        }

        [HttpGet]
        [Route("popular")]
        public async Task<IActionResult> GetPopularCourses()
        {
            var courses = await _courseRepository.GetPopularCourses();
            return Ok(_mapper.Map<List<CourseDto>>(courses));
        }

        [HttpGet]
        [Route("category/{categoryId}")]
        public async Task<IActionResult> GetCoursesByCategory([FromRoute] int categoryId)
        {
            var courses = await _courseRepository.GetCoursesByCategory(categoryId);
            return Ok(_mapper.Map<List<CourseDto>>(courses));
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetCourseById([FromRoute] int id)
        {
            var course = await _courseRepository.GetCourse(id);
            if(course == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<CourseDto>(course));
        }

        [HttpPost]
        public async Task<IActionResult> CreateCourse([FromForm] CreateCourseRequestDto course)
        {
            var courseDomainModel = _mapper.Map<Course>(course);

            if((course.Status == CourseStatuses.Paid || course.Status == CourseStatuses.Subscription) && course.Price == null)
            {
                return BadRequest("A price must be assigned for this type of status!");
            }

            var newCourse = await _courseRepository.CreateCourse(courseDomainModel);


            return Ok(_mapper.Map<CourseDto>(newCourse));
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateCourse([FromRoute] int id, [FromForm] UpdateCourseRequestDto updateCourseRequestDto)
        {
            var courseDomainModel = _mapper.Map<Course>(updateCourseRequestDto);

            if ((courseDomainModel.Status == CourseStatuses.Paid || courseDomainModel.Status == CourseStatuses.Subscription) && (courseDomainModel.Price == 0 || courseDomainModel.Price == null))
            {
                return BadRequest("A price must be assigned for this type of status!");
            }


            courseDomainModel = await _courseRepository.UpdateCourse(id, courseDomainModel);

            if( courseDomainModel == null )
            {
                return NotFound();
            }

            return Ok(_mapper.Map<CourseDto>(courseDomainModel));
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteCourse([FromRoute] int id)
        {
            var course = await _courseRepository.GetCourse(id);
            if(course == null)
            {
                return NotFound();
            }

            await _courseRepository.DeleteCourse(id);

            return NoContent();
        }
    }
}
