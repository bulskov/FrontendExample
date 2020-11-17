using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DataServiceLib;
using FrontendExample.Models;
using Microsoft.AspNetCore.Mvc;

namespace FrontendExample.Controllers
{
    [ApiController]
    [Route("api/categories")]
    public class CategoriesController : ControllerBase
    {
        private readonly IDataService _dataService;
        private readonly IMapper _mapper;

        public CategoriesController(IDataService dataService, IMapper mapper)
        {
            _dataService = dataService;
            _mapper = mapper;
        }

        [HttpGet(Name = nameof(GetCategories))]
        public IActionResult GetCategories()
        {
            var categories = _dataService.GetCategories().Select(CreateCategoryDto);
            return Ok(categories);
        }

        [HttpGet("{id}", Name = nameof(GetCategory))]
        public IActionResult GetCategory(int id)
        {
            var category = _dataService.GetCategory(id);
            if (category == null)
            {
                return NotFound();
            }

            return Ok(CreateCategoryDto(category));
        }

        [HttpPost]
        public IActionResult CreateCategory(CategoryForCreationDto dto)
        {
            var category = _dataService.CreateCategory(dto.Name, dto.Description);

            return CreatedAtRoute(null, CreateCategoryDto(category));
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCategory(int id)
        {
            if (!_dataService.DeleteCategory(id))
            {
                return NotFound();
            }
            return NoContent();
        }


        /**
         *
         *  Helpers
         *
         */

        CategoryDto CreateCategoryDto(Category category)
        {
            var dto = _mapper.Map<CategoryDto>(category);
            dto.Url = Url.Link(nameof(GetCategory), new {category.Id});
            return dto;
        }

    }
}
