using StoreFront2.DATA.EF;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PagedList;
using StoreFront2.Models;
using StoreFront2.UI.MVC.Utilities;

namespace StoreFront2.Controllers
{
    public class ProductsController : Controller
    {
        private StoreFrontEntities db = new StoreFrontEntities();

        public ActionResult Index(string searchString, int page = 1)
        {
            int pageSize = 6;

            var products = db.Products.OrderBy(p => p.Name).ToList();

            if (!String.IsNullOrEmpty(searchString))
            {
                products = (
                            from p in products
                            where p.Name.ToLower().Contains(searchString.ToLower()) ||
                                  p.Category.CategoryName.ToLower().Contains(searchString.ToLower())
                            select p
                         ).ToList();
            }

            ViewBag.SearchString = searchString;

            return View(products.ToPagedList(page, pageSize));
        }



        // GET: Products
        //public ActionResult Index()
        //{
        //    var products = db.Products.Include(p => p.Category).Include(p => p.Product_Status);
        //    return View(products.ToList());
        //}

        // GET: Products
        public ActionResult ProductsTable()
        {
            var products = db.Products.Include(p => p.Category).Include(p => p.Product_Status);
            return View(products.ToList());
        }

        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        public ActionResult AddToCart(int qty, int productID)
        {
            Dictionary<int, CartItemViewModel> shoppingCart = null;

            if (Session["cart"] != null)
            {
                shoppingCart = (Dictionary<int, CartItemViewModel>)Session["cart"];
            }
            else
            {
                shoppingCart = new Dictionary<int, CartItemViewModel>();
            }

            //find the product the user is trying to add to the cart
            Product product = db.Products.Where(b => b.ProductID == productID).FirstOrDefault();

            if (product == null)
            {
                //if a bad ID was passed to this method, kick user back to some page to try again or we could throw an error.
                return RedirectToAction("Index");
            }
            else
            {
                //if book ID is valid, add the line-item to the cart
                CartItemViewModel item = new CartItemViewModel(qty, product);

                //put item in the local shoppingCart collection. BUT if we already have that product as a cart-item , then we will
                //update the qty only
                if (shoppingCart.ContainsKey(product.ProductID))

                {
                    shoppingCart[product.ProductID].Qty += qty;
                }
                else
                {
                    shoppingCart.Add(product.ProductID, item);
                }

                //now update the Session version of the cart so we can maintain that info between Request and Response cycles
                Session["cart"] = shoppingCart; //implicit casting aka boxing
            }

            //send to a view that shows a list of all items in the cart
            return RedirectToAction("Index", "ShoppingCart");

        }

        // GET: Products/Create
        public ActionResult Create()
        {
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "CategoryName");
            ViewBag.ProductStatusID = new SelectList(db.Product_Status, "ProductStatusID", "ProductStatusName");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductID,Name,Description,CategoryID,Price,ProductStatusID,Quanity,ProductImage")] Product product, HttpPostedFileBase productImage)
        {
            if (ModelState.IsValid)
            {
                //Image Upload Utility Step 6
                #region File Upload
                string file = "NoImage.png";

                if (productImage != null)
                {
                    //Process the file that was uploaded by the user
                    file = productImage.FileName;
                    string ext = file.Substring(file.LastIndexOf('.'));
                    string[] goodExts = { ".jpeg", ".jpg", ".png", ".gif" };
                    //This if checks to see the file uploaded is the right type of file
                    //i.e. file extension would be included in the goodExts
                    //We will also require the file uploaded to be 4 MB or less in size
                    if (goodExts.Contains(ext.ToLower()) && productImage.ContentLength <= 4194304)
                    {
                        //Create a new file name using a GUID (Globally Unique Identifier)
                        file = Guid.NewGuid() + ext;

                        string savePath = Server.MapPath("~/Content/images/"); //This is where the images will be saved
                        Image convertedImage = Image.FromStream(productImage.InputStream);
                        int maxImageSize = 500;
                        int maxThumbSize = 100;

                        ImageUtility.ResizeImage(savePath, file, convertedImage, maxImageSize, maxThumbSize);
                    }
                }
                //no matter what, update the BookImage with the value of the file variable
                product.ProductImage = file;
                #endregion

                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "CategoryName", product.CategoryID);
            ViewBag.ProductStatusID = new SelectList(db.Product_Status, "ProductStatusID", "ProductStatusName", product.ProductStatusID);
            return View(product);
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "CategoryName", product.CategoryID);
            ViewBag.ProductStatusID = new SelectList(db.Product_Status, "ProductStatusID", "ProductStatusName", product.ProductStatusID);
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductID,Name,Description,CategoryID,Price,ProductStatusID,Quanity,ProductImage")] Product product, HttpPostedFileBase productImage)
        {
            if (ModelState.IsValid)
            {
                //Image Upload Utility Step 10
                #region File Upload
                if (productImage != null)
                {
                    //get file name
                    string file = productImage.FileName;

                    //get the file extension
                    string ext = file.Substring(file.LastIndexOf('.'));

                    //create a list of good extensions
                    string[] goodExts = { ".jpeg", ".jpg", ".png", ".gif" };

                    if (goodExts.Contains(ext.ToLower()) && productImage.ContentLength <= 4194304)
                    {
                        file = Guid.NewGuid() + ext;
                        string savePath = Server.MapPath("~/Content/images/");
                        Image convertedImage = Image.FromStream(productImage.InputStream);
                        int maxImageSize = 500;
                        int maxThumbSize = 100;

                        ImageUtility.ResizeImage(savePath, file, convertedImage, maxImageSize, maxThumbSize);

                        if (product.ProductImage != null && product.ProductImage != "NoImage.png")
                        {
                            string path = Server.MapPath("~/Content/images/");
                            ImageUtility.Delete(path, product.ProductImage);
                        }

                        product.ProductImage = file; 
                    }
                }
                #endregion

                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "CategoryName", product.CategoryID);
            ViewBag.ProductStatusID = new SelectList(db.Product_Status, "ProductStatusID", "ProductStatusName", product.ProductStatusID);
            return View(product);
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            string path = Server.MapPath("/Content/images/");
            ImageUtility.Delete(path, product.ProductImage);
            db.Products.Remove(product);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
