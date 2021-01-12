using System;
using System.Collections.Generic;
using System.Text;

namespace HumanResource
{
    public class PersonFactory : IPersonFactory
    {
        public IPerson CreatePerson(string type, string name, string gender, int age)
        {
            switch (type.ToLower())
            {
                case "student":
                    return new Student(Guid.NewGuid().ToString(), name, gender, age);
                case "worker":
                    return new Worker(Guid.NewGuid().ToString(), name, gender, age);
                default:
                    throw new ArgumentException();
            }
        }
    }
}
