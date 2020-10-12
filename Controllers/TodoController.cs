using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyCoreApi.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyCoreApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly TodoContext _context;
        public TodoController(TodoContext context)
        {
            _context = context; 
            
            if (_context.TodoItems.Count() == 0)
            {
                _context.TodoItems.Add(new TodoItem { Name = "Item1", IsComplete = false });
                _context.TodoItems.Add(new TodoItem { Name = "Item2", IsComplete = false });
                _context.TodoItems.Add(new TodoItem { Name = "Item3", IsComplete = true });
                _context.SaveChanges();
            }
        }

        [HttpGet]
        public IEnumerable<TodoItem> GetAll()
        {
            return _context.TodoItems.ToList();
        }

        /// <summary>
        /// 根据Id查找数据
        /// </summary>
        /// <param name="Id">序号</param>
        /// <returns></returns>
        [HttpGet("{Id}")]
        public async Task<TodoItem> GetByID(int Id)
        {
            var item = await _context.TodoItems.FirstOrDefaultAsync(t => t.Id == Id);

            return item;
        }

        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TodoItem item)
        {
            if (item == null)
            {
                return BadRequest();
            }
            else
            {
                _context.TodoItems.Add(item);
                await _context.SaveChangesAsync();
                return Ok(item);
            }
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] TodoItem item)
        {
            if (item == null)
            {
                return BadRequest();
            }
            else
            {
                var todo = _context.TodoItems.SingleOrDefault(t => t.Id == id);

                if (todo == null)
                {
                    return NotFound();
                }
                else
                {
                    todo.IsComplete = item.IsComplete;
                    todo.Name = item.Name;

                    _context.TodoItems.Update(todo);
                    await _context.SaveChangesAsync();

                    return Ok(todo);
                }

            }
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete(int Id)
        {
            var todo = _context.TodoItems.SingleOrDefault(t => t.Id == Id);

            if (todo == null)
            {

                return NotFound();
            }
            else
            {
                _context.TodoItems.Remove(todo);
                await _context.SaveChangesAsync();

                return Ok();
            }
        }
    }
}
