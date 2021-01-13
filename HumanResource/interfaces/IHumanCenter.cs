using System;
using System.Collections.Generic;
using System.Text;

namespace HumanResource
{
    /// <summary>
    /// 接口：人力资源和机构管理
    /// </summary>
    public interface IHumanCenter
    {
        void AddOrganization(IOrganization org);
        void RemoveOrganization(string id);
        IOrganization GetOrganizationByName(string name);
        IOrganization GetOrganizationById(string id);
        List<IOrganization> GetOrganizations();
    }
}
