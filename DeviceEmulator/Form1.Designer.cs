namespace Device.Emulator
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblFromServer = new System.Windows.Forms.Label();
            this.lblFromEmulator = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.lblServerRequest = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblFromServer
            // 
            this.lblFromServer.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblFromServer.Location = new System.Drawing.Point(86, 5);
            this.lblFromServer.Name = "lblFromServer";
            this.lblFromServer.Size = new System.Drawing.Size(355, 13);
            this.lblFromServer.TabIndex = 0;
            this.lblFromServer.Text = "DATA";
            this.lblFromServer.TextChanged += new System.EventHandler(this.OnLabelFromServerChanged);
            // 
            // lblFromEmulator
            // 
            this.lblFromEmulator.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblFromEmulator.Location = new System.Drawing.Point(13, 21);
            this.lblFromEmulator.Name = "lblFromEmulator";
            this.lblFromEmulator.Size = new System.Drawing.Size(425, 24);
            this.lblFromEmulator.TabIndex = 1;
            this.lblFromEmulator.TextChanged += new System.EventHandler(this.OnLabelFromEmulatorChanged);
            // 
            // button2
            // 
            this.button2.Enabled = false;
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.Location = new System.Drawing.Point(14, 48);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(53, 32);
            this.button2.TabIndex = 2;
            this.button2.Text = "Insert Card";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Enabled = false;
            this.button3.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button3.Location = new System.Drawing.Point(75, 48);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(53, 32);
            this.button3.TabIndex = 3;
            this.button3.Text = "Remove Card";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button5
            // 
            this.button5.Enabled = false;
            this.button5.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button5.Location = new System.Drawing.Point(193, 48);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(53, 32);
            this.button5.TabIndex = 4;
            this.button5.Text = "Enter PIN";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Visible = false;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button4
            // 
            this.button4.Enabled = false;
            this.button4.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button4.Location = new System.Drawing.Point(134, 48);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(53, 32);
            this.button4.TabIndex = 5;
            this.button4.Text = "Enter ZIP";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Visible = false;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(385, 48);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(53, 32);
            this.button1.TabIndex = 6;
            this.button1.Text = "Exit";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // lblServerRequest
            // 
            this.lblServerRequest.Location = new System.Drawing.Point(15, 5);
            this.lblServerRequest.Name = "lblServerRequest";
            this.lblServerRequest.Size = new System.Drawing.Size(62, 13);
            this.lblServerRequest.TabIndex = 7;
            this.lblServerRequest.Text = "REQUEST:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(450, 81);
            this.Controls.Add(this.lblServerRequest);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.lblFromEmulator);
            this.Controls.Add(this.lblFromServer);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "DEVICE EMULATOR";
            this.Load += new System.EventHandler(this.OnFormLoad);
            this.LocationChanged += new System.EventHandler(this.OnLocationChanged);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.OnMouseDown);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblFromServer;
        private System.Windows.Forms.Label lblFromEmulator;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label lblServerRequest;
    }
}

