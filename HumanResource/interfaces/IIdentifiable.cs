using System;
using System.Collections.Generic;
using System.Text;

namespace HumanResource
{
    /// <summary>
    /// Objects with a globally unique id
    /// </summary>
    public interface IIdentifiable
    {
        string Id { get; set; }
    }
}
