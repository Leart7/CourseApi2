using AutoMapper;
using CourseApi.DTOs;
using CourseApi.Models;
using CourseApi.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CourseApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryController(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
            var categories = await _categoryRepository.GetCategories();

            var mappedData = categories.Select(data =>
            {
                var categoryDto = _mapper.Map<CategoryDto>(data.Item1);
                categoryDto.CoursesCount = data.Item2;
                return categoryDto;
            }).ToList();

            return Ok(mappedData);
        }


        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromForm] CreateCategoryRequestDto category )
        {
            var categoryNames = await _categoryRepository.GetCategoryNames();

            if (categoryNames.Contains(category.Name.ToLower()))
            {
                return Conflict("Category with this name already exists!");
            }

            var categoryDomainModel = _mapper.Map<Category>(category);
            categoryDomainModel = await _categoryRepository.CreateCategory(categoryDomainModel);

            return Ok(_mapper.Map<CategoryDto>(categoryDomainModel));
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateCategory([FromRoute] int id, [FromForm] UpdateCategoryRequestDto updateCategoryRequestDto)
        {
            var categoryNames = await _categoryRepository.GetCategoryNames();

            if (categoryNames.Contains(updateCategoryRequestDto.Name.ToLower()))
            {
                return Conflict("Category with this name already exists!");
            }

            var categoryDomainModel = _mapper.Map<Category>(updateCategoryRequestDto);
            categoryDomainModel = await _categoryRepository.UpdateCategory(id, categoryDomainModel);

            if(categoryDomainModel == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<CategoryDto>(categoryDomainModel));
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteCategory([FromRoute] int id)
        {
            var category = await _categoryRepository.DeleteCategory(id);
            if (category == null)
            {
                return NotFound();
            }

            return NoContent();
        }

    }
}
