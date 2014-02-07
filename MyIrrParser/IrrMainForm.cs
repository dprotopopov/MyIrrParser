using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Windows.Forms;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;
using MyParser.Library.Forms;

namespace MyIrrParser
{
    public partial class IrrMainForm : RibbonForm
    {
        private readonly StartupForm _startupForm = new StartupForm();

        public IrrMainForm()
        {
            InitializeComponent();
        }

        private void barButtonItem1_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (_startupForm.ShowDialog() == DialogResult.OK)
            {
                string rubric;
                try
                {
                    rubric = ((KeyValuePair<string, string>) _startupForm.comboBoxRubric.SelectedItem).Key;
                }
                catch (Exception)
                {
                    rubric = @"//";
                }
                var irrChildForm = new IrrChildForm
                {
                    MaxPages = Convert.ToInt32(_startupForm.textBoxMaxPages.Value),
                    MaxThreads = Convert.ToInt32(_startupForm.textBoxMaxThreads.Value),
                    Keywords = _startupForm.textBoxSearch.Text,
                    Rubric = rubric,
                    OnlyTitle = _startupForm.checkBoxOnlyTitle.Checked,
                    MdiParent = this
                };
                irrChildForm.Show();
                irrChildForm.GenerateTasks();
                irrChildForm.StartWorker();
            }
        }

        private void barButtonItem2_ItemClick(object sender, ItemClickEventArgs e)
        {
            var irrChildForm = ActiveMdiChild as IChildForm;
            if (irrChildForm != null) irrChildForm.StartWorker();
        }

        private void barButtonItem3_ItemClick(object sender, ItemClickEventArgs e)
        {
            var irrChildForm = ActiveMdiChild as IChildForm;
            if (irrChildForm != null) irrChildForm.StopWorker();
        }

        private void barButtonItem4_ItemClick(object sender, ItemClickEventArgs e)
        {
            var irrChildForm = ActiveMdiChild as IChildForm;
            if (irrChildForm != null && saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                irrChildForm.SaveAs(saveFileDialog1.FileName);
            }
        }

        private void barButtonItem5_ItemClick(object sender, ItemClickEventArgs e)
        {
            foreach (Form form in MdiChildren)
            {
                var irrChildForm = form as IChildForm;
                Debug.Assert(irrChildForm != null, "IrrChildForm != null");
                irrChildForm.AbortWorker();
            }
            Close();
        }

        private void barButtonItem6_ItemClick(object sender, ItemClickEventArgs e)
        {
            var about = new AboutBox {Assembly = Assembly.GetExecutingAssembly()};
            about.ShowDialog();
        }
    }
}