using System.Collections.Generic;
using Core.Entities;

namespace API.Helpers
{
    public class Paginator<T> where T : class
    {
        public Paginator(int pageIndex, int pageSize, int count, IReadOnlyList<T> result)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            Count = count;
            Result = result;
        }

        /// <summary>
        /// The number of the page returned by Paginator
        /// </summary>
        public int PageIndex { get; set; }

        /// <summary>
        /// Max number of entities on page
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// Total number of entities returned by query
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// The entities on the page returned
        /// </summary>
        public IReadOnlyList<T> Result { get; set; }
    }
}