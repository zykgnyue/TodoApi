using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using TodoApi.Mmodels;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TodoApi.Controllers
{
    //public class MyDemoController : Controller
    //{
    //    [Route("")]
    //    [Route("Home")]
    //    [Route("Home/Index")]
    //    public IActionResult MyIndex()
    //    {
    //        return View("Index");
    //    }
    //}

    [Route("api/[controller]")]
    public class TodoController : Controller
    {
        private readonly TodoContext _context;
        public TodoController(TodoContext context)
        {
            _context = context;
            if (_context.TodoItems.Count() == 0)
            {
                _context.TodoItems.Add(new TodoItem { Name = "Item1" });
                _context.SaveChanges();
            }
        }

        [HttpGet]
        public IEnumerable<TodoItem> GetAll()
        {
            return _context.TodoItems.ToList();
        }

        [HttpGet("{Id}",Name="GetTodo")]
        public IActionResult GetById(long id)
        {
            var item = _context.TodoItems.FirstOrDefault(t => t.Id == id);
            if(item==null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }

        [HttpPost]
        public IActionResult Create([FromBody] TodoItem item)
        {
            if(item==null)
            {
                return BadRequest();
            }
            _context.TodoItems.Add(item);
            _context.SaveChanges();
            return CreatedAtRoute("GetTodo", new { id = item.Id }, item);
        }

        /// <summary>
        /// Update method: HTTTP put
        /// </summary>
        /// <param name="id">Id is from URL </param>
        /// <param name="item">from post body </param>
        /// <returns>204 no content</returns>
        [HttpPut("{id}")]
        public IActionResult Update(long id ,[FromBody] TodoItem item)
        {
           
            if(item ==null || item.Id!=id)
            {
                return BadRequest();
            }
            var todo = _context.TodoItems.FirstOrDefault(t => t.Id == id);
            if(todo ==null)
            {
                return NotFound();
            }
            todo.IsComplete = item.IsComplete;
            todo.Name = item.Name;

            _context.TodoItems.Update(todo);
            _context.SaveChanges();
            return new NoContentResult();

        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var todo = _context.TodoItems.FirstOrDefault(t => t.Id == id);
            if(todo ==null)
            {
                return NotFound();
            }
            _context.TodoItems.Remove(todo);
            _context.SaveChanges();
            //responses:204 
            return new NoContentResult();
        }
    }
}
