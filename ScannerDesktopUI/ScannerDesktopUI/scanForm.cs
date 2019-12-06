using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using twainNative;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;

using System.Drawing.Imaging;
using namedPipeTransfere;




namespace ScannerDesktopUI
{
    public partial class MainForm : Form
    {
        //set scanned image size manually
        static AreaSettings AreaSettings = new AreaSettings(Units.Centimeters, 0.1f, 0.1f, 28.05301f, 20.43f);

        //scanner setting
        public Twain _twain;
        public ScanSettings _settings;

        //holding original image before croping & logo image
        System.Drawing.Image _originalImage, _logoImage;

        //draw cropping borders
        int cropX, cropY, cropWidth, cropHeight, delRect;
        Pen borderpen = new Pen(Color.Orange, 2);
        SolidBrush rectbrush = new SolidBrush(Color.FromArgb(100, Color.White));

        public MainForm()
        {
            InitializeComponent();

            try
            {
                // this.ShowInTaskbar = false;
                this.BringToFront();
                _twain = new Twain(new WinFormsWindowMessageHook(this));
                // transfer scanned images (1 by 1)
                _twain.TransferImage += delegate(Object sender, TransferImageEventArgs args)
                {
                    if (args.Image != null)
                    {
                        createPictureBox(args.Image);
                    }
                };
                _twain.ScanningComplete += delegate { Enabled = true; };
                _logoImage = pictureBox1.Image;
                disableEditButtons();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void selectSource_Click(object sender, EventArgs e)
        {
            _twain.SelectSource();
        }
        public void scan_Click(object sender, EventArgs e)
        {
            Enabled = false;
            _settings = scanSetting();
            try
            {
                _twain.StartScanning(_settings);
                flowLayoutPanel1.BackColor = System.Drawing.Color.LightGray;
            }
            catch (TwainException ex)
            {
                MessageBox.Show(ex.Message);
                Enabled = true;
            }

        }
        public void saveButton_Click(object sender, EventArgs e)
        {
            Enabled = false;
            if (flowLayoutPanel1.Controls.Count > 0)
            {
                SaveFileDialog dialog = new SaveFileDialog();

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    System.Drawing.Image[] images = new System.Drawing.Image[flowLayoutPanel1.Controls.Count];
                    for (int i = 0, len = flowLayoutPanel1.Controls.Count; i < len; i++)
                    {
                        images[i] = ((PictureBox)flowLayoutPanel1.Controls[i]).Image;
                    }
                    if (radioPdf.Checked)
                    {
                        saveAsPDF(dialog, images);
                    }
                    if (radioTif.Checked)
                    {
                        saveAsTIFF(dialog, images);
                    }
                    sendToService_Click(null, null);
                    btnClear_MouseClick(null, null);
                }
            }
            else
            {
                MessageBox.Show("No Files to save, scan and try again!");
            }
            Enabled = true;
        }
        public void sendToService_Click(object sender, EventArgs e)
        {
            Enabled = false;
            //var diagnostics = new Diagnostics(new WinFormsWindowMessageHook(this)); 
            System.Drawing.Image[] images = new System.Drawing.Image[flowLayoutPanel1.Controls.Count];
            for (int i = 0, len = flowLayoutPanel1.Controls.Count; i < len; i++)
            {
                images[i] = ((PictureBox)flowLayoutPanel1.Controls[i]).Image;
            }
            //pdfDocument(images);
            serveScanRes(images);
            Enabled = true;
        }
        public void createPictureBox(System.Drawing.Image image)
        {

            PictureBox pb = new PictureBox();
            pb.Image = image;
            pb.Image.Tag = flowLayoutPanel1.Controls.Count;
            pb.Height = 80;
            pb.Width = 100;
            pb.SizeMode = PictureBoxSizeMode.Zoom;
            pb.Cursor = System.Windows.Forms.Cursors.Hand;
            pb.MouseClick += (s, e) => { pictureBox_MouseClick(s, e); };

            flowLayoutPanel1.Controls.Add(pb);
            widthLabel.Text = "Width: " + pb.Image.Width;
            heightLabel.Text = "Height: " + pb.Image.Height;

            if (flowLayoutPanel1.Controls.Count > 0)
            {
                btnClear.Enabled = true;
                ddlSize.Enabled = true;
                saveButton.Enabled = true;
                radioPdf.Enabled = true;
                radioTif.Enabled = true;
                diagnosticsButton.Enabled = true;
            }
        }
        public void pictureBox_MouseClick(object sender, MouseEventArgs e)
        {
            PictureBox bx = (PictureBox)sender;
            pictureBox1.Image = bx.Image;
            pictureBox1.Image.Tag = bx.Image.Tag;

            for (int i = 0, len = flowLayoutPanel1.Controls.Count; i < len; i++)
            {
                ((PictureBox)flowLayoutPanel1.Controls[i]).BorderStyle = BorderStyle.None;
            }
            bx.BorderStyle = BorderStyle.FixedSingle;

            //btnRotateDown.Enabled = true;
            btnRotateRigth.Enabled = true;
            btnRotateUp.Enabled = true;
            btnRotateLeft.Enabled = true;
        }
        public ScanSettings scanSetting()
        {
            ScanSettings settings = new ScanSettings();
            settings.UseDocumentFeeder = useAdfCheckBox.Checked;
            settings.ShowTwainUI = useUICheckBox.Checked;
            settings.ShowProgressIndicatorUI = showProgressIndicatorUICheckBox.Checked;
            settings.UseDuplex = useDuplexCheckBox.Checked;

            settings.Resolution = blackAndWhiteCheckBox.Checked ? ResolutionSettings.Fax
               : (chkGrey.Checked ? ResolutionSettings.Photocopier
               : (chkColor.Checked ? ResolutionSettings.ColourPhotocopier : ResolutionSettings.ColourPhotocopier));
            settings.Resolution.Dpi = ddlResolution.SelectedItem != null ? int.Parse(ddlResolution.SelectedItem.ToString()) : settings.Resolution.Dpi;

            settings.Area = !checkBoxArea.Checked ? null : AreaSettings;
            settings.ShouldTransferAllPages = true;

            settings.Rotation = new RotationSettings()
            {
                AutomaticRotate = autoRotateCheckBox.Checked,
                AutomaticBorderDetection = autoDetectBorderCheckBox.Checked,
            };

            return settings;

        }

        public ScanSettings scanSetting(bool ui, bool adf, bool dublex, int resolution, string color)
        {

            ScanSettings settings = new ScanSettings();
            settings.UseDocumentFeeder = adf;
            settings.ShowTwainUI = ui;
            settings.ShowProgressIndicatorUI = false;
            settings.UseDuplex = dublex;

            settings.Resolution = color == "bw" ? ResolutionSettings.Fax
               : (color == "grey" ? ResolutionSettings.Photocopier
               : (color == "color" ? ResolutionSettings.ColourPhotocopier : ResolutionSettings.ColourPhotocopier));
            settings.Resolution.Dpi = resolution != -1 ? resolution : settings.Resolution.Dpi;

            //settings.Area = !checkBoxArea.Checked ? null : AreaSettings;
            settings.ShouldTransferAllPages = true;

            //settings.Rotation = new RotationSettings()
            //{
            //    AutomaticRotate = autoRotateCheckBox.Checked,
            //    AutomaticBorderDetection = autoDetectBorderCheckBox.Checked,
            //};

            return settings;
        }
        public void buttonRotate_Click(object sender, EventArgs e)
        {
            //rotate(0);
            RotateFlipType degree = RotateFlipType.RotateNoneFlipNone;
            var x = (Button)sender;
            switch (x.Name)
            {
                case "btnRotateUp":
                    degree = RotateFlipType.Rotate180FlipX;
                    break;
                case "btnRotateRigth":
                    degree = RotateFlipType.Rotate270FlipXY;
                    break;
                case "btnRotateLeft":
                    degree = RotateFlipType.Rotate90FlipXY;
                    break;

            }
            pictureBox1.Image.RotateFlip(degree);
            pictureBox1.Refresh();
            flowLayoutPanel1.Refresh();
        }
        public void checkBox_MouseClick(object sender, MouseEventArgs e)
        {
            var x = (CheckBox)sender;
            switch (x.Name)
            {
                case "blackAndWhiteCheckBox":
                    blackAndWhiteCheckBox.Checked = true;
                    chkColor.Checked = false;
                    chkGrey.Checked = false;

                    break;
                case "chkColor":
                    chkColor.Checked = true;
                    chkGrey.Checked = false;
                    blackAndWhiteCheckBox.Checked = false;
                    break;
                case "chkGrey":
                    chkGrey.Checked = true;
                    chkColor.Checked = false;
                    blackAndWhiteCheckBox.Checked = false;
                    break;

            }

        }
        public void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            if (pictureBox1.Image != _logoImage)
            {
                this.pictureBox1.MouseMove -= (this.pictureBox1_MouseMove);
                this.pictureBox1.Paint -= (this.pictureBox1_Paint);

                btnOriginalImage.Enabled = true;
                Cursor = Cursors.Default;
                if (cropWidth < 1 || cropHeight < 1)
                {
                    return;
                }
                System.Drawing.Rectangle rect = new System.Drawing.Rectangle(cropX, cropY, cropWidth, cropHeight);
                Bitmap bit = new Bitmap(pictureBox1.Image, pictureBox1.Width, pictureBox1.Height);
                Bitmap crop = new Bitmap(cropWidth, cropHeight);
                Graphics gfx = Graphics.FromImage(crop);
                gfx.InterpolationMode = InterpolationMode.HighQualityBicubic;//here add  System.Drawing.Drawing2D namespace;
                gfx.PixelOffsetMode = PixelOffsetMode.HighQuality;//here add  System.Drawing.Drawing2D namespace;
                gfx.CompositingQuality = CompositingQuality.HighQuality;//here add  System.Drawing.Drawing2D namespace;
                gfx.DrawImage(bit, 0, 0, rect, GraphicsUnit.Pixel);
                gfx.ExcludeClip(rect);
                pictureBox1.Image = crop;
                pictureBox1.Refresh();

                ((PictureBox)flowLayoutPanel1.Controls[int.Parse(_originalImage.Tag.ToString())]).Image = crop.Clone() as System.Drawing.Image;
                delRect = 1;


            }

        }
        public void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {

            if (pictureBox1.Image == null)
                return;

            if (pictureBox1.Image != _logoImage)
            {
                if (e.Button == MouseButtons.Left)//here i have use mouse click left button only
                {
                    pictureBox1.Refresh();
                    cropWidth = e.X - cropX;
                    cropHeight = e.Y - cropY;
                }
                pictureBox1.Refresh();
            }
        }
        public void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            if (pictureBox1.Image != _logoImage)
            {
                System.Drawing.Rectangle rect = new System.Drawing.Rectangle(cropX, cropY, cropWidth, cropHeight);
                Graphics gfx = e.Graphics;
                if (delRect == 1)
                {
                    return;
                }
                gfx.DrawRectangle(borderpen, rect);
                gfx.FillRectangle(rectbrush, rect);
            }
        }
        public void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            this.pictureBox1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseMove);
            this.pictureBox1.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox1_Paint);

            if (pictureBox1.Image != _logoImage)
            {
                _originalImage = pictureBox1.Image;
                delRect = 0;
                if (e.Button == MouseButtons.Left)//here i have use mouse click left button only
                {
                    pictureBox1.Refresh();
                    cropX = e.X;
                    cropY = e.Y;
                    Cursor = Cursors.Cross;
                }
                pictureBox1.Refresh();
            }
        }
        public void btnOriginalImage_Click_1(object sender, EventArgs e)
        {
            pictureBox1.Image = _originalImage;
            var bx = ((PictureBox)flowLayoutPanel1.Controls[int.Parse(_originalImage.Tag.ToString())]);
            bx.Image = pictureBox1.Image;
        }
        public void btnClear_MouseClick(object sender, MouseEventArgs e)
        {
            pictureBox1.Image = _logoImage;

            clearImagesBox();
            flowLayoutPanel1.BackColor = System.Drawing.Color.Transparent;

            disableEditButtons();
            flowLayoutPanel1.Refresh();
            pictureBox1.Refresh();
        }
        public void saveAsTIFF(SaveFileDialog sfd, System.Drawing.Image[] images)
        {
            MemoryStream stream = new MemoryStream();
            images[0].Save(stream, ImageFormat.Png);
            System.Drawing.Image tiff = System.Drawing.Image.FromStream(stream);
            stream.Close();

            ImageCodecInfo encoderInfo = ImageCodecInfo.GetImageEncoders().First(i => i.MimeType == "image/tiff");

            EncoderParameters encoderParams = new EncoderParameters(2);
            EncoderParameter parameter = new EncoderParameter(
                System.Drawing.Imaging.Encoder.Compression, (long)EncoderValue.CompressionNone);
            encoderParams.Param[0] = parameter;
            parameter = new EncoderParameter(System.Drawing.Imaging.Encoder.SaveFlag,
                        (long)EncoderValue.MultiFrame);
            encoderParams.Param[1] = parameter;


            tiff.Save(sfd.FileName + ".tiff", encoderInfo, encoderParams);



            EncoderParameters EncoderParams = new EncoderParameters(2);
            EncoderParameter SaveEncodeParam = new EncoderParameter(
                 System.Drawing.Imaging.Encoder.SaveFlag,
                 (long)EncoderValue.FrameDimensionPage);
            EncoderParameter CompressionEncodeParam = new EncoderParameter(
                 System.Drawing.Imaging.Encoder.Compression, (long)EncoderValue.CompressionNone);
            EncoderParams.Param[0] = CompressionEncodeParam;
            EncoderParams.Param[1] = SaveEncodeParam;
            for (int i = 1; i < images.Length; i++)
            {
                tiff.SaveAdd(images[i], EncoderParams);

            }
            SaveEncodeParam = new EncoderParameter(
                System.Drawing.Imaging.Encoder.SaveFlag, (long)EncoderValue.Flush);
            EncoderParams = new EncoderParameters(1);
            EncoderParams.Param[0] = SaveEncodeParam;
            tiff.SaveAdd(EncoderParams);
        }
        public void saveAsPDF(SaveFileDialog sfd, System.Drawing.Image[] images)
        {
            #region pageSize
            var size = PageSize.A4;
            if (ddlSize.SelectedItem != null)
            {
                switch (ddlSize.SelectedItem.ToString())
                {
                    case "A0":
                        size = PageSize.A0;
                        break;
                    case "A1":
                        size = PageSize.A0;
                        break;
                    case "A2":
                        size = PageSize.A2;
                        break;
                    case "A3":
                        size = PageSize.A3;
                        break;
                    case "A4":
                        size = PageSize.A4;
                        break;
                    case "A5":
                        size = PageSize.A5;
                        break;
                    case "B0":
                        size = PageSize.B0;
                        break;
                    case "B1":
                        size = PageSize.B1;
                        break;
                    case "B2":
                        size = PageSize.B2;
                        break;
                    case "B3":
                        size = PageSize.B3;
                        break;
                    case "B4":
                        size = PageSize.B4;
                        break;
                    case "B5":
                        size = PageSize.B5;
                        break;
                    case "LETTER":
                        size = PageSize.LETTER;
                        break;
                    case "NOTE":
                        size = PageSize.NOTE;
                        break;
                    case "POSTCARD":
                        size = PageSize.POSTCARD;
                        break;
                }
            }
            #endregion
            MemoryStream mstr = new MemoryStream();
            Document doc = new Document(size);
            PdfWriter wri = PdfWriter.GetInstance(doc, new FileStream(sfd.FileName + ".pdf", FileMode.Create));
            try
            {
                doc.Open();
                for (int i = 0; i < images.Length; i++)
                {
                    iTextSharp.text.Image png = iTextSharp.text.Image.GetInstance(images[i], ImageFormat.Png);
                    png.ScaleToFit(doc.PageSize);
                    doc.Add(png);
                    doc.NewPage();
                }

                doc.Close();
                wri.Close();
                mstr.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                btnClear.PerformClick();
            }

            finally
            {
            }

        }
        public void pdfDocument(System.Drawing.Image[] images)
        {
            #region pageSize
            var size = PageSize.A4;
            if (ddlSize.SelectedItem != null)
            {
                switch (ddlSize.SelectedItem.ToString())
                {
                    case "A0":
                        size = PageSize.A0;
                        break;
                    case "A1":
                        size = PageSize.A0;
                        break;
                    case "A2":
                        size = PageSize.A2;
                        break;
                    case "A3":
                        size = PageSize.A3;
                        break;
                    case "A4":
                        size = PageSize.A4;
                        break;
                    case "A5":
                        size = PageSize.A5;
                        break;
                    case "B0":
                        size = PageSize.B0;
                        break;
                    case "B1":
                        size = PageSize.B1;
                        break;
                    case "B2":
                        size = PageSize.B2;
                        break;
                    case "B3":
                        size = PageSize.B3;
                        break;
                    case "B4":
                        size = PageSize.B4;
                        break;
                    case "B5":
                        size = PageSize.B5;
                        break;
                    case "LETTER":
                        size = PageSize.LETTER;
                        break;
                    case "NOTE":
                        size = PageSize.NOTE;
                        break;
                    case "POSTCARD":
                        size = PageSize.POSTCARD;
                        break;
                }
            }
            #endregion

            #region pdfDoc
            MemoryStream mstr = new MemoryStream();
            Document doc = new Document(size, 0, 0, 10, 10);
            PdfWriter wri = PdfWriter.GetInstance(doc, mstr);
            wri.CompressionLevel = PdfStream.BEST_COMPRESSION;

            try
            {
                images = compressImage(images.ToList<System.Drawing.Image>()).ToArray<System.Drawing.Image>();
                doc.Open();
                if (images.Length > 0)
                {
                    for (int i = 0; i < images.Length; i++)
                    {
                        iTextSharp.text.Image img = iTextSharp.text.Image.GetInstance(images[i], ImageFormat.Png);
                        img.ScaleToFit(doc.PageSize);
                        img.SetAbsolutePosition(0, 0);
                        img.ScaleAbsoluteHeight(doc.PageSize.Height);
                        doc.Add(img);
                        doc.NewPage();
                    }
                }
                else
                {
                    PdfPTable tbl = new PdfPTable(1);
                    PdfPCell cell = new PdfPCell(new Phrase("لم يتم مسح اي وثائق باستخدام الواجهة التفاعلية"));
                    cell.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
                    cell.ArabicOptions = ColumnText.DIGITS_EN2AN;
                    cell.BorderWidth = 0;
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                    cell.PaddingBottom = 5;
                    tbl.AddCell(cell);
                    doc.Add(tbl);
                }

                doc.Close();
                wri.Close();
                servicePipe.SendMessageToServer(mstr.ToArray());
                mstr.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                btnClear.PerformClick();
            }

            finally
            {
                Application.Exit();
            }
            #endregion
        }
        public void disableEditButtons()
        {
            //btnRotateDown.Enabled = false;
            btnRotateRigth.Enabled = false;
            btnRotateUp.Enabled = false;
            btnRotateLeft.Enabled = false;
            btnOriginalImage.Enabled = false;
            btnClear.Enabled = false;
            ddlSize.Enabled = false;
            saveButton.Enabled = false;
            diagnosticsButton.Enabled = false;
            radioPdf.Enabled = false;
            radioTif.Enabled = false;
        }
        public void clearImagesBox()
        {
            while (flowLayoutPanel1.Controls.Count > 0)
            {
                flowLayoutPanel1.Controls.Remove(flowLayoutPanel1.Controls[0]);
            }
        }
        public static List<System.Drawing.Image> compressImage(List<System.Drawing.Image> images)
        {
            List<System.Drawing.Image> newImages = new List<System.Drawing.Image>();
            System.Drawing.Imaging.ImageCodecInfo encoder = System.Drawing.Imaging.ImageCodecInfo.GetImageEncoders().First(i => i.MimeType == "image/png");

            System.Drawing.Imaging.EncoderParameters encoderParams = new System.Drawing.Imaging.EncoderParameters(2);
            System.Drawing.Imaging.EncoderParameter parameter = new System.Drawing.Imaging.EncoderParameter(
                System.Drawing.Imaging.Encoder.Compression, (long)100);
            encoderParams.Param[0] = parameter;
            parameter = new System.Drawing.Imaging.EncoderParameter(System.Drawing.Imaging.Encoder.Quality,
                        (long)100);
            encoderParams.Param[1] = parameter;
            for (int i = 0; i < images.Count; i++)
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    System.Drawing.Image newImage = new System.Drawing.Bitmap(images[i], new System.Drawing.Size(500, 650));
                    newImage.Save(ms, encoder, encoderParams);
                    newImages.Add(System.Drawing.Image.FromStream(ms));
                }
            }
            return newImages;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
        }

        public void serveScanRes(System.Drawing.Image[] images)
        {
            try
            {
                images = compressImage(images.ToList<System.Drawing.Image>()).ToArray<System.Drawing.Image>();
                List<byte[]> bytes = new List<byte[]>();
                for (int i = 0; i < images.Length; i++)
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        images[i].Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                        bytes.Add(ms.GetBuffer());
                    }
                }
                servicePipe.serveScanRes(bytes);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                btnClear.PerformClick();
            }

            finally
            {
                Application.Exit();
            }
        }
    }
}
