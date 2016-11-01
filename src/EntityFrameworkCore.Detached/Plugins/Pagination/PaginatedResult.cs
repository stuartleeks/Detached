﻿using System.Collections.Generic;

namespace EntityFrameworkCore.Detached.Plugins.Pagination
{
    public class PaginatedResult<TEntity> : IPaginatedResult<TEntity>
    {
        public List<TEntity> Items { get; set; }

        public int PageCount { get; set; }

        public int PageIndex { get; set; }

        public int PageSize { get; set; }

        public int RowCount { get; set; }
    }
}
