using ApiEmployee_NewWayForMinimalApi_.Models;

namespace ApiEmployee_NewWayForMinimalApi_.Repositories
{
    public class EmployeeRepository
    {
        private readonly Dictionary<int, Employee> _employees = new();

        public void Create(Employee employee)
        {
            if (employee is null)
            {
                return;
            }
            _employees[employee.Id] = employee;
        }

        public Employee GetById(int id)
        {
            try
            {
                return _employees[id];
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<Employee> GetAll()
        {
            return _employees.Values.ToList();
        }

        public void Update(Employee employee)
        {
            var existingEmployee = GetById(employee.Id);
            if (existingEmployee is null)
            {
                return;
            }
            _employees[employee.Id] = employee;
        }

        public void Delete(int id)
        {
            _employees.Remove(id);
        }

        public double GetAverageSalary()
        {
            double sum = 0;
            int length = _employees.Count();
            foreach (var employee in _employees)
            {
                sum += employee.Value.Salary;
            }
            return sum / length;
        }

        public double GetTotalSumOfSalaries()
        {
            double sum = 0;
            foreach (var employee in _employees)
            {
                sum += employee.Value.Salary;
            }
            return sum;
        }

        public List<Employee> SearchByName(string name)
        {
            var employeeNames = from employee in _employees.Values.ToList()
                                where employee.FirstName.Contains(name) ||
                                employee.LastName.Contains(name)
                                select employee;
            return employeeNames.ToList();
        }

        public List<Employee> SearchBySkill(string skill)
        {
            var employeeSkills = from employee in _employees.Values.ToList()
                                where employee.Skills.Contains(skill)
                                select employee;
            return employeeSkills.ToList();
        }

        public List<Employee> GetSalariesBiggerThen(double salary)
        {
            var employeeSalaries = from employee in _employees.Values.ToList()
                                where employee.Salary > salary
                                select employee;
            return employeeSalaries.ToList();
        }

        public List<Employee> GetSalariesLessThen(double salary)
        {
            var employeeSalaries = from employee in _employees.Values.ToList()
                                   where employee.Salary < salary
                                   select employee;
            return employeeSalaries.ToList();
        }
    }
}
