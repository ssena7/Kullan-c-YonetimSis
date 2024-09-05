using System;
using System.Linq;
using System.Web.Http;
using Projednm.Models;


public class UsersController : ApiController
{
    private YourDbContext db = new YourDbContext();

    // GET api/users
    [HttpGet]
    public IHttpActionResult Get()
    {
        var users = db.Users.ToList();
        if (users == null || !users.Any())
        {
            return NotFound();
        }
        return Ok(users);
    }

    // GET api/users/5
    [HttpGet]
    public IHttpActionResult Get(Guid id)
    {
        var user = db.Users.Find(id);
        if (user == null)
        {
            return NotFound();
        }
        return Ok(user);
    }

    [HttpPost]
    public IHttpActionResult Post([FromBody] User user)
    {
        try
        {
            if (user == null)
            {
                return BadRequest("User data is null.");
            }

            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();
                return BadRequest(string.Join(", ", errors));
            }

            user.UserUUID = Guid.NewGuid(); // UUID'yi otomatik olarak oluştur
            user.CreationDate = DateTime.UtcNow; // CreationDate'yi otomatik olarak ayarlayın
            db.Users.Add(user);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = user.UserUUID }, user);
        }
        catch (Exception ex)
        {
            return InternalServerError(ex);
        }
    }

    // PUT api/users/5
    [HttpPut]
    public IHttpActionResult Put(Guid id, [FromBody] User user)
    {
        var existingUser = db.Users.Find(id);
        if (existingUser == null)
        {
            return NotFound();
        }

        existingUser.FirstName = user.FirstName;
        existingUser.LastName = user.LastName;
        existingUser.Email = user.Email;
        existingUser.PhoneNumber = user.PhoneNumber;
        existingUser.City = user.City;
        db.SaveChanges();

        return Ok(existingUser);
    }

    // DELETE api/users/5
    [HttpDelete]
    public IHttpActionResult Delete(Guid id)
    {
        var user = db.Users.Find(id);
        if (user == null)
        {
            return NotFound();
        }

        db.Users.Remove(user);
        db.SaveChanges();

        return Ok();
    }
}
