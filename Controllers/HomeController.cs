using CrudWithDBFA.Models;
using Microsoft.AspNetCore.Mvc;

namespace CrudWithDBFA.Controllers
{
    public class HomeController : Controller
    {
        //Create ReadOnly property for datacontext
        private readonly CompanyDBDataContext datacontext;

        ////Pass IConfiguration object
        public HomeController(IConfiguration configuration)
        {
            // Retrieve the connection string from appsettings.json
            string connectionString = configuration.GetConnectionString("ConStr");

            //Assigning the connectionString to the datacontext
            datacontext = new CompanyDBDataContext(connectionString);
        }


        public IActionResult Index()
        {
            List<Employee> list = datacontext.GetEmployees();

            return View(list);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }



        [HttpPost]
        public IActionResult Create(Employee employee)
        {
            try
            {
                bool isInserted = datacontext.InsertEmployee(employee);

                if (isInserted)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return View();
                }
            }
            catch (Exception ex)
            {
                ViewBag.Mes = ex + "\n" ;
                return View();
            }
        }

        public IActionResult Details(int id)
        {
            Employee obj = datacontext.GetEmployee(id);
            return View(obj);
        }

        public ActionResult Edit(int id)
        {
            var employee = datacontext.GetEmployee(id);
            return View(employee);
        }

        [HttpPost]
        public ActionResult Edit(Employee model)
        {
            string value = datacontext.UpdateEmployee(model);
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            var obj=datacontext.GetEmployee(id);
            return View(obj);
        }

        [HttpPost]
        public ActionResult Delete(string id)
        {
            datacontext.DeleteEmployee(Convert.ToInt32(id));
            return RedirectToAction("Index");
        }


    }
    }

