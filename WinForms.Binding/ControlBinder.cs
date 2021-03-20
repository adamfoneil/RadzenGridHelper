using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Windows.Forms;

namespace WinForms.Binding
{
    public delegate void PropertyUpdatedHandler<T>(object sender, T @object, string propertyName);

    public partial class ControlBinder<T>
    {
        public T Object
        {
            get => _object;
            set
            {
                _object = value;
                LoadValues();
            }
        }

        private T _object;
        private bool _dirty = false;
        private bool _suspend = false;
        private List<Action<T>> _setControls = new List<Action<T>>();
        private List<Action> _clearControls = new List<Action>();
        private Dictionary<Control, bool> _textChanged = new Dictionary<Control, bool>();

        public event EventHandler IsDirtyChanged;
        public event EventHandler ClearingValues;
        public event PropertyUpdatedHandler<T> PropertyUpdated;
        public event EventHandler<T> LoadingValues;

        public bool IsDirty
        {
            get { return _dirty; }
            set
            {
                if (_dirty != value)
                {
                    _dirty = value;
                    IsDirtyChanged?.Invoke(this, new EventArgs());
                }
            }
        }

        public void LoadValues()
        {
            _suspend = true;
            foreach (var setter in _setControls) setter.Invoke(Object);
            LoadingValues?.Invoke(this, Object);
            _suspend = false;
            IsDirty = false;
        }

        private PropertyInfo GetProperty<TValue>(Expression<Func<T, TValue>> property)
        {
            string propName = PropertyNameFromLambda(property);
            PropertyInfo pi = typeof(T).GetProperty(propName);
            return pi;
        }

        private string PropertyNameFromLambda(Expression expression)
        {
            // thanks to http://odetocode.com/blogs/scott/archive/2012/11/26/why-all-the-lambdas.aspx
            // thanks to http://stackoverflow.com/questions/671968/retrieving-property-name-from-lambda-expression

            LambdaExpression le = expression as LambdaExpression;
            if (le == null) throw new ArgumentException("expression");

            MemberExpression me = null;
            if (le.Body.NodeType == ExpressionType.Convert)
            {
                me = ((UnaryExpression)le.Body).Operand as MemberExpression;
            }
            else if (le.Body.NodeType == ExpressionType.MemberAccess)
            {
                me = le.Body as MemberExpression;
            }

            if (me == null) throw new ArgumentException("expression");

            return me.Member.Name;
        }
    }
}
