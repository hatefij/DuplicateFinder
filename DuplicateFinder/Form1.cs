using System;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using Microsoft.VisualBasic.FileIO;

namespace DuplicateFinder
{
    // todo: finish form icon
    // todo: add progress bar to population of result treeview
    // todo: add treeview right-click ability to expand/contract all nodes
    public partial class Form1 : Form
    {
        #region Delegates

        public delegate void UpdateStatusLabel();
        public UpdateStatusLabel updateStatusLabelDelegate;

        public delegate void ResetStatusLabel();
        public ResetStatusLabel resetStatusLabelDelegate;

        #endregion

        #region Thread Objects

        protected FileSearchThread fileSearchThread;
        protected UpdateLabelThread updateLabelThread;

        #endregion

        #region Private Members

        private int runTime;

        string[] pictureBoxFileExtensions = new string[]
        {
            ".gif", ".jpg", ".jpeg", ".bmp", ".wmf", ".png"
        };

        string[] mediaPlayerFileExtensions = new string[] // list of file extensions provided at: https://support.microsoft.com/en-us/help/316992/file-types-supported-by-windows-media-player
        {
            ".asf", ".wma", ".wmv", ".wm", // Windows Media formats
            ".asx", ".wax", ".wvx", ".wmx", ".wpl", // Windows Media Metafiles
            ".dvr-ms", // Microsoft Digital Video Recording
            ".wmd", // Windows Media Download Package
            ".avi", // Audio Visual Interleave
            ".mpg", ".mpeg", ".m1v", ".mp2", ".mp3", ".mpa", ".mpe", ".m3u", // Moving Pictures Experts Group
            ".mid", ".midi", ".rmi", // Musical Instrument Digital Interface
            ".aif", ".aifc", ".aiff", // Audio Interchange File Format
            ".au", ".snd", // Sun Microsystems and NeXT
            ".wav", // Audio for Windows
            ".cda", // CD Audio Track
            ".ivf", // Indeo Video Technology
            ".wmz", ".wms", // Windows Media Player Skins
            ".mov", // QuickTime Movie file
            ".m4a", // MP4 Audio file
            ".mp4", ".m4v", ".mp4v", ".3g2", ".3gp2", ".3gp", ".3gpp", // MP4 Video file
            ".aac", ".adt", ".adts",  // Windows audio file
            ".m2ts", // MPEG-2 TS Video file
            ".flac" // Free Lossless Audio Codec
        };

        string[] browserFileExtensions = new string[]
        {
            ".pdf", ".doc", ".docx"
        };

        #endregion

        public Form1()
        {
            InitializeComponent();

            axWindowsMediaPlayer1.settings.mute = true;

            updateStatusLabelDelegate = new UpdateStatusLabel(UpdateLabel);
            resetStatusLabelDelegate = new ResetStatusLabel(LabelReset);

            fileSearchThread = new FileSearchThread();
            updateLabelThread = new UpdateLabelThread(this);

            ResetPreviewWindows();
        }

        #region Helper Functions

        private void ResetForm()
        {
            treeResults.Nodes.Clear();
            UpdateNumberOfNonUniqueHashes();
            DisplayNumberOfTotalFiles();
            pictureBox1.ImageLocation = string.Empty;
        }

        private void UpdateNumberOfNonUniqueHashes()
        {
            if (treeResults.Nodes.Count > 0)
            {
                toolStripLabelNumEntries.Text = "Number of non-unique hashes: " + treeResults.Nodes.Count;
            }
            else
            {
                toolStripLabelNumEntries.Text = string.Empty;
            }
        }

        private void DisplayNumberOfTotalFiles()
        {
            if (fileSearchThread.NumberOfFiles > 0)
            {
                toolStripNumberOfFiles.Text = "Files Scanned: " + fileSearchThread.NumberOfFiles.ToString();
            }
            else
            {
                toolStripNumberOfFiles.Text = string.Empty;
            }
        }

        private void DisplayRuntime()
        {
            TimeSpan timeSpan = TimeSpan.FromSeconds(runTime);

            toolStripRunTime.Text = string.Format("Runtime: {0:d2}:{1:d2}:{2:d2}", timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);
        }

        private void ResetPreviewWindows()
        {
            pictureBox1.Visible = false;
            axWindowsMediaPlayer1.Visible = false;
            webBrowser1.Visible = false;

            pictureBox1.ImageLocation = string.Empty;
            axWindowsMediaPlayer1.URL = string.Empty;
            webBrowser1.Navigate("about:blank");
        }

        private void DeleteSelection(TreeView treeView)
        {
            if (treeView.SelectedNode.Text.Contains("."))
            {
                if (MessageBox.Show("Are You Sure?", "Confirm", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    ResetPreviewWindows();

                    RecycleOption recycleOption = RecycleOption.SendToRecycleBin;

                    if (!chkBxRecycle.Checked)
                    {
                        recycleOption = RecycleOption.DeletePermanently;
                    }

                    FileSystem.DeleteFile(treeView.SelectedNode.Text, UIOption.OnlyErrorDialogs, recycleOption);

                    TreeNode parentNode = treeView.SelectedNode.Parent;

                    treeView.SelectedNode.Remove();

                    if (parentNode.Nodes.Count < 2)
                    {
                        parentNode.Remove();
                    }

                    UpdateNumberOfNonUniqueHashes();
                }
            }
            else
            {
                ResetPreviewWindows();
            }
        }

        #endregion

        #region Button Events

        private void btnRootDir_Click(object sender, EventArgs e)
        {
            var folderBrowser = folderBrowserDialog1.ShowDialog();

            if (folderBrowser == DialogResult.OK)
            {
                txtRootDir.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (!fileSearchThread.Active && !updateLabelThread.Active) // make sure nothing is already running
            {
                btnRootDir.Enabled = false;
                btnSearch.Enabled = false;
                btnStopSearch.Enabled = true;
                btnDelete.Enabled = false;

                toolStripCurrentStatus.Text = "In Progress";

                ResetForm();

                fileSearchThread.RootDirectory = txtRootDir.Text;
                fileSearchThread.Active = true;
                updateLabelThread.Active = true;

                Thread searchThread = new Thread(new ParameterizedThreadStart(SearchThread))
                {
                    IsBackground = true
                };

                searchThread.Start(fileSearchThread);

                Thread labelUpdateThread = new Thread(new ParameterizedThreadStart(UiUpdateThread))
                {
                    IsBackground = true
                };

                labelUpdateThread.Start(updateLabelThread);

                runTime = 0;
                timer1.Start();
            }
        }

        private void btnStopSearch_Click(object sender, EventArgs e)
        {
            updateLabelThread.Active = false;
            fileSearchThread.Active = false;

            timer1.Stop();
            toolStripRunTime.Text = string.Empty;

            btnRootDir.Enabled = true;
            btnSearch.Enabled = true;
            btnStopSearch.Enabled = false;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DeleteSelection(treeResults);
        }

        #endregion

        #region TreeView Events

        private void treeResults_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            try
            {
                // Look for a file extension.
                if (e.Node.Text.Contains("."))
                {
                    System.Diagnostics.Process.Start(e.Node.Text);
                }
            }
            // If the file is not found, handle the exception and inform the user.
            catch (System.ComponentModel.Win32Exception)
            {
                MessageBox.Show("File not found.");
            }
        }

        private void treeResults_KeyDown(object sender, KeyEventArgs e)
        {
            TreeView treeView = sender as TreeView;

            try
            {
                if (e.KeyCode == Keys.Delete)
                {
                    DeleteSelection(treeView);
                }
                else if (e.KeyCode == Keys.Enter)
                {
                    try
                    {
                        // Look for a file extension.
                        if (treeView.SelectedNode.Text.Contains("."))
                        {
                            System.Diagnostics.Process.Start(treeView.SelectedNode.Text);
                        }
                    }
                    // If the file is not found, handle the exception and inform the user.
                    catch (System.ComponentModel.Win32Exception)
                    {
                        MessageBox.Show("File not found.");
                    }
                }
            }
            catch (System.ComponentModel.Win32Exception)
            {
                MessageBox.Show("Error attempting to delete file.");
            }
        }

        private void treeResults_AfterSelect(object sender, TreeViewEventArgs e)
        {
            ResetPreviewWindows();

            if (e.Node.IsSelected) // sanity test
            {
                if (e.Node.Text.Contains("."))
                {
                    btnDelete.Enabled = true;
                }
                else
                {
                    btnDelete.Enabled = false;
                }

                if (pictureBoxFileExtensions.Any(extension => e.Node.Text.EndsWith(extension, StringComparison.OrdinalIgnoreCase)))
                {
                    pictureBox1.Visible = true;
                    pictureBox1.ImageLocation = e.Node.Text;
                }
                else if (mediaPlayerFileExtensions.Any(extension => e.Node.Text.EndsWith(extension, StringComparison.OrdinalIgnoreCase)))
                {
                    axWindowsMediaPlayer1.Visible = true;
                    axWindowsMediaPlayer1.URL = e.Node.Text;
                }
                else if (browserFileExtensions.Any(extension => e.Node.Text.EndsWith(extension, StringComparison.OrdinalIgnoreCase)))
                {
                    webBrowser1.Visible = true;
                    webBrowser1.Navigate(e.Node.Text);
                }
            }
        }

        #endregion

        #region Timer Events

        private void timer1_Tick(object sender, EventArgs e)
        {
            runTime++;
        }

        #endregion

        #region Thread Functions

        protected void SearchThread(object obj)
        {
            FileSearchThread searchThread = (FileSearchThread)obj;

            searchThread.RecursiveSearch();
        }

        protected void UiUpdateThread(object obj)
        {
            UpdateLabelThread uiUpdateThread = (UpdateLabelThread)obj;

            uiUpdateThread.UpdateStatus();
        }

        #endregion

        #region Delegate Methods

        public void UpdateLabel()
        {
            toolStripCurrentStatus.Text = fileSearchThread.CurrentFile;

            DisplayNumberOfTotalFiles();
            DisplayRuntime();

            updateLabelThread.Active = fileSearchThread.Active;
        }

        public void LabelReset()
        {
            toolStripCurrentStatus.Text = "Idle";

            btnRootDir.Enabled = true;
            btnSearch.Enabled = true;
            btnStopSearch.Enabled = false;

            timer1.Stop();

            foreach (var hashKeyValue in fileSearchThread.HashedFiles)
            {
                if (hashKeyValue.Value.Count > 1)
                {
                    TreeNode treeNode = new TreeNode(hashKeyValue.Key);

                    foreach(var hashEntry in hashKeyValue.Value)
                    {
                        treeNode.Nodes.Add(hashEntry.Name);
                    }

                    treeResults.Nodes.Add(treeNode);
                }
            }

            UpdateNumberOfNonUniqueHashes();
            DisplayNumberOfTotalFiles();
        }

        #endregion

        #region Menu Items

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Use Delete button to delete selected file.\nUse Enter to open currently selected file with its default application.\nDouble-click a file to also open it with the default application.", "Help", MessageBoxButtons.OK);
        }

        #endregion
    }
}
