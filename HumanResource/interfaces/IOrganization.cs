using System;
using System.Collections.Generic;
using System.Text;

namespace HumanResource
{
    /// <summary>
    /// 接口：机构
    /// </summary>
    public interface IOrganization : IIdentifiable
    {
        /// <summary>
        /// 机构名
        /// </summary>
        string Name { get; set; }
        /// <summary>
        /// 机构类别，如学校、公司等
        /// </summary>
        string Type { get; }
        /// <summary>
        /// 机构人数
        /// </summary>
        int Count { get; set; }
        /// <summary>
        /// 机构描述
        /// </summary>
        string Description { get; set; }
        /// <summary>
        /// 增加成员
        /// </summary>
        /// <param name="person"></param>
        void AddMember(IPerson person);
        /// <summary>
        /// 移除成员
        /// </summary>
        /// <param name="person"></param>
        void RemoveMember(IPerson person);
        /// <summary>
        /// 获取特定Id的成员
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        IPerson GetMember(string Id);
        /// <summary>
        /// 判断机构是否包含成员
        /// </summary>
        /// <param name="person"></param>
        /// <returns></returns>
        bool ContainsMember(IPerson person);
        /// <summary>
        /// 获取机构全部成员
        /// </summary>
        /// <returns></returns>
        List<IPerson> GetMembers();
        /// <summary>
        /// 生活方式
        /// </summary>
        void Live();
    }
}
