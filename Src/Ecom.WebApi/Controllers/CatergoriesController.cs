using AutoMapper;
using Ecom.Core.DTos;
using Ecom.Core.Entities;
using Ecom.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecom.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatergoriesController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public CatergoriesController( IUnitOfWork unitOfWork,IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        [HttpGet("GetAllGategories")]
        public async Task<IActionResult> GetAllGategories()
        {
            var cateories= await unitOfWork.Category.GetAllAsync();
            if (cateories != null && cateories.Count() > 0)
            {
                var result = cateories.Select(e => mapper.Map<CategoryDto>(e));
                return Ok(result);

            }
            return BadRequest("There is no categories");

        }
        [HttpGet("GetCategoryById/{Id}")]
        public IActionResult GetCategoryById(int Id)
        {
            var category= unitOfWork.Category.GetById(Id).Result;
            if (category != null)
                // return Ok(new CategoryDto { Name=category.Name,Descrption=category.Description}); // using manual mapping
                return Ok(mapper.Map<CategoryDto>(category)); //using Auto Mapper
            return BadRequest("There is no object Matched this ID");
        }
        [HttpPost("AddCategory")]
        public IActionResult AddCategory(CategoryDto category)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = unitOfWork.Category.AddElement(mapper.Map<Category>(category)).Result;
                    return Ok(result);
                }
                catch (Exception ex)
                {

                    return BadRequest(ex.Message);
                }
            }
            return BadRequest();
        }
        [HttpPut("UpdateGategory/{Id}")]
        public IActionResult UpdateGategory(int Id, CategoryDto category)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var extingCategory = unitOfWork.Category.GetById(Id).Result;
                    if (extingCategory != null)
                    {
                        //extingCategory.Description = category.Descrption;
                        //extingCategory.Name = category.Name;
                         mapper.Map(category,extingCategory);

                         unitOfWork.Category.UpdateElement(extingCategory, Id);
                        return Ok(extingCategory);
                    }
                       
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
               
            }
            return BadRequest($"your are not Update this {Id}");
        }

        [HttpDelete("DeleteGategoryById/{Id}")]
        public async Task<IActionResult> DeleteGategoryById(int Id)
        {
            var exitingCategory = unitOfWork.Category.GetById(Id).Result;
            if (exitingCategory != null)
            {
                try
                {
                    await unitOfWork.Category.DeleteElement(exitingCategory);
                    return Ok("your Category is deleted");
                }

                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }

            return BadRequest("your Category is Not Found ");
        }
    }
}
