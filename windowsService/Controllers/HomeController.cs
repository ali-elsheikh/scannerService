using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Net.Http;

using namePipeTransfere;
using windowsService.utilityClasses;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace windowsService
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class HomeController : ApiController
    {

        [HttpPost]
        public string checkService()
        {
            return "1";
        }
        [HttpPost]
        public List<string> getScannerList()
        {
            return twainHandler.Twain.SourceNames.ToList();
        }
        [HttpPost]
        public string getDocument(Dictionary<string, string> paramsObj)
        {
            Dictionary<string, object> result = new Dictionary<string, object>();
            try
            {
                if (paramsObj["ui"].ToLower() == "true")
                {
                    utility.fireScannerUI();
                    scannerPipe.connect();
                    List<byte[]> bytes = scannerPipe.deserializeScannedList();
                    twainHandler.Images = utility.convertBytesToImages(bytes);
                    //return scannerPipe.GetScannedDocument();
                    return utility.Serialize(new { data = utility.scannedImage() });
                }
                else
                {
                    //System.Diagnostics.Debugger.Launch();
                    twainHandler.Twain.SelectSource(paramsObj["src"]);
                    twainHandler.scanSetting(paramsObj);
                    twainHandler.Twain.StartScanning(twainHandler.Settings);
                    twainHandler.Images = utility.compressImage(twainHandler.Images);
                    result.Add("data", utility.scannedImage());
                    return utility.Serialize(result);
                }
            }
            catch (Exception ex)
            {
                result.Add("ex", ex.Message);
                return utility.Serialize(result);
            }
        }
        [HttpPost]
        public string nextImage()
        {
            Dictionary<string, object> result = new Dictionary<string, object>();
            try
            {
                result.Add("data", utility.scannedImage());
                return utility.Serialize(result);
            }
            catch (Exception ex)
            {
                result.Add("ex", ex.Message);
                return utility.Serialize(result);
            }
        }
        [HttpPost]
        public string editImage(Dictionary<string, string> paramsObj)
        {
            try
            {
                string base64String = (paramsObj.ContainsKey("bytes") && !string.IsNullOrEmpty(paramsObj["bytes"])) ? paramsObj["bytes"] : "";
                byte[] bytes = Convert.FromBase64String(base64String);
                if ((paramsObj.ContainsKey("edit") && !string.IsNullOrEmpty(paramsObj["edit"])))
                {
                    using (System.IO.MemoryStream ms = new System.IO.MemoryStream(bytes))
                    {
                        System.Drawing.Image img = System.Drawing.Image.FromStream(ms);
                        if (paramsObj["edit"].ToLower() == "rotate")
                        {
                            if ((paramsObj.ContainsKey("rotate") && !string.IsNullOrEmpty(paramsObj["rotate"])))
                                img = utility.rotateImage(img, paramsObj["rotate"]);
                        }
                        else if (paramsObj["edit"].ToLower() == "crop")
                            img = utility.cropImage(img, int.Parse(paramsObj["cropX"]), int.Parse(paramsObj["cropY"]), int.Parse(paramsObj["cropWidth"]), int.Parse(paramsObj["cropHeight"]));
                        using (System.IO.MemoryStream mstr = new System.IO.MemoryStream())
                        {
                            img.Save(mstr, System.Drawing.Imaging.ImageFormat.Png);
                            bytes = mstr.GetBuffer();
                        }
                    }
                }
                return utility.Serialize(new { data = bytes });
            }
            catch (Exception ex)
            {
                return utility.Serialize(new { ex = ex.Message });
            }
        }
       
    }
}
