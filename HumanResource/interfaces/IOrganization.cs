using System;
using System.Collections.Generic;
using System.Text;

namespace HumanResource
{
    public interface IOrganization : IIdentifiable
    {
        string Name { get; set; }
        string Type { get; }
        int Count { get; }
        string Description { get; set; }
        //ICollection<IPerson> People { get; }
        void AddMember(IPerson person);
        void RemoveMember(IPerson person);
        IPerson GetMember(string Id);
        void Live();
        void Introduce();
    }
}
