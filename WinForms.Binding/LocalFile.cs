using System;
using System.IO;
using System.Text.Json;
using System.Windows.Forms;

namespace WinForms.Binding
{
    public static class LocalFile
    {
        public static (bool ok, string fileName) PromptSaveJson<T>(T @object, string defaultExtension = ".json", string filter = "Json Files|*.json", JsonSerializerOptions options = null)
        {
            var dlg = new SaveFileDialog()
            {
                Filter = filter,
                DefaultExt = defaultExtension
            };

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                var json = JsonSerializer.Serialize(@object, options);
                File.WriteAllTextAsync(dlg.FileName, json);
                return (true, dlg.FileName);
            }

            return (false, null);
        }

        public static (bool ok, string fileName) PromptOpenJson<T>(Action<T> openAction, string defaultExtension = ".json", string filter = "Json Files|*.json")
        {
            var dlg = new OpenFileDialog()
            {
                Filter = filter,
                DefaultExt = defaultExtension
            };

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                var json = File.ReadAllText(dlg.FileName);
                T obj = JsonSerializer.Deserialize<T>(json);
                openAction.Invoke(obj);
                return (true, dlg.FileName);
            }

            return (false, null);
        }
    }
}
