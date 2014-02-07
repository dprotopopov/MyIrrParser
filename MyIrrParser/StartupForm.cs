using System;
using System.Diagnostics;
using DevExpress.XtraEditors;
using MyParser.Library;

namespace MyIrrParser
{
    public partial class StartupForm : XtraForm, ILastError
    {
        public StartupForm()
        {
            InitializeComponent();
            RubricQuery = new IrrRubricQuery
            {
                Option = "RubricOption",
                Value = "RubricValue",
                OnQueryCompliteCallback = OnRubricQueryComplite,
                ReturnFieldInfos = IrrChildForm.ReturnFieldInfos,
            };
            RubricQuery.Query();
        }

        public IrrRubricQuery RubricQuery { get; set; }

        internal void OnRubricQueryComplite(IWebQuery task)
        {
            Debug.Assert(RubricQuery == task);
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (comboBoxRubric.InvokeRequired)
            {
                OnWebQueryDelegate d = OnRubricQueryComplite;
                var arr = new object[] {task};
                try
                {
                    Invoke(d, arr);
                }
                catch (Exception exception)
                {
                    LastError = exception;
                }
            }
            else
            {
                comboBoxRubric.Items.Clear();
                foreach (var item in RubricQuery.Dictionary)
                {
                    comboBoxRubric.Items.Add(item);
                }
            }
        }

        public object LastError { get; set; }
    }
}