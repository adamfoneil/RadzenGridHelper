using RadzenGridHelper.Classes;
using RadzenGridHelper.Models;
using System.Collections.Generic;
using System.Linq;

namespace RadzenGridHelper.Services
{
    public static class RadzenMarkup
    {
        public static string BuildGrid(Grid grid, char indent = default, int count = 0)
        {
            var root = new XmlElement("RadzenGrid", new
            {
                TItem = grid.ItemType
            });

            var columns = new XmlElement("Columns");
            root.Children.Add(columns);

            columns.Children.AddRange(grid.Columns.Select(col =>
            {
                var ele = new XmlElement("RadzenGridColumn", new
                {
                    TItem = grid.ItemType,
                    Title = col.Title,
                    Property = col.PropertyName
                });

                if (!string.IsNullOrEmpty(col.EditorControl))
                {
                    var edit = new XmlElement("EditTemplate", new
                    {
                        Context = grid.ContextVariable
                    });

                    edit.Children.Add(new XmlElement(col.EditorControl, new Dictionary<string, object>()
                    {
                        ["@bind-Value"] = $"{grid.ContextVariable}.{col.PropertyName}"
                    }));

                    ele.Children.Add(edit);
                }

                return ele;
            }));

            return (indent.Equals(default)) ? root.ToString() : root.ToString(indent, count);
        }
    }
}
