using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using twainNative;
using twainNative.WinFroms;


namespace windowsService.utilityClasses
{
    public static class twainHandler
    {
        private static IntPtr handler;  
        private static Twain _twain;
        private static ScanSettings settings;
        private static List<System.Drawing.Image> images;

        public static IntPtr Handler
        {
            get { return twainHandler.handler; }
            set { twainHandler.handler = value; }
        }
        public static Twain Twain
        {
            get { return twainHandler._twain; }
            set { twainHandler._twain = value; }
        }
        public static List<System.Drawing.Image> Images
        {
            get { return twainHandler.images; }
            set { twainHandler.images = value; }
        }
        public static ScanSettings Settings
        {
            get { return twainHandler.settings; }
            set { twainHandler.settings = value; }
        }
        public static void setTwain()
        {
            Images = new List<System.Drawing.Image>();
            Twain = new Twain(new WinFormsWindowMessageHook(Handler));
            Twain.TransferImage += delegate(Object sender, TransferImageEventArgs args)
            {
                if (args.Image != null)
                {
                    //System.Diagnostics.Debugger.Launch();
                    Images.Add(args.Image);
                }
            };
        }
        public static void scanSetting(Dictionary<string, string> paramsObj)
        {
            #region PageType
            twainNative.TwainNative.PageType size = twainNative.TwainNative.PageType.A4;
            if (paramsObj.ContainsKey("pageSize") && paramsObj["pageSize"] != "-1")
            {
                switch (paramsObj["pageSize"])
                {
                    case "A0":
                        size = twainNative.TwainNative.PageType.A0;
                        break;
                    case "A1":
                        size = twainNative.TwainNative.PageType.A1;
                        break;
                    case "A2":
                        size = twainNative.TwainNative.PageType.A2;
                        break;
                    case "A3":
                        size = twainNative.TwainNative.PageType.A3;
                        break;
                    case "A5":
                        size = twainNative.TwainNative.PageType.A5;
                        break;
                    case "A6":
                        size = twainNative.TwainNative.PageType.A6;
                        break;
                    case "A7":
                        size = twainNative.TwainNative.PageType.A7;
                        break;
                    case "A8":
                        size = twainNative.TwainNative.PageType.A8;
                        break;
                    case "A9":
                        size = twainNative.TwainNative.PageType.A9;
                        break;
                    case "A10":
                        size = twainNative.TwainNative.PageType.A10;
                        break;
                    case "UsLetter":
                        size = twainNative.TwainNative.PageType.UsLetter;
                        break;
                    case "UsLegal":
                        size = twainNative.TwainNative.PageType.UsLegal;
                        break;
                    case "UsStatement":
                        size = twainNative.TwainNative.PageType.UsStatement;
                        break;
                    case "BusinessCard":
                        size = twainNative.TwainNative.PageType.BusinessCard;
                        break;
                    case "MaxSize":
                        size = twainNative.TwainNative.PageType.MaxSize;
                        break;
                }
            }
            #endregion

            Settings = new ScanSettings();
            Settings.Page = new PageSettings();
            Settings.Page.Size = size;
            Settings.UseDocumentFeeder = paramsObj["adf"] == "True" ? true : false;
            Settings.ShowTwainUI = paramsObj["ui"] == "True" ? true : false;
            Settings.UseDuplex = paramsObj["duplex"] == "True" ? true : false; ;
            Settings.Resolution = ((paramsObj["colors"] == "bw") ? (ResolutionSettings.Fax)
               : (paramsObj["colors"] == "grey") ? (ResolutionSettings.Photocopier)
               : (paramsObj["colors"] == "color" ? ResolutionSettings.ColourPhotocopier : ResolutionSettings.ColourPhotocopier));
            Settings.Resolution.Dpi = paramsObj["resolution"] != "-1" ? int.Parse(paramsObj["resolution"]) : Settings.Resolution.Dpi;
            Settings.ShouldTransferAllPages = true;
        }
       
       
    }
}

