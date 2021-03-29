using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RadzenGridHelper.Classes
{
    public class XmlElement
    {
        public XmlElement(string name)
        {
            Name = name;            
            Attributes = new Dictionary<string, object>();
            Children = new List<XmlElement>();
        }

        public XmlElement(string name, Dictionary<string, object> attributes) : this(name)
        {
            Attributes = attributes;
        }

        public XmlElement(string name, object attributes) : this(name)
        {
            var props = attributes.GetType().GetProperties();
            foreach (var pi in props) Attributes.Add(pi.Name, pi.GetValue(attributes));
        }

        public string Name { get; }

        public string InnerText { get; init; }

        public List<XmlElement> Children { get; }

        public Dictionary<string, object> Attributes { get; }

        public string ToString(char indent, int count)
        {
            StringBuilder result = new StringBuilder();

            AddChildren(result, this, 0);

            return result.ToString();

            void AddChildren(StringBuilder stringBuilder, XmlElement current, int depth)
            {
                string indentStr = new string(indent, depth * count);

                if (current.Children?.Any() ?? false)
                {
                    stringBuilder.AppendLine(indentStr + StartTag(current));
                    foreach (var child in current.Children)
                    {
                        depth++;
                        AddChildren(stringBuilder, child, depth);
                        depth--;
                    }
                    stringBuilder.AppendLine(indentStr + EndTag(current));
                }
                else
                {
                    if (string.IsNullOrEmpty(current.InnerText))
                    {
                        stringBuilder.AppendLine(indentStr + Tag(current));
                    }
                    else
                    {
                        stringBuilder.AppendLine(indentStr + StartTag(current) + current.InnerText + EndTag(current));
                    }                    
                }
            }
        }

        public override string ToString() => ToString('\t', 1);

        private static string StartTag(XmlElement element) => $"<{element.Name}{AttributeString(element)}>";

        private static string Tag(XmlElement element) => $"<{element.Name}{AttributeString(element)}/>";

        private static string EndTag(XmlElement element) => $"</{element.Name}>";

        private static string AttributeString(XmlElement element) => (element.Attributes?.Any() ?? false) ?
            " " + string.Join(" ", element.Attributes.Select(kp => $"{kp.Key}=\"{kp.Value?.ToString().Replace("\"", "&quot;")}\"")) :
            string.Empty;
    }
}
