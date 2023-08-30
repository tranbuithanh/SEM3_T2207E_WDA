using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Diagnostics;
using Web3.ADO.NET.Models;
using System.Data.SqlClient;
using Microsoft.Data.SqlClient;

namespace Web3.ADO.NET.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            DataTable dtPerson = this.GetPersons();
            List<Person> persons = this.GetPersonDTO(dtPerson);
            return View(persons);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        /*ADO.NET*/
        private DataTable GetPersons()
        {
            DataTable dtPerson = new DataTable();
            /*1. Connnect*/
            string connectionString = @"Server=localhost,1433;Database=SEM3_WDA;User Id=sa;Password=Abcd@1234;TrustServerCertificate=true";
            string sqlCommand = "select top 100 * from Person";
            using (SqlCommand cmd = new SqlCommand(sqlCommand, new SqlConnection(connectionString)))
            {
                cmd.Connection.Open();
                dtPerson.Load(cmd.ExecuteReader());

            }
            /*2. Get DataTable*/
            return dtPerson;
        }


        private List<Person> GetPersonDTO(DataTable dtPerson)
        {
            List<Person> list = new List<Person>(); 
            foreach (DataRow dr in dtPerson.Rows)
            {
                list.Add(new Person()
                {
                    Title = dr["Title"].ToString(),
                    FirstName = dr["FirstName"].ToString(),
                    LastName = dr["LastName"].ToString(),
                });
            }
            return list;
        }
    }
}