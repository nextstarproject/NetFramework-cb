using System.Linq.Dynamic.Core;
using NextStar.Framework.Core.Https;

namespace NextStar.Framework.Core.Utils;

public static class SortHelper
{
    /// <summary>
    /// 根据传入的排序字段列表，对指定数据源进行排序，将排序后的数据返回
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="sorts"></param>
    /// <param name="dataQuery"></param>
    /// <param name="defaultOrder"></param>
    /// <returns></returns>
    public static IOrderedQueryable<T> Sort<T>(List<SortDescriptor> sorts, IQueryable<T> dataQuery, string defaultOrder = "")
    {
        //判断字段是否正确
        string sortString = GetSortString(sorts, typeof(T));
        if (string.IsNullOrEmpty(sortString))
            sortString = defaultOrder;

        //结果排序
        return dataQuery.AsQueryable().OrderBy(sortString);
    }

    /// <summary>
    /// 判断指定Type是否存在对应的属性，如果此字段在类型中不存在，则跳过此排序字段
    /// </summary>
    /// <param name="sorts"></param>
    /// <param name="type"></param>
    /// <returns></returns>
    private static string GetSortString(List<SortDescriptor>? sorts, Type type)
    {
        if (sorts == null || sorts.Count == 0)
            return "";

        var sortStrings = new List<string>();

        foreach (SortDescriptor sort in sorts.Where(s => !s.PropertyName.IsNullOrWhiteSpace()))
        {
            if (type.GetProperty(sort.PropertyName) != null)
            {
                if (sort.Direction == SortDirection.Desc)
                {
                    sortStrings.Add(string.Format("{0} desc", sort.PropertyName));
                }
                else
                {
                    sortStrings.Add(sort.PropertyName);
                }
            }
        }
        return string.Join(',', sortStrings);
    }

}
