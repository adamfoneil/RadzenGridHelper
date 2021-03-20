using System;
using System.Linq.Expressions;
using System.Reflection;
using System.Windows.Forms;

namespace WinForms.Binding
{
    public partial class ControlBinder<T>
    {
        public void Add(ToolStripTextBox control, Func<T, string> setProperty, Action<T> setControl)
        {
            _setControls.Add(setControl);

            control.TextChanged += (sender, args) =>
            {
                if (_suspend) return;
                var propertyName = setProperty.Invoke(Object);
                IsDirty = true;
                PropertyUpdated?.Invoke(this, Object, propertyName);
            };
        }

        public void Add(ToolStripTextBox control, Expression<Func<T, object>> property)
        {
            PropertyInfo pi = GetProperty(property);
            Func<T, string> setProperty = (doc) =>
            {
                pi.SetValue(doc, control.Text);
                return pi.Name;
            };

            var func = property.Compile();
            Action<T> setControl = (doc) =>
            {
                control.Text = func.Invoke(doc)?.ToString();
            };

            Add(control, setProperty, setControl);
        }
    }
}
