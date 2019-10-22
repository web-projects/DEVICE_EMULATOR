using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
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
        #region -- attributes section --
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

        #endregion

        public Form1()
        {
            InitializeComponent();

            emulator = new EmulatorForm();
            emulator.EmulatorButtonOKClick += (sender, e) => this.lblFromEmulator.Text = e.Message;
            this.SetEmulatorScreenKeyEntryCallback += new EmulatorForm.SetEmulatorScreenKeyEntry(emulator.SetEmulatorScreenKeyEntryCallbackFn);
            this.SetEmulatorScreenMessageCallback += new EmulatorForm.SetEmulatorScreenMessage(emulator.SetEmulatorScreenMessageCallbackFn);
        }

        /********************************************************************************************************/
        // ATTRIBUTES SECTION
        /********************************************************************************************************/
        #region -- events --

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
        // ATTRIBUTES SECTION
        /********************************************************************************************************/
        #region -- generic buttons --

        public void button1_Click(object sender, EventArgs e)
        {
            SetEmulatorScreenKeyEntryCallback(string.Format(getZipCode, this.button1.Text));
            this.lblFromEmulator.Text = string.Empty;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SetEmulatorScreenKeyEntryCallback(string.Format(getPINCode, this.button2.Text));
            this.lblFromEmulator.Text = string.Empty;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            statusidle = !statusidle;
            emulator.SetEmulatorStatus = (statusidle ? "Device Idle" : "Device Busy");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            SetEmulatorScreenMessageCallback(string.Format(displayText, "Insert card"));
        }

        #endregion
    }
}
