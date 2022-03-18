using System;
using System.Collections.Generic;
using System.Text;

namespace Identity.Service.EventHandlers.Response
{
    public class IdentityAccess
    {
        public bool Succedded { get; set; }
        public string AccessToken { get; set; }
    }
}
