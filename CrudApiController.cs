using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApiApp.Models;

namespace WebApiApp.Controllers
{
    public class CrudApiController : ApiController
    {
        EmployeeDBEntities EmpDB = new EmployeeDBEntities();
        [HttpGet]
        public IHttpActionResult GetContacts()
        {
            List<Contact> EmpContacts = EmpDB.Contacts.ToList().OrderByDescending(x => x.Id).ToList();
            return Ok(EmpContacts);
        }

        [HttpGet]
        public IHttpActionResult GetContactById(int Id)
        {
            var EmpContact = EmpDB.Contacts.Where(model => model.Id == Id).FirstOrDefault();
            return Ok(EmpContact);
        }
        [HttpPost]
        public IHttpActionResult ContactInsert(Contact con)
        {
            EmpDB.Contacts.Add(con);
            EmpDB.SaveChanges();
            return Ok();
        }
        [HttpPut]
        public IHttpActionResult ContactUpdate(Contact con)
        {
            EmpDB.Entry(con).State = System.Data.Entity.EntityState.Modified;
            EmpDB.SaveChanges();
            return Ok();
        }
        [HttpDelete]
        public IHttpActionResult DelContactById(int Id)
        {
            var EmpContact = EmpDB.Contacts.Where(model => model.Id == Id).FirstOrDefault();
            EmpDB.Entry(EmpContact).State = System.Data.Entity.EntityState.Deleted;
            EmpDB.SaveChanges();
            return Ok();
        }

    }
}
