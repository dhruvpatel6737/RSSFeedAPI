using System;
using System.Collections.Generic;

namespace RSSFeedAPI.Common
{
    public static class Utilities
    {
        public static Dictionary<TKey, TValue> ToDictionary<TSource, TKey, TValue>(
    IEnumerable<TSource> items,
    Converter<TSource, TKey> keySelector,
    Converter<TSource, TValue> valueSelector)
        {
            Dictionary<TKey, TValue> result = new Dictionary<TKey, TValue>();
            foreach (TSource item in items)
            {
                result.Add(keySelector(item), valueSelector(item));
            }
            return result;
        }
    }
}
