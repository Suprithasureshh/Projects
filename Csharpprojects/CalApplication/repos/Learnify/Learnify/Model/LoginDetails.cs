using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Learnify.Model
{
    public class LoginDetails
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
