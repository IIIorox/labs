using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using final.Models;

namespace final.Services
{
    public static class UserSession
    {
        public static User? CurrentUser { get; private set; }

        public static void SetUser(User user)
        {
            CurrentUser = user;
        }

        public static void Clear()
        {
            CurrentUser = null;
        }

        public static bool IsAuthenticated => CurrentUser != null;
    }
}
