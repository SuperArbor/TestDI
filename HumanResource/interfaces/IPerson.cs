using System;
using System.Collections.Generic;
using System.Text;

namespace HumanResource
{
    public interface IPerson : IIdentifiable
    {
        string Type { get; }
        string Name { get; set; }
        Gender Gender { get; set; }
        int Age { get; set; }
        string Organization { get; set; }
        void SelfIntroduce();
        void Live();
    }
    public enum Gender
    {
        Male,
        Female,
    }
}
