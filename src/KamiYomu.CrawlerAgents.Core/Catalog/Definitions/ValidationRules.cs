using System.Text.RegularExpressions;

namespace KamiYomu.CrawlerAgents.Core.Catalog.Definitions
{
    internal static class ValidationRules
    {
        /// <summary>
        /// Regex pattern that matches valid display names, including accented characters and common punctuation.
        /// </summary>
        public static readonly Regex DisplayNamePattern = new(@"[A-Za-zÀ-ÖØ-öø-ÿ0-9 \.\-,'\'\)\(~!\+]*");
    }
}
