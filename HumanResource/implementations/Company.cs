using System;
using System.Collections.Generic;
using System.Text;

namespace HumanResource
{
    public class Company : Organization
    {
        public Company(string id, string name, 
            Action<IOrganization, IPerson> onMemberEntered = null,
            Action<IOrganization, IPerson> onMemberLeft = null) 
            : base(id, name, onMemberEntered, onMemberLeft)
        {
        }

        public override string Type => "Company";
    }
}
