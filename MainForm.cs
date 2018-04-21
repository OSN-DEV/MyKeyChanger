using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyKeyChanger {
    public partial class MainForm : Form {

        #region Constructor
        public MainForm() {
            InitializeComponent();
        }
        #endregion

        #region Form Event
        private void MainForm_Load(object sender, EventArgs e) {
            this.cMenu_Start.Checked = true;
            this.cMenu_Stop.Checked = false;
            this.StartHook();
            this.cNotify.Visible = true;
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e) {
            this.StopHook();
            this.cNotify.Visible = false;
        }
        #endregion

        #region Control Event
        private void cStart_CheckedChanged(object sender, EventArgs e) {
            if (this.cStart.Checked) {
                this.StartHook();
            }
        }

        private void cStop_CheckedChanged(object sender, EventArgs e) {
            if(this.cStop.Checked) {
                this.StopHook();
            }
        }

        private void cExit_Click(object sender, EventArgs e) {
            Application.Exit();
        }

        private void cMenu_Start_Click(object sender, EventArgs e) {
            this.cMenu_Start.Checked = true;
            this.cMenu_Stop.Checked = false;
            this.cStart.PerformClick();
        }

        private void cMenu_Stop_Click(object sender, EventArgs e) {
            this.cMenu_Start.Checked = false;
            this.cMenu_Stop.Checked = true;
            this.cStop.PerformClick();
        }

        private void cMenu_Exit_Click(object sender, EventArgs e) {
            this.cExit.PerformClick();
        }
        #endregion

        #region Private Method
        /// <summary>
        /// 
        /// </summary>
        private void StartHook() {
            if (KeyboardGlobalHook.IsHooking) {
                return;
            }
            KeyboardGlobalHook.Start();
        }

        /// <summary>
        /// 
        /// </summary>
        private void StopHook() {
            if (!KeyboardGlobalHook.IsHooking) {
                return;
            }
            KeyboardGlobalHook.Stop();
        }



        #endregion


    }
}
