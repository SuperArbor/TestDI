using System;
using System.Collections.Generic;
using System.Text;

namespace HumanResource
{
    /// <summary>
    /// 接口：用于创建Person的工厂
    /// </summary>
    public interface IPersonFactory
    {
        IPerson CreatePerson(string type, string name, string gender, int age);
    }
}
