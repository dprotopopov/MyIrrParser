using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Web;
using System.Windows.Forms;
using MyParser.Library;
using MyParser.Library.Forms;
using MyParser.Managed;
using MyWebSimulator;

namespace MyIrrParser
{
    public partial class IrrChildForm : Form, IChildForm
    {
        public static readonly ReturnFieldInfos ReturnFieldInfos = new ReturnFieldInfos
        {
            Defaults.ReturnFieldInfos.Url,
            Defaults.ReturnFieldInfos.Email,
            Defaults.ReturnFieldInfos.Phone,
            new ReturnFieldInfo
            {
                ReturnFieldId = Defaults.ReturnFieldInfos.PublicationLink.ReturnFieldId,
                ReturnFieldXpathTemplate = @"//div[@data-item-id]/a[@href][1]",
                ReturnFieldResultTemplate = Defaults.ReturnFieldInfos.PublicationLink.ReturnFieldResultTemplate,
                ReturnFieldRegexPattern = Defaults.ReturnFieldInfos.PublicationLink.ReturnFieldRegexPattern,
                ReturnFieldRegexReplacement = Defaults.ReturnFieldInfos.PublicationLink.ReturnFieldRegexReplacement,
                ReturnFieldRegexMatchPattern = Defaults.ReturnFieldInfos.PublicationLink.ReturnFieldRegexMatchPattern
            },
            new ReturnFieldInfo
            {
                ReturnFieldId = Defaults.ReturnFieldInfos.PublicationId.ReturnFieldId,
                ReturnFieldXpathTemplate = @"//div[@data-item-id]",
                ReturnFieldResultTemplate = @"{{data-item-id}}",
                ReturnFieldRegexPattern = Defaults.ReturnFieldInfos.PublicationId.ReturnFieldRegexPattern,
                ReturnFieldRegexReplacement = Defaults.ReturnFieldInfos.PublicationId.ReturnFieldRegexReplacement,
                ReturnFieldRegexMatchPattern = Defaults.ReturnFieldInfos.PublicationId.ReturnFieldRegexMatchPattern
            },
            new ReturnFieldInfo
            {
                ReturnFieldId = Defaults.ReturnFieldInfos.PublicationDate.ReturnFieldId,
                ReturnFieldXpathTemplate = @"//div[@data-item-id]",
                ReturnFieldResultTemplate = Defaults.ReturnFieldInfos.PublicationDate.ReturnFieldResultTemplate,
                ReturnFieldRegexPattern = Defaults.ReturnFieldInfos.PublicationDate.ReturnFieldRegexPattern,
                ReturnFieldRegexReplacement = Defaults.ReturnFieldInfos.PublicationDate.ReturnFieldRegexReplacement,
                ReturnFieldRegexMatchPattern = Defaults.ReturnFieldInfos.PublicationDate.ReturnFieldRegexMatchPattern
            },
            new ReturnFieldInfo
            {
                ReturnFieldId = "RubricOption",
                ReturnFieldXpathTemplate = @"//ul[@class='rubric_list']//a[@href]",
                ReturnFieldResultTemplate = @"{{href}}",
                ReturnFieldRegexPattern = Defaults.ReturnFieldInfos.Option.ReturnFieldRegexPattern,
                ReturnFieldRegexReplacement = Defaults.ReturnFieldInfos.Option.ReturnFieldRegexReplacement,
                ReturnFieldRegexMatchPattern = Defaults.ReturnFieldInfos.Option.ReturnFieldRegexMatchPattern
            },
            new ReturnFieldInfo
            {
                ReturnFieldId = "RubricValue",
                ReturnFieldXpathTemplate = @"//ul[@class='rubric_list']//a[@href]",
                ReturnFieldResultTemplate = Defaults.ReturnFieldInfos.Value.ReturnFieldResultTemplate,
                ReturnFieldRegexPattern = Defaults.ReturnFieldInfos.Value.ReturnFieldRegexPattern,
                ReturnFieldRegexReplacement = Defaults.ReturnFieldInfos.Value.ReturnFieldRegexReplacement,
                ReturnFieldRegexMatchPattern = Defaults.ReturnFieldInfos.Value.ReturnFieldRegexMatchPattern
            },
            new ReturnFieldInfo
            {
                ReturnFieldId = "ClickUrl",
                ReturnFieldXpathTemplate = @"//a[@href][contains(text(),'Посмотреть')]",
                ReturnFieldResultTemplate = Defaults.ReturnFieldInfos.Url.ReturnFieldResultTemplate,
                ReturnFieldRegexPattern = Defaults.ReturnFieldInfos.Url.ReturnFieldRegexPattern,
                ReturnFieldRegexReplacement = Defaults.ReturnFieldInfos.Url.ReturnFieldRegexReplacement,
                ReturnFieldRegexMatchPattern = Defaults.ReturnFieldInfos.Url.ReturnFieldRegexMatchPattern
            },
            new ReturnFieldInfo
            {
                ReturnFieldId = "PhoneImageUrl",
                ReturnFieldXpathTemplate = @"//a[@href='#phone']",
                ReturnFieldResultTemplate = Defaults.ReturnFieldInfos.Url.ReturnFieldResultTemplate,
                ReturnFieldRegexPattern = Defaults.ReturnFieldInfos.Url.ReturnFieldRegexPattern,
                ReturnFieldRegexReplacement = Defaults.ReturnFieldInfos.Url.ReturnFieldRegexReplacement,
                ReturnFieldRegexMatchPattern = Defaults.ReturnFieldInfos.Url.ReturnFieldRegexMatchPattern
            },
        };

        private int _navigatingCountdown = 3;

        public IrrChildForm()
        {
            InitializeComponent();
            WebTaskManager = new WebTaskManager
            {
                ListView = listViewQueue,
                MaxLevel = 2
            };
            WebSimulator = new WebSimulator {WebBrowser = new ManagedWebBrowser(webBrowser1)};
        }

        public string Rubric { get; set; }
        public string Keywords { get; set; }
        public bool OnlyTitle { get; set; }
        public int MaxPages { get; set; }
        public IWebTaskManager WebTaskManager { get; set; }
        public IWebSimulator WebSimulator { get; set; }

        public bool IsRunning
        {
            get { return timerLauncher.Enabled; }
            set
            {
                if (value)
                {
                    WebTaskManager.ResumeAllTasks();
                    WebTaskManager.StartAllTasks();
                    timerLauncher.Enabled = true;
                }
                else
                {
                    timerLauncher.Enabled = false;
                }
            }
        }

        public int MaxLevel
        {
            get { return WebTaskManager.MaxLevel; }
            set { WebTaskManager.MaxLevel = value; }
        }

        public int MaxThreads
        {
            get { return WebTaskManager.MaxThreads; }
            set { WebTaskManager.MaxThreads = value; }
        }

        public object LastError { get; set; }

        public void GenerateTasks()
        {
            for (int pageId = 0; pageId < MaxPages; pageId++)
            {
                try
                {
                    Debug.Assert(WebTaskManager != null, "WebTaskManager != null");
                    WebTaskManager.AddTask(new IrrSearchLookup
                    {
                        PageId = pageId,
                        Keywords = HttpUtility.UrlEncode(Keywords),
                        Rubric = Rubric,
                        OnlyTitle = OnlyTitle ? 1 : 0,
                        OnStartCallback = OnWebTaskDefault,
                        OnAbortCallback = OnWebTaskDefault,
                        OnResumeCallback = OnWebTaskDefault,
                        OnErrorCallback = OnWebTaskDefault,
                        OnCompliteCallback = OnWebTaskComplite,
                        ReturnFieldInfos = ReturnFieldInfos,
                    });
                }
                catch (Exception exception)
                {
                    LastError = exception;
                    Debug.WriteLine(MethodBase.GetCurrentMethod().Name + ":" + LastError);
                }
            }
        }

        public void ShowAdvert()
        {
            WebSimulator.Click(@"//*");
        }

        public void StartWorker()
        {
            IsRunning = true;
        }

        public void StopWorker()
        {
            IsRunning = false;
        }

        public void AbortWorker()
        {
            WebTaskManager.AbortAllTasks();
        }

        public void OnWebTaskDefault(IWebTask webTask)
        {
            OnWebTaskDelegate d = WebTaskManager.OnWebTaskDefault;
            object[] arr = {webTask};
            try
            {
                Invoke(d, arr);
            }
            catch (Exception exception)
            {
                LastError = exception;
                Debug.WriteLine(MethodBase.GetCurrentMethod().Name + ":" + LastError);
            }
        }

        public void OnWebTaskComplite(IWebTask webTask)
        {
            Debug.Assert(webTask != null, "webTask != null");
            OnWebTaskDefault(webTask);

            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (listViewResult.InvokeRequired || listViewQueue.InvokeRequired)
            {
                OnWebTaskDelegate d = OnWebTaskComplite;
                object[] arr = {webTask};
                try
                {
                    Invoke(d, arr);
                }
                catch (Exception exception)
                {
                    LastError = exception;
                    Debug.WriteLine(MethodBase.GetCurrentMethod().Name + ":" + LastError);
                }
            }
            else
            {
                Debug.Assert(webTask != null, "webTask != null");
                var searchLookup = webTask as IrrSearchLookup;
                if (searchLookup != null)
                {
                    var baseUri = new Uri(searchLookup.Url);
                    foreach (Uri uri in searchLookup.ReturnFields.OtherPageUrl.Select(url => new Uri(baseUri, url)))
                    {
                        try
                        {
                            Debug.Assert(WebTaskManager != null, "WebTaskManager != null");
                            WebTaskManager.AddTask(new IrrSearchLookup
                            {
                                Url = uri.ToString(),
                                Level = searchLookup.Level + 1,
                                PageId = searchLookup.PageId,
                                Keywords = searchLookup.Keywords,
                                Rubric = searchLookup.Rubric,
                                OnlyTitle = searchLookup.OnlyTitle,
                                OnStartCallback = OnWebTaskDefault,
                                OnAbortCallback = OnWebTaskDefault,
                                OnResumeCallback = OnWebTaskDefault,
                                OnErrorCallback = OnWebTaskDefault,
                                OnCompliteCallback = OnWebTaskComplite,
                                ReturnFieldInfos = ReturnFieldInfos,
                            });
                        }
                        catch (Exception exception)
                        {
                            LastError = exception;
                            Debug.WriteLine(MethodBase.GetCurrentMethod().Name + ":" + LastError);
                        }
                    }
                    foreach (Uri uri in searchLookup.ReturnFields.Url.Select(url => new Uri(baseUri, url)))
                    {
                        try
                        {
                            Debug.Assert(WebTaskManager != null, "WebTaskManager != null");
                            WebTaskManager.AddTask(new IrrPublicationLookup
                            {
                                Url = uri.ToString(),
                                OnStartCallback = OnWebTaskDefault,
                                OnAbortCallback = OnWebTaskDefault,
                                OnResumeCallback = OnWebTaskDefault,
                                OnErrorCallback = OnWebTaskDefault,
                                OnCompliteCallback = OnWebTaskComplite,
                                ReturnFieldInfos = ReturnFieldInfos,
                            });
                        }
                        catch (Exception exception)
                        {
                            LastError = exception;
                            Debug.WriteLine(MethodBase.GetCurrentMethod().Name + ":" + LastError);
                        }
                    }
                }
                else
                {
                    var lookup = webTask as IrrPublicationLookup;
                    if (lookup != null)
                    {
                        ListViewItem viewItem = lookup.ReturnFields.ToListViewItem(
                            lookup.PublicationId,
                            lookup.ReturnFieldInfos.Keys.ToList());
                        viewItem.Tag = lookup.ReturnFields;
                        listViewResult.Items.Add(viewItem);
                    }
                }
            }
        }

        public void NavigateToAdvert(object sender, WebBrowserNavigatingEventArgs e)
        {
            if (_navigatingCountdown == 0)
            {
                e.Cancel = true;
                String url = e.Url.ToString();
                Process.Start(url);
            }
        }

        public void SaveAs(String fileName)
        {
            var outfile = new StreamWriter(fileName);
            foreach (ListViewItem lvi in listViewResult.Items)
            {
                outfile.WriteLine(lvi.Text);
            }
            outfile.Close();
        }

        public void AdvertRefresh(object sender, EventArgs e)
        {
            _navigatingCountdown = 3;
            webBrowser1.Refresh();
        }

        private void childForm_Load(object sender, EventArgs e)
        {
            listViewResult.SuspendLayout();
            listViewQueue.SuspendLayout();
        }

        private void ResumeSuspendLayout(object sender, EventArgs e)
        {
            listViewResult.ResumeLayout();
            listViewQueue.ResumeLayout();
            Thread.Sleep(0);
            listViewResult.SuspendLayout();
            listViewQueue.SuspendLayout();
        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            WebSimulator.Window = WebSimulator.TopmostWindow;
            _navigatingCountdown--;
        }

        private void timerLauncher_Tick(object sender, EventArgs e)
        {
            WebTaskManager.StartAllTasks();
        }

        private void listView2_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (listViewQueue.SelectedItems.Count == 1)
            {
                ListView.SelectedListViewItemCollection items = listViewQueue.SelectedItems;
                ListViewItem item = items[0];
                int index = listViewQueue.Items.IndexOf(item);
                IWebTask task = WebTaskManager.Tasks[index];
                string url = task.Url;
                Process.Start(url);
            }
        }

        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            if (listViewResult.SelectedItems.Count == 1)
            {
                ListView.SelectedListViewItemCollection items = listViewResult.SelectedItems;
                ListViewItem item = items[0];
            }
        }

        private void ChildForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            WebTaskManager.AbortAllTasks();
            Thread.Sleep(0);
        }
    }
}