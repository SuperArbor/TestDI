using System;
using System.Collections.Generic;
using System.Text;

namespace HumanResource
{
    public interface IPersonFactory
    {
        IPerson CreatePerson(string type, string name, string gender, int age);
    }
}
