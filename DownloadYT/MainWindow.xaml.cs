using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using Path = System.IO.Path;

namespace DownloadYT
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Process _downloadingProcess = null;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (this._downloadingProcess != null && !this._downloadingProcess.HasExited)
            {
                var process = this._downloadingProcess;
                this._downloadingProcess = null;
                process.Kill(true);

                this.ShowOutput("THE DOWNLOAD PROCESS HAS BEEN STOPPED.", Brushes.OrangeRed);
                this.btnGo.Content = "Download";
                this.btnGo.Foreground = this.Foreground;
            }
            else
            {
                this.stpOutput.Children.Clear();
                this.btnGo.Content = "Stop";
                this.btnGo.Foreground = Brushes.Red;
                this.DownloadURL();
            }
        }

        private void DownloadURL()
        {
            _downloadingProcess = new Process();
            _downloadingProcess.StartInfo.FileName = "youtube-dl.exe";
            if (this.cbAudioOnly.IsChecked.GetValueOrDefault())
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
                this.stpOutput.Dispatcher.Invoke(new Action(() => this.ShowOutput(e.Data)));
            };
            _downloadingProcess.ErrorDataReceived += delegate (object sender, DataReceivedEventArgs e)
            {
                this.stpOutput.Dispatcher.Invoke(new Action(() => this.ShowOutput(e.Data, Brushes.Red)));
            };
            _downloadingProcess.Exited += delegate
            {
                if (_downloadingProcess != null)
                {
                    this.stpOutput.Dispatcher.Invoke(new Action(() =>
                    {
                        this.btnGo.Content = "Download";
                        this.btnGo.Foreground = this.Foreground;
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
            var p = new Process();
            p.StartInfo.FileName = "youtube-dl.exe";
            p.StartInfo.ArgumentList.Add("--get-filename");
            p.StartInfo.ArgumentList.Add(this.txtUrl.Text);
            p.StartInfo.CreateNoWindow = true;
            p.EnableRaisingEvents = true;
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardOutput = true;
            p.OutputDataReceived += delegate (object sender, DataReceivedEventArgs e)
            {
                this.Dispatcher.Invoke(new Action(() => this.SaveFile(e.Data)));
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
            if (this.cbAudioOnly.IsChecked.GetValueOrDefault())
            {
                filePath = Path.Combine(Environment.CurrentDirectory,
                    Path.GetFileNameWithoutExtension(data) + ".mp3");
            }
            if (!File.Exists(filePath))
            {
                this.ShowOutput($"[local error]  \"{filePath}\" does not exist.", Brushes.Red);
                return;
            }
            SaveFileDialog sfd = new SaveFileDialog()
            {
                FileName = Path.GetFileName(filePath),
                InitialDirectory = Environment.CurrentDirectory,
                OverwritePrompt = true
            };
            if (sfd.ShowDialog().GetValueOrDefault())
            {
                if (File.Exists(sfd.FileName))
                {
                    File.Delete(sfd.FileName);
                }
                File.Move(filePath, sfd.FileName);
                ShowOutput($"THE FILE \"{sfd.FileName}\" HAS BEEN SUCCESSFULLY CREATED...........", Brushes.LightGreen);
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }
            }
        }

        private void ShowOutput(string data, Brush brush = null)
        {
            TextBlock tb = null;
            if (!string.IsNullOrEmpty(data) &&
                (data.StartsWith('\r') || data.StartsWith("[download]")))
            {
                var el = this.stpOutput.Children[^1];
                if (el is TextBlock block && block.Text.StartsWith("[download]"))
                {
                    tb = el as TextBlock;
                }
            }
            if (tb == null)
            {
                tb = new TextBlock();
                tb.Foreground = brush ?? Brushes.LightSteelBlue;
                this.stpOutput.Children.Add(tb);
            }
            tb.Text = data;
            this.scrollViewerOutput.ScrollToEnd();
        }
    }
}
