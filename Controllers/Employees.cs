using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BussinessEmployes.Models;

namespace BussinessEmployes.Controllers
{
    public class EmployeesController : Controller
    {
        Db_Connection db = new Db_Connection();
        // GET: Employees
        public ActionResult Index()
        {
            List<Employee> list = new List<Employee>();
            SqlCommand cmd = new SqlCommand("select * from dbo.Employee_Details", db.con);
            db.con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(new Employee
                {
                    Id = Convert.ToInt32(reader["Id"]),
                    Name = reader["Name"].ToString(),
                    Salary = Convert.ToInt32(reader["Salary"]),
                    Company = reader["Company_Name"].ToString(),
                    City = reader["city"].ToString()
                    
                });
            }
            reader.Close();
            db.con.Close();

            return View(list);
        }

        public ActionResult Create() => View();

        [HttpPost]
        public ActionResult Create(Employee emp)
        {
            SqlCommand cmd = new SqlCommand("INSERT INTO dbo.Employee_Details (Id, Name, Salary, Company_Name, City) VALUES (@Id, @Name, @salary, @comp, @city)", db.con);
            cmd.Parameters.AddWithValue("@Id", emp.Id);
            cmd.Parameters.AddWithValue("@Name", emp.Name);
            cmd.Parameters.AddWithValue("@Salary", emp.Salary);
            cmd.Parameters.AddWithValue("@Comp", emp.Company);
            cmd.Parameters.AddWithValue("@City", emp.City);
            
            db.con.Open();
            cmd.ExecuteNonQuery();
            db.con.Close();

            return RedirectToAction("Index");
        }


        public ActionResult Edit(int id)
        {
            Employee employee = new Employee();

            SqlCommand cmd = new SqlCommand("SELECT * FROM dbo.Employee_Details WHERE Id=@Id", db.con);
            cmd.Parameters.AddWithValue("@Id", id);
            db.con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {

                employee.Id = Convert.ToInt32(reader["Id"]);
                employee.Name = reader["Name"].ToString();
                employee.Salary = Convert.ToInt32(reader["Salary"]);
                employee.Company = reader["Company_Name"].ToString();
                employee.City = reader["city"].ToString();

            }
            db.con.Close();

            return View(employee);
        }

        [HttpPost]
        public ActionResult Edit(Employee employee)
        {

            SqlCommand cmd = new SqlCommand("UPDATE dbo.Employee_Details SET Name=@Name, Salary=@Salary, Company_Name=@Comp, City=@City WHERE Id=@Id", db.con);
            cmd.Parameters.AddWithValue("@Id", employee.Id);
            cmd.Parameters.AddWithValue("@Name", employee.Name);
            cmd.Parameters.AddWithValue("@Salary", employee.Salary);
            cmd.Parameters.AddWithValue("@comp", employee.Company);
            cmd.Parameters.AddWithValue("@City", employee.City);
            db.con.Open();
            cmd.ExecuteNonQuery();
            db.con.Close();

            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {

            SqlCommand cmd = new SqlCommand("DELETE FROM dbo.Employee_Details WHERE Id = @Id", db.con);
            cmd.Parameters.AddWithValue("@Id", id);
            db.con.Open();
            cmd.ExecuteNonQuery();
            db.con.Close();

            return RedirectToAction("Index");
        }


    }
}