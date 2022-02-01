using StoreFront2.DATA.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity; 
using PagedList;
using PagedList.Mvc;

namespace StoreFront2.UI.MVC.Controllers
{
    public class FiltersController : Controller
    {
        private StoreFrontEntities db = new StoreFrontEntities();

        // GET: Filters
        //public ActionResult Index()
        //{
        //    return View();
        //}

        public ActionResult FilterProducts()
        {
            var products = db.Products.Include(p => p.Category).Include(p => p.Product_Status);
            return View(products.ToList());
        }

        public ActionResult ProductSearch(string searchFilter, int categoryId = 0)                                                               
        {

            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "CategoryName");

            var products = db.Products.ToList();



            if (!String.IsNullOrEmpty(searchFilter))
            {
                products = db.Products
                .Where(p => p.Name.ToLower().Contains(searchFilter.ToLower()) ||
                p.Description.ToLower().Contains(searchFilter.ToLower()))
                .Include(p => p.Category).Include(p => p.Product_Status)
                .ToList();

            }

            if (categoryId != 0)
            {
                products = products.Where(b => b.CategoryID == categoryId).ToList();
            }

            return View(products);
        }

        //public ActionResult LabBooksQS(string searchFilter, int bookStatusId = 0) // int[] cbValue for checkboxes
        //{
        //    ViewBag.pookStatusId = new SelectList(db.pookStatuses, "BookStatusId", "BookStatusName");

        //    var books = db.pooks.ToList();

        //    if (!String.IsNullOrEmpty(searchFilter))
        //    {
        //        books = db.pooks
        //        .Where(b => b.pookTitle.ToLower().Contains(searchFilter.ToLower()) ||
        //        b.ISBN.ToLower().Contains(searchFilter.ToLower()))
        //        .Include(b => b.pookRoyalty).Include(b => b.pookStatus).Include(b => b.Genre).Include(b => b.Publisher)
        //        .ToList();

        //    }

        //    if (bookStatusId != 0)
        //    {
        //        books = books.Where(b => b.pookStatusID == bookStatusId).ToList();
        //    }

        //    return View(books);
        //}

        public ActionResult StoreFrontMVCPaging(string searchString, int page = 1)
        {
            int pageSize = 6;

            var products = db.Products.OrderBy(p => p.Name).ToList();

            if (!String.IsNullOrEmpty(searchString))
            {
                products = (
                            from p in products
                            where p.Name.ToLower().Contains(searchString.ToLower())
                            select p
                         ).ToList();
            }

            ViewBag.SearchString = searchString;

            return View(products.ToPagedList(page, pageSize));
        }

        //public ActionResult LabMagazinesMVCPaging(string searchString, int page = 1)
        //{
        //    int pageSize = 5; //allows us to set how many records/objects are shown per "page"

        //    var Magazines = db.Magazines.OrderBy(b => b.MagazineTitle).ToList(); //this is where our paged list collection will get its data from

        //    if (!String.IsNullOrEmpty(searchString))
        //    {
        //        Magazines = (
        //                    from m in Magazines
        //                    where m.MagazineTitle.ToLower().Contains(searchString.ToLower())
        //                    select m
        //                 ).ToList();
        //    }


        //    ViewBag.SearchString = searchString;

        //    return View(Magazines.ToPagedList(page, pageSize));
        //}
    }
}