using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Web3.MVC_Example.Models;
using Microsoft.Data.SqlClient;

namespace Web3.MVC_Example.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        Console.WriteLine("Index");
        DataTable dt = this.GetPersion();
        return View();
    }
    public IActionResult GetProduct()
    {
        // conect to db get data
        // model 
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

    private DataTable GetPersion()
    {
        DataTable dtPerson = new DataTable();
        //DataSet dataSet = new DataSet();
        string connectionString = "Server=localhost;Database=SEM3_WDA;User Id=sa;Password=Abcd@1234;TrustServerCertificate=true";
        string sqlCommand = "select * from Person";
        using (SqlCommand cmd = new SqlCommand(sqlCommand, new SqlConnection(connectionString)))
        {
            cmd.Connection.Open();
            dtPerson.Load(cmd.ExecuteReader());
            
        }
        //dataSet.Tables.Add(dtPerson);
        return dtPerson;
    }
}

