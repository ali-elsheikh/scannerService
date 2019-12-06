namespace ScannerDesktopUI
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.selectSource = new System.Windows.Forms.Button();
            this.useAdfCheckBox = new System.Windows.Forms.CheckBox();
            this.useUICheckBox = new System.Windows.Forms.CheckBox();
            this.saveButton = new System.Windows.Forms.Button();
            this.blackAndWhiteCheckBox = new System.Windows.Forms.CheckBox();
            this.widthLabel = new System.Windows.Forms.Label();
            this.heightLabel = new System.Windows.Forms.Label();
            this.diagnosticsButton = new System.Windows.Forms.Button();
            this.checkBoxArea = new System.Windows.Forms.CheckBox();
            this.showProgressIndicatorUICheckBox = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.useDuplexCheckBox = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.autoRotateCheckBox = new System.Windows.Forms.CheckBox();
            this.autoDetectBorderCheckBox = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.scan = new System.Windows.Forms.Button();
            this.btnRotateLeft = new System.Windows.Forms.Button();
            this.btnRotateRigth = new System.Windows.Forms.Button();
            this.btnRotateUp = new System.Windows.Forms.Button();
            this.chkGrey = new System.Windows.Forms.CheckBox();
            this.chkColor = new System.Windows.Forms.CheckBox();
            this.ddlResolution = new System.Windows.Forms.ComboBox();
            this.btnOriginalImage = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.ddlSize = new System.Windows.Forms.ComboBox();
            this.radioPdf = new System.Windows.Forms.RadioButton();
            this.radioTif = new System.Windows.Forms.RadioButton();
            this.lblCount = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // selectSource
            // 
            this.selectSource.Location = new System.Drawing.Point(12, 12);
            this.selectSource.Name = "selectSource";
            this.selectSource.Size = new System.Drawing.Size(85, 25);
            this.selectSource.TabIndex = 0;
            this.selectSource.Text = "Select Source";
            this.selectSource.UseVisualStyleBackColor = true;
            this.selectSource.Click += new System.EventHandler(this.selectSource_Click);
            // 
            // useAdfCheckBox
            // 
            this.useAdfCheckBox.AutoSize = true;
            this.useAdfCheckBox.Location = new System.Drawing.Point(12, 44);
            this.useAdfCheckBox.Name = "useAdfCheckBox";
            this.useAdfCheckBox.Size = new System.Drawing.Size(69, 17);
            this.useAdfCheckBox.TabIndex = 3;
            this.useAdfCheckBox.Text = "Use ADF";
            this.useAdfCheckBox.UseVisualStyleBackColor = true;
            // 
            // useUICheckBox
            // 
            this.useUICheckBox.AutoSize = true;
            this.useUICheckBox.Location = new System.Drawing.Point(12, 92);
            this.useUICheckBox.Name = "useUICheckBox";
            this.useUICheckBox.Size = new System.Drawing.Size(59, 17);
            this.useUICheckBox.TabIndex = 4;
            this.useUICheckBox.Text = "Use UI";
            this.useUICheckBox.UseVisualStyleBackColor = true;
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(12, 560);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(89, 25);
            this.saveButton.TabIndex = 2;
            this.saveButton.Text = "Save";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // blackAndWhiteCheckBox
            // 
            this.blackAndWhiteCheckBox.AutoSize = true;
            this.blackAndWhiteCheckBox.Location = new System.Drawing.Point(12, 140);
            this.blackAndWhiteCheckBox.Name = "blackAndWhiteCheckBox";
            this.blackAndWhiteCheckBox.Size = new System.Drawing.Size(56, 17);
            this.blackAndWhiteCheckBox.TabIndex = 6;
            this.blackAndWhiteCheckBox.Text = "B && W";
            this.blackAndWhiteCheckBox.UseVisualStyleBackColor = true;
            this.blackAndWhiteCheckBox.MouseClick += new System.Windows.Forms.MouseEventHandler(this.checkBox_MouseClick);
            // 
            // widthLabel
            // 
            this.widthLabel.AutoSize = true;
            this.widthLabel.Location = new System.Drawing.Point(13, 250);
            this.widthLabel.Name = "widthLabel";
            this.widthLabel.Size = new System.Drawing.Size(35, 13);
            this.widthLabel.TabIndex = 7;
            this.widthLabel.Text = "Width";
            // 
            // heightLabel
            // 
            this.heightLabel.AutoSize = true;
            this.heightLabel.Location = new System.Drawing.Point(13, 263);
            this.heightLabel.Name = "heightLabel";
            this.heightLabel.Size = new System.Drawing.Size(38, 13);
            this.heightLabel.TabIndex = 8;
            this.heightLabel.Text = "Height";
            // 
            // diagnosticsButton
            // 
            this.diagnosticsButton.Location = new System.Drawing.Point(12, 591);
            this.diagnosticsButton.Name = "diagnosticsButton";
            this.diagnosticsButton.Size = new System.Drawing.Size(85, 22);
            this.diagnosticsButton.TabIndex = 3;
            this.diagnosticsButton.Text = "Send";
            this.diagnosticsButton.UseVisualStyleBackColor = true;
            this.diagnosticsButton.Click += new System.EventHandler(this.sendToService_Click);
            // 
            // checkBoxArea
            // 
            this.checkBoxArea.AutoSize = true;
            this.checkBoxArea.Location = new System.Drawing.Point(12, 230);
            this.checkBoxArea.Name = "checkBoxArea";
            this.checkBoxArea.Size = new System.Drawing.Size(73, 17);
            this.checkBoxArea.TabIndex = 10;
            this.checkBoxArea.Text = "Grab area";
            this.checkBoxArea.UseVisualStyleBackColor = true;
            // 
            // showProgressIndicatorUICheckBox
            // 
            this.showProgressIndicatorUICheckBox.AutoSize = true;
            this.showProgressIndicatorUICheckBox.Location = new System.Drawing.Point(12, 107);
            this.showProgressIndicatorUICheckBox.Name = "showProgressIndicatorUICheckBox";
            this.showProgressIndicatorUICheckBox.Size = new System.Drawing.Size(97, 17);
            this.showProgressIndicatorUICheckBox.TabIndex = 11;
            this.showProgressIndicatorUICheckBox.Text = "Show Progress";
            this.showProgressIndicatorUICheckBox.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label1.Location = new System.Drawing.Point(15, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 1);
            this.label1.TabIndex = 12;
            // 
            // useDuplexCheckBox
            // 
            this.useDuplexCheckBox.AutoSize = true;
            this.useDuplexCheckBox.Location = new System.Drawing.Point(12, 59);
            this.useDuplexCheckBox.Name = "useDuplexCheckBox";
            this.useDuplexCheckBox.Size = new System.Drawing.Size(81, 17);
            this.useDuplexCheckBox.TabIndex = 13;
            this.useDuplexCheckBox.Text = "Use Duplex";
            this.useDuplexCheckBox.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label2.Location = new System.Drawing.Point(12, 79);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 1);
            this.label2.TabIndex = 14;
            // 
            // label3
            // 
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label3.Location = new System.Drawing.Point(12, 127);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(75, 1);
            this.label3.TabIndex = 15;
            // 
            // label4
            // 
            this.label4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label4.Location = new System.Drawing.Point(13, 278);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(75, 1);
            this.label4.TabIndex = 16;
            // 
            // autoRotateCheckBox
            // 
            this.autoRotateCheckBox.AutoSize = true;
            this.autoRotateCheckBox.Location = new System.Drawing.Point(13, 314);
            this.autoRotateCheckBox.Name = "autoRotateCheckBox";
            this.autoRotateCheckBox.Size = new System.Drawing.Size(83, 17);
            this.autoRotateCheckBox.TabIndex = 18;
            this.autoRotateCheckBox.Text = "Auto Rotate";
            this.autoRotateCheckBox.UseVisualStyleBackColor = true;
            // 
            // autoDetectBorderCheckBox
            // 
            this.autoDetectBorderCheckBox.AutoSize = true;
            this.autoDetectBorderCheckBox.Location = new System.Drawing.Point(13, 291);
            this.autoDetectBorderCheckBox.Name = "autoDetectBorderCheckBox";
            this.autoDetectBorderCheckBox.Size = new System.Drawing.Size(92, 17);
            this.autoDetectBorderCheckBox.TabIndex = 17;
            this.autoDetectBorderCheckBox.Text = "Detect Border";
            this.autoDetectBorderCheckBox.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label5.Location = new System.Drawing.Point(13, 334);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(75, 1);
            this.label5.TabIndex = 19;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoScroll = true;
            this.flowLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            this.flowLayoutPanel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.flowLayoutPanel1.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(128, 513);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(460, 100);
            this.flowLayoutPanel1.TabIndex = 20;
            // 
            // scan
            // 
            this.scan.Location = new System.Drawing.Point(13, 338);
            this.scan.Name = "scan";
            this.scan.Size = new System.Drawing.Size(85, 24);
            this.scan.TabIndex = 1;
            this.scan.Text = "Scan";
            this.scan.UseVisualStyleBackColor = true;
            this.scan.Click += new System.EventHandler(this.scan_Click);
            // 
            // btnRotateLeft
            // 
            this.btnRotateLeft.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRotateLeft.Location = new System.Drawing.Point(57, 410);
            this.btnRotateLeft.Name = "btnRotateLeft";
            this.btnRotateLeft.Size = new System.Drawing.Size(35, 23);
            this.btnRotateLeft.TabIndex = 24;
            this.btnRotateLeft.Text = "←";
            this.btnRotateLeft.UseVisualStyleBackColor = true;
            this.btnRotateLeft.Click += new System.EventHandler(this.buttonRotate_Click);
            // 
            // btnRotateRigth
            // 
            this.btnRotateRigth.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRotateRigth.Location = new System.Drawing.Point(56, 381);
            this.btnRotateRigth.Name = "btnRotateRigth";
            this.btnRotateRigth.Size = new System.Drawing.Size(35, 23);
            this.btnRotateRigth.TabIndex = 25;
            this.btnRotateRigth.Text = "→";
            this.btnRotateRigth.UseVisualStyleBackColor = true;
            this.btnRotateRigth.Click += new System.EventHandler(this.buttonRotate_Click);
            // 
            // btnRotateUp
            // 
            this.btnRotateUp.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRotateUp.Location = new System.Drawing.Point(15, 381);
            this.btnRotateUp.Name = "btnRotateUp";
            this.btnRotateUp.Size = new System.Drawing.Size(35, 52);
            this.btnRotateUp.TabIndex = 27;
            this.btnRotateUp.Text = "↑↓";
            this.btnRotateUp.UseVisualStyleBackColor = true;
            this.btnRotateUp.Click += new System.EventHandler(this.buttonRotate_Click);
            // 
            // chkGrey
            // 
            this.chkGrey.AutoSize = true;
            this.chkGrey.Location = new System.Drawing.Point(12, 163);
            this.chkGrey.Name = "chkGrey";
            this.chkGrey.Size = new System.Drawing.Size(75, 17);
            this.chkGrey.TabIndex = 28;
            this.chkGrey.Text = "GreyScale";
            this.chkGrey.UseVisualStyleBackColor = true;
            this.chkGrey.MouseClick += new System.Windows.Forms.MouseEventHandler(this.checkBox_MouseClick);
            // 
            // chkColor
            // 
            this.chkColor.AutoSize = true;
            this.chkColor.Checked = true;
            this.chkColor.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkColor.Location = new System.Drawing.Point(12, 186);
            this.chkColor.Name = "chkColor";
            this.chkColor.Size = new System.Drawing.Size(49, 17);
            this.chkColor.TabIndex = 29;
            this.chkColor.Text = "RGB";
            this.chkColor.UseVisualStyleBackColor = true;
            this.chkColor.MouseClick += new System.Windows.Forms.MouseEventHandler(this.checkBox_MouseClick);
            // 
            // ddlResolution
            // 
            this.ddlResolution.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ddlResolution.FormattingEnabled = true;
            this.ddlResolution.Items.AddRange(new object[] {
            "50",
            "100",
            "150",
            "200",
            "300",
            "400",
            "500",
            "600"});
            this.ddlResolution.Location = new System.Drawing.Point(12, 203);
            this.ddlResolution.Name = "ddlResolution";
            this.ddlResolution.Size = new System.Drawing.Size(86, 24);
            this.ddlResolution.TabIndex = 30;
            this.ddlResolution.Text = "Resolution";
            // 
            // btnOriginalImage
            // 
            this.btnOriginalImage.Location = new System.Drawing.Point(13, 439);
            this.btnOriginalImage.Name = "btnOriginalImage";
            this.btnOriginalImage.Size = new System.Drawing.Size(85, 25);
            this.btnOriginalImage.TabIndex = 31;
            this.btnOriginalImage.Text = "Show Original";
            this.btnOriginalImage.UseVisualStyleBackColor = true;
            this.btnOriginalImage.Click += new System.EventHandler(this.btnOriginalImage_Click_1);
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(15, 470);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(52, 25);
            this.btnClear.TabIndex = 32;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.MouseClick += new System.Windows.Forms.MouseEventHandler(this.btnClear_MouseClick);
            // 
            // ddlSize
            // 
            this.ddlSize.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ddlSize.FormattingEnabled = true;
            this.ddlSize.Items.AddRange(new object[] {
            "A0",
            "A1",
            "A2",
            "A3",
            "A4",
            "A5",
            "B0",
            "B1",
            "B2",
            "B3",
            "B4",
            "B5",
            "LETTER",
            "NOTE",
            "POSTCARD"});
            this.ddlSize.Location = new System.Drawing.Point(16, 501);
            this.ddlSize.Name = "ddlSize";
            this.ddlSize.Size = new System.Drawing.Size(85, 24);
            this.ddlSize.TabIndex = 33;
            this.ddlSize.Text = "Size";
            // 
            // radioPdf
            // 
            this.radioPdf.AutoSize = true;
            this.radioPdf.Checked = true;
            this.radioPdf.Location = new System.Drawing.Point(12, 531);
            this.radioPdf.Name = "radioPdf";
            this.radioPdf.Size = new System.Drawing.Size(46, 17);
            this.radioPdf.TabIndex = 34;
            this.radioPdf.TabStop = true;
            this.radioPdf.Text = "PDF";
            this.radioPdf.UseVisualStyleBackColor = true;
            // 
            // radioTif
            // 
            this.radioTif.AutoSize = true;
            this.radioTif.Location = new System.Drawing.Point(57, 531);
            this.radioTif.Name = "radioTif";
            this.radioTif.Size = new System.Drawing.Size(47, 17);
            this.radioTif.TabIndex = 35;
            this.radioTif.Text = "TIFF";
            this.radioTif.UseVisualStyleBackColor = true;
            // 
            // lblCount
            // 
            this.lblCount.AutoSize = true;
            this.lblCount.Location = new System.Drawing.Point(69, 476);
            this.lblCount.Name = "lblCount";
            this.lblCount.Size = new System.Drawing.Size(0, 13);
            this.lblCount.TabIndex = 36;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.White;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(128, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(479, 477);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseDown);
            this.pictureBox1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseUp);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(619, 625);
            this.ControlBox = false;
            this.Controls.Add(this.lblCount);
            this.Controls.Add(this.radioTif);
            this.Controls.Add(this.radioPdf);
            this.Controls.Add(this.ddlSize);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnOriginalImage);
            this.Controls.Add(this.ddlResolution);
            this.Controls.Add(this.chkColor);
            this.Controls.Add(this.chkGrey);
            this.Controls.Add(this.btnRotateUp);
            this.Controls.Add(this.btnRotateRigth);
            this.Controls.Add(this.btnRotateLeft);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.autoRotateCheckBox);
            this.Controls.Add(this.autoDetectBorderCheckBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.useDuplexCheckBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.showProgressIndicatorUICheckBox);
            this.Controls.Add(this.checkBoxArea);
            this.Controls.Add(this.diagnosticsButton);
            this.Controls.Add(this.heightLabel);
            this.Controls.Add(this.widthLabel);
            this.Controls.Add(this.blackAndWhiteCheckBox);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.useUICheckBox);
            this.Controls.Add(this.useAdfCheckBox);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.scan);
            this.Controls.Add(this.selectSource);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AL QEMAM Scanner UI";
            this.TransparencyKey = System.Drawing.Color.Maroon;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button selectSource;
        private System.Windows.Forms.CheckBox useAdfCheckBox;
        private System.Windows.Forms.CheckBox useUICheckBox;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.CheckBox blackAndWhiteCheckBox;
        private System.Windows.Forms.Label widthLabel;
        private System.Windows.Forms.Label heightLabel;
        private System.Windows.Forms.Button diagnosticsButton;
        private System.Windows.Forms.CheckBox checkBoxArea;
        private System.Windows.Forms.CheckBox showProgressIndicatorUICheckBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox useDuplexCheckBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox autoRotateCheckBox;
        private System.Windows.Forms.CheckBox autoDetectBorderCheckBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button scan;
        private System.Windows.Forms.Button btnRotateLeft;
        private System.Windows.Forms.Button btnRotateRigth;
        private System.Windows.Forms.Button btnRotateUp;
        private System.Windows.Forms.CheckBox chkGrey;
        private System.Windows.Forms.CheckBox chkColor;
        private System.Windows.Forms.ComboBox ddlResolution;
        private System.Windows.Forms.Button btnOriginalImage;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.ComboBox ddlSize;
        private System.Windows.Forms.RadioButton radioPdf;
        private System.Windows.Forms.RadioButton radioTif;
        private System.Windows.Forms.Label lblCount;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}

