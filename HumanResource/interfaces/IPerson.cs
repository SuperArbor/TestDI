using System;
using System.Collections.Generic;
using System.Text;

namespace HumanResource
{
    /// <summary>
    /// 接口：人
    /// </summary>
    public interface IPerson : IIdentifiable
    {
        /// <summary>
        /// 人物类型，如学生、工人等
        /// </summary>
        string Type { get; }
        /// <summary>
        /// 姓名
        /// </summary>
        string Name { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        Gender Gender { get; set; }
        /// <summary>
        /// 年龄
        /// </summary>
        int Age { get; set; }
        /// <summary>
        /// 所属机构名称
        /// </summary>
        string Organization { get; set; }
        /// <summary>
        /// 自我介绍
        /// </summary>
        void SelfIntroduce();
        /// <summary>
        /// 生活方式
        /// </summary>
        void Live();
    }
    public enum Gender
    {
        Male,
        Female,
    }
}
