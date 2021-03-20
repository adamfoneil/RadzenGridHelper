﻿namespace RadzenGridHelper.Models
{
    public class Grid
    {
        public string ItemType { get; set; }
        public string ContextVariable { get; set; }
        public Column[] Columns { get; set; }

        public class Column
        {
            public string PropertyName { get; set; }
            public string Title { get; set; }
            public string EditorControl { get; set; }
        }
    }
}
