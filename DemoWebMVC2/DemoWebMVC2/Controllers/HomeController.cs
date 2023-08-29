using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using DemoWebMVC2.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace DemoWebMVC2.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        DataTable dt = new DataTable();
        dt = this.GetPerson();
        return View();
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

    private DataTable GetPerson()
    {
        DataTable dtPerson = new DataTable();
        DataSet dataSet = new DataSet();
        string connectionString = @"Server=localhost;
            Database=SEM3_WDA;User=sa;Password=Abcd@1234;TrustServerCertificate=True";
        string sqlCommand = "SELECT * FROM Person";
        using (var con = new SqlConnection(connectionString))
        {
            con.Open();
            using (SqlCommand cmd = new SqlCommand(sqlCommand, new SqlConnection(connectionString)))
            {
                cmd.Connection.Open();
                dtPerson.Load(cmd.ExecuteReader());
            }
        };
        dataSet.Tables.Add(dtPerson);
        return dtPerson;
    }
}

