using System;

namespace Samples.Data.OracleHelper
{
    class Program    {
        static EmployeeManager manager = new EmployeeManager();
        

        static void Main(string[] args) {
            
            
            string[] options = {"Create","Update","Query" };
            string option = "0";
            do
            {
                try
                {
                    Console.Clear();
                    Utilities.SetTitle("- Oracle ADO.NET Data Helper example -");
                    Utilities.DisplayMenu(options);
                    option = Utilities.Scanf("Select and option");
                    switch (option)
                    {
                        case "1":
                            CreateEmployee();
                            break;
                        case "2":
                            UpdateEmployee();
                            break;
                        case "3":
                            SelectEmployee();
                            break;
                        default:
                            Console.WriteLine("\tPlease choose an option!");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Utilities.PrintLabel("Error", ex.Message);
                }
            } while (!option.Equals("0"));
            
            Utilities.Pause();
        }

        private static void UpdateEmployee()
        {
            string answer = "y";
            do
            {
                try
                {
                    DateTime dt;
                    Console.Clear();
                    Utilities.SetTitle("Updating employee");
                    string sid = Utilities.Scanf("Enter id");
                    int id = 0;
                    Int32.TryParse(sid, out id);
                    Employee employee = manager.Retrieve(id);
                    Utilities.PrintLine();
                    Utilities.PrintLabel("Id", employee.EmployeeId.ToString());
                    Utilities.PrintLabel("1) First name", employee.FirstName);
                    Utilities.PrintLabel("2) Last name", employee.LastName);
                    Utilities.PrintLabel("3) Email", employee.Email);
                    Utilities.PrintLabel("4) Phone number", employee.PhoneNumber);
                    DateTime.TryParse(employee.HireDate, out dt);
                    employee.HireDate = dt.ToString("dd-MMM-yy");
                    Utilities.PrintLabel("5) Hire Date", employee.HireDate);
                    Utilities.PrintLabel("6) Job id", employee.JobId);
                    Utilities.PrintLabel("7) Salary", employee.Salary);
                    Utilities.PrintLabel("8) Commmission", employee.Commission);
                    Utilities.PrintLabel("9) Manager Id", employee.ManagerId);
                    Utilities.PrintLabel("10) Department id", employee.DepartmentId);
                    string op = Utilities.Scanf("Enter the number of row to update");
                    string val = null;
                    int col = 0;
                    Int32.TryParse(op,out col);
                    if(col > 0 && col < 11)
                        val = Utilities.Scanf("Enter the new " + CommandsText.employeesCols[col]);
                    else
                        Utilities.PrintMessage("Not valid option");
                    bool b = manager.Update(id, CommandsText.employeesCols[col], val);
                    if (b)
                        Utilities.PrintMessage("(1) record affected");
                }
                catch (Exception ex)
                {
                    Utilities.PrintMessage(ex.Message);
                }
                finally
                {
                    answer = Utilities.Scanf("Continue? (y/n)");
                    
                }
                if (answer.ToLower().Equals("n"))
                    break;
            } while (answer.ToLower().Equals("y"));

            

        }

        static void SelectEmployee()
        {
            string answer = "y";
            do
            {
                try
                {
                    Console.Clear();
                    Utilities.SetTitle("Querying employee");
                    string sid = Utilities.Scanf("Enter id");
                    int id = 0;
                    Int32.TryParse(sid, out id);
                    Employee employee = manager.Retrieve(id);
                    if (employee != null)
                    {
                        Console.WriteLine();
                        Utilities.PrintLabel("Id", employee.EmployeeId.ToString());
                        Utilities.PrintLabel("First name", employee.FirstName);
                        Utilities.PrintLabel("Last name", employee.LastName);
                        Utilities.PrintLabel("Email", employee.Email);
                        Utilities.PrintLabel("Phone number", employee.PhoneNumber);
                        Utilities.PrintLabel("Hire Date", employee.HireDate);
                        Utilities.PrintLabel("Job id", employee.JobId);
                        Utilities.PrintLabel("Salary", employee.Salary);
                        Utilities.PrintLabel("Commmission", employee.Commission);
                        Utilities.PrintLabel("Manager Id", employee.ManagerId);
                        Utilities.PrintLabel("Department id", employee.DepartmentId);
                        Console.WriteLine();
                    }
                    else
                        Utilities.PrintMessage("Not found! ");
                }
                catch (Exception ex)
                {
                    Utilities.PrintMessage(ex.Message);
                }
                finally
                {
                    answer = Utilities.Scanf("Continue? (y/n)");
                }
                if (answer.ToLower().Equals("n"))
                    break;
            } while (answer.ToLower().Equals("y"));

            
        }

        static void CreateEmployee()
        {
            string answer = "y";
            do
            {
                try
                {
                    Console.Clear();
                    Utilities.SetTitle("Adding a new employee");
                    Console.WriteLine("\tEnter the following values.\n");
                    string sid = Utilities.Scanf("Enter id");
                    int id = 0;
                    Int32.TryParse(sid, out id);
                    Employee e = new Employee();
                    e.EmployeeId = id;
                    e.FirstName = Utilities.Scanf("First name");
                    e.LastName = Utilities.Scanf("Last name");
                    e.Email = Utilities.Scanf("Email");
                    e.PhoneNumber = Utilities.Scanf("Phone number");
                    e.HireDate = Utilities.Scanf("Hire Date");
                    e.JobId = Utilities.Scanf("Job id");
                    e.Salary = Utilities.Scanf("Salary");
                    e.Commission = Utilities.Scanf("Commission");
                    e.ManagerId = Utilities.Scanf("Manager id");
                    e.DepartmentId = Utilities.Scanf("Department id");
                    bool b = manager.Create(e);
                    if (b)
                        Utilities.PrintMessage("(1) Record affected");
                }
                catch (Exception ex)
                {
                    Utilities.PrintMessage(ex.Message);
                }
                finally {
                    answer = Utilities.Scanf("Continue? (y/n)");
                }
                if (answer.ToLower().Equals("n"))
                    break;

            } while (answer.ToLower().Equals("y"));
        }
    }
}