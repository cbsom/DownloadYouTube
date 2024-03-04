
namespace DownloadYouTube
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            cbAudioOnly = new System.Windows.Forms.CheckBox();
            label1 = new System.Windows.Forms.Label();
            txtUrl = new System.Windows.Forms.TextBox();
            btnGo = new System.Windows.Forms.Button();
            label2 = new System.Windows.Forms.Label();
            txtSaveIn = new System.Windows.Forms.TextBox();
            listView1 = new System.Windows.Forms.ListView();
            lblVersion = new System.Windows.Forms.Label();
            toolTip1 = new System.Windows.Forms.ToolTip(components);
            SuspendLayout();
            // 
            // cbAudioOnly
            // 
            cbAudioOnly.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            cbAudioOnly.AutoSize = true;
            cbAudioOnly.ForeColor = System.Drawing.Color.FromArgb(224, 224, 224);
            cbAudioOnly.Location = new System.Drawing.Point(853, 26);
            cbAudioOnly.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            cbAudioOnly.Name = "cbAudioOnly";
            cbAudioOnly.Size = new System.Drawing.Size(84, 24);
            cbAudioOnly.TabIndex = 0;
            cbAudioOnly.Text = "Audio only";
            cbAudioOnly.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(25, 27);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(34, 20);
            label1.TabIndex = 1;
            label1.Text = "URL";
            // 
            // txtUrl
            // 
            txtUrl.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            txtUrl.BackColor = System.Drawing.Color.AliceBlue;
            txtUrl.Font = new System.Drawing.Font("Arial Narrow", 10F);
            txtUrl.ForeColor = System.Drawing.Color.RoyalBlue;
            txtUrl.Location = new System.Drawing.Point(25, 52);
            txtUrl.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            txtUrl.Name = "txtUrl";
            txtUrl.Size = new System.Drawing.Size(912, 23);
            txtUrl.TabIndex = 2;
            // 
            // btnGo
            // 
            btnGo.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            btnGo.BackColor = System.Drawing.Color.AliceBlue;
            btnGo.ForeColor = System.Drawing.Color.RoyalBlue;
            btnGo.Location = new System.Drawing.Point(800, 98);
            btnGo.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            btnGo.Name = "btnGo";
            btnGo.Size = new System.Drawing.Size(137, 36);
            btnGo.TabIndex = 3;
            btnGo.Text = "Start Download";
            btnGo.UseVisualStyleBackColor = false;
            btnGo.Click += btnGo_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(25, 81);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(49, 20);
            label2.TabIndex = 1;
            label2.Text = "Save In";
            // 
            // txtSaveIn
            // 
            txtSaveIn.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            txtSaveIn.BackColor = System.Drawing.Color.AliceBlue;
            txtSaveIn.ForeColor = System.Drawing.Color.SlateGray;
            txtSaveIn.Location = new System.Drawing.Point(25, 103);
            txtSaveIn.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            txtSaveIn.Name = "txtSaveIn";
            txtSaveIn.Size = new System.Drawing.Size(639, 25);
            txtSaveIn.TabIndex = 2;
            // 
            // listView1
            // 
            listView1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            listView1.BackColor = System.Drawing.Color.Black;
            listView1.Font = new System.Drawing.Font("Lucida Console", 11.25F);
            listView1.ForeColor = System.Drawing.Color.White;
            listView1.Location = new System.Drawing.Point(25, 159);
            listView1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            listView1.Name = "listView1";
            listView1.Size = new System.Drawing.Size(916, 435);
            listView1.TabIndex = 4;
            listView1.UseCompatibleStateImageBehavior = false;
            listView1.View = System.Windows.Forms.View.List;
            // 
            // lblVersion
            // 
            lblVersion.Anchor = System.Windows.Forms.AnchorStyles.Top;
            lblVersion.AutoSize = true;
            lblVersion.Cursor = System.Windows.Forms.Cursors.Hand;
            lblVersion.ForeColor = System.Drawing.Color.CornflowerBlue;
            lblVersion.Location = new System.Drawing.Point(373, 9);
            lblVersion.Name = "lblVersion";
            lblVersion.Size = new System.Drawing.Size(220, 20);
            lblVersion.TabIndex = 5;
            lblVersion.Text = "Using  _yt_dlp_FileName version 1.0.0";
            toolTip1.SetToolTip(lblVersion, "Click to update...");
            lblVersion.Click += lblVersion_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 20F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.Color.FromArgb(64, 64, 64);
            ClientSize = new System.Drawing.Size(966, 607);
            Controls.Add(lblVersion);
            Controls.Add(listView1);
            Controls.Add(btnGo);
            Controls.Add(txtSaveIn);
            Controls.Add(label2);
            Controls.Add(txtUrl);
            Controls.Add(label1);
            Controls.Add(cbAudioOnly);
            Font = new System.Drawing.Font("Arial Narrow", 11.25F);
            ForeColor = System.Drawing.Color.FromArgb(224, 224, 224);
            Name = "Form1";
            Text = "Download YT";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.CheckBox cbAudioOnly;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtUrl;
        private System.Windows.Forms.Button btnGo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtSaveIn;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.Label lblVersion;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}

