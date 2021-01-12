using System;
using System.Collections.Generic;
using System.Text;

namespace HumanResource
{
    public interface IHumanCenter
    {
        void AddOrganization(IOrganization org);
        void RemoveOrganization(string id);
        IOrganization GetOrganizationByName(string name);
        IOrganization GetOrganizationById(string id);
        List<IOrganization> GetOrganizations();
    }
}
