namespace MyKeyChanger {
    partial class MainForm {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.cNotify = new System.Windows.Forms.NotifyIcon(this.components);
            this.cTaskTrayMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cMenu_Start = new System.Windows.Forms.ToolStripMenuItem();
            this.cMenu_Stop = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.cMenu_Exit = new System.Windows.Forms.ToolStripMenuItem();
            this.cStart = new System.Windows.Forms.RadioButton();
            this.cStop = new System.Windows.Forms.RadioButton();
            this.cExit = new System.Windows.Forms.Button();
            this.cTaskTrayMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // cNotify
            // 
            this.cNotify.ContextMenuStrip = this.cTaskTrayMenu;
            this.cNotify.Icon = ((System.Drawing.Icon)(resources.GetObject("cNotify.Icon")));
            this.cNotify.Text = "MyKeyChanger";
            this.cNotify.Visible = true;
            // 
            // cTaskTrayMenu
            // 
            this.cTaskTrayMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cMenu_Start,
            this.cMenu_Stop,
            this.toolStripSeparator1,
            this.cMenu_Exit});
            this.cTaskTrayMenu.Name = "cTaskTrayMenu";
            this.cTaskTrayMenu.Size = new System.Drawing.Size(99, 76);
            // 
            // cMenu_Start
            // 
            this.cMenu_Start.Name = "cMenu_Start";
            this.cMenu_Start.Size = new System.Drawing.Size(98, 22);
            this.cMenu_Start.Text = "Start";
            this.cMenu_Start.Click += new System.EventHandler(this.cMenu_Start_Click);
            // 
            // cMenu_Stop
            // 
            this.cMenu_Stop.Name = "cMenu_Stop";
            this.cMenu_Stop.Size = new System.Drawing.Size(98, 22);
            this.cMenu_Stop.Text = "Stop";
            this.cMenu_Stop.Click += new System.EventHandler(this.cMenu_Stop_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(95, 6);
            // 
            // cMenu_Exit
            // 
            this.cMenu_Exit.Name = "cMenu_Exit";
            this.cMenu_Exit.Size = new System.Drawing.Size(98, 22);
            this.cMenu_Exit.Text = "Exit";
            this.cMenu_Exit.Click += new System.EventHandler(this.cMenu_Exit_Click);
            // 
            // cStart
            // 
            this.cStart.Appearance = System.Windows.Forms.Appearance.Button;
            this.cStart.Checked = true;
            this.cStart.Location = new System.Drawing.Point(12, 12);
            this.cStart.Name = "cStart";
            this.cStart.Size = new System.Drawing.Size(177, 43);
            this.cStart.TabIndex = 1;
            this.cStart.TabStop = true;
            this.cStart.Text = "Start";
            this.cStart.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.cStart.UseVisualStyleBackColor = true;
            this.cStart.CheckedChanged += new System.EventHandler(this.cStart_CheckedChanged);
            // 
            // cStop
            // 
            this.cStop.Appearance = System.Windows.Forms.Appearance.Button;
            this.cStop.Location = new System.Drawing.Point(12, 75);
            this.cStop.Name = "cStop";
            this.cStop.Size = new System.Drawing.Size(177, 43);
            this.cStop.TabIndex = 2;
            this.cStop.Text = "Stop";
            this.cStop.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.cStop.UseVisualStyleBackColor = true;
            this.cStop.CheckedChanged += new System.EventHandler(this.cStop_CheckedChanged);
            // 
            // cExit
            // 
            this.cExit.Location = new System.Drawing.Point(12, 181);
            this.cExit.Name = "cExit";
            this.cExit.Size = new System.Drawing.Size(177, 31);
            this.cExit.TabIndex = 3;
            this.cExit.Text = "Exit";
            this.cExit.UseVisualStyleBackColor = true;
            this.cExit.Click += new System.EventHandler(this.cExit_Click);
            // 
            // MainForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(205, 228);
            this.Controls.Add(this.cExit);
            this.Controls.Add(this.cStop);
            this.Controls.Add(this.cStart);
            this.Font = new System.Drawing.Font("Meiryo UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.ShowInTaskbar = false;
            this.Text = "KeyChanger";
            this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.cTaskTrayMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.NotifyIcon cNotify;
        private System.Windows.Forms.RadioButton cStart;
        private System.Windows.Forms.RadioButton cStop;
        private System.Windows.Forms.Button cExit;
        private System.Windows.Forms.ContextMenuStrip cTaskTrayMenu;
        private System.Windows.Forms.ToolStripMenuItem cMenu_Start;
        private System.Windows.Forms.ToolStripMenuItem cMenu_Stop;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem cMenu_Exit;
    }
}

