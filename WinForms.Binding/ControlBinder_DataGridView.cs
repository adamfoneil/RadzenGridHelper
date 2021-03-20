using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Windows.Forms;

namespace WinForms.Binding
{
    public partial class ControlBinder<T>
    {
        public void AddDataGridView<TRow>(DataGridView dataGridView, Expression<Func<T, IEnumerable<TRow>>> collection, Action<IEnumerable<TRow>, T> saveAction) where TRow : class
        {
            _setControls.Add((obj) => InitGridViewBinding(obj, dataGridView, collection));

            InitGridViewBinding(Object, dataGridView, collection);

            dataGridView.RowValidated += (sender, args) =>
            {
                var bindingSource = ((sender as DataGridView).DataSource) as BindingSource;
                var collection = bindingSource.DataSource as IEnumerable<TRow>;
                saveAction.Invoke(collection, Object);
            };

            dataGridView.UserDeletedRow += (sender, args) =>
            {
                IsDirty = true;
            };

            dataGridView.UserAddedRow += (sender, args) =>
            {
                IsDirty = true;
            };

            dataGridView.CellEndEdit += (sender, args) =>
            {
                IsDirty = true;
            };
        }

        private void InitGridViewBinding<TRow>(T @object, DataGridView dataGridView, Expression<Func<T, IEnumerable<TRow>>> collection)
        {
            var boundList = new BindingList<TRow>(collection.Compile().Invoke(@object).ToList());
            BindingSource bs = new BindingSource();
            bs.DataSource = boundList;
            dataGridView.DataSource = bs;
        }
    }
}
