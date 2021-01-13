using System;
using System.Collections.Generic;
using System.Text;

namespace HumanResource
{
    /// <summary>
    /// 接口：法人
    /// </summary>
    public interface ILegalPerson
    {
        /// <summary>
        /// 权利
        /// </summary>
        string Rights { get; set; }
        /// <summary>
        /// 义务
        /// </summary>
        string Responsibility { get; set; }
    }
}
