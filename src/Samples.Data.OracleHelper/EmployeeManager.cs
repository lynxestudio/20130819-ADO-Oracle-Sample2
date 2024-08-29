using System.Collections.Generic;
using System.Data;
using Oracle.DataAccess.Client;

namespace Samples.Data.OracleHelper
{
    public sealed class EmployeeManager {
        public bool Create(Employee c) {
            bool resp = false;
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            //my parameters, if my commandText is a store procedure
            parameters.Add("prmEmployeeId", c.EmployeeId);
            parameters.Add("prmFirstName", c.FirstName);
            parameters.Add("prmLastName", c.LastName);
            parameters.Add("prmEmail", c.Email);
            parameters.Add("prmPhoneNumber", c.PhoneNumber);
            parameters.Add("prmHireDate", c.HireDate);
            parameters.Add("prmJobId",c.JobId);
            parameters.Add("prmSalary",c.Salary);
            parameters.Add("prmCommission",c.Commission);
            parameters.Add("prmManagerId",c.ManagerId);
            parameters.Add("prmDepartmentId",c.DepartmentId);
            resp = (OracleDataBaseCommand.Insert(CommandsText.Insert, parameters,
                         CommandType.Text) > 0 ? true : false);
            return resp;
        }
        public bool Update(int id, string col,string val) 
        {
            bool resp = false;
            string commandText = CommandsText.Update + col + " = :prm" + col + " Where EMPLOYEE_ID = :prmEmployeeId";
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            //my parameters, if my commandText is a store procedure
            parameters.Add("prm" + col, val);
            parameters.Add("prmEmployeeId", id);
            resp = (OracleDataBaseCommand.Update(commandText, parameters,
                                CommandType.Text) > 0 ? true : false);
            return resp;
        }
        public Employee Retrieve(int id) {
            Employee resp = null;
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            //my parameters, if my commandText is a store procedure
            parameters.Add("id", id);
            using (OracleDataReader reader = OracleDataBaseCommand.GetReader(CommandsText.SelectAll,
              parameters, CommandType.Text)) {
                if (reader.HasRows) {
                    while (reader.Read()) 
                    {
                        resp = new Employee();
                        resp.EmployeeId = reader.GetInt32(0);
                        resp.FirstName = reader["FIRST_NAME"].ToString();
                        resp.LastName = reader["LAST_NAME"].ToString();
                        resp.Email = reader["EMAIL"].ToString();
                        resp.PhoneNumber = reader["PHONE_NUMBER"].ToString();
                        resp.HireDate = reader["HIRE_DATE"].ToString();
                        resp.JobId = reader["JOB_ID"].ToString();
                        resp.Salary = reader["SALARY"].ToString();
                        resp.Commission = reader["COMMISSION_PCT"].ToString();
                        resp.ManagerId = reader["MANAGER_ID"].ToString();
                        resp.DepartmentId = reader["DEPARTMENT_ID"].ToString();
                    }
                }
            }
            return resp;
        }
    }
}