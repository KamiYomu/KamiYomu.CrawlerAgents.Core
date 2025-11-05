using System;
using System.Collections.Generic;
using System.Text;

namespace KamiYomu.CrawlerAgents.Core.Inputs
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
    public class CrawlerTextAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CrawlerTextAttribute"/> class.
        /// </summary>
        /// <param name="name">An identifier of the input.text field (e.g., "Username").</param>
        public CrawlerTextAttribute(string name, string legend)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Legend = legend; 
        }
        /// <summary>
        /// Gets the key for this option, used as internal identifier.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets the descriptive label or explanation for this option.
        /// </summary>
        public string Legend { get; }

        /// <inheritdoc/>
        public override string ToString() => Name;
    }
}
