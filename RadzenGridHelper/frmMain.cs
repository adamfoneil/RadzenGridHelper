using RadzenGridHelper.Models;
using System.Windows.Forms;

namespace RadzenGridHelper
{
    public partial class frmMain : Form
    {        
        //private JsonSDI<Grid> _doc = new JsonSDI<Grid>(".json", "Json Files|*.json", "Save changes?");

        public frmMain()
        {
            InitializeComponent();                        
            dgvColumns.AutoGenerateColumns = false;
        }

        private void frmMain_Load(object sender, System.EventArgs e)
        {
            //_doc.Document = new Grid();
            InitBinding();
        }

        private void InitBinding()
        {
            //_doc.Controls.Add(tbItemType, field => field.ItemType);
            //_doc.Controls.Add(tbContextVar, field => field.ContextVariable);

            BindingSource bsColumns = new BindingSource();
            //bsColumns.DataSource = _doc.Document.Columns;
            dgvColumns.DataSource = bsColumns;
        }

        private async void btnSave_Click(object sender, System.EventArgs e)
        {
            //await _doc.SaveAsync();
        }

        private void btnBuildMarkup_Click(object sender, System.EventArgs e)
        {
            //var element = 
        }
    }
}
