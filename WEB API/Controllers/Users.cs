using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using System.Text.RegularExpressions;
using WEB_API.Data;
using WEB_API.Models;

namespace WEB_API.Controllers
{
    [ApiController]
    [Route("api")]
    public class Users : ControllerBase
    {

        private UserDB db = new UserDB();
        
        [HttpGet]
        [Route("users")]
        public IActionResult getUsers()
        {
            try
            {
                var users = from u in db.Users
                            select u;
                if (users != null)
                {
                    return Ok(users);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException.Message);
            }
        }

        /* [HttpPost]
         [Route("new_user")]
         public dynamic createUsers(User user)
         {
             db.Users.Add(user);
             db.SaveChanges();
             return new
             {
                 succes = true,
                 message = "New user registered",
                 result = user
             };
         }*/

        [HttpPost]
        [Route("new_user")]
        public IActionResult createUsers(User user)
        {
            if (string.IsNullOrWhiteSpace(user.Name) || string.IsNullOrWhiteSpace(user.Last_name) || string.IsNullOrWhiteSpace(user.Email) || string.IsNullOrWhiteSpace(user.Phone))
            {
                return BadRequest("All fields are required");
            }

            if (user.Name.Length < 3)
            {
                return BadRequest("Name must have at least 3 characters");
            }

            if (!mailValidation(user.Email))
            {
                return BadRequest("Enter a valid email address");
            }

            if (!phoneValidation(user.Phone))
            {
                return BadRequest("Enter a valid phone number");
            }

            try
            {
                db.Users.Add(user);
                db.SaveChanges();
                return Ok(new
                {
                    succes = true,
                    message = "New user registered",
                    result = user
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException.Message);
            }
        }

        private bool mailValidation(string email)
        {
            string pattern = @"^[a-zA-Z0-9._-]+@[a-zA-Z]+\.[a-zA-Z]{2,4}$";
            return Regex.IsMatch(email, pattern);
        }

        private bool phoneValidation(string phone)
        {
            string pattern = @"^\d{3}[-\s]?\d{3}[-\s]?\d{2}[-\s]?\d{2}$";
            return Regex.IsMatch(phone, pattern);
        }

        [HttpGet]
        [Route("userdetail/{id}")]
        public IActionResult UserDetail(int id)
        {
            try
            {
                var users = db.Users.FirstOrDefault(u => u.Id == id);
                if (users != null)
                {
                    return Ok(users);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException.Message);
            }           
        }

    }
}
