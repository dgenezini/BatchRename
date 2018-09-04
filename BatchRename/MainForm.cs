using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BatchRename
{
    public partial class MainForm : Form
    {
        private List<FileChanges> Files = new List<FileChanges>();

        public MainForm()
        {
            InitializeComponent();

            cmbNumberFormat.SelectedIndex = 0;
            dataGridView1.DataSource = Files;
        }

        private void btnRenameAll_Click(object sender, EventArgs e)
        {
            foreach (var FileItem in Files)
            {
                File.Move(FileItem.FileName, FileItem.NewFileName);
            }
        }

        private void UpdateNewFileNames()
        {
            int Number = (int)edtInitialNumber.Value;

            if (chkOrderByFileName.Checked)
            {
                Files = Files.OrderBy(a => a.FileName).ToList();
            }

            foreach (var FileItem in Files)
            {
                string NumberStr = Number.ToString().PadLeft(cmbNumberFormat.Text.Length, '0');

                string Directory = Path.GetDirectoryName(FileItem.FileName);
                string NewFileName = $"{edtPrefix.Text}{NumberStr}{edtSufix.Text}{Path.GetExtension(FileItem.FileName)}";
                FileItem.NewFileName = Path.Combine(Directory, NewFileName);

                Number++;
            }

            RefreshGrid();
        }

        private void RefreshGrid()
        {
            BindingSource GridBindingSource = new BindingSource();
            GridBindingSource.DataSource = Files;
            dataGridView1.DataSource = GridBindingSource;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                foreach (var FileName in openFileDialog1.FileNames)
                {
                    Files.Add(new FileChanges() { FileName = FileName });
                }

                UpdateNewFileNames();
            }
        }

        private void ValueChanged(object sender, EventArgs e)
        {
            UpdateNewFileNames();
        }

        private void dataGridView1_Paint(object sender, PaintEventArgs e)
        {
            dataGridView1.Columns[0].Width = (dataGridView1.Width - 5) / 2;
            dataGridView1.Columns[1].Width = (dataGridView1.Width - 5) / 2;
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.RemoveAt(dataGridView1.SelectedRows[0].Index);

            UpdateNewFileNames();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            Files.Clear();

            RefreshGrid();
        }
    }
}