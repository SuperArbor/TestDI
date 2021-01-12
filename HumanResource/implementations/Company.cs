using System;
using System.Collections.Generic;
using System.Text;

namespace HumanResource
{
    public class Company : Organization
    {
        public Company(string id, string name) : base(id, name)
        {
        }

        public Company(string id, string name, ICollection<IPerson> people) : base(id, name, people)
        {
        }

        public override string Type => "Company";
    }
}
