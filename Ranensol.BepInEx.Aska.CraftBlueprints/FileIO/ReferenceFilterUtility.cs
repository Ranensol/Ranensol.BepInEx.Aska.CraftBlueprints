namespace Ranensol.BepInEx.Aska.CraftBlueprints.FileIO
{
    /// <summary>
    /// Utility class for filtering reference file entries based on include/exclude prefix rules
    /// </summary>
    public static class ReferenceFilterUtility
    {
        /// <summary>
        /// Parse a comma-separated config string into a list of lowercase prefixes
        /// </summary>
        public static List<string> ParsePrefixList(string configValue)
        {
            return string.IsNullOrWhiteSpace(configValue)
                ? []
                : configValue
                .Split(',')
                .Select(s => s.Trim().ToLower())
                .Where(s => !string.IsNullOrEmpty(s))
                .ToList();
        }

        /// <summary>
        /// Filter a list of items based on include and exclude prefix rules
        /// </summary>
        /// <param name="items">Full list of items to filter</param>
        /// <param name="includePrefixes">If not empty, only items starting with these prefixes are included (whitelist)</param>
        /// <param name="excludePrefixes">Items starting with these prefixes are removed (blacklist)</param>
        /// <returns>Filtered list of items</returns>
        public static List<string> FilterByPrefixes(
            List<string> items,
            List<string> includePrefixes,
            List<string> excludePrefixes)
        {
            if (items == null || items.Count == 0)
                return [];

            // Apply include filter (whitelist)
            List<string> filtered;
            if (includePrefixes != null && includePrefixes.Count > 0)
            {
                filtered = items
                    .Where(item => includePrefixes.Any(prefix =>
                        item.ToLower().StartsWith(prefix)))
                    .ToList();
            }
            else
            {
                // No include filter - start with all items
                filtered = [.. items];
            }

            // Apply exclude filter (blacklist)
            if (excludePrefixes != null && excludePrefixes.Count > 0)
            {
                filtered = filtered
                    .Where(item => !excludePrefixes.Any(prefix =>
                        item.ToLower().StartsWith(prefix)))
                    .ToList();
            }

            return filtered;
        }

        /// <summary>
        /// Filter items for reference file generation using config values
        /// </summary>
        public static List<string> FilterItems(List<string> items)
        {
            var includePrefixes = ParsePrefixList(PluginConfig.ItemsIncludePrefixes.Value);
            var excludePrefixes = ParsePrefixList(PluginConfig.ItemsExcludePrefixes.Value);
            return FilterByPrefixes(items, includePrefixes, excludePrefixes);
        }

        /// <summary>
        /// Filter categories for reference file generation using config values
        /// </summary>
        public static List<string> FilterCategories(List<string> categories)
        {
            var includePrefixes = ParsePrefixList(PluginConfig.CategoriesIncludePrefixes.Value);
            var excludePrefixes = ParsePrefixList(PluginConfig.CategoriesExcludePrefixes.Value);
            return FilterByPrefixes(categories, includePrefixes, excludePrefixes);
        }

        /// <summary>
        /// Filter menu lists for reference file generation using config values
        /// </summary>
        public static List<string> FilterMenuLists(List<string> menuLists)
        {
            var includePrefixes = ParsePrefixList(PluginConfig.MenuListsIncludePrefixes.Value);
            var excludePrefixes = ParsePrefixList(PluginConfig.MenuListsExcludePrefixes.Value);
            return FilterByPrefixes(menuLists, includePrefixes, excludePrefixes);
        }

        /// <summary>
        /// Filter interactions for reference file generation using config values
        /// </summary>
        public static List<string> FilterInteractions(List<string> interactions)
        {
            var includePrefixes = ParsePrefixList(PluginConfig.InteractionsIncludePrefixes.Value);
            var excludePrefixes = ParsePrefixList(PluginConfig.InteractionsExcludePrefixes.Value);
            return FilterByPrefixes(interactions, includePrefixes, excludePrefixes);
        }

        /// <summary>
        /// Filter rules for reference file generation using config values
        /// </summary>
        public static List<string> FilterRules(List<string> rules)
        {
            var includePrefixes = ParsePrefixList(PluginConfig.RulesIncludePrefixes.Value);
            var excludePrefixes = ParsePrefixList(PluginConfig.RulesExcludePrefixes.Value);
            return FilterByPrefixes(rules, includePrefixes, excludePrefixes);
        }
    }
}