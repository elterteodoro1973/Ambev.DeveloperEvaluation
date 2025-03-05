using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Domain.Events
{    
    public class UserCreatedEvent
    {
        public UserCreatedEvent(string userName, string mensager)
        {
            UserName = userName;
            Mensager = mensager;
        }
        public string UserName { get; }
        public string Mensager { get; }
    }
}
