using System;
using System.Threading;
using System.Windows.Forms;
using Microsoft.VisualBasic.FileIO;

namespace DuplicateFinder
{
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

        public Form1()
        {
            InitializeComponent();

            updateStatusLabelDelegate = new UpdateStatusLabel(UpdateLabel);
            resetStatusLabelDelegate = new ResetStatusLabel(LabelReset);

            fileSearchThread = new FileSearchThread();
            updateLabelThread = new UpdateLabelThread(this);
        }

        #region Helper Functions

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

                toolStripCurrentStatus.Text = "In Progress";

                treeResults.Nodes.Clear();

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
            }
        }

        private void btnStopSearch_Click(object sender, EventArgs e)
        {
            updateLabelThread.Active = false;
            fileSearchThread.Active = false;

            btnRootDir.Enabled = true;
            btnSearch.Enabled = true;
            btnStopSearch.Enabled = false;
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
                    if (treeView.SelectedNode.Text.Contains("."))
                    {
                        if (MessageBox.Show("Are You Sure?", "Confirm", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
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

                                pictureBox1.ImageLocation = string.Empty;
                            }

                            UpdateNumberOfNonUniqueHashes();
                        }
                    }
                    else
                    {
                        pictureBox1.ImageLocation = string.Empty;
                    }
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
            if (e.Node.IsSelected) // sanity test
            {
                // compatible file types: *.gif, *.jpg, *.jpeg, *.bmp, *.wmf, *.png
                if ((e.Node.Text.EndsWith(".gif", StringComparison.OrdinalIgnoreCase)) || 
                    (e.Node.Text.EndsWith(".jpg", StringComparison.OrdinalIgnoreCase)) || 
                    (e.Node.Text.EndsWith(".jpeg", StringComparison.OrdinalIgnoreCase)) || 
                    (e.Node.Text.EndsWith(".bmp", StringComparison.OrdinalIgnoreCase)) || 
                    (e.Node.Text.EndsWith(".wmf", StringComparison.OrdinalIgnoreCase)) || 
                    (e.Node.Text.EndsWith(".png", StringComparison.OrdinalIgnoreCase)))
                {
                    pictureBox1.ImageLocation = e.Node.Text;
                }
                else
                {
                    // clear the picture box
                    pictureBox1.ImageLocation = string.Empty;
                }
            }
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

            updateLabelThread.Active = fileSearchThread.Active;
        }

        public void LabelReset()
        {
            toolStripCurrentStatus.Text = "Idle";

            btnRootDir.Enabled = true;
            btnSearch.Enabled = true;
            btnStopSearch.Enabled = false;

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
