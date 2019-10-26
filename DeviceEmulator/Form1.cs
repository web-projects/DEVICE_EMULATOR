using MockPipelines.NamedPipeline;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Device.Emulator
{
    public partial class Form1 : Form
    {
        [DllImport("user32.dll")]
        static extern bool HideCaret(IntPtr hWnd);
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);
        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        /********************************************************************************************************/
        // ATTRIBUTES SECTION
        /********************************************************************************************************/
        #region -- attributes --
        // Always on TOP
        private static readonly IntPtr HWND_TOPMOST = new IntPtr(-1);
        private const UInt32 SWP_NOSIZE = 0x0001;
        private const UInt32 SWP_NOMOVE = 0x0002;
        private const UInt32 TOPMOST_FLAGS = SWP_NOMOVE | SWP_NOSIZE;
        // Main Form Reposition
        public const int HT_CAPTION = 0x2;
        public const int WM_NCLBUTTONDOWN = 0xA1;

        private EmulatorForm emulator;
        private bool statusidle = true;
        private string getPINCode  = "{{ \"DALActionRequest\": {{ \"DeviceUIRequest\": {{ \"UIAction\": \"InputRequest\", \"EntryType\": \"PIN\", \"MinLength\": \"4\", \"MaxLength\": \"4\", \"AlphaNumeric\": \"false\", \"ReportCardPresented\": \"true\", \"DisplayText\": [\"{0}\"] }} }} }}";
        private string getZipCode  = "{{ \"DALActionRequest\": {{ \"DeviceUIRequest\": {{ \"UIAction\": \"InputRequest\", \"EntryType\": \"ZIP\", \"MinLength\": \"5\", \"MaxLength\": \"5\", \"AlphaNumeric\": \"false\", \"ReportCardPresented\": \"true\", \"DisplayText\": [\"{0}\"] }} }} }}";
        private string displayText = "{{ \"DALActionRequest\": {{ \"DeviceUIRequest\": {{ \"UIAction\": \"Display\", \"DisplayText\": [\"{0}\"] }} }} }}";

        public EmulatorForm.SetEmulatorScreenKeyEntry SetEmulatorScreenKeyEntryCallback;
        public EmulatorForm.SetEmulatorScreenMessage  SetEmulatorScreenMessageCallback;

        private readonly string _pipeName = "TC_DEVICE_EMULATOR_PIPELINE";
        private ClientPipeline _clientpipe;
        private bool connected;

        #endregion

        /********************************************************************************************************/
        // CONSTRUCTION SECTION
        /********************************************************************************************************/
        #region -- construction --

        public Form1()
        {
            InitializeComponent();

            emulator = new EmulatorForm();
            emulator.EmulatorButtonOKClick += (sender, e) => this.lblFromEmulator.Invoke((MethodInvoker)(() => this.lblFromEmulator.Text = e.Message));
            this.SetEmulatorScreenKeyEntryCallback += new EmulatorForm.SetEmulatorScreenKeyEntry(emulator.SetEmulatorScreenKeyEntryCallbackFn);
            this.SetEmulatorScreenMessageCallback += new EmulatorForm.SetEmulatorScreenMessage(emulator.SetEmulatorScreenMessageCallbackFn);
        }

        private void OnFormLoad(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.None;
            this.MinimizeBox = false;
            this.MaximizeBox = false;
            SetWindowPos(this.Handle, HWND_TOPMOST, 0, 0, 0, 0, TOPMOST_FLAGS);

            Rectangle rect = Screen.FromControl(this).Bounds;
            this.Location = new Point(rect.Width / 2, rect.Height / 2);

            // Emulator
            emulator.Location = new Point(this.Left, this.Top - emulator.Height);
            emulator.Show();

            StartClientPipeline();
        }
        #endregion

        /********************************************************************************************************/
        // IMPLEMENTATION SECTION
        /********************************************************************************************************/
        #region -- IMPLEMENTATION --

        private void StartClientPipeline()
        {
            //TODO: Allow Server-Side GUID
            //string _pipeName = Clipboard.GetText();
            Debug.WriteLine($"client: pipeline started with GUID=[{_pipeName}]");
            _clientpipe = new ClientPipeline(string.IsNullOrEmpty(_pipeName) ? Guid.NewGuid().ToString() : _pipeName);
            _clientpipe.ClientPipeMessage += (sender, e) => this.lblFromServer.Invoke((MethodInvoker)(() => this.lblFromServer.Text = e.Message));
 
            new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = true;
                Debug.WriteLine("client: pipe started! +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++\n");

                _clientpipe.Start();
                _clientpipe.SendMessage("CLIENT CONNECTED");

                this.Invoke(new MethodInvoker(() =>
                {
                    this.button2.Enabled = false;
                    this.button3.Enabled = false;
                    this.button4.Enabled = false;
                    this.button5.Enabled = false;
                    this.lblFromServer.Text = "WAITING FOR SERVER REQUEST...";
                }));

                connected = true;
                while (connected)
                {
                    Thread.Sleep(100);
                }

            }).Start();
        }

        private void OnLabelFromServerChanged(object sender, EventArgs e)
        {
            statusidle = false;
            string lblText = "REQUEST:";

            switch (this.lblFromServer.Text)
            {
                case "Insert Card":
                {
                    this.button2.Enabled = true;
                    this.lblFromEmulator.Text = string.Empty;
                    SetEmulatorScreenMessageCallback(string.Format(displayText, "Insert card"));
                    break;
                }

                case "Remove Card":
                {
                    this.button3.Enabled = true;
                    this.lblFromEmulator.Text = string.Empty;
                    SetEmulatorScreenMessageCallback(string.Format(displayText, "Remove card"));
                    break;
                }

                case "Enter Zip Code":
                {
                    this.button4.Enabled = true;
                    SetEmulatorScreenKeyEntryCallback(string.Format(getZipCode, "Enter ZIP"));
                    break;
                }

                case "Enter PIN":
                {
                    this.button5.Enabled = true;
                    SetEmulatorScreenKeyEntryCallback(string.Format(getPINCode, "Enter PIN"));
                    break;
                }

                case "WELCOME":
                case "STATUS: APPROVED":
                {
                    SetEmulatorScreenMessageCallback(string.Format(displayText, this.lblFromServer.Text));
                    this.lblFromEmulator.Text = string.Empty;
                    break;
                }

                default:
                {
                    lblText = "REPLY:";
                    statusidle = true;
                    break;
                }
            }
            this.lblServerRequest.Text = lblText;
            emulator.SetEmulatorStatus = (statusidle ? "Device Idle" : "Device Busy");
        }

        private void OnLabelFromEmulatorChanged(object sender, EventArgs e)
        {
            if(this.button4.Enabled && this.lblFromEmulator.Text != string.Empty)
            {
                if (_clientpipe != null)
                {
                    _clientpipe.SendMessage(this.lblFromEmulator.Text);
                    this.lblFromServer.Text = this.lblFromEmulator.Text;
                    SetEmulatorScreenMessageCallback(string.Format(displayText, "Processing..."));
                }
                this.button4.Enabled = false;
            }
            else if (this.button5.Enabled && this.lblFromEmulator.Text != string.Empty)
            {
                if (_clientpipe != null)
                {
                    _clientpipe.SendMessage(this.lblFromEmulator.Text);
                    this.lblFromServer.Text = this.lblFromEmulator.Text;
                    SetEmulatorScreenMessageCallback(string.Format(displayText, "Processing..."));
                }
                this.button5.Enabled = false;
            }
        }

        private void OnLocationChanged(object sender, EventArgs e)
        {
            // Emulator
            emulator.Location = new Point(this.Left, this.Top - emulator.Height);
        }

        private void OnMouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        #endregion

        /********************************************************************************************************/
        // BUTTONS ACTIONS
        /********************************************************************************************************/
        #region -- buttons actions --

        private void button1_Click(object sender, EventArgs e)
        {
            if (_clientpipe != null)
            {
                _clientpipe.SendMessage("CLIENT EXITING...");
                _clientpipe.Stop();
                connected = false;
            }

            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (_clientpipe != null)
            {
                _clientpipe.SendMessage("Card Inserted");
                this.lblFromServer.Text = "1234 5678 9090 1212";
                SetEmulatorScreenMessageCallback(string.Format(displayText, "Processing..."));
            }
            this.button2.Enabled = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (_clientpipe != null)
            {
                _clientpipe.SendMessage("Card Removed");
                this.lblFromServer.Text = "**** **** **** ****";
                SetEmulatorScreenMessageCallback(string.Format(displayText, "Processing..."));
            }
            this.button3.Enabled = false;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //if (_clientpipe != null)
            //{
            //    _clientpipe.SendMessage("1234");
            //    this.lblFromServer.Text = "1234";
            //}
            //this.button4.Enabled = false;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //if (_clientpipe != null)
            //{
            //    _clientpipe.SendMessage("****");
            //    this.lblFromServer.Text = "****";
            //}
            //this.button5.Enabled = false;
        }

        #endregion
    }
}
