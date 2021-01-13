using System;
using System.Collections.Generic;
using System.Text;

namespace HumanResource
{
    public class Worker : Person
    {
        public Worker(string id, string name, string gender, int age) : base(id, name, gender, age)
        {
        }
        public override string Type => "Worker";

        public override void Live()
        {
            Console.WriteLine($"{Name}: 996");
        }
    }
}
