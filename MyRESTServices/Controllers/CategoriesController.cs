using Microsoft.AspNetCore.Mvc;
using MyWebFormApp.BLL.DTOs;
using MyWebFormApp.BLL.Interfaces;
using MyWebFormApp.BO;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyRESTServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryBLL _categoryBLL;
        public CategoriesController(ICategoryBLL categoryBLL)
        {
            _categoryBLL = categoryBLL;
        }
        // GET: api/<CategoriesController>
        [HttpGet]
        public IEnumerable<CategoryDTO> Get()
        {
            var result = _categoryBLL.GetAll();
            return result;
        }

        // GET api/<CategoriesController>/5
        [HttpGet("{id}")]
        public ActionResult<CategoryDTO> GetById(int id)
        {
            var category = _categoryBLL.GetById(id);
            if (category == null)
            {
                return NotFound($"CategoryID {id} not found!");
            }
            return Ok(category);
        }

        // POST api/<CategoriesController>
        [HttpPost]
        public IActionResult Insert(CategoryCreateDTO categoryCreate)
        {
            _categoryBLL.Insert(categoryCreate);
            return Ok($"category {categoryCreate.CategoryName} berhasil ditambahkan");
        }

        // PUT api/<CategoriesController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, CategoryUpdateDTO category)
        {
            var result = _categoryBLL.GetById(id);
            if (result == null)
            {
                return NotFound();
            }
            result.CategoryName = category.CategoryName;
            _categoryBLL.Update(category);
            return Ok($"Categori {id} berhasil diupdate");
        }

        // DELETE api/<CategoriesController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = _categoryBLL.GetById(id);
            if (result == null)
            {
                return NotFound();
            }
            _categoryBLL.Delete(id);
            return Ok($"Category ID : {id} berhasil dihapus");
        }
    }
}
