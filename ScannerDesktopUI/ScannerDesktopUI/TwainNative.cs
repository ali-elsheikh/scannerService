using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.ComponentModel;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using log4net;
using System.Windows.Forms;

using System.Diagnostics;
namespace twainNative
{


    /// <summary>
    /// Twain spec ICAP_AUTOSIZE values Added 2.0 
    /// </summary>
    public enum AutoSize
    {
        None = 0,
        Auto = 1,
        Current = 2,
    }

    public enum Capabilities : short
    {
        // all data sources are REQUIRED to support these capabilities
        XferCount = 0x0001,

        // image data sources are REQUIRED to support these capabilities
        ICompression = 0x0100,
        IPixelType = 0x0101,
        IUnits = 0x0102,
        IXferMech = 0x0103,

        // all data sources MAY support these capabilities
        Author = 0x1000,
        Caption = 0x1001,
        FeederEnabled = 0x1002,
        FeederLoaded = 0x1003,
        Timedate = 0x1004,
        SupportedCapabilities = 0x1005,
        Extendedcaps = 0x1006,
        AutoFeed = 0x1007,
        ClearPage = 0x1008,
        FeedPage = 0x1009,
        RewindPage = 0x100a,

        [Description("Controls progress indicator window during transfer and acquisition")]
        Indicators = 0x100b,
        SupportedCapsExt = 0x100c,
        PaperDetectable = 0x100d,
        UIControllable = 0x100e,
        DeviceOnline = 0x100f,
        AutoScan = 0x1010,
        ThumbnailsEnabled = 0x1011,
        Duplex = 0x1012,
        DuplexEnabled = 0x1013,
        Enabledsuionly = 0x1014,
        CustomdsData = 0x1015,
        Endorser = 0x1016,
        JobControl = 0x1017,
        Alarms = 0x1018,
        AlarmVolume = 0x1019,
        AutomaticCapture = 0x101a,
        TimeBeforeFirstCapture = 0x101b,
        TimeBetweenCaptures = 0x101c,
        ClearBuffers = 0x101d,
        MaxBatchBuffers = 0x101e,
        DeviceTimeDate = 0x101f,
        PowerSupply = 0x1020,
        CameraPreviewUI = 0x1021,
        DeviceEvent = 0x1022,
        SerialNumber = 0x1024,
        Printer = 0x1026,
        PrinterEnabled = 0x1027,
        PrinterIndex = 0x1028,
        PrinterMode = 0x1029,
        PrinterString = 0x102a,
        PrinterSuffix = 0x102b,
        Language = 0x102c,
        FeederAlignment = 0x102d,
        FeederOrder = 0x102e,
        ReAcquireAllowed = 0x1030,
        BatteryMinutes = 0x1032,
        BatteryPercentage = 0x1033,
        CameraSide = 0x1034,
        Segmented = 0x1035,
        CameraEnabled = 0x1036,
        CameraOrder = 0x1037,
        MicrEnabled = 0x1038,
        FeederPrep = 0x1039,
        Feederpocket = 0x103a,

        // image data sources MAY support these capabilities
        Autobright = 0x1100,
        Brightness = 0x1101,
        Contrast = 0x1103,
        CustHalftone = 0x1104,
        ExposureTime = 0x1105,
        Filter = 0x1106,
        Flashused = 0x1107,
        Gamma = 0x1108,
        Halftones = 0x1109,
        Highlight = 0x110a,
        ImageFileFormat = 0x110c,
        LampState = 0x110d,
        LightSource = 0x110e,
        Orientation = 0x1110,
        PhysicalWidth = 0x1111,
        PhysicalHeight = 0x1112,
        Shadow = 0x1113,
        Frames = 0x1114,
        XNativeResolution = 0x1116,
        YNativeResolution = 0x1117,
        XResolution = 0x1118,
        YResolution = 0x1119,
        MaxFrames = 0x111a,

        Tiles = 0x111b,
        Bitorder = 0x111c,
        Ccittkfactor = 0x111d,
        Lightpath = 0x111e,
        Pixelflavor = 0x111f,
        Planarchunky = 0x1120,
        Rotation = 0x1121,
        Supportedsizes = 0x1122,
        Threshold = 0x1123,
        Xscaling = 0x1124,
        Yscaling = 0x1125,
        Bitordercodes = 0x1126,
        Pixelflavorcodes = 0x1127,
        Jpegpixeltype = 0x1128,
        Timefill = 0x112a,
        BitDepth = 0x112b,
        Bitdepthreduction = 0x112c,
        Undefinedimagesize = 0x112d,
        Imagedataset = 0x112e,
        Extimageinfo = 0x112f,
        Minimumheight = 0x1130,
        Minimumwidth = 0x1131,
        Fliprotation = 0x1136,
        Barcodedetectionenabled = 0x1137,
        Supportedbarcodetypes = 0x1138,
        Barcodemaxsearchpriorities = 0x1139,
        Barcodesearchpriorities = 0x113a,
        Barcodesearchmode = 0x113b,
        Barcodemaxretries = 0x113c,
        Barcodetimeout = 0x113d,
        Zoomfactor = 0x113e,
        Patchcodedetectionenabled = 0x113f,
        Supportedpatchcodetypes = 0x1140,
        Patchcodemaxsearchpriorities = 0x1141,
        Patchcodesearchpriorities = 0x1142,
        Patchcodesearchmode = 0x1143,
        Patchcodemaxretries = 0x1144,
        Patchcodetimeout = 0x1145,
        Flashused2 = 0x1146,
        Imagefilter = 0x1147,
        Noisefilter = 0x1148,
        Overscan = 0x1149,
        Automaticborderdetection = 0x1150,
        Automaticdeskew = 0x1151,
        Automaticrotate = 0x1152,
        Jpegquality = 0x1153,
        Feedertype = 0x1154,
        Iccprofile = 0x1155,
        Autosize = 0x1156,
        AutomaticCropUsesFrame = 0x1157,            /* Added 2.1 */
        AutomaticLengthDetection = 0x1158,          /* Added 2.1 */
        AutomaticColorEnabled = 0x1159,             /* Added 2.1 */
        AutomaticColorNonColorPixelType = 0x115a,   /* Added 2.1 */
        ColorManagementEnabled = 0x115b,            /* Added 2.1 */
        ImageMerge = 0x115c,                        /* Added 2.1 */
        ImageMergeHeightThreshold = 0x115d,       /* Added 2.1 */
        SupoortedExtImageInfo = 0x115e,             /* Added 2.1 */
        Audiofileformat = 0x1201,
        Xfermech = 0x1202
    }

    /// <summary>
    /// /* TWON_ARRAY. Container for array of values (a simplified TW_ENUMERATION) */
    /// typedef struct {
    ///    TW_UINT16  ItemType;
    ///    TW_UINT32  NumItems;    /* How many items in ItemList           */
    ///    TW_UINT8   ItemList[1]; /* Array of ItemType values starts here */
    /// } TW_ARRAY, FAR * pTW_ARRAY;
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 2)]
    public class CapabilityArrayValue
    {
        public TwainType TwainType { get; set; }
        public int ItemCount { get; set; }

        /// <summary>
        /// The start of the array values
        /// </summary>
        byte _valueStart;


    }

    public enum TwainType : short
    {
        Int8 = 0x0000,
        Int16 = 0x0001,
        Int32 = 0x0002,
        UInt8 = 0x0003,
        UInt16 = 0x0004,
        UInt32 = 0x0005,
        Bool = 0x0006,
        Fix32 = 0x0007,
        Frame = 0x0008,
        Str32 = 0x0009,
        Str64 = 0x000a,
        Str128 = 0x000b,
        Str255 = 0x000c,
        Str1024 = 0x000d,
        Str512 = 0x000e
    }

    /// <summary>
    /// /* TWON_ENUMERATION. Container for a collection of values. */
    /// typedef struct {
    ///    TW_UINT16  ItemType;
    ///    TW_UINT32  NumItems;     /* How many items in ItemList                 */
    ///    TW_UINT32  CurrentIndex; /* Current value is in ItemList[CurrentIndex] */
    ///    TW_UINT32  DefaultIndex; /* Powerup value is in ItemList[DefaultIndex] */
    ///    TW_UINT8   ItemList[1];  /* Array of ItemType values starts here       */
    /// } TW_ENUMERATION, FAR * pTW_ENUMERATION;
    /// </summary>
    public class CapabilityEnumValue
    {
        public TwainType TwainType { get; set; }
        public int ItemCount { get; set; }

        public int CurrentIndex { get; set; }
        public int DefaultIndex { get; set; }

#pragma warning disable 169
        /// <summary>
        /// The start of the array values
        /// </summary>
        byte _valueStart;
#pragma warning restore 169
    }

    /// <summary>
    /// /* TWON_ONEVALUE. Container for one value. */
    /// typedef struct {
    ///    TW_UINT16  ItemType;
    ///    TW_UINT32  Item;
    /// } TW_ONEVALUE, FAR * pTW_ONEVALUE;
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 2)]
    public class CapabilityOneValue
    {
        public CapabilityOneValue(TwainType twainType, int value)
        {
            Value = value;
            TwainType = twainType;
        }

        public TwainType TwainType { get; set; }
        public int Value { get; set; }
    }

    public enum Command
    {
        Not = -1,
        Null = 0,
        TransferReady = 1,
        CloseRequest = 2,
        CloseOk = 3,
        DeviceEvent = 4
    }

    /// <summary>
    /// Twain spec ICAP_COMPRESSION values.
    /// </summary>
    public enum Compression : short
    {
        None = 0,
        PackBits = 1,
        Group31d = 2,    /* Follows CCITT spec (no End Of Line)          */
        Group31dEol = 3, /* Follows CCITT spec (has End Of Line)         */
        Group32d = 4,    /* Follows CCITT spec (use cap for K Factor)    */
        Group4 = 5,      /* Follows CCITT spec                           */
        Jpeg = 6,        /* Use capability for more info                 */
        Lzw = 7,         /* Must license from Unisys and IBM to use      */
        Jbig = 8,        /* For Bitonal images  -- Added 1.7 KHL         */
        Png = 9,         /* Added 1.8 */
        Rle4 = 10,       /* Added 1.8 */
        Rle8 = 11,       /* Added 1.8 */
        BitFields = 12   /* Added 1.8 */

    }

    public enum ConditionCode : short
    {
        Success = 0,

        /// <summary>
        /// Unknown failure.
        /// </summary>
        Bummer = 1,

        LowMemory = 2,

        NoDataSource = 3,

        /// <summary>
        /// Data source is already connected to the maximum number of connections.
        /// </summary>
        MaxConntions = 4,

        /// <summary>
        /// Data source or data source manager reported an error.
        /// </summary>
        OperationError = 5,

        BadCapability = 6,

        /// <summary>
        /// Unknown data group/data argument type combination.
        /// </summary>
        BadProtocol = 9,

        BadValue = 10,

        /// <summary>
        /// Message out of expected sequence.
        /// </summary>
        SequenceError = 11,

        /// <summary>
        /// Unknown destination application/source.
        /// </summary>
        BadDestination = 12,

        CapabilityUnsupported = 13,

        /// <summary>
        /// Operation not supported by capability.
        /// </summary>
        CapabilityBadOperation = 14,

        CapabilitySequenceError = 15,

        /// <summary>
        /// File system operation is denied.
        /// </summary>
        Denied = 16,

        FileExists = 17,

        FileNotFound = 18,

        DirectoryNotEmpty = 19,

        PaperJam = 20,

        PaperDoubleFeed = 21,

        FileWriteError = 22,

        CheckDeviceOnline = 23
    }

    /// <summary>
    /// TWON_...
    /// </summary>
    public enum ContainerType : short
    {
        Array = 0x0003,
        Enum = 0x0004,
        One = 0x0005,
        Range = 0x0006,
        DontCare = -1
    }

    public enum Country : short
    {
        USA = 1
    }

    public enum DataArgumentType : short
    {
        Null = 0x0000,
        Capability = 0x0001,
        Event = 0x0002,
        Identity = 0x0003,
        Parent = 0x0004,
        PendingXfers = 0x0005,
        SetupMemXfer = 0x0006,
        SetupFileXfer = 0x0007,
        Status = 0x0008,
        UserInterface = 0x0009,
        XferGroup = 0x000a,
        TwunkIdentity = 0x000b,
        CustomDSData = 0x000c,
        DeviceEvent = 0x000d,
        FileSystem = 0x000e,
        PassThru = 0x000f,

        ImageInfo = 0x0101,
        ImageLayout = 0x0102,
        ImageMemXfer = 0x0103,
        ImageNativeXfer = 0x0104,
        ImageFileXfer = 0x0105,
        CieColor = 0x0106,
        GrayResponse = 0x0107,
        RgbResponse = 0x0108,
        JpegCompression = 0x0109,
        Palette8 = 0x010a,
        ExtImageInfo = 0x010b,

        SetupFileXfer2 = 0x0301
    }


    [Flags]
    public enum DataGroup : int
    {
        Control = 0x0001,
        Image = 0x0002,
        Audio = 0x0004,
        DsmMask = 0xFFFF,

        //TWAIN 2.0 values
        //Dsm2 = 0x10000000,
        //App2 = 0x20000000,
        //Ds2 = 0x30000000,
    }

    public enum Duplex : short
    {
        None = 0,
        OnePass = 1,
        TwoPass = 2
    }

    public enum Message : short
    {
        Null = 0x0000,
        Get = 0x0001,
        GetCurrent = 0x0002,
        GetDefault = 0x0003,
        GetFirst = 0x0004,
        GetNext = 0x0005,
        Set = 0x0006,
        Reset = 0x0007,
        QuerySupport = 0x0008,

        XFerReady = 0x0101,
        CloseDSReq = 0x0102,
        CloseDSOK = 0x0103,
        DeviceEvent = 0x0104,

        CheckStatus = 0x0201,

        OpenDSM = 0x0301,
        CloseDSM = 0x0302,

        OpenDS = 0x0401,
        CloseDS = 0x0402,
        UserSelect = 0x0403,

        DisableDS = 0x0501,
        EnableDS = 0x0502,
        EnableDSUIOnly = 0x0503,

        ProcessEvent = 0x0601,

        EndXfer = 0x0701,
        StopFeeder = 0x0702,

        ChangeDirectory = 0x0801,
        CreateDirectory = 0x0802,
        Delete = 0x0803,
        FormatMedia = 0x0804,
        GetClose = 0x0805,
        GetFirstFile = 0x0806,
        GetInfo = 0x0807,
        GetNextFile = 0x0808,
        Rename = 0x0809,
        Copy = 0x080A,
        AutoCaptureDir = 0x080B,

        PassThru = 0x0901
    }

    [StructLayout(LayoutKind.Sequential, Pack = 2)]
    public struct Event
    {
        public IntPtr EventPtr;
        public Message Message;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 2)]
    public class Fix32
    {
        public short Whole;

        public ushort Frac;

        public Fix32(float f)
        {
            // http://www.dosadi.com/forums/archive/index.php?t-2534.html
            var val = (int)(f * 65536.0F);
            this.Whole = Convert.ToInt16(val >> 16);    // most significant 16 bits
            this.Frac = Convert.ToUInt16(val & 0xFFFF); // least
        }

        public float ToFloat()
        {
            var frac = Convert.ToSingle(this.Frac);
            return this.Whole + frac / 65536.0F;
        }
    }

    public enum FlipRotation
    {
        Book = 0,
        FanFold = 1
    }

    // all values are in Inches (2.54 cm)
    [StructLayout(LayoutKind.Sequential, Pack = 2)]
    public class Frame
    {
        public Fix32 Left;

        public Fix32 Top;

        public Fix32 Right;

        public Fix32 Bottom;
    }

    public enum Language : short
    {
        USA = 13
    }

    /// <summary>
    /// TW_VERSION
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 2, CharSet = CharSet.Ansi)]
    public struct TwainVersion
    {
        public short MajorNum;
        public short MinorNum;
        public Language Language;
        public Country Country;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 34)]
        public string Info;

        public TwainVersion Clone()
        {
            return (TwainVersion)MemberwiseClone();
        }
    }

    [StructLayout(LayoutKind.Sequential, Pack = 2, CharSet = CharSet.Ansi)]
    public class Identity
    {
        public int Id;
        public TwainVersion Version;
        public short ProtocolMajor;
        public short ProtocolMinor;
        public int SupportedGroups;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 34)]
        public string Manufacturer;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 34)]
        public string ProductFamily;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 34)]
        public string ProductName;

        public Identity Clone()
        {
            var id = (Identity)MemberwiseClone();
            id.Version = Version.Clone();
            return id;
        }
    }

    /// <summary>
    /// Twain spec ICAP_IMAGEFILEFORMAT values.
    /// </summary>
    public enum ImageFileFormat : ushort
    {
        Tiff = 0,       /* Tagged Image File Format     */
        Pict = 1,       /* Macintosh PICT               */
        Bmp = 2,        /* Windows Bitmap               */
        Xbm = 3,        /* X-Windows Bitmap             */
        Jpeg = 4,       /* JPEG File Interchange Format */
        Fpx = 5,        /* Flash Pix                    */
        TiffMulti = 6,  /* Multi-page tiff file         */
        Png = 7,
        Spiff = 8,
        Exif = 9,
        Pdf = 10,       /* 1.91 NB: this is not PDF/A */
        Jp2 = 11,       /* 1.91 */
        Jpx = 13,       /* 1.91 */
        Dejavu = 14,    /* 1.91 */
        PdfA = 15,      /* 2.0 Adobe PDF/A, Version 1*/
        PdfA2 = 16      /* 2.1 Adobe PDF/A, Version 2*/
    }

    [StructLayout(LayoutKind.Sequential, Pack = 2)]
    public class ImageInfo
    {
        public int XResolution;
        public int YResolution;
        public int ImageWidth;
        public int ImageLength;
        public short SamplesPerPixel;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
        public short[] BitsPerSample;
        public short BitsPerPixel;
        public short Planar;
        public short PixelType;
        public Compression Compression;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 2)]
    public class ImageLayout
    {
        public Frame Frame;

        public uint DocumentNumber;

        public uint PageNumber;

        public uint FrameNumber;
    }

    /// <summary>
    /// Twain spec ICAP_ORIENTATION values.
    /// </summary>
    public enum Orientation
    {
        /// <summary>
        /// Default is zero rotation, same as Portrait.
        /// </summary>
        Default = 0,
        Rotate90 = 1,
        Rotate180 = 2,
        Rotate270 = 3,
        Portrait = Default,
        Landscape = Rotate270,

        /// <summary>
        /// Following require Twain 2.0+. 
        /// </summary>
        Auto = 4,
        AutoText = 5,
        AutoPicture = 6
    }

    public enum PageType
    {
        None = 0,

        /// <summary>
        /// A4 ad A4Letter
        /// </summary>
        A4 = 1,

        /// <summary>
        /// JISB5 and B5Letter
        /// </summary>
        JISB5 = 2,

        UsLetter = 3,
        UsLegal = 4,
        A5 = 5,

        /// <summary>
        /// ISOB4 and B4
        /// </summary>
        ISOB4 = 6,

        /// <summary>
        /// ISOB6 and B6
        /// </summary>
        ISOB6 = 7,

        B = 8,
        UsLedger = 9,
        UsExecutive = 10,
        A3 = 11,

        /// <summary>
        /// ISOB3 and B3
        /// </summary>
        ISOB3 = 12,

        A6 = 13,
        C4 = 14,
        C5 = 15,
        C6 = 16,
        _4A0 = 17,
        _2A0 = 18,
        A0 = 19,
        A1 = 20,
        A2 = 21,
        A7 = 22,
        A8 = 23,
        A9 = 24,
        A10 = 25,
        ISOB0 = 26,
        ISOB1 = 27,
        ISOB2 = 28,
        ISOB5 = 29,
        ISOB7 = 30,
        ISOB8 = 31,
        ISOB9 = 32,
        ISOB10 = 33,
        JISB0 = 34,
        JISB1 = 35,
        JISB2 = 36,
        JISB3 = 37,
        JISB4 = 38,
        JISB6 = 39,
        JISB7 = 40,
        JISB8 = 41,
        JISB9 = 42,
        JISB10 = 43,
        C0 = 44,
        C1 = 45,
        C2 = 46,
        C3 = 47,
        C7 = 48,
        C8 = 49,
        C9 = 50,
        C10 = 51,
        UsStatement = 52,
        BusinessCard = 53,
        MaxSize = 54,
    }


    /// <summary>
    /// /* DAT_PENDINGXFERS. Used with MSG_ENDXFER to indicate additional data. */
    /// typedef struct {
    ///    TW_UINT16 Count;
    ///    union {
    ///       TW_UINT32 EOJ;
    ///       TW_UINT32 Reserved;
    ///    };
    /// } TW_PENDINGXFERS, FAR *pTW_PENDINGXFERS;
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 2)]
    public class PendingXfers
    {
        public short Count;
        public int Eoj;
    }

    public enum PixelType : short
    {
        BlackAndWhite = 0,
        Grey = 1,
        Rgb = 2,
        Palette = 3,
        Cmy = 4,
        Cmyk = 5,
        Yuv = 6,
        Yuvk = 7,
        CieXyz = 8,
        Lab = 9,
        Srgb = 10,
        Scrbg = 11,
        Infrared = 16
    }

    [StructLayout(LayoutKind.Sequential, Pack = 2)]
    public class Status
    {
        public ConditionCode ConditionCode;
        public short Reserved;
    }

    /// <summary>
    /// ICAP_XFERMECH values (Image Transfer)
    /// </summary>
    public enum TransferMechanism : short
    {
        Native = 0,

        File = 1,

        Memory = 2,

        // Value 3 was removed

        MemFile = 4
    }

    public enum TwainResult : short
    {
        Success = 0x0000,
        Failure = 0x0001,
        CheckStatus = 0x0002,
        Cancel = 0x0003,
        DSEvent = 0x0004,
        NotDSEvent = 0x0005,
        XferDone = 0x0006,
        EndOfList = 0x0007,
        InfoNotSupported = 0x0008,
        DataNotAvailable = 0x0009
    }

    /// <summary>
    /// /* DAT_CAPABILITY. Used by application to get/set capability from/in a data source. */
    /// typedef struct {
    ///    TW_UINT16  Cap; /* id of capability to set or get, e.g. CAP_BRIGHTNESS */
    ///    TW_UINT16  ConType; /* TWON_ONEVALUE, _RANGE, _ENUMERATION or _ARRAY   */
    ///    TW_HANDLE  hContainer; /* Handle to container of type Dat              */
    /// } TW_CAPABILITY, FAR * pTW_CAPABILITY;
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 2)]
    public class TwainCapability : IDisposable
    {
        Capabilities _capabilities;
        ContainerType _containerType;
        IntPtr _handle;
        object _value;

        protected TwainCapability(Capabilities capabilities, ContainerType containerType, object value)
        {
            _capabilities = capabilities;
            _containerType = containerType;
            _value = value;

            _handle = Kernel32Native.GlobalAlloc(GlobalAllocFlags.Handle, Marshal.SizeOf(value));

            IntPtr p = Kernel32Native.GlobalLock(_handle);

            try
            {
                Marshal.StructureToPtr(value, p, false);
            }
            finally
            {
                Kernel32Native.GlobalUnlock(_handle);
            }
        }

        ~TwainCapability()
        {
            Dispose(false);
        }

        public void ReadBackValue()
        {
            IntPtr p = Kernel32Native.GlobalLock(_handle);

            try
            {
                Marshal.PtrToStructure(p, _value);
            }
            finally
            {
                Kernel32Native.GlobalUnlock(_handle);
            }
        }

        public static TwainCapability From<TValue>(Capabilities capabilities, TValue value)
        {
            ContainerType containerType;
            Type structType = typeof(TValue);

            if (structType == typeof(CapabilityOneValue))
            {
                containerType = ContainerType.One;
            }
            else
            {
                throw new NotSupportedException("Unsupported type: " + structType);
            }

            return new TwainCapability(capabilities, containerType, value);
        }

        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_handle != IntPtr.Zero)
            {
                Kernel32Native.GlobalFree(_handle);
            }
        }
    }

    class TwainConstants
    {
        public const short ProtocolMajor = 1;
        public const short ProtocolMinor = 9;
    }

    
   


    public enum Units
    {
        Inches = 0,

        Centimeters = 1,

        Picas = 2,

        Points = 3,

        Twips = 4,

        Pixels = 5,

        Millimeters = 6
    }


    /// <summary>
    /// DAT_USERINTERFACE. Coordinates UI between application and data source. 
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 2)]
    public class UserInterface
    {
        /// <summary>
        /// TRUE if DS should bring up its UI
        /// </summary>
        public short ShowUI;				// bool is strictly 32 bit, so use short

        /// <summary>
        /// For Mac only - true if the DS's UI is modal
        /// </summary>
        public short ModalUI;

        /// <summary>
        /// For windows only - Application window handle
        /// </summary>
        public IntPtr ParentHand;
    }

    public class AreaSettings : INotifyPropertyChanged
    {
        private Units _units;
        public Units Units
        {
            get { return _units; }
            set
            {
                _units = value;
                OnPropertyChanged("Units");
            }
        }

        private float _top;
        public float Top
        {
            get { return _top; }
            private set
            {
                _top = value;
                OnPropertyChanged("Top");
            }
        }

        private float _left;
        public float Left
        {
            get { return _left; }
            private set
            {
                _left = value;
                OnPropertyChanged("Left");
            }
        }

        private float _bottom;
        public float Bottom
        {
            get { return _bottom; }
            private set
            {
                _bottom = value;
                OnPropertyChanged("Bottom");
            }
        }

        private float _right;
        public float Right
        {
            get { return _right; }
            private set
            {
                _right = value;
                OnPropertyChanged("Right");
            }
        }

        public AreaSettings(Units units, float top, float left, float bottom, float right)
        {
            _units = units;
            _top = top;
            _left = left;
            _bottom = bottom;
            _right = right;
        }

        #region INotifyPropertyChanged Members

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        #endregion
    }



    public abstract class CapabilityResult
    {
        public ConditionCode ConditionCode { get; set; }

        public TwainResult ErrorCode { get; set; }
    }

    public class BasicCapabilityResult : CapabilityResult
    {
        public int RawBasicValue { get; set; }

        public bool BoolValue { get { return RawBasicValue == 1; } }

        public short Int16Value { get { return (short)RawBasicValue; } }

        public int Int32Value { get { return RawBasicValue; } }
    }


    public class DataSource : IDisposable
    {
        Identity _applicationId;
        IWindowsMessageHook _messageHook;

        public DataSource(Identity applicationId, Identity sourceId, IWindowsMessageHook messageHook)
        {
            _applicationId = applicationId;
            SourceId = sourceId.Clone();
            _messageHook = messageHook;
        }

        ~DataSource()
        {
            Dispose(false);
        }

        public Identity SourceId { get; private set; }

        public void NegotiateTransferCount(ScanSettings scanSettings)
        {
            try
            {
                scanSettings.TransferCount = Capability.SetCapability(
                        Capabilities.XferCount,
                        scanSettings.TransferCount,
                        _applicationId,
                        SourceId);
            }
            catch
            {
                // Do nothing if the data source does not support the requested capability
            }
        }

        public void NegotiateFeeder(ScanSettings scanSettings)
        {

            try
            {
                if (scanSettings.UseDocumentFeeder.HasValue)
                {
                    Capability.SetCapability(Capabilities.FeederEnabled, scanSettings.UseDocumentFeeder.Value, _applicationId, SourceId);
                }
            }
            catch
            {
                // Do nothing if the data source does not support the requested capability
            }

            try
            {
                if (scanSettings.UseAutoFeeder.HasValue)
                {
                    Capability.SetCapability(Capabilities.AutoFeed, scanSettings.UseAutoFeeder == true && scanSettings.UseDocumentFeeder == true, _applicationId, SourceId);
                }
            }
            catch
            {
                // Do nothing if the data source does not support the requested capability
            }

            try
            {
                if (scanSettings.UseAutoScanCache.HasValue)
                {
                    Capability.SetCapability(Capabilities.AutoScan, scanSettings.UseAutoScanCache.Value, _applicationId, SourceId);
                }
            }
            catch
            {
                // Do nothing if the data source does not support the requested capability
            }

        }

        public PixelType GetPixelType(ScanSettings scanSettings)
        {
            switch (scanSettings.Resolution.ColourSetting)
            {
                case ColourSetting.BlackAndWhite:
                    return PixelType.BlackAndWhite;

                case ColourSetting.GreyScale:
                    return PixelType.Grey;

                case ColourSetting.Colour:
                    return PixelType.Rgb;
            }

            throw new NotImplementedException();
        }

        public short GetBitDepth(ScanSettings scanSettings)
        {
            switch (scanSettings.Resolution.ColourSetting)
            {
                case ColourSetting.BlackAndWhite:
                    return 1;

                case ColourSetting.GreyScale:
                    return 8;

                case ColourSetting.Colour:
                    return 16;
            }

            throw new NotImplementedException();
        }

        public bool PaperDetectable
        {
            get
            {
                try
                {
                    return Capability.GetBoolCapability(Capabilities.FeederLoaded, _applicationId, SourceId);
                }
                catch
                {
                    return false;
                }
            }
        }

        public bool SupportsDuplex
        {
            get
            {
                try
                {
                    var cap = new Capability(Capabilities.Duplex, TwainType.Int16, _applicationId, SourceId);
                    return ((Duplex)cap.GetBasicValue().Int16Value) != Duplex.None;
                }
                catch
                {
                    return false;
                }
            }
        }

        public void NegotiateColour(ScanSettings scanSettings)
        {
            try
            {
                Capability.SetBasicCapability(Capabilities.IPixelType, (ushort)GetPixelType(scanSettings), TwainType.UInt16, _applicationId, SourceId);
            }
            catch
            {
                // Do nothing if the data source does not support the requested capability
            }

            // TODO: Also set this for colour scanning
            try
            {
                if (scanSettings.Resolution.ColourSetting != ColourSetting.Colour)
                {
                    Capability.SetCapability(Capabilities.BitDepth, GetBitDepth(scanSettings), _applicationId, SourceId);
                }
            }
            catch
            {
                // Do nothing if the data source does not support the requested capability
            }

        }

        public void NegotiateResolution(ScanSettings scanSettings)
        {
            try
            {
                if (scanSettings.Resolution.Dpi.HasValue)
                {
                    int dpi = scanSettings.Resolution.Dpi.Value;
                    Capability.SetBasicCapability(Capabilities.XResolution, dpi, TwainType.Fix32, _applicationId, SourceId);
                    Capability.SetBasicCapability(Capabilities.YResolution, dpi, TwainType.Fix32, _applicationId, SourceId);
                }
            }
            catch
            {
                // Do nothing if the data source does not support the requested capability
            }
        }

        public void NegotiateDuplex(ScanSettings scanSettings)
        {
            try
            {
                if (scanSettings.UseDuplex.HasValue && SupportsDuplex)
                {
                    Capability.SetCapability(Capabilities.DuplexEnabled, scanSettings.UseDuplex.Value, _applicationId, SourceId);
                }
            }
            catch
            {
                // Do nothing if the data source does not support the requested capability
            }
        }

        public void NegotiateOrientation(ScanSettings scanSettings)
        {
            // Set orientation (default is portrait)
            try
            {
                var cap = new Capability(Capabilities.Orientation, TwainType.Int16, _applicationId, SourceId);
                if ((Orientation)cap.GetBasicValue().Int16Value != Orientation.Default)
                {
                    Capability.SetBasicCapability(Capabilities.Orientation, (ushort)scanSettings.Page.Orientation, TwainType.UInt16, _applicationId, SourceId);
                }
            }
            catch
            {
                // Do nothing if the data source does not support the requested capability
            }
        }

        /// <summary>
        /// Negotiates the size of the page.
        /// </summary>
        /// <param name="scanSettings">The scan settings.</param>
        public void NegotiatePageSize(ScanSettings scanSettings)
        {
            try
            {
                var cap = new Capability(Capabilities.Supportedsizes, TwainType.Int16, _applicationId, SourceId);
                if ((PageType)cap.GetBasicValue().Int16Value != PageType.UsLetter)
                {
                    Capability.SetBasicCapability(Capabilities.Supportedsizes, (ushort)scanSettings.Page.Size, TwainType.UInt16, _applicationId, SourceId);
                }
            }
            catch
            {
                // Do nothing if the data source does not support the requested capability
            }
        }

        /// <summary>
        /// Negotiates the automatic rotation capability.
        /// </summary>
        /// <param name="scanSettings">The scan settings.</param>
        public void NegotiateAutomaticRotate(ScanSettings scanSettings)
        {
            try
            {
                if (scanSettings.Rotation.AutomaticRotate)
                {
                    Capability.SetCapability(Capabilities.Automaticrotate, true, _applicationId, SourceId);
                }
            }
            catch
            {
                // Do nothing if the data source does not support the requested capability
            }
        }

        /// <summary>
        /// Negotiates the automatic border detection capability.
        /// </summary>
        /// <param name="scanSettings">The scan settings.</param>
        public void NegotiateAutomaticBorderDetection(ScanSettings scanSettings)
        {
            try
            {
                if (scanSettings.Rotation.AutomaticBorderDetection)
                {
                    Capability.SetCapability(Capabilities.Automaticborderdetection, true, _applicationId, SourceId);
                }
            }
            catch
            {
                // Do nothing if the data source does not support the requested capability
            }
        }

        /// <summary>
        /// Negotiates the indicator.
        /// </summary>
        /// <param name="scanSettings">The scan settings.</param>
        public void NegotiateProgressIndicator(ScanSettings scanSettings)
        {
            try
            {
                if (scanSettings.ShowProgressIndicatorUI.HasValue)
                {
                    Capability.SetCapability(Capabilities.Indicators, scanSettings.ShowProgressIndicatorUI.Value, _applicationId, SourceId);
                }
            }
            catch
            {
                // Do nothing if the data source does not support the requested capability
            }
        }

        public bool Open(ScanSettings settings)
        {
            OpenSource();

            if (settings.AbortWhenNoPaperDetectable && !PaperDetectable)
                throw new FeederEmptyException();

            // Set whether or not to show progress window
            NegotiateProgressIndicator(settings);
            NegotiateTransferCount(settings);
            NegotiateFeeder(settings);
            NegotiateDuplex(settings);

            if (settings.UseDocumentFeeder == true &&
                settings.Page != null)
            {
                NegotiatePageSize(settings);
                NegotiateOrientation(settings);
            }

            if (settings.Area != null)
            {
                NegotiateArea(settings);
            }

            if (settings.Resolution != null)
            {
                NegotiateColour(settings);
                NegotiateResolution(settings);
            }

            // Configure automatic rotation and image border detection
            if (settings.Rotation != null)
            {
                NegotiateAutomaticRotate(settings);
                NegotiateAutomaticBorderDetection(settings);
            }

            return Enable(settings);
        }

        private bool NegotiateArea(ScanSettings scanSettings)
        {
            var area = scanSettings.Area;

            if (area == null)
            {
                return false;
            }

            try
            {
                var cap = new Capability(Capabilities.IUnits, TwainType.Int16, _applicationId, SourceId);
                if ((Units)cap.GetBasicValue().Int16Value != area.Units)
                {
                    Capability.SetCapability(Capabilities.IUnits, (short)area.Units, _applicationId, SourceId);
                }
            }
            catch
            {
                // Do nothing if the data source does not support the requested capability
            }

            var imageLayout = new ImageLayout
            {
                Frame = new Frame
                {
                    Left = new Fix32(area.Left),
                    Top = new Fix32(area.Top),
                    Right = new Fix32(area.Right),
                    Bottom = new Fix32(area.Bottom)
                }
            };


            var result = Twain32Native.DsImageLayout(
                _applicationId,
                SourceId,
                DataGroup.Image,
                DataArgumentType.ImageLayout,
                Message.Set,
                imageLayout);

            if (result != TwainResult.Success)
            {
                throw new TwainException("DsImageLayout.GetDefault error", result);
            }

            return true;
        }

        public void OpenSource()
        {
            var result = Twain32Native.DsmIdentity(
                   _applicationId,
                   IntPtr.Zero,
                   DataGroup.Control,
                   DataArgumentType.Identity,
                   Message.OpenDS,
                   SourceId);

            if (result != TwainResult.Success)
            {
                throw new TwainException("Error opening data source", result);
            }
        }

        public bool Enable(ScanSettings settings)
        {
            UserInterface ui = new UserInterface();
            ui.ShowUI = (short)(settings.ShowTwainUI ? 1 : 0);
            ui.ModalUI = 1;
            ui.ParentHand = _messageHook.WindowHandle;

            var result = Twain32Native.DsUserInterface(
                _applicationId,
                SourceId,
                DataGroup.Control,
                DataArgumentType.UserInterface,
                Message.EnableDS,
                ui);

            if (result != TwainResult.Success)
            {
                Dispose();
                return false;
            }
            return true;
        }

        public static DataSource GetDefault(Identity applicationId, IWindowsMessageHook messageHook)
        {
            var defaultSourceId = new Identity();

            // Attempt to get information about the system default source

            var result = Twain32Native.DsmIdentity(
                applicationId,
                IntPtr.Zero,
                DataGroup.Control,
                DataArgumentType.Identity,
                Message.GetDefault,
                defaultSourceId);

            if (result != TwainResult.Success)
            {
                var status = DataSourceManager.GetConditionCode(applicationId, null);
                throw new TwainException("Error getting information about the default source: " + result, result, status);
            }

            return new DataSource(applicationId, defaultSourceId, messageHook);
        }

        public static DataSource UserSelected(Identity applicationId, IWindowsMessageHook messageHook)
        {
            var defaultSourceId = new Identity();

            // Show the TWAIN interface to allow the user to select a source
            Twain32Native.DsmIdentity(
                applicationId,
                IntPtr.Zero,
                DataGroup.Control,
                DataArgumentType.Identity,
                Message.UserSelect,
                defaultSourceId);

            return new DataSource(applicationId, defaultSourceId, messageHook);
        }

        public static List<DataSource> GetAllSources(Identity applicationId, IWindowsMessageHook messageHook)
        {
            var sources = new List<DataSource>();
            Identity id = new Identity();

            // Get the first source
            var result = Twain32Native.DsmIdentity(
                applicationId,
                IntPtr.Zero,
                DataGroup.Control,
                DataArgumentType.Identity,
                Message.GetFirst,
                id);

            if (result == TwainResult.EndOfList)
            {
                return sources;
            }
            else if (result != TwainResult.Success)
            {
                throw new TwainException("Error getting first source.", result);
            }
            else
            {
                sources.Add(new DataSource(applicationId, id, messageHook));
            }

            while (true)
            {
                // Get the next source
                result = Twain32Native.DsmIdentity(
                    applicationId,
                    IntPtr.Zero,
                    DataGroup.Control,
                    DataArgumentType.Identity,
                    Message.GetNext,
                    id);

                if (result == TwainResult.EndOfList)
                {
                    break;
                }
                else if (result != TwainResult.Success)
                {
                    throw new TwainException("Error enumerating sources.", result);
                }

                sources.Add(new DataSource(applicationId, id, messageHook));
            }

            return sources;
        }

        public static DataSource GetSource(string sourceProductName, Identity applicationId, IWindowsMessageHook messageHook)
        {
            // A little slower than it could be, if enumerating unnecessary sources is slow. But less code duplication.
            foreach (var source in GetAllSources(applicationId, messageHook))
            {
                if (sourceProductName.Equals(source.SourceId.ProductName, StringComparison.InvariantCultureIgnoreCase))
                {
                    return source;
                }
            }

            return null;
        }


        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected void Dispose(bool disposing)
        {
            if (disposing)
            {
                Close();
            }
        }

        public void Close()
        {
            if (SourceId.Id != 0)
            {
                UserInterface userInterface = new UserInterface();

                TwainResult result = Twain32Native.DsUserInterface(
                    _applicationId,
                    SourceId,
                    DataGroup.Control,
                    DataArgumentType.UserInterface,
                    Message.DisableDS,
                    userInterface);

                if (result != TwainResult.Failure)
                {
                    result = Twain32Native.DsmIdentity(
                        _applicationId,
                        IntPtr.Zero,
                        DataGroup.Control,
                        DataArgumentType.Identity,
                        Message.CloseDS,
                        SourceId);
                }
            }
        }
    }
    public class DataSourceManager : IDisposable
    {
        /// <summary>
        /// The logger for this class.
        /// </summary>
        static ILog log = LogManager.GetLogger(typeof(DataSourceManager));

        IWindowsMessageHook _messageHook;
        Event _eventMessage;

        public Identity ApplicationId { get; private set; }
        public DataSource DataSource { get; private set; }

        public DataSourceManager(Identity applicationId, IWindowsMessageHook messageHook)
        {
            // Make a copy of the identity in case it gets modified
            ApplicationId = applicationId.Clone();
            ScanningComplete += delegate { };
            TransferImage += delegate { };

            _messageHook = messageHook;
            _messageHook.FilterMessageCallback = FilterMessage;
            IntPtr windowHandle = _messageHook.WindowHandle;

            _eventMessage.EventPtr = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(WindowsMessage)));

            // Initialise the data source manager
            TwainResult result = Twain32Native.DsmParent(
                ApplicationId,
                IntPtr.Zero,
                DataGroup.Control,
                DataArgumentType.Parent,
                Message.OpenDSM,
                ref windowHandle);
          
            if (result == TwainResult.Success)
            {
                //according to the 2.0 spec (2-10) if (applicationId.SupportedGroups
                // | DataGroup.Dsm2) > 0 then we should call DM_Entry(id, 0, DG_Control, DAT_Entrypoint, MSG_Get, wh)
                //right here
                DataSource = DataSource.GetDefault(ApplicationId, _messageHook);
            }
            else
            {
                throw new TwainException("Error initialising DSM: " + result, result);
            }
        }

        ~DataSourceManager()
        {
            Dispose(false);
        }

        /// <summary>
        /// Notification that the scanning has completed.
        /// </summary>
        public event EventHandler<ScanningCompleteEventArgs> ScanningComplete;

        public event EventHandler<TransferImageEventArgs> TransferImage;

        public IWindowsMessageHook MessageHook { get { return _messageHook; } }

        public void StartScan(ScanSettings settings)
        {
            bool scanning = false;

            try
            {
                _messageHook.UseFilter = true;
                scanning = DataSource.Open(settings);
            }
            catch (TwainException e)
            {
                DataSource.Close();
                EndingScan();
                throw e;
            }
            finally
            {
                // Remove the message hook if scan setup failed
                if (!scanning)
                {
                    EndingScan();
                }
            }
        }

        protected IntPtr FilterMessage(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            if (DataSource.SourceId.Id == 0)
            {
                handled = false;
                return IntPtr.Zero;
            }

            int pos = User32Native.GetMessagePos();

            WindowsMessage message = new WindowsMessage();
            message.hwnd = hwnd;
            message.message = msg;
            message.wParam = wParam;
            message.lParam = lParam;
            message.time = User32Native.GetMessageTime();
            message.x = (short)pos;
            message.y = (short)(pos >> 16);

            Marshal.StructureToPtr(message, _eventMessage.EventPtr, false);
            _eventMessage.Message = 0;

            TwainResult result = Twain32Native.DsEvent(
                ApplicationId,
                DataSource.SourceId,
                DataGroup.Control,
                DataArgumentType.Event,
                Message.ProcessEvent,
                ref _eventMessage);

            if (result == TwainResult.NotDSEvent)
            {
                handled = false;
                return IntPtr.Zero;
            }

            switch (_eventMessage.Message)
            {
                case Message.XFerReady:
                    Exception exception = null;
                    try
                    {
                        TransferPictures();
                    }
                    catch (Exception e)
                    {
                        exception = e;
                    }
                    CloseDsAndCompleteScanning(exception);
                    break;

                case Message.CloseDS:
                case Message.CloseDSOK:
                case Message.CloseDSReq:
                    CloseDsAndCompleteScanning(null);
                    break;

                case Message.DeviceEvent:
                    break;

                default:
                    break;
            }

            handled = true;
            return IntPtr.Zero;
        }

        protected void TransferPictures()
        {
            if (DataSource.SourceId.Id == 0)
            {
                return;
            }

            PendingXfers pendingTransfer = new PendingXfers();
            TwainResult result;
            try
            {
                do
                {
                    pendingTransfer.Count = 0;
                    IntPtr hbitmap = IntPtr.Zero;

                    // Get the image info
                    ImageInfo imageInfo = new ImageInfo();
                    result = Twain32Native.DsImageInfo(
                        ApplicationId,
                        DataSource.SourceId,
                        DataGroup.Image,
                        DataArgumentType.ImageInfo,
                        Message.Get,
                        imageInfo);

                    if (result != TwainResult.Success)
                    {
                        DataSource.Close();
                        break;
                    }

                    // Transfer the image from the device
                    result = Twain32Native.DsImageTransfer(
                        ApplicationId,
                        DataSource.SourceId,
                        DataGroup.Image,
                        DataArgumentType.ImageNativeXfer,
                        Message.Get,
                        ref hbitmap);

                    if (result != TwainResult.XferDone)
                    {
                        DataSource.Close();
                        break;
                    }

                    // End pending transfers
                    result = Twain32Native.DsPendingTransfer(
                        ApplicationId,
                        DataSource.SourceId,
                        DataGroup.Control,
                        DataArgumentType.PendingXfers,
                        Message.EndXfer,
                        pendingTransfer);

                    if (result != TwainResult.Success)
                    {
                        DataSource.Close();
                        break;
                    }

                    if (hbitmap == IntPtr.Zero)
                    {
                        log.Warn("Transfer complete but bitmap pointer is still null.");
                    }
                    else
                    {
                        using (var renderer = new BitmapRenderer(hbitmap))
                        {
                            TransferImageEventArgs args = new TransferImageEventArgs(renderer.RenderToBitmap(), pendingTransfer.Count != 0);
                            TransferImage(this, args);
                            if (!args.ContinueScanning)
                                break;
                        }
                    }
                }
                while (pendingTransfer.Count != 0);
            }
            finally
            {
                // Reset any pending transfers
                result = Twain32Native.DsPendingTransfer(
                    ApplicationId,
                    DataSource.SourceId,
                    DataGroup.Control,
                    DataArgumentType.PendingXfers,
                    Message.Reset,
                    pendingTransfer);

                if (pendingTransfer.Count == 0)
                {
                    DataSource.Close();
                }
            }
        }

        protected void CloseDsAndCompleteScanning(Exception exception)
        {
            EndingScan();
            DataSource.Close();
            try
            {
                ScanningComplete(this, new ScanningCompleteEventArgs(exception));
            }
            catch
            {
            }
        }

        protected void EndingScan()
        {
            _messageHook.UseFilter = false;
        }

        public void SelectSource()
        {
            DataSource.Dispose();
            DataSource = DataSource.UserSelected(ApplicationId, _messageHook);
        }

        public void SelectSource(DataSource dataSource)
        {
            DataSource.Dispose();
            DataSource = dataSource;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            Marshal.FreeHGlobal(_eventMessage.EventPtr);

            if (disposing)
            {
                DataSource.Dispose();

                IntPtr windowHandle = _messageHook.WindowHandle;

                if (ApplicationId.Id != 0)
                {
                    // Close down the data source manager
                    Twain32Native.DsmParent(
                        ApplicationId,
                        IntPtr.Zero,
                        DataGroup.Control,
                        DataArgumentType.Parent,
                        Message.CloseDSM,
                        ref windowHandle);
                }

                ApplicationId.Id = 0;
            }
        }

        public static ConditionCode GetConditionCode(Identity applicationId, Identity sourceId)
        {
            Status status = new Status();

            Twain32Native.DsmStatus(
                applicationId,
                sourceId,
                DataGroup.Control,
                DataArgumentType.Status,
                Message.Get,
                status);

            return status.ConditionCode;
        }

        public static readonly Identity DefaultApplicationId = new Identity()
        {
            Id = BitConverter.ToInt32(Guid.NewGuid().ToByteArray(), 0),
            Version = new TwainVersion()
            {
                MajorNum = 1,
                MinorNum = 1,
                Language = Language.USA,
                Country = Country.USA,
                Info = Assembly.GetExecutingAssembly().FullName
            },
            ProtocolMajor = TwainConstants.ProtocolMajor,
            ProtocolMinor = TwainConstants.ProtocolMinor,
            SupportedGroups = (int)(DataGroup.Image | DataGroup.Control),
            Manufacturer = "TwainDotNet",
            ProductFamily = "TwainDotNet",
            ProductName = "TwainDotNet",
        };
    }
    public class Capability
    {
        /// <summary>
        /// The logger for this class.
        /// </summary>
        static ILog log = LogManager.GetLogger(typeof(Capability));

        Identity _applicationId;
        Identity _sourceId;
        Capabilities _capability;
        TwainType _twainType;

        public Capability(Capabilities capability, TwainType twainType, Identity applicationId, Identity sourceId)
        {
            _capability = capability;
            _applicationId = applicationId;
            _sourceId = sourceId;
            _twainType = twainType;
        }

        public BasicCapabilityResult GetBasicValue()
        {
            var oneValue = new CapabilityOneValue(_twainType, 0);
            var twainCapability = TwainCapability.From(_capability, oneValue);

            var result = Twain32Native.DsCapability(
                    _applicationId,
                    _sourceId,
                    DataGroup.Control,
                    DataArgumentType.Capability,
                    Message.Get,
                    twainCapability);

            if (result != TwainResult.Success)
            {
                var conditionCode = GetStatus();

                log.Debug(string.Format("Failed to get capability:{0} reason: {1}",
                    _capability, conditionCode));

                return new BasicCapabilityResult()
                {
                    ConditionCode = conditionCode,
                    ErrorCode = result
                };
            }

            twainCapability.ReadBackValue();

            return new BasicCapabilityResult()
            {
                RawBasicValue = oneValue.Value
            };
        }

        protected ConditionCode GetStatus()
        {
            return DataSourceManager.GetConditionCode(_applicationId, _sourceId);
        }

        public void SetValue(short value)
        {
            SetValue<short>(value);
        }

        protected void SetValue<T>(T value)
        {
            log.Debug(string.Format("Attempting to set capabilities:{0}, value:{1}, type:{1}",
                _capability, value, _twainType));

            int rawValue = Convert.ToInt32(value);
            var oneValue = new CapabilityOneValue(_twainType, rawValue);
            var twainCapability = TwainCapability.From(_capability, oneValue);

            TwainResult result = Twain32Native.DsCapability(
                    _applicationId,
                    _sourceId,
                    DataGroup.Control,
                    DataArgumentType.Capability,
                    Message.Set,
                    twainCapability);

            if (result != TwainResult.Success)
            {
                log.Debug(string.Format("Failed to set capabilities:{0}, value:{1}, type:{1}, result:{2}",
                    _capability, value, _twainType, result));

                if (result == TwainResult.Failure)
                {
                    var conditionCode = GetStatus();

                    log.Error(string.Format("Failed to set capabilites:{0} reason: {1}",
                        _capability, conditionCode));

                    throw new TwainException("Failed to set capability.", result, conditionCode);
                }
                else if (result == TwainResult.CheckStatus)
                {
                    log.Debug("Value changed but not to requested value");
                }
                else
                {
                    throw new TwainException("Failed to set capability.", result);
                }
            }
            else
            {
                log.Debug("Set capabilities successfully");
            }
        }

        public static short SetCapability(Capabilities capability, short value, Identity applicationId,
            Identity sourceId)
        {
            return (short)SetBasicCapability(capability, value, TwainType.Int16, applicationId, sourceId);
        }

        public static int SetBasicCapability(Capabilities capability, int rawValue, TwainType twainType, Identity applicationId,
            Identity sourceId)
        {
            var c = new Capability(capability, twainType, applicationId, sourceId);
            var capResult = c.GetBasicValue();

            // Check that the device supports the capability
            if (capResult.ConditionCode != ConditionCode.Success)
            {
                throw new TwainException(string.Format("Unsupported capability {0}", capability),
                    capResult.ErrorCode, capResult.ConditionCode);
            }

            if (capResult.RawBasicValue == rawValue)
            {
                // Value is already set
                return rawValue;
            }

            // TODO: Check the set of Available Values that are supported by the Source for that
            // capability.

            //if (value in set of available values)
            //{
            c.SetValue(rawValue);
            //}

            // Verify that the new values have been accepted by the Source.
            capResult = c.GetBasicValue();

            // Check that the device supports the capability
            if (capResult.ConditionCode != ConditionCode.Success)
            {
                throw new TwainException(string.Format("Unexpected failure verifying capability {0}", capability),
                    capResult.ErrorCode, capResult.ConditionCode);
            }

            return capResult.RawBasicValue;
        }

        public static void SetCapability(Capabilities capability, bool value, Identity applicationId,
            Identity sourceId)
        {
            var c = new Capability(capability, TwainType.Bool, applicationId, sourceId);
            var capResult = c.GetBasicValue();

            // Check that the device supports the capability
            if (capResult.ConditionCode != ConditionCode.Success)
            {
                throw new TwainException(string.Format("Unsupported capability {0}", capability),
                    capResult.ErrorCode, capResult.ConditionCode);
            }

            if (capResult.BoolValue == value)
            {
                // Value is already set
                return;
            }

            c.SetValue(value);

            // Verify that the new values have been accepted by the Source.
            capResult = c.GetBasicValue();

            // Check that the device supports the capability
            if (capResult.ConditionCode != ConditionCode.Success)
            {
                throw new TwainException(string.Format("Unexpected failure verifying capability {0}", capability),
                    capResult.ErrorCode, capResult.ConditionCode);
            }
            else if (capResult.BoolValue != value)
            {
                throw new TwainException(string.Format("Failed to set value for capability {0}", capability),
                    capResult.ErrorCode, capResult.ConditionCode);
            }
        }

        public static bool GetBoolCapability(Capabilities capability, Identity applicationId,
            Identity sourceId)
        {
            var c = new Capability(capability, TwainType.Int16, applicationId, sourceId);
            var capResult = c.GetBasicValue();

            // Check that the device supports the capability
            if (capResult.ConditionCode != ConditionCode.Success)
            {
                throw new TwainException(string.Format("Unsupported capability {0}", capability),
                    capResult.ErrorCode, capResult.ConditionCode);
            }

            return capResult.BoolValue;
        }
    }


    public class Diagnostics
    {
        public Diagnostics(IWindowsMessageHook messageHook)
        {
            using (var dataSourceManager = new DataSourceManager(DataSourceManager.DefaultApplicationId, messageHook))
            {
                dataSourceManager.SelectSource();

                var dataSource = dataSourceManager.DataSource;
                dataSource.OpenSource();
                string res = "";
                foreach (Capabilities capability in Enum.GetValues(typeof(Capabilities)))
                {
                    try
                    {
                        var result = Capability.GetBoolCapability(capability, dataSourceManager.ApplicationId, dataSource.SourceId);

                       res+=string.Format("{0}: {1}\n", capability, result);
                    }
                    catch (TwainException e)
                    {
                        res += string.Format("{0}: {1} {2} {3}\n", capability, e.Message, e.ReturnCode, e.ConditionCode);
                    }

                }

            }
        }
    }

    public class FeederEmptyException : TwainException
    {
        public FeederEmptyException()
            : this(null, null)
        {
        }

        public FeederEmptyException(string message)
            : this(message, null)
        {
        }

        protected FeederEmptyException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }

        public FeederEmptyException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }


    public interface IWindowsMessageHook
    {
        /// <summary>
        /// Gets or sets if the message filter is in use.
        /// </summary>
        bool UseFilter { get; set; }

        /// <summary>
        /// The delegate to call back then the filter is in place and a message arrives.
        /// </summary>
        FilterMessage FilterMessageCallback { get; set; }

        /// <summary>
        /// The handle to the window that is performing the scanning.
        /// </summary>
        IntPtr WindowHandle { get; }
    }

    public delegate IntPtr FilterMessage(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled);

    /// <summary>
    /// Page settings used for automatic document feeders
    /// scanning.
    /// </summary>
    public class PageSettings : INotifyPropertyChanged
    {
        Orientation _orientation;

        /// <summary>
        /// Gets or sets the page orientation.
        /// </summary>
        /// <value>The orientation.</value>
        public Orientation Orientation
        {
            get { return _orientation; }
            set
            {
                if (value != _orientation)
                {
                    _orientation = value;
                    OnPropertyChanged("Orientation");
                }
            }
        }

        PageType _size;
        /// <summary>
        /// Gets or sets the Page Size.
        /// </summary>
        /// <value>The size.</value>
        public PageType Size
        {
            get { return _size; }
            set
            {
                if (value != _size)
                {
                    _size = value;
                    OnPropertyChanged("PaperSize");
                }
            }
        }

        public PageSettings()
        {
            Size = PageType.UsLetter;
            Orientation = Orientation.Default;
        }

        /// <summary>
        /// Default Page setup - A4 Letter and Portrait orientation
        /// </summary>
        public static readonly PageSettings Default = new PageSettings()
        {
            Size = PageType.UsLetter,
            Orientation = Orientation.Default
        };

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }

    public class ResolutionSettings : INotifyPropertyChanged
    {
        int? _dpi;

        /// <summary>
        /// The DPI to scan at. Set to null to use the current default setting.
        /// </summary>
        public int? Dpi
        {
            get { return _dpi; }
            set
            {
                if (value != _dpi)
                {
                    _dpi = value;
                    OnPropertyChanged("Dpi");
                }
            }
        }

        ColourSetting _colourSettings;

        /// <summary>
        /// The colour settings to use.
        /// </summary>
        public ColourSetting ColourSetting
        {
            get { return _colourSettings; }
            set
            {
                if (value != _colourSettings)
                {
                    _colourSettings = value;
                    OnPropertyChanged("ColourSetting");
                }
            }
        }

        #region INotifyPropertyChanged Members

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        #endregion

        /// <summary>
        /// Fax quality resolution.
        /// </summary>
        public static readonly ResolutionSettings Fax = new ResolutionSettings()
        {
            Dpi = 200,
            ColourSetting = ColourSetting.BlackAndWhite
        };

        /// <summary>
        /// Photocopier quality resolution.
        /// </summary>
        public static readonly ResolutionSettings Photocopier = new ResolutionSettings()
        {
            Dpi = 300,
            ColourSetting = ColourSetting.GreyScale
        };

        /// <summary>
        /// Colour photocopier quality resolution.
        /// </summary>
        public static readonly ResolutionSettings ColourPhotocopier = new ResolutionSettings()
        {
            Dpi = 300,
            ColourSetting = ColourSetting.Colour
        };
    }

    public enum ColourSetting
    {
        BlackAndWhite,

        GreyScale,

        Colour
    }


    /// <summary>
    /// Settings for hardware image rotation.  Includes
    /// hardware deskewing detection
    /// </summary>
    public class RotationSettings : INotifyPropertyChanged
    {
        private bool automaticDeskew;
        private bool automaticBorderDetection;
        private bool automaticRotate;
        private FlipRotation flipSideRotation;

        /// <summary>
        /// Gets or sets a value indicating whether [automatic deskew].
        /// </summary>
        /// <value><c>true</c> if [automatic deskew]; otherwise, <c>false</c>.</value>
        public bool AutomaticDeskew
        {
            get
            {
                return automaticDeskew;
            }
            set
            {
                if (value != automaticDeskew)
                {
                    automaticDeskew = value;
                    OnPropertyChanged("AutomaticDeskew");
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [automatic border detection].
        /// </summary>
        /// <value>
        /// 	<c>true</c> if [automatic border detection]; otherwise, <c>false</c>.
        /// </value>
        public bool AutomaticBorderDetection
        {
            get
            {
                return automaticBorderDetection;
            }
            set
            {
                if (value != automaticBorderDetection)
                {
                    automaticBorderDetection = value;
                    OnPropertyChanged("AutomaticBorderDetection");
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [automatic rotate].
        /// </summary>
        /// <value><c>true</c> if [automatic rotate]; otherwise, <c>false</c>.</value>
        public bool AutomaticRotate
        {
            get
            {
                return automaticRotate;
            }
            set
            {
                if (value != automaticRotate)
                {
                    automaticRotate = value;
                    OnPropertyChanged("AutomaticRotate");
                }
            }
        }

        /// <summary>
        /// Gets or sets the flip side rotation.
        /// </summary>
        /// <value>The flip side rotation.</value>
        public FlipRotation FlipSideRotation
        {
            get
            {
                return flipSideRotation;
            }
            set
            {
                if (value != flipSideRotation)
                {
                    flipSideRotation = value;
                    OnPropertyChanged("FlipSideRotation");
                }
            }
        }



        #region INotifyPropertyChanged Members

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        #endregion
    }


    public class ScanningCompleteEventArgs : EventArgs
    {
        public Exception Exception { get; private set; }

        public ScanningCompleteEventArgs(Exception exception)
        {
            this.Exception = exception;
        }
    }

    public class ScanSettings : INotifyPropertyChanged
    {
        public ScanSettings()
        {
            ShouldTransferAllPages = true;
        }

        bool _showTwainUI;

        /// <summary>
        /// Indicates if the TWAIN/driver user interface should be used to pick the scan settings.
        /// </summary>
        public bool ShowTwainUI
        {
            get { return _showTwainUI; }
            set
            {
                if (value != _showTwainUI)
                {
                    _showTwainUI = value;
                    OnPropertyChanged("ShowTwainUI");
                }
            }
        }

        bool? _showProgressIndicatorUI;

        /// <summary>
        /// Gets or sets a value indicating whether [show progress indicator ui].
        /// If TRUE, the Source will display a progress indicator during acquisition and transfer, regardless of whether the Source's user interface is active. 
        /// If FALSE, the progress indicator will be suppressed if the Source's user interface is inactive.
        /// The Source will continue to display device-specific instructions and error messages even with the Source user interface and progress indicators turned off. 
        /// </summary>
        /// <value><c>true</c> if [show progress indicator ui]; otherwise, <c>false</c>.</value>
        public bool? ShowProgressIndicatorUI
        {
            get { return _showProgressIndicatorUI; }
            set
            {
                if (value != _showProgressIndicatorUI)
                {
                    _showProgressIndicatorUI = value;
                    OnPropertyChanged("ShowProgressIndicatorUI");
                }
            }
        }

        bool? _useDocumentFeeder;

        /// <summary>
        /// Indicates if the automatic document feeder (ADF) should be the source of the document(s) to scan.
        /// </summary>
        public bool? UseDocumentFeeder
        {
            get { return _useDocumentFeeder; }
            set
            {
                if (value != _useDocumentFeeder)
                {
                    _useDocumentFeeder = value;
                    OnPropertyChanged("UseDocumentFeeder");
                }
            }
        }

        bool? _useAutoFeeder;

        /// <summary>
        /// Indicates if the automatic document feeder (ADF) should continue feeding document(s) to scan after the negotiated number of pages are acquired.
        /// UseDocumentFeeder must be true
        /// </summary>
        public bool? UseAutoFeeder
        {
            get { return _useAutoFeeder; }
            set
            {
                if (value != _useAutoFeeder)
                {
                    _useAutoFeeder = value;
                    OnPropertyChanged("UseAutoFeeder");
                }
            }
        }

        bool? _useAutoScanCache;

        /// <summary>
        /// Indicates if the source should continue scanning without waiting for the application to request the image transfers.
        /// </summary>
        public bool? UseAutoScanCache
        {
            get { return _useAutoScanCache; }
            set
            {
                if (value != _useAutoScanCache)
                {
                    _useAutoScanCache = value;
                    OnPropertyChanged("UseAutoScanCache");
                }
            }
        }

        bool _abortWhenNoPaperDetectable;

        /// <summary>
        /// Indicates if the transfer should not start when no paper was detected (e.g. by the ADF).
        /// </summary>
        public bool AbortWhenNoPaperDetectable
        {
            get { return _abortWhenNoPaperDetectable; }
            set
            {
                if (value != _abortWhenNoPaperDetectable)
                {
                    _abortWhenNoPaperDetectable = value;
                    OnPropertyChanged("AbortWhenNoPaperDetectable");
                }
            }
        }

        short _transferCount;

        /// <summary>
        /// The number of pages to transfer.
        /// </summary>
        public short TransferCount
        {
            get { return _transferCount; }
            set
            {
                if (value != _transferCount)
                {
                    _transferCount = value;
                    OnPropertyChanged("TransferCount");
                    OnPropertyChanged("ShouldTransferAllPages");
                }
            }
        }

        /// <summary>
        /// Indicates if all pages should be transferred.
        /// </summary>
        public bool ShouldTransferAllPages
        {
            get { return _transferCount == TransferAllPages; }
            set { TransferCount = value ? TransferAllPages : (short)1; }
        }

        ResolutionSettings _resolution;

        /// <summary>
        /// The resolution settings. Set null to use current defaults.
        /// </summary>
        public ResolutionSettings Resolution
        {
            get { return _resolution; }
            set
            {
                if (value != _resolution)
                {
                    _resolution = value;
                    OnPropertyChanged("Resolution");
                }
            }
        }

        bool? _useDuplex;

        /// <summary>
        /// Whether to use duplexing, if supported.
        /// </summary>
        public bool? UseDuplex
        {
            get { return _useDuplex; }
            set
            {
                if (value != _useDuplex)
                {
                    _useDuplex = value;
                    OnPropertyChanged("UseDuplex");
                }
            }
        }

        AreaSettings _area;

        public AreaSettings Area
        {
            get { return _area; }
            set
            {
                if (value != _area)
                {
                    _area = value;
                    OnPropertyChanged("Area");
                }
            }
        }

        PageSettings _page;

        /// <summary>
        /// The page / paper settings. Set null to use current defaults.
        /// </summary>
        /// <value>The page.</value>
        public PageSettings Page
        {
            get { return _page; }
            set
            {
                if (value != _page)
                {
                    _page = value;
                    OnPropertyChanged("Page");
                }
            }
        }

        RotationSettings _rotation;

        /// <summary>
        /// Gets or sets the rotation.
        /// </summary>
        /// <value>The rotation.</value>
        public RotationSettings Rotation
        {
            get { return _rotation; }
            set
            {
                if (value != _rotation)
                {
                    _rotation = value;
                    OnPropertyChanged("Rotation");
                }
            }
        }


        #region INotifyPropertyChanged Members

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        #endregion

        /// <summary>
        /// Default scan settings.
        /// </summary>
        public static readonly ScanSettings Default = new ScanSettings()
        {
            Resolution = ResolutionSettings.ColourPhotocopier,
            Page = PageSettings.Default,
            Rotation = new RotationSettings()
        };

        /// <summary>
        /// The value to set to scan all available pages.
        /// </summary>
        public const short TransferAllPages = -1;
    }


    public class TransferImageEventArgs : EventArgs
    {
        public Bitmap Image { get; private set; }
        public bool ContinueScanning { get; set; }

        public TransferImageEventArgs(Bitmap image, bool continueScanning)
        {
            this.Image = image;
            this.ContinueScanning = continueScanning;
        }
    }


    public class Twain
    {
        DataSourceManager _dataSourceManager;

        public Twain(IWindowsMessageHook messageHook)
        {
            ScanningComplete += delegate { };
            TransferImage += delegate { };

            _dataSourceManager = new DataSourceManager(DataSourceManager.DefaultApplicationId, messageHook);
            _dataSourceManager.ScanningComplete += delegate(object sender, ScanningCompleteEventArgs args)
            {
                ScanningComplete(this, args);
            };
            _dataSourceManager.TransferImage += delegate(object sender, TransferImageEventArgs args)
            {
                TransferImage(this, args);
            };
        }

        /// <summary>
        /// Notification that the scanning has completed.
        /// </summary>
        public event EventHandler<ScanningCompleteEventArgs> ScanningComplete;

        public event EventHandler<TransferImageEventArgs> TransferImage;

        /// <summary>
        /// Starts scanning.
        /// </summary>
        public void StartScanning(ScanSettings settings)
        {
            _dataSourceManager.StartScan(settings);
        }

        /// <summary>
        /// Shows a dialog prompting the use to select the source to scan from.
        /// </summary>
        public void SelectSource()
        {
            _dataSourceManager.SelectSource();
        }

        /// <summary>
        /// Selects a source based on the product name string.
        /// </summary>
        /// <param name="sourceName">The source product name.</param>
        public void SelectSource(string sourceName)
        {
            var source = DataSource.GetSource(
                sourceName,
                _dataSourceManager.ApplicationId,
                _dataSourceManager.MessageHook);

            _dataSourceManager.SelectSource(source);
        }

        /// <summary>
        /// Gets the product name for the default source.
        /// </summary>
        public string DefaultSourceName
        {
            get
            {
                using (var source = DataSource.GetDefault(_dataSourceManager.ApplicationId, _dataSourceManager.MessageHook))
                {
                    return source.SourceId.ProductName;
                }
            }
        }

        /// <summary>
        /// Gets a list of source product names.
        /// </summary>
        public IList<string> SourceNames
        {
            get
            {
                var result = new List<string>();
                var sources = DataSource.GetAllSources(
                    _dataSourceManager.ApplicationId,
                    _dataSourceManager.MessageHook);

                foreach (var source in sources)
                {
                    result.Add(source.SourceId.ProductName);
                    source.Dispose();
                }

                return result;
            }
        }
    }

    public class TwainException : ApplicationException
    {
        public TwainException()
            : this(null, null)
        {
        }

        public TwainException(string message)
            : this(message, null)
        {
        }

        public TwainException(string message, TwainResult returnCode)
            : this(message, null)
        {
            ReturnCode = returnCode;
        }

        public TwainException(string message, TwainResult returnCode, ConditionCode conditionCode)
            : this(message, null)
        {
            ReturnCode = returnCode;
            ConditionCode = conditionCode;
        }

        protected TwainException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }

        public TwainException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public TwainResult? ReturnCode { get; private set; }

        public ConditionCode? ConditionCode { get; private set; }
    }

    /// <summary>
    /// A windows message hook for WinForms applications.
    /// </summary>
    public class WinFormsWindowMessageHook : IWindowsMessageHook, IMessageFilter
    {
        IntPtr _windowHandle;
        bool _usingFilter;

        public WinFormsWindowMessageHook(Form window)
        {
            _windowHandle = window.Handle;
        }

        public bool PreFilterMessage(ref System.Windows.Forms.Message m)
        {
            if (FilterMessageCallback != null)
            {
                bool handled = false;
                FilterMessageCallback(m.HWnd, m.Msg, m.WParam, m.LParam, ref handled);
                return handled;
            }

            return false;
        }

        public IntPtr WindowHandle { get { return _windowHandle; } }

        public bool UseFilter
        {
            get
            {
                return _usingFilter;
            }
            set
            {
                if (!_usingFilter && value == true)
                {
                    Application.AddMessageFilter(this);
                    _usingFilter = true;
                }

                if (_usingFilter && value == false)
                {
                    Application.RemoveMessageFilter(this);
                    _usingFilter = false;
                }
            }
        }

        public FilterMessage FilterMessageCallback { get; set; }
    }
}