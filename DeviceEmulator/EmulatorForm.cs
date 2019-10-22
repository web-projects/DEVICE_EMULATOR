using Device.Emulator.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.Runtime.InteropServices;

namespace Device.Emulator
{
    public partial class EmulatorForm : Form
    {
        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        /********************************************************************************************************/
        // ATTRIBUTES SECTION
        /********************************************************************************************************/
        #region -- attributes section --

        // Main Form Reposition
        public const int HT_CAPTION = 0x2;
        public const int WM_NCLBUTTONDOWN = 0xA1;

        private bool collectKeys;
        private bool pinMode;

        public event EventHandler<MessageEventArgs> EmulatorButtonOKClick;

        public delegate void SetEmulatorScreenKeyEntry(string message);
        public delegate void SetEmulatorScreenMessage(string message);

        public string SetEmulatorStatus
        {
            get { return this.lblStatus.Text; }
            set { this.lblStatus.Text = value; }
        }

        private string keysPressed;

        #endregion

        public EmulatorForm()
        {
            InitializeComponent();
        }

        private void OnMouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void ResetAll()
        {
            keysPressed = string.Empty;
            collectKeys = false;
            pinMode     = false;
        }

        public void SetEmulatorScreenKeyEntryCallbackFn(string message)
        {
            string value = System.Text.RegularExpressions.Regex.Replace(message.Trim('\"'), "[\\\\]+", string.Empty);
            DalActionRequestRoot request = JsonConvert.DeserializeObject<DalActionRequestRoot>(value);
            if (request != null)
            {
                tbLCD.Text = request.DALActionRequest.DeviceUIRequest.DisplayText[0] + ":\r\n";
                keysPressed = string.Empty;
                collectKeys = true;
                if (request.DALActionRequest.DeviceUIRequest.DisplayText[0].IndexOf("PIN", StringComparison.InvariantCultureIgnoreCase) >= 0)
                    pinMode = true;
            }
        }
        public void SetEmulatorScreenMessageCallbackFn(string message)
        {
            string value = System.Text.RegularExpressions.Regex.Replace(message.Trim('\"'), "[\\\\]+", string.Empty);
            DalActionRequestRoot request = JsonConvert.DeserializeObject<DalActionRequestRoot>(value);
            if (request != null)
            {
                tbLCD.Text = request.DALActionRequest.DeviceUIRequest.DisplayText[0] + "\r\n";
                keysPressed = string.Empty;
                collectKeys = true;
                if (request.DALActionRequest.DeviceUIRequest.DisplayText[0].IndexOf("PIN", StringComparison.InvariantCultureIgnoreCase) >= 0)
                    pinMode = true;
            }
        }

        /********************************************************************************************************/
        // ATTRIBUTES SECTION
        /********************************************************************************************************/
        #region -- numeric input --

        public void button1_Click(object sender, EventArgs e)
        {
            if (collectKeys)
            {
                keysPressed += this.button1.Text;
                this.tbLCD.Text += pinMode ? "*" : this.button1.Text;
            }
        }

        public void button2_Click(object sender, EventArgs e)
        {
            if (collectKeys)
            { 
                keysPressed += this.button2.Text;
                this.tbLCD.Text += pinMode ? "*" : this.button2.Text;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (collectKeys)
            {
                keysPressed += this.button3.Text;
                this.tbLCD.Text += pinMode ? "*" : this.button3.Text;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (collectKeys)
            {
                keysPressed += this.button4.Text;
                this.tbLCD.Text += pinMode ? "*" : this.button4.Text;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (collectKeys)
            {
                keysPressed += this.button5.Text;
                this.tbLCD.Text += pinMode ? "*" : this.button5.Text;
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (collectKeys)
            {
                keysPressed += this.button6.Text;
                this.tbLCD.Text += pinMode ? "*" : this.button6.Text;
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (collectKeys)
            {
                keysPressed += this.button7.Text;
                this.tbLCD.Text += pinMode ? "*" : this.button7.Text;
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (collectKeys)
            {
                keysPressed += this.button8.Text;
                this.tbLCD.Text += pinMode ? "*" : this.button8.Text;
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (collectKeys)
            {
                keysPressed += this.button9.Text;
                this.tbLCD.Text += pinMode ? "*" : this.button9.Text;
            }
        }

        private void button0_Click(object sender, EventArgs e)
        {
            if (collectKeys)
            {
                keysPressed += this.button0.Text;
                this.tbLCD.Text += pinMode ? "*" : this.button0.Text;
            }
        }

        #endregion

        /********************************************************************************************************/
        // ATTRIBUTES SECTION
        /********************************************************************************************************/
        #region -- generic buttons --

        private void buttonDown_Click(object sender, EventArgs e)
        {

        }

        private void buttonUp_Click(object sender, EventArgs e)
        {

        }

        private void buttonStop_Click(object sender, EventArgs e)
        {

        }

        private void buttonCorrect_Click(object sender, EventArgs e)
        {

        }

        private void buttonInfo_Click(object sender, EventArgs e)
        {

        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            if (keysPressed != string.Empty)
            {
                EmulatorButtonOKClick?.Invoke(this, new MessageEventArgs(keysPressed));
                ResetAll();
            }
        }

        #endregion
    }
}
