using System;
using System.Collections.Generic;
using System.Text;

namespace HumanResource
{
    public class School : Organization
    {
        public override string Type => "School";
        public School(string id, string name,
            Action<IOrganization, IPerson> onMemberEntered = null,
            Action<IOrganization, IPerson> onMemberLeft = null)
            : base(id, name, onMemberEntered, onMemberLeft)
        {
        }
    }
}
