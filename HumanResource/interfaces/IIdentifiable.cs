using System;
using System.Collections.Generic;
using System.Text;

namespace HumanResource
{
    /// <summary>
    /// 接口：表示由全局唯一Id的对象
    /// </summary>
    public interface IIdentifiable
    {
        string Id { get; set; }
    }
}
