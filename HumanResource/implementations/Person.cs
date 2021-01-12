using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace HumanResource
{
    public abstract class Person : IPerson, ILegalPerson
    {
        public abstract string Type { get; }
        public string Id { get; set; }
        public string Name { get; set; }
        public Gender Gender { get; set; }
        public int Age { get; set; }
        public IOrganization Organization { get; set; }
        public string Rights { get; set; }
        public string Responsibility { get; set; }

        public abstract void Live();
        public Person(string id, string name, string gender, int age)
        {
            this.Id = id;
            this.Name = name;
            this.Gender = (Gender)Gender.Parse(typeof(Gender), gender, true);
            this.Age = age;
        }

        public override string ToString()
        {
            string s = $"Name:{Name}\tGender:{Gender}\tAge:{Age}\tOrganization:{Organization}";
            return s;
        }
        public void SelfIntroduce()
        {
            Console.WriteLine(this.ToString());
        }
    }
}