using System;
using System.Collections.Generic;
using System.Text;

namespace HumanResource
{
    public interface IOrganization : IIdentifiable
    {
        string Name { get; set; }
        string Type { get; }
        int Count { get; set; }
        string Description { get; set; }
        void AddMember(IPerson person);
        void RemoveMember(IPerson person);
        IPerson GetMember(string Id);
        bool ContainsMember(IPerson person);
        List<IPerson> GetMembers();
        void Live();
        void Introduce();
    }
}
