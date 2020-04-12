using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace AnyTest.DataService.Controllers
{
    /// <summary>
    /// \~english An API controller to indicate the service is running
    /// \~ukrainian Контроллер API для демонстрації того, що служба працює
    /// /// </summary>
    [Route("/")]
    public class StartupController : Controller
    {
        /// <summary>
        /// \~english A method that returns a "Service Up" messagge
        /// \~ukrainian Метод що повертає повідомлення про стан служби
        /// </summary>
        /// <returns>
        /// \~english A message about service state
        /// \~ukrainian Повідомлення про стан служби
        /// </returns>
        /// /// <example>
        /// \~english An expample of HTTP request for service state
        /// \~ukrainian Приклад HTTP запиту про стан служби
        /// <code>
        /// GET: /
        /// </code>
        /// </example>
        [HttpGet]
        public string Index()
        {
            return "Server Up";
        }
    }
}