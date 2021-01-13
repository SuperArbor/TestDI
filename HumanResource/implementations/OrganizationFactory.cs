using System;
using System.Collections.Generic;
using System.Text;

namespace HumanResource
{
    public class OrganizationFactory : IOrganizationFactory
    {
        public IOrganization CreateOrganization(string type, string name, Action<IOrganization, IPerson> onEntered = null, Action<IOrganization, IPerson> onLeft = null)
        {
            switch (type.ToLower())
            {
                case "school":
                    return new School(Guid.NewGuid().ToString(), name, onEntered, onLeft);
                case "company":
                    return new Company(Guid.NewGuid().ToString(), name, onEntered, onLeft);
                default:
                    throw new ArgumentException();
            }
        }
    }
}
