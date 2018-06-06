using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using lab26_login_page.Models;

namespace lab26_login_page.Controllers
{


    public class HomeController : Controller
    {

        public ActionResult Index()
        {
            CoffeeShopDBEntities db = new CoffeeShopDBEntities();
            List<Item> items = db.Items.ToList();
            ViewBag.Items = items;
            //declare total variables
            double subtotal = 0;
            double tax = 0;
            double grandTotal = 0;

            //check if the Cart object already exists
            if (Session["Cart"] == null)
            {
                //if it doesn't, make a new list of items
                List<Item> cart = new List<Item>();
                //add the list to the session
                Session.Add("Cart", cart);
            }

            else
            {

                //if it does exist, get the list
                List<Item> cart = (List<Item>)(Session["Cart"]);

                //get the subtotal
                subtotal = cart.Sum(item => (double)item.Price);
            }
            // calculate totals
            tax = subtotal * .06;
            grandTotal = subtotal + tax;

            //viewbags for totals
            ViewBag.Subtotal = $"Subtotal: {subtotal.ToString("C2")}";
            ViewBag.Tax = $"Tax: {tax.ToString("C2")}";
            ViewBag.grandTotal = $"Grand Total: {grandTotal.ToString("C2")}";

            //return view
            return View();
        }

        [Authorize]
        public ActionResult Menu()
        {
            CoffeeShopDBEntities db = new CoffeeShopDBEntities();
            List<Item> items = db.Items.ToList();
            ViewBag.Items = items;
            //declare total variables
            double subtotal = 0;
            double tax = 0;
            double grandTotal = 0;

            //check if the Cart object already exists
            if (Session["Cart"] == null)
            {
                //if it doesn't, make a new list of items
                List<Item> cart = new List<Item>();
                //add the list to the session
                Session.Add("Cart", cart);
            }

            else
            {

                //if it does exist, get the list
                List<Item> cart = (List<Item>)(Session["Cart"]);

                //get the subtotal
                subtotal = cart.Sum(item => (double)item.Price);
            }
            // calculate totals
            tax = subtotal * .06;
            grandTotal = subtotal + tax;

            //viewbags for totals
            ViewBag.Subtotal = $"Subtotal: {subtotal.ToString("C2")}";
            ViewBag.Tax = $"Tax: {tax.ToString("C2")}";
            ViewBag.grandTotal = $"Grand Total: {grandTotal.ToString("C2")}";

            //return view
            return View();
        }

        public ActionResult Clear()
        {
            CoffeeShopDBEntities db = new CoffeeShopDBEntities();
            List<Item> cart = (List<Item>)(Session["Cart"]);
            cart.Clear();

            return Redirect(Request.UrlReferrer.ToString());
        }

        [Authorize]
        public ActionResult Cart()
        {

            //declare total variables
            double subtotal = 0;
            double tax = 0;
            double grandTotal = 0;

            //declare db model
            CoffeeShopDBEntities db = new CoffeeShopDBEntities();

            //check if the Cart object already exists
            if (Session["Cart"] == null)
            {
                //if it doesn't, make a new list of items
                List<Item> cart = new List<Item>();
                //add the list to the session
                Session.Add("Cart", cart);
            }

            else
            {

                //if it does exist, get the list
                List<Item> cart = (List<Item>)(Session["Cart"]);

                //get the subtotal
                subtotal = cart.Sum(item => (double)item.Price);
            }
            // calculate totals
            tax = subtotal * .06;
            grandTotal = subtotal + tax;

            //viewbags for totals
            ViewBag.Subtotal = $"Subtotal: {subtotal.ToString("C2")}";
            ViewBag.Tax = $"Tax: {tax.ToString("C2")}";
            ViewBag.grandTotal = $"Grand Total: {grandTotal.ToString("C2")}";

            //return view
            return View();
        }

        public ActionResult Add(int id)
        {
            CoffeeShopDBEntities db = new CoffeeShopDBEntities();

            //check if the Cart object already exists
            if (Session["Cart"] == null)
            {
                //if it doesn't, make a new list of items
                List<Item> cart = new List<Item>();
                //add this item to it
                cart.Add((from b in db.Items
                          where b.ID == id
                          select b).Single());
                //add the list to the session
                Session.Add("Cart", cart);
            }
            else
            {
                //if it does exist, get the list
                List<Item> cart = (List<Item>)(Session["Cart"]);
                //add this item to it
                cart.Add((from b in db.Items
                          where b.ID == id
                          select b).Single());
            }
            return Redirect(Request.UrlReferrer.ToString());
        }

        public ActionResult ItemListSorted(string column)
        {
            CoffeeShopDBEntities db = new CoffeeShopDBEntities();
            //LINQ Query
            if (column == "Name")
            {
                ViewBag.Items = (from i in db.Items
                                 orderby i.Name
                                 select i).ToList();
            }
            else if (column == "Price")
            {
                ViewBag.Items = (from i in db.Items
                                 orderby i.Price
                                 select i).ToList();
            }
            else if (column == "Quantity")
            {
                ViewBag.Items = (from i in db.Items
                                 orderby i.Quantity
                                 select i).ToList();
            }

            return View("Menu");
        }
    }
}