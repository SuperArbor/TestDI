using System;
using System.Collections.Generic;
using System.Text;

namespace HumanResource
{
    public class School : Organization
    {
        public override string Type => "School";
        public School(string id, string name) : base(id, name)
        {
        }

        public School(string id, string name, ICollection<IPerson> people) : base(id, name, people)
        {
        }
    }
}
