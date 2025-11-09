using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KamiYomu.CrawlerAgents.Core.Inputs
{
    /// <summary>
    /// Serves as a base class for defining custom input attributes.
    /// </summary>
    /// <remarks>Inherit from this class to create attributes that can be used to annotate input parameters or
    /// properties, providing metadata or behavior customization in derived classes.</remarks>
    public abstract class AbstractInputAttribute : Attribute
    {
    }
}
