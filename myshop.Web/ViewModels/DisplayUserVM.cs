namespace myshop.Web.ViewModels
{
    public class DisplayUserVM
    {
        public string Id { get; set; }
        public string UserName { get; set; }

        public string Email { get; set; }

        public string CurrentRole { get; set; }

        public bool LockStatus { get; set; }
    }
}
