using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace DownloadYouTube
{
    public partial class Form1 : Form
    {
        private Process _downloadingProcess;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.txtSaveIn.Text = Properties.Settings.Default.SaveInPath;
        }

        private void btnGo_Click(object sender, EventArgs e)
        {
            if (this._downloadingProcess is { HasExited: false })
            {
                var process = this._downloadingProcess;
                this._downloadingProcess = null;
                process.Kill(true);

                this.ShowOutput("THE DOWNLOAD PROCESS HAS BEEN STOPPED.",
                    Color.OrangeRed);
                this.btnGo.Text = "Download";
                this.btnGo.ForeColor = Color.SteelBlue;
            }
            else
            {
                this.listView1.Items.Clear();
                this.btnGo.Text = "Stop";
                this.btnGo.ForeColor = Color.Red;
                this.DownloadUrl();
            }
        }

        private void DownloadUrl()
        {
            _downloadingProcess = new Process
            {
                StartInfo = { FileName = "youtube-dl.exe" }
            };
            if (this.cbAudioOnly.Checked)
            {
                _downloadingProcess.StartInfo.ArgumentList.Add("-x");
                _downloadingProcess.StartInfo.ArgumentList.Add("--audio-format");
                _downloadingProcess.StartInfo.ArgumentList.Add("mp3");
                _downloadingProcess.StartInfo.ArgumentList.Add("--audio-quality");
                _downloadingProcess.StartInfo.ArgumentList.Add("0");
            }
            _downloadingProcess.StartInfo.ArgumentList.Add(this.txtUrl.Text);
            _downloadingProcess.StartInfo.CreateNoWindow = true;
            _downloadingProcess.EnableRaisingEvents = true;
            _downloadingProcess.StartInfo.UseShellExecute = false;
            _downloadingProcess.StartInfo.RedirectStandardOutput = true;
            _downloadingProcess.StartInfo.RedirectStandardError = true;
            _downloadingProcess.OutputDataReceived += delegate (object sender, DataReceivedEventArgs e)
            {
                this.Invoke(new Action(() => this.ShowOutput(e.Data, Color.LimeGreen)));
            };
            _downloadingProcess.ErrorDataReceived += delegate (object sender, DataReceivedEventArgs e)
            {
                this.Invoke(new Action(() => this.ShowOutput(e.Data, Color.Red)));
            };
            _downloadingProcess.Exited += delegate
            {
                if (_downloadingProcess != null)
                {
                    this.Invoke(new Action(() =>
                    {
                        this.btnGo.Text = "Download";
                        this.btnGo.ForeColor = Color.SteelBlue;
                        if (_downloadingProcess.ExitCode == 0)
                        {
                            this.CopyDownloadedFile();
                        }
                    }));

                    _downloadingProcess.Dispose();
                    _downloadingProcess = null;
                }
            };
            _downloadingProcess.Start();
            _downloadingProcess.BeginOutputReadLine();
            _downloadingProcess.BeginErrorReadLine();
        }

        private void CopyDownloadedFile()
        {
            //First we need the file name
            var p = new Process
            {
                StartInfo = { FileName = "youtube-dl.exe" }
            };
            p.StartInfo.ArgumentList.Add("--get-filename");
            p.StartInfo.ArgumentList.Add(this.txtUrl.Text);
            p.StartInfo.CreateNoWindow = true;
            p.EnableRaisingEvents = true;
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardOutput = true;
            p.OutputDataReceived += delegate (object sender, DataReceivedEventArgs e)
            {
                this.Invoke(new Action(() => this.SaveFile(e.Data)));
            };
            p.Start();
            p.BeginOutputReadLine();
        }

        private void SaveFile(string data)
        {
            if (string.IsNullOrEmpty(data))
            {
                return;
            }
            var filePath = Path.Combine(Environment.CurrentDirectory, data);
            if (this.cbAudioOnly.Checked)
            {
                filePath = Path.Combine(Environment.CurrentDirectory,
                    Path.GetFileNameWithoutExtension(data) + ".mp3");
            }
            if (!File.Exists(filePath))
            {
                this.ShowOutput($"[local error]  \"{filePath}\" does not exist.",
                    Color.Red);
                return;
            }

            if (!string.IsNullOrWhiteSpace(this.txtSaveIn.Text))
            {
                Properties.Settings.Default.SaveInPath = this.txtSaveIn.Text;
                Properties.Settings.Default.Save();

                string newPath = Path.Combine(this.txtSaveIn.Text, Path.GetFileName(filePath));
                if (File.Exists(newPath) &&
                    MessageBox.Show($"The file {newPath} already exists.\nOverwrite it?",
                                  "Save File - Overwrite",
                                  MessageBoxButtons.YesNo,
                                  MessageBoxIcon.Question,
                                  MessageBoxDefaultButton.Button2) != DialogResult.Yes)
                {
                    return;
                }
                File.Move(filePath, newPath);
                ShowOutput($"THE FILE \"{newPath}\" HAS BEEN SUCCESSFULLY CREATED...........",
                    Color.LightGreen);
                if (File.Exists(newPath) && File.Exists(filePath))
                {
                    File.Delete(newPath);
                }
            }
            else
            {
                var sfd = new SaveFileDialog()
                {
                    FileName = Path.GetFileName(filePath),
                    InitialDirectory = Environment.CurrentDirectory,
                    OverwritePrompt = true
                };
                if (sfd.ShowDialog() != DialogResult.OK)
                    return;
                if (File.Exists(sfd.FileName))
                {
                    File.Delete(sfd.FileName);
                }
                File.Move(filePath, sfd.FileName);
                ShowOutput($"THE FILE \"{sfd.FileName}\" HAS BEEN SUCCESSFULLY CREATED...........",
                    Color.LightGreen);
                if (File.Exists(filePath) && File.Exists(sfd.FileName))
                {
                    File.Delete(filePath);
                }
            }
        }

        private void ShowOutput(string data, Color color)
        {
            ListViewItem tb = null;
            if (!string.IsNullOrEmpty(data) &&
                (data.StartsWith('\r') || data.StartsWith("[download]")))
            {
                var el = this.listView1.Items[^1];
                if (el.Text.StartsWith("[download]"))
                {
                    tb = el;
                }
            }
            if (tb == null)
            {
                tb = new ListViewItem
                {
                    ForeColor = color
                };
                this.listView1.Items.Add(tb);
            }
            tb.Text = data;
            this.listView1.Items[^1].EnsureVisible();
        }
    }
}
