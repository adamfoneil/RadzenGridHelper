using RadzenGridHelper.Models;
using System.Linq;
using System.Windows.Forms;
using WinForms.Binding;

namespace RadzenGridHelper
{
    public partial class frmMain : Form
    {
        private ControlBinder<Grid> _binder;

        public frmMain()
        {
            InitializeComponent();                        
            dgvColumns.AutoGenerateColumns = false;
        }

        private void frmMain_Load(object sender, System.EventArgs e)
        {
            _binder = new ControlBinder<Grid>();
            _binder.Object = new Grid();
            InitBinding();
        }

        private void InitBinding()
        {            
            _binder.Add(tbItemType, field => field.ItemType);
            _binder.Add(tbContextVar, field => field.ContextVariable);
            _binder.AddDataGridView(dgvColumns, g => g.Columns, (rows, obj) => obj.Columns = rows.ToArray());
        }

        private void btnSave_Click(object sender, System.EventArgs e)
        {
            LocalFile.PromptSaveJson(_binder.Object);
        }

        private void btnBuildMarkup_Click(object sender, System.EventArgs e)
        {
            //var element = 
        }

        private void btnOpen_Click(object sender, System.EventArgs e)
        {
            LocalFile.PromptOpenJson<Grid>((obj) => _binder.Object = obj);
        }
    }
}
