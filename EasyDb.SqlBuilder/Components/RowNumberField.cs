﻿using System;
using System.Collections.Generic;
using System.Text;

namespace EasyDb.SqlBuilder.Components
{
    public class RowNumberField : ISqlField
    {
        public List<SortField> PartitionBy { get; set; } = new List<SortField>();

        public List<SortField> OrderBy { get; set; } = new List<SortField>();

        public RowNumberField Partition(SortField field)
        {
            PartitionBy.Add(field);
            return this;
        }

        public RowNumberField Order(SortField field)
        {
            OrderBy.Add(field);
            return this;
        }

        public void Accept(ISqlComponentVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
