using Microsoft.AspNetCore.Mvc;
using my_contacts_api.Context;
using my_contacts_api.Models;
using System.Collections.Generic;
using System.Linq;

namespace my_contacts_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        private ContactsAppContext _context;
        public ContactsController (ContactsAppContext context) { _context = context; }

        [HttpGet("{id}")]
        public List<Contacts> GetContacts(int id)
        {
            return (_context.Contacts.Where(u => !u.isDeleted && u.UsersId == id).ToList());
        }
        [HttpPost]
        public void PutContact(Contacts contact)
        {
            _context.Contacts.Add(contact);
            _context.SaveChanges();
        }
        [HttpPut]
        public void UpdateContact(Contacts contact)
        {
            _context.Contacts.Update(contact);
            _context.SaveChanges();
        }
        [HttpDelete("{id}")]
        public void DeleteContact(int id)
        {
            Contacts contact = _context.Contacts.FirstOrDefault(c => c.ContactsId == id);
            contact.isDeleted = true;
            _context.Contacts.Update(contact);
            _context.SaveChanges();
        }
    }
}
