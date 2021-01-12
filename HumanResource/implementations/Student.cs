using System;
using System.Collections.Generic;
using System.Text;

namespace HumanResource
{
    public class Student : Person
    {
        public Student(string id, string name, string gender, int age) : base(id, name, gender, age)
        {
        }

        public override string Type => "Student";

        public override void Live()
        {
            Console.WriteLine($"{Name}: Get up in the morning, Go to school in the day, Sleep in the evening");
        }
    }
}
