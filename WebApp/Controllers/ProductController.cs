using EFCore;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class ProductController : Controller
    {

        AppDbContext _db;

        public ProductController()
            {

            _db = new AppDbContext();
            }

        public IActionResult Index()
        {
            var data = _db.Products.ToList();
            return View(data);
        }

        public IActionResult Create()
        {
            ViewBag.CategoryList = _db.Categories;
            return View();
        }

        [HttpPost]
        public IActionResult Create(ProductViewModel model)
        {
            ModelState.Remove("ProductId");
            if (ModelState.IsValid)
            {
                Product product = new Product
                {
                    Name = model.Name,
                    Description = model.Description,
                    UnitPrice = model.UnitPrice,
                    CategoryId = model.CategoryId
                };
                _db.Products.Add(product);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryList = _db.Categories;
            return View();
        }

        public IActionResult Update(int? Id)
        {

            if (Id is null or <= 0)
                return BadRequest();

            var product = _db.Products.Find(Id);

            if (product == null)
                return NotFound();

            return View(product);

        }

        [HttpPost]
        public IActionResult Update(ProductViewModel model)
        {
            if (!ModelState.IsValid)
               return View(model);

            Product product = new Product
            {
                Name = model.Name,
                Description = model.Description,
                UnitPrice = model.UnitPrice,
                CategoryId = model.CategoryId
            };

            _db.Products.Update(product);
            _db.SaveChanges();
            return RedirectToAction("Index");
            
        }

        public IActionResult Delete(int? Id)
        {

            if (Id is null or <= 0)
                return BadRequest();

            var product = _db.Products.Find(Id);

            if (product == null)
                return NotFound();

            _db.Products.Remove(product);
            _db.SaveChanges();
            return RedirectToAction("Index");

        }

    }

}
