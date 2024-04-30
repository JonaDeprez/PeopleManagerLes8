using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PeopleManager.Core;
using PeopleManager.Model;

namespace PeopleManager.Ui.Mvc.Controllers
{
    public class PeopleController : Controller
    {
        private readonly PeopleManagerDbContext _dbContext;

        public PeopleController(PeopleManagerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var people = _dbContext.People
                .Include(p => p.Organization)
                .ToList();

            return View(people);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return CreateEditView();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Person person)
        {
            if (!ModelState.IsValid)
            {
                return CreateEditView(person);
            }

            _dbContext.People.Add(person);
            _dbContext.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit([FromRoute]int id)
        {
            var person = _dbContext.People
                .FirstOrDefault(p => p.Id == id);

            if (person is null)
            {
                return RedirectToAction("Index");
            }

            return CreateEditView(person);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute]int id, [FromForm]Person person)
        {
            if (!ModelState.IsValid)
            {
                return CreateEditView(person);
            }

            var dbPerson = _dbContext.People
                .FirstOrDefault(p => p.Id == id);

            if (dbPerson is null)
            {
                return RedirectToAction("Index");
            }

            dbPerson.FirstName = person.FirstName;
            dbPerson.LastName = person.LastName;
            dbPerson.Email = person.Email;
            dbPerson.OrganizationId = person.OrganizationId;

            _dbContext.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete([FromRoute] int id)
        {
            var person = _dbContext.People
                .FirstOrDefault(p => p.Id == id);

            return View(person);
        }

        [HttpPost("/[controller]/Delete/{id:int?}"), ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var person = _dbContext.People
                .FirstOrDefault(p => p.Id == id);

            if (person is null)
            {
                return RedirectToAction("Index");
            }

            _dbContext.People.Remove(person);
            _dbContext.SaveChanges();

            return RedirectToAction("Index");
        }
        
        private IActionResult CreateEditView(Person? person = null)
        {
            var organizations = _dbContext.Organizations.ToList();
            ViewBag.Organizations = organizations;
            return View(person);
        }
    }
}
