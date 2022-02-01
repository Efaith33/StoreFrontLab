using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StoreFront2.Models
{
    public class ContactViewModel
    {
        [Required(ErrorMessage = "*")]
        public string Name { get; set; }

        [Required(ErrorMessage = "*")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        public string Subject { get; set; }

        [Required(ErrorMessage = "*")]
        [UIHint("MultilineText")]
        public string Message { get; set; }
    }
}