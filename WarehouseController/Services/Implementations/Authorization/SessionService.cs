using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarehouseController.Services.Implementations.Authorization
{
    public static class SessionService
    {
        public static int LoggedInUserId { get; set; }
        public static string LoggedInUserLogin { get; set; }
        public static string LoggedInUserRole { get; set; }
    }
}
