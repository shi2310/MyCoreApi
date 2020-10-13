using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace MyCoreApi.Models
{
    public class TodoItemFilePost
    {
        [Required(ErrorMessage = "{0}必填")]
        public string name { get; set; }

        public bool isComplete { get; set; }

        public IFormFile file { get; set; }
    }
}