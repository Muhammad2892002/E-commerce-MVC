using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myshop.BLL.Dto
{
    public class DisplayUserDtoBLL
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public string CurrentRole { get; set; }

        public bool LockStatus { get; set; }
    }
}
