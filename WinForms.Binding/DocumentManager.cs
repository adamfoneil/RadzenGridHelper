using System;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinForms.Binding
{
    public class DocumentManager<T>
    {
        private string _fileName;

        public DocumentManager(Form form)
        {
            Binder = new ControlBinder<T>();

            SerializerOptions = new JsonSerializerOptions()
            {
                WriteIndented = true
            };

            form.FormClosing += async (sender, args) =>
            {
                if (Binder.IsDirty)
                {
                    var response = MessageBox.Show("Save changes?", "Form Closing", MessageBoxButtons.YesNoCancel);

                    switch (response)
                    {
                        case DialogResult.Yes:
                            var result = await PromptSaveAsync();
                            if (!result) args.Cancel = true;
                            break;

                        case DialogResult.Cancel:
                            args.Cancel = true;
                            break;
                    }
                }
            };
        }

        public JsonSerializerOptions SerializerOptions { get; set; }
        public string FileDialogFilter { get; init; }
        public string FileDialogExtension { get; init; }

        public ControlBinder<T> Binder { get; }

        public async Task<bool> PromptNewAsync(Func<T> initializer)
        {
            if (Binder.IsDirty)
            {
                if (!await PromptSaveAsync()) return false;
            }

            Binder.Object = initializer.Invoke();
            _fileName = null;
            return true;
        }

        public async Task<bool> PromptOpenAsync()
        {
            var dlg = new OpenFileDialog()
            {
                Filter = FileDialogFilter,
                DefaultExt = FileDialogExtension
            };

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                await OpenAsync(dlg.FileName);
                return true;
            }

            return false;
        }

        public async Task OpenAsync(string fileName)
        {
            var json = await File.ReadAllTextAsync(fileName);
            T obj = JsonSerializer.Deserialize<T>(json);
            Binder.Object = obj;
            _fileName = fileName;
        }

        public async Task<bool> PromptSaveAsync()
        {
            if (!File.Exists(_fileName))
            {
                var dlg = new SaveFileDialog()
                {
                    Filter = FileDialogFilter,
                    DefaultExt = FileDialogExtension
                };

                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    await SaveAsync(dlg.FileName);
                    return true;
                }
                else
                {
                    return false;
                }
            }

            await SaveAsync(_fileName);
            return true;
        }

        public async Task SaveAsync(string fileName)
        {
            var json = JsonSerializer.Serialize(Binder.Object, SerializerOptions);
            await File.WriteAllTextAsync(fileName, json);
            _fileName = fileName;
            Binder.IsDirty = false;
        }
    }
}
