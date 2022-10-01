namespace my_contacts_api.Models
{
    public class Contacts
    {
        public int ContactsId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public int UsersId { get; set; }
        public bool isDeleted { get; set; }
    }
}
