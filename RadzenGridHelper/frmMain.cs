using RadzenGridHelper.Models;
using RadzenGridHelper.Services;
using System;
using System.Linq;
using System.Windows.Forms;
using WinForms.Binding;

namespace RadzenGridHelper
{
    public partial class frmMain : Form
    {
        private DocumentManager<Grid> _doc;

        public frmMain()
        {
            InitializeComponent();
            dgvColumns.AutoGenerateColumns = false;
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            _doc = new DocumentManager<Grid>(this)
            {
                FileDialogExtension = ".json",
                FileDialogFilter = "Json Files|*.json|All Files|*.*"
            };

            _doc.Binder.Object = new Grid();
            InitBinding();
        }

        private void InitBinding()
        {
            _doc.Binder.Add(tbItemType, field => field.ItemType);
            _doc.Binder.Add(tbContextVar, field => field.ContextVariable);
            _doc.Binder.AddDataGridView(dgvColumns, g => g.Columns, (rows, obj) => obj.Columns = rows.ToArray());
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            await _doc.PromptSaveAsync();
        }

        private async void btnOpen_Click(object sender, EventArgs e)
        {
            await _doc.PromptOpenAsync();
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            try
            {
                tbMarkup.Text = RadzenMarkup.BuildGrid(_doc.Binder.Object);
                Clipboard.SetText(tbMarkup.Text);
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        private async void btnNew_Click(object sender, EventArgs e)
        {
            await _doc.PromptNewAsync(() => new Grid());
        }
    }
}
