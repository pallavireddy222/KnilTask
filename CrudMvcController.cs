using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using WebApiApp.Models;

namespace WebApiApp.Controllers
{
    public class CrudMvcController : Controller
    {

        HttpClient client = new HttpClient();
        // GET: CrudMvc
        public ActionResult Index()
        {
            List<Contact> lstContacts = new List<Contact>();
            client.BaseAddress = new Uri("http://localhost:49967/api/crudapi");
            var response = client.GetAsync("CrudApi");
            response.Wait();

            var test = response.Result;
            if(test.IsSuccessStatusCode)
            {
                var display = test.Content.ReadAsAsync<List<Contact>>();
                display.Wait();
                lstContacts = display.Result;
            }

            return View(lstContacts);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Contact con)
        {
            client.BaseAddress = new Uri("http://localhost:49967/api/crudapi");
            var response = client.PostAsJsonAsync<Contact>("CrudApi", con);
            response.Wait();

            var test = response.Result;
            if (test.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View("Create");
        }
        public ActionResult Details(int Id)
        {
            Contact con = null;
            client.BaseAddress = new Uri("http://localhost:49967/api/crudapi");
            var response = client.GetAsync("CrudApi?Id="+Id);
            response.Wait();

            var test = response.Result;
            if (test.IsSuccessStatusCode)
            {
                var display = test.Content.ReadAsAsync<Contact>();
                display.Wait();
                con = display.Result;
            }

            return View(con);
        }

        public ActionResult Edit(int Id)
        {
            Contact con = null;
            client.BaseAddress = new Uri("http://localhost:49967/api/crudapi");
            var response = client.GetAsync("CrudApi?Id=" + Id);
            response.Wait();

            var test = response.Result;
            if (test.IsSuccessStatusCode)
            {
                var display = test.Content.ReadAsAsync<Contact>();
                display.Wait();
                con = display.Result;
            }

            return View(con);
        }
        [HttpPost]
        public ActionResult Edit(Contact con)
        {
            client.BaseAddress = new Uri("http://localhost:49967/api/crudapi");
            var response = client.PutAsJsonAsync<Contact>("CrudApi", con);
            response.Wait();

            var test = response.Result;
            if (test.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View("Edit");
        }

        public ActionResult Delete(int Id)
        {
            Contact con = null;
            client.BaseAddress = new Uri("http://localhost:49967/api/crudapi");
            var response = client.GetAsync("CrudApi?Id=" + Id);
            response.Wait();

            var test = response.Result;
            if (test.IsSuccessStatusCode)
            {
                var display = test.Content.ReadAsAsync<Contact>();
                display.Wait();
                con = display.Result;
            }

            return View(con);
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteContact(int Id)
        {
            client.BaseAddress = new Uri("http://localhost:49967/api/crudapi");
            var response = client.DeleteAsync("CrudApi/"+Id);
            response.Wait();

            var test = response.Result;
            if (test.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View("Delete");
        }
    }
}