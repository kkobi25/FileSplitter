using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace FileSpitter
{
    public partial class FileSplitter : Form
    {
        private List<string> _fileList = null;
        private string _outputDir = null;

        private const int _headerRow = 1;
        private double _thresholdinMB = 5;
        public FileSplitter()
        {
            InitializeComponent();
            _fileList = new List<string>();
        }

        private void btn_Start_Click(object sender, EventArgs e)
        {
            _fileList.Clear();
            _thresholdinMB = (double)nud_sizeMB.Value;
            // Select files to convert
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "CSV file (*.csv)|*.csv;*.CSV|All files (*.*)|*.*";
                ofd.RestoreDirectory = true;
                ofd.Title = "Select files to convert.";
                ofd.Multiselect = true;

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    foreach (string fn in ofd.FileNames)
                    {
                        _fileList.Add(fn);
                    }
                }
                else
                {
                    return;
                }
            }

            // Select directory to save converted files
            using (FolderBrowserDialog fbd = new FolderBrowserDialog())
            {
                fbd.Description = "Select folder to save converted file";
                fbd.ShowNewFolderButton = true;

                if (fbd.ShowDialog() == DialogResult.OK)
                {
                    _outputDir = fbd.SelectedPath;
                }
                else
                {
                    return;
                }
            }
            List<Task<bool>> tasks = new List<Task<bool>>();
            foreach (var file in _fileList)
            {
                tasks.Add(SplitCSVAysnc(file));
            }
            Task.WaitAll(tasks.ToArray());
            MessageBox.Show("Done!");
        }




        private async Task<bool> SplitCSVAysnc(string path_OriginalFile)
        {
            if (string.IsNullOrEmpty(path_OriginalFile) || !File.Exists(path_OriginalFile))
                return false;

            StringBuilder sb = new StringBuilder();
            string headerLine = "";
            int totalBytes = 0;
            int fileNumber = 1;
            string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(path_OriginalFile);
            string outputFileNameWithoutExtension = Path.Combine(_outputDir, fileNameWithoutExtension);
            await Task.Run(() =>
            {
                using (StreamReader sr = new StreamReader(path_OriginalFile))
                {
                    headerLine = sr.ReadLine();
                    int headerBytes = Encoding.UTF8.GetByteCount(headerLine + "\n");
                    totalBytes = headerBytes;
                    while (!sr.EndOfStream)
                    {
                        string newLine = sr.ReadLine();
                        int newLineBytes = Encoding.UTF8.GetByteCount(newLine + "\n");
                        if (totalBytes + newLineBytes >= _thresholdinMB * 1024.0 * 1024.0)
                        {
                            CreateCSV($"{outputFileNameWithoutExtension}_{fileNumber}.csv",
                                headerLine,
                                sb);
                            sb.Clear();
                            totalBytes = headerBytes;
                            fileNumber++;
                        }

                        sb.AppendLine(newLine);
                        totalBytes += newLineBytes;

                    }
                }
                if (sb.Length > 0)
                {
                    CreateCSV($"{outputFileNameWithoutExtension}_{fileNumber}.csv",
                        headerLine,
                        sb);
                }
            }).ConfigureAwait(false);


            return true;
        }

        private bool CreateCSV(string fileName, string headerLine, StringBuilder values)
        {
            if (values == null)
                return false;
            try
            {
                using (StreamWriter sw = new StreamWriter(fileName, false, Encoding.UTF8))
                //using (StreamWriter sw = new StreamWriter(fileName, false, Encoding.GetEncoding("utf-16")))
                {
                    sw.WriteLine(headerLine);
                    sw.Write(values.ToString());
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
