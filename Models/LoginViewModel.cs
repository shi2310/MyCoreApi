using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyCoreApi.Models
{
    public class LoginViewModel
    {
        [JsonProperty("username")]
        [Required(ErrorMessage = "{0}必填")]
        public string Username { get; set; }

        [JsonProperty("password")]
        [Required(ErrorMessage = "{0}必填")]
        public string Password { get; set; }
    }
}