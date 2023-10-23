using System.Data.SqlClient;
using System.Data;


namespace CrudWithDBFA.Models
{
    public class CompanyDBDataContext
    {
        private readonly string _connectionString;
        public CompanyDBDataContext(string connectionString)
        {
            //Retrevie the connectionString from HomeController IConfiguration
            _connectionString = connectionString;
        }



        public List<Employee> GetEmployees()
        {
            List<Employee> list = new List<Employee>();

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                string query = "GetAllEmployees";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read()) 
                        {
                            Employee employee = new Employee();
                            employee.Eno = Convert.ToInt32(dr["Eno"]);
                            employee.EName = dr["Ename"].ToString();
                            employee.Job = dr["Job"].ToString();
                            employee.Salary = Convert.ToDouble(dr["Salary"]);
                            employee.Dname = dr["Dname"].ToString();
                            list.Add(employee);
                        }
                    }
                    con.Close();
                }
            }
            return list;
        }

        public Employee GetEmployee(int Eno)
        {
            Employee obj = new Employee();

            SqlConnection con = new SqlConnection(_connectionString);
            string query = "GetEmployee";
            SqlCommand cmd = new SqlCommand(query, con);    
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Eno",Eno);
            con.Open();

            SqlDataReader dr = cmd.ExecuteReader();
                    
                        if(dr.HasRows && dr.Read())
                        {
                            obj.Eno = Convert.ToInt32(dr["Eno"]);
                            obj.EName = dr["Ename"].ToString();
                            obj.Job = dr["Job"].ToString();
                            obj.Salary = Convert.ToDouble(dr["Salary"]);
                            obj.Dname = dr["Dname"].ToString();
                        }
            con.Close();     
            return obj;
        }

        public bool InsertEmployee(Employee employee)
        {
            SqlConnection con = new SqlConnection(_connectionString);
            string query = "InsertEmployee";
            SqlCommand cmd = new SqlCommand(query,con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Eno",employee.Eno);
            cmd.Parameters.AddWithValue("@EName",employee.EName);
            cmd.Parameters.AddWithValue("@Job",employee.Job);
            cmd.Parameters.AddWithValue("@Salary",employee.Salary);
            cmd.Parameters.AddWithValue("@Dname",employee.Dname);
            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();
            if (i != 0)
            {
                return true;
            }
            return false;
        }

        public string UpdateEmployee(Employee employee)
        {
            SqlConnection con = new SqlConnection(_connectionString);
            string query = "UpdateEmployee";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Eno", employee.Eno);
            cmd.Parameters.AddWithValue("@EName", employee.EName);
            cmd.Parameters.AddWithValue("@Job", employee.Job);
            cmd.Parameters.AddWithValue("@Salary", employee.Salary);
            cmd.Parameters.AddWithValue("@Dname", employee.Dname);
            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();
            if (i != 0)
            {
                return "Updated Successfully";
            }
            return "Record Not Updated";
        }

        public void DeleteEmployee(int Eno)
        {
            Employee emp = new Employee();
            SqlConnection con = new SqlConnection(_connectionString);
            string query = "DeleteEmployee";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Eno",Eno);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }



    }
}

