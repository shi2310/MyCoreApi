using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MyCoreApi.Models
{
    public class TodoItem
    {
        //[DatabaseGenerated(DatabaseGeneratedOption.None)] 取消自增
        public long Id { get; set; }
        public string Name { get; set; }
        public bool IsComplete { get; set; }
    }
}
