using System;
using System.Collections.Generic;
using System.Text;

namespace HumanResource
{
    public interface IOrganizationFactory
    {
        IOrganization CreateOrganization(string type, string name,
            Action<IOrganization, IPerson> onEntered = null,
            Action<IOrganization, IPerson> onLeft = null);
    }
}
