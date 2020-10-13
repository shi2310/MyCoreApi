using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyCoreApi.Entity;
using MyCoreApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
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
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] TodoItemFilePost model)
        {
            if (ModelState.IsValid)
            {
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), string.Format("{0}{1}", Guid.NewGuid(), Path.GetExtension(model.file.FileName)));
                using (var stream = System.IO.File.Create(filePath))
                {
                    await model.file.CopyToAsync(stream);
                }

                TodoItem item = new TodoItem() { Name = model.name, IsComplete = model.isComplete };
                _context.TodoItems.Add(item);
                await _context.SaveChangesAsync();
                return Ok(item);
            }
            else
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromForm] TodoItemFilePost model)
        {
            if (ModelState.IsValid)
            {
                var todo = _context.TodoItems.SingleOrDefault(t => t.Id == id);

                if (todo == null)
                {
                    return NotFound();
                }
                else
                {
                    if (model.file.Length > 0)
                    {
                        var filePath = Path.Combine(Directory.GetCurrentDirectory(), string.Format("{0}{1}", Guid.NewGuid(), Path.GetExtension(model.file.FileName)));
                        using (var stream = System.IO.File.Create(filePath))
                        {
                            await model.file.CopyToAsync(stream);
                        }
                    }

                    todo.IsComplete = model.isComplete;
                    todo.Name = model.name;

                    _context.TodoItems.Update(todo);
                    await _context.SaveChangesAsync();

                    return Ok(todo);
                }
            }
            else
            {
                return BadRequest();
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