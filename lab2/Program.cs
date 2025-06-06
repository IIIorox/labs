namespace lab2
{
    abstract class Employee
    {
        protected string _name;
        protected int _salary;

        public Employee(string name, int salary)
        {
            _name = name;
            _salary = salary;
        }

        public virtual void PrintInfo()
        {
            Console.WriteLine(
                $"Name: {_name}\n" +
                $"Salary: {_salary}"
                );
        }
    }

    class Manager: Employee
    {
        private string _departament;

        public Manager(string name, int salary, string departament) : base(name, salary)
        {
            _departament = departament;
        }

        public override void PrintInfo()
        {
            Console.WriteLine(
                $"Name: {_name}\n" +
                $"Salary: {_salary}\n" +
                $"Departament: {_departament}"
                );
        }
    }

    class Developer: Employee
    {
        private string _programmingLanguage;

        public Developer(string name, int salary, string programmingLanguage) : base(name, salary)
        {
            _programmingLanguage = programmingLanguage;
        }

        public override void PrintInfo()
        {
            Console.WriteLine(
                $"Name: {_name}\n" +
                $"Salary: {_salary}\n" +
                $"ProgrammingLanguage: {_programmingLanguage}"
                );
        }
    }

    public class Bootstrap
    {
        public static void Main(String[] args)
        {
            Employee manager = new Manager("Alex", 5, "TK");
            manager.PrintInfo();
            Console.WriteLine("#################");
            Employee developer = new Developer("Bob", 10, "TK");
            developer.PrintInfo();
        }
    }
}