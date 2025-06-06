using System.Runtime.CompilerServices;

namespace lab3
{
    class Student
    {
        private List<int> _grades = new List<int>();
        public string Name
        {
            get;
            private set;
        }

        private int _age;

        public int Age
        {
            get => _age;
            private set => _age = value is >= 16 and <= 100 ? value : throw new Exception("Daun?");
        }

        public int AvarageGrade
        {
            get => _grades.Sum() / _grades.Count;
            private set;
        }

        public Student(string name, int age)
        {
            Name = name;
            Age = age;
        }

        public void AddGrade(int grade) => _grades.Add(grade);
    }
    public class Bootstrap
    {
        public static void Main(string[] args)
        {
            Student student = new Student("Kto", 17);

            student.AddGrade(5);
            student.AddGrade(3);

            Console.WriteLine(student.AvarageGrade);
        }
    }
}