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
        private bool singleKey;

        public event EventHandler<MessageEventArgs> EmulatorButtonOKClick;

        public delegate void SetEmulatorScreenKeyEntry(string message, bool terminate = true);
        public delegate void SetEmulatorScreenMessage(string message);

        public string SetEmulatorStatus
        {
            get { return this.lblStatus.Text; }
            set { this.lblStatus.Text = value; }
        }

        private string keysPressed;

        #endregion

        /********************************************************************************************************/
        // CONSTRUCTION SECTION
        /********************************************************************************************************/
        #region -- construction --

        public EmulatorForm()
        {
            InitializeComponent();
        }

        private void OnMouseDown(object sender, MouseEventArgs e)
        {

        }

        private void ResetAll()
        {
            keysPressed = string.Empty;
            collectKeys = false;
            pinMode     = false;
            singleKey    = false;
            SetKeyEnabled(true);
        }

        public void SetEmulatorScreenKeyEntryCallbackFn(string message, bool terminate)
        {
            string value = System.Text.RegularExpressions.Regex.Replace(message.Trim('\"'), "[\\\\]+", string.Empty);
            DalActionRequestRoot request = JsonConvert.DeserializeObject<DalActionRequestRoot>(value);
            if (request != null)
            {
                tbLCD.Text = request.DALActionRequest.DeviceUIRequest.DisplayText[0] + (terminate ? ":\r\n" : "");
                keysPressed = string.Empty;
                if (request.DALActionRequest.DeviceUIRequest.DisplayText[0].IndexOf("PIN", StringComparison.InvariantCultureIgnoreCase) >= 0)
                {
                    pinMode = true;
                    collectKeys = true;
                }
                else if (request.DALActionRequest.DeviceUIRequest.DisplayText[0].IndexOf("ZIP", StringComparison.InvariantCultureIgnoreCase) >= 0)
                {
                    pinMode = false;
                    collectKeys = true;
                }
                else if (request.DALActionRequest.DeviceUIRequest.DisplayText[0].IndexOf("Enter Card Type", StringComparison.InvariantCultureIgnoreCase) >= 0)
                {
                    collectKeys = true;
                    singleKey = true;
                }
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
                if (request.DALActionRequest.DeviceUIRequest.DisplayText[0].IndexOf("PIN", StringComparison.InvariantCultureIgnoreCase) >= 0)
                {
                    pinMode = true;
                    collectKeys = true;
                }
                else if (request.DALActionRequest.DeviceUIRequest.DisplayText[0].IndexOf("ZIP", StringComparison.InvariantCultureIgnoreCase) >= 0)
                {
                    pinMode = false;
                    collectKeys = true;
                }
            }
        }

        #endregion

        /********************************************************************************************************/
        // NUMERIC INPUT
        /********************************************************************************************************/
        #region -- numeric input --

        private void SetKeyEnabled(bool mode)
        {
            this.buttonInfo.Enabled = this.buttonCorrect.Enabled = this.buttonStop.Enabled =
            this.buttonUp.Enabled = this.buttonDown.Enabled =
            this.button1.Enabled = this.button2.Enabled = this.button3.Enabled = this.button4.Enabled =
            this.button5.Enabled = this.button6.Enabled = this.button7.Enabled = this.button8.Enabled =
            this.button9.Enabled = this.button0.Enabled = mode;
        }

        public void button1_Click(object sender, EventArgs e)
        {
            if (collectKeys)
            {
                keysPressed += this.button1.Text;
                if(singleKey)
                { 
                    collectKeys = false;
                    SetKeyEnabled(false);
                }
                else
                    this.tbLCD.Text += pinMode ? "*" : this.button1.Text;
            }
        }

        public void button2_Click(object sender, EventArgs e)
        {
            if (collectKeys)
            { 
                keysPressed += this.button2.Text;
                if (singleKey)
                {
                    collectKeys = false;
                    SetKeyEnabled(false);
                }
                else
                    this.tbLCD.Text += pinMode ? "*" : this.button2.Text;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (collectKeys)
            {
                keysPressed += this.button3.Text;
                if (singleKey)
                {
                    collectKeys = false;
                    SetKeyEnabled(false);
                }
                else
                    this.tbLCD.Text += pinMode ? "*" : this.button3.Text;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (collectKeys)
            {
                keysPressed += this.button4.Text;
                if (singleKey)
                {
                    collectKeys = false;
                    SetKeyEnabled(false);
                }
                else
                    this.tbLCD.Text += pinMode ? "*" : this.button4.Text;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (collectKeys)
            {
                keysPressed += this.button5.Text;
                if (singleKey)
                {
                    collectKeys = false;
                    SetKeyEnabled(false);
                }
                else
                    this.tbLCD.Text += pinMode ? "*" : this.button5.Text;
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (collectKeys)
            {
                keysPressed += this.button6.Text;
                if (singleKey)
                {
                    collectKeys = false;
                    SetKeyEnabled(false);
                }
                else
                    this.tbLCD.Text += pinMode ? "*" : this.button6.Text;
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (collectKeys)
            {
                keysPressed += this.button7.Text;
                if (singleKey)
                {
                    collectKeys = false;
                    SetKeyEnabled(false);
                }
                else
                    this.tbLCD.Text += pinMode ? "*" : this.button7.Text;
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (collectKeys)
            {
                keysPressed += this.button8.Text;
                if (singleKey)
                {
                    collectKeys = false;
                    SetKeyEnabled(false);
                }
                else
                    this.tbLCD.Text += pinMode ? "*" : this.button8.Text;
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (collectKeys)
            {
                keysPressed += this.button9.Text;
                if (singleKey)
                {
                    collectKeys = false;
                    SetKeyEnabled(false);
                }
                else
                    this.tbLCD.Text += pinMode ? "*" : this.button9.Text;
            }
        }

        private void button0_Click(object sender, EventArgs e)
        {
            if (collectKeys)
            {
                keysPressed += this.button0.Text;
                if (singleKey)
                {
                    collectKeys = false;
                    SetKeyEnabled(false);
                }
                else
                    this.tbLCD.Text += pinMode ? "*" : this.button0.Text;
            }
        }

        #endregion

        /********************************************************************************************************/
        // BUTTON ACTION
        /********************************************************************************************************/
        #region -- button action --

        private void OnLCDTextChanged(object sender, EventArgs e)
        {
            if(pinMode && keysPressed?.Length >= 4)
            {
                SetKeyEnabled(false);
            }
            else if (keysPressed?.Length >= 5)
            {
                SetKeyEnabled(false);
            }
        }

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
