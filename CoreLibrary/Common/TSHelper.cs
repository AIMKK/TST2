using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace CoreLibrary
{
    //////remoteClientName = TSManager.GetRemoteClientName();
    ////////NetWorkUtilsLog.WriteLog("vtemp username:" + remoteClientName);
    //////if (string.IsNullOrEmpty(remoteClientName))
    //////{
    //////    ip = "";
    //////}
    //////else
    //////{
    //////    ip = TSManager.GetRemoteClientIP();
    //////}

    /// <summary>
    /// 
    /// </summary>
    public class TSHelper
    {
        /// <summary>
        /// WTSOpenServer Opens the handle to Server
        /// </summary>
        /// <param name="pServerName"></param>
        /// <returns></returns>
        [DllImport("wtsapi32.dll")]
        static extern IntPtr WTSOpenServer([MarshalAs(UnmanagedType.LPStr)] String pServerName);

        /// <summary>
        /// WTSCloseServer, close the handle to server
        /// </summary>
        /// <param name="hServer"></param>
        [DllImport("wtsapi32.dll")]
        static extern void WTSCloseServer(IntPtr hServer);

        /// <summary>
        /// WTSEnumerateSessions, Enumerate Sessions on a Server
        /// </summary>
        /// <param name="hServer"></param>
        /// <param name="Reserved"></param>
        /// <param name="Version"></param>
        /// <param name="ppSessionInfo"></param>
        /// <param name="pCount"></param>
        /// <returns></returns>
        [DllImport("wtsapi32.dll")]
        static extern Int32 WTSEnumerateSessions(
            IntPtr hServer,
            [MarshalAs(UnmanagedType.U4)] Int32 Reserved,
            [MarshalAs(UnmanagedType.U4)] Int32 Version,
            ref IntPtr ppSessionInfo,
            [MarshalAs(UnmanagedType.U4)] ref Int32 pCount);

        /// <summary>
        /// Structure holding TS Session
        /// </summary>
        public struct TsSession
        {
            public int SessionID;
            public string StationName;
            public string ConnectionState;
            public string UserName;
            public string DomainName;
            public string ClientName;
            public string IpAddress;
        }

        public const int WTS_CURRENT_SESSION = -1;


        /// <summary>
        /// WTSQuerySessionInformation - Query Session Information
        /// </summary>
        /// <param name="hServer"></param>
        /// <param name="sessionId"></param>
        /// <param name="wtsInfoClass"></param>
        /// <param name="ppBuffer"></param>
        /// <param name="pBytesReturned"></param>
        /// <returns></returns>
        [DllImport("Wtsapi32.dll")]
        public static extern bool WTSQuerySessionInformation(
            System.IntPtr hServer, int sessionId, WTS_INFO_CLASS wtsInfoClass, out System.IntPtr ppBuffer, out uint pBytesReturned);

        /// <summary>
        /// Free WTS Memory
        /// </summary>
        /// <param name="pMemory"></param>
        [DllImport("wtsapi32.dll")]
        static extern void WTSFreeMemory(IntPtr pMemory);

        //Structure for TS Client IP Address 
        [StructLayout(LayoutKind.Sequential)]
        private struct WTS_CLIENT_ADDRESS
        {
            public int AddressFamily;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] Address;
        }
        //Structure for Terminal Service Session Client Display
        [StructLayout(LayoutKind.Sequential)]
        private struct WTS_CLIENT_DISPLAY
        {
            public int iHorizontalResolution;
            public int iVerticalResolution;
            //1 = The display uses 4 bits per pixel for a maximum of 16 colors.
            //2 = The display uses 8 bits per pixel for a maximum of 256 colors.
            //4 = The display uses 16 bits per pixel for a maximum of 2^16 colors.
            //8 = The display uses 3-byte RGB values for a maximum of 2^24 colors.
            //16 = The display uses 15 bits per pixel for a maximum of 2^15 colors.
            public int iColorDepth;
        }
        /// <summary>
        /// Session Info
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        private struct WTS_SESSION_INFO
        {
            public Int32 SessionID;

            [MarshalAs(UnmanagedType.LPStr)]
            public String pWinStationName;

            public WTS_CONNECTSTATE_CLASS State;
        }

        #region Enumurations

        public enum WTS_CONNECTSTATE_CLASS
        {
            WTSActive,
            WTSConnected,
            WTSConnectQuery,
            WTSShadow,
            WTSDisconnected,
            WTSIdle,
            WTSListen,
            WTSReset,
            WTSDown,
            WTSInit
        }

        public enum WTS_INFO_CLASS
        {
            WTSInitialProgram,
            WTSApplicationName,
            WTSWorkingDirectory,
            WTSOEMId,
            WTSSessionId,
            WTSUserName,
            WTSWinStationName,
            WTSDomainName,
            WTSConnectState,
            WTSClientBuildNumber,
            WTSClientName,
            WTSClientDirectory,
            WTSClientProductId,
            WTSClientHardwareId,
            WTSClientAddress,
            WTSClientDisplay,
            WTSClientProtocolType
        }

        #endregion

        /// <summary>
        /// Wrapper to Open a Server
        /// </summary>
        /// <param name="Name"></param>
        /// <returns></returns>
        public static IntPtr OpenServer(String Name)
        {
            IntPtr server = WTSOpenServer(Name);
            return server;
        }

        /// <summary>
        /// Wrapper to Close Server
        /// </summary>
        /// <param name="ServerHandle"></param>
        public static void CloseServer(IntPtr ServerHandle)
        {
            WTSCloseServer(ServerHandle);
        }

        /// <summary>
        /// Get a List of all Sessions on a server and IP address.
        /// </summary>
        /// <param name="ServerName"></param>
        /// <returns></returns>
        public static List<String> ListSessions(String ServerName)
        {
            IntPtr server = IntPtr.Zero;
            List<String> ret = new List<string>();
            server = OpenServer(ServerName);

            try
            {
                IntPtr ppSessionInfo = IntPtr.Zero;

                Int32 count = 0;
                Int32 retval = WTSEnumerateSessions(server, 0, 1, ref ppSessionInfo, ref count);
                Int32 dataSize = Marshal.SizeOf(typeof(WTS_SESSION_INFO));

                Int32 current = (int)ppSessionInfo;

                if (retval != 0)
                {

                    for (int i = 0; i < count; i++)
                    {

                        WTS_SESSION_INFO si = (WTS_SESSION_INFO)Marshal.PtrToStructure((System.IntPtr)current, typeof(WTS_SESSION_INFO));
                        current += dataSize;

                        #region OTsSession
                        uint returned = 0; ;
                        TsSession oTsSession = new TsSession();
                        //IP address 
                        IntPtr addr = IntPtr.Zero;
                        if (WTSQuerySessionInformation(server, si.SessionID, WTS_INFO_CLASS.WTSClientAddress, out addr, out returned) == true)
                        {
                            WTS_CLIENT_ADDRESS obj = new WTS_CLIENT_ADDRESS();
                            obj = (WTS_CLIENT_ADDRESS)Marshal.PtrToStructure(addr, obj.GetType());
                            oTsSession.IpAddress = obj.Address[2] + "." + obj.Address[3] + "." + obj.Address[4] + "." + obj.Address[5];
                        }

                        #endregion

                        ret.Add(si.SessionID + " " + si.State + " " + si.pWinStationName + "  " + oTsSession.IpAddress);

                    }

                    WTSFreeMemory(ppSessionInfo);
                }
            }
            finally
            {
                CloseServer(server);
            }

            return ret;
        }

        /// <summary>
        /// GetRemoteClientIP
        /// </summary>
        /// <returns></returns>
        public static string GetRemoteClientIP()
        {
            WTS_CLIENT_ADDRESS oClientAddres = new WTS_CLIENT_ADDRESS();
            string sIPAddress = string.Empty;
            uint iReturned = 0;

            //Get the IP address of the Terminal Services User
            IntPtr pAddress = IntPtr.Zero;
            if (WTSQuerySessionInformation(IntPtr.Zero, -1, WTS_INFO_CLASS.WTSClientAddress, out pAddress, out iReturned) == true)
            {
                oClientAddres = (WTS_CLIENT_ADDRESS)Marshal.PtrToStructure(pAddress, oClientAddres.GetType());
                sIPAddress = oClientAddres.Address[2] + "." + oClientAddres.Address[3] + "." + oClientAddres.Address[4] + "." + oClientAddres.Address[5];
            }
            return sIPAddress;
        }

        /// <summary>
        /// GetRemoteUserName
        /// </summary>
        /// <returns></returns>
        public static string GetRemoteUserName()
        {

            string sUserName = string.Empty;
            uint iReturned = 0;

            IntPtr pUserName = IntPtr.Zero;
            //Get the User Name of the Terminal Services User
            if (WTSQuerySessionInformation(IntPtr.Zero, -1, WTS_INFO_CLASS.WTSUserName, out pUserName, out iReturned) == true)
            {
                sUserName = Marshal.PtrToStringAnsi(pUserName);
            }
            return sUserName;
        }

        /// <summary>
        /// GetRemoteUserName
        /// </summary>
        /// <returns></returns>
        public static string GetRemoteClientName()
        {

            string sClientName = string.Empty;
            uint iReturned = 0;

            IntPtr pClientName = IntPtr.Zero;
            //Get the User Name of the Terminal Services User
            if (WTSQuerySessionInformation(IntPtr.Zero, -1, WTS_INFO_CLASS.WTSClientName, out pClientName, out iReturned) == true)
            {
                sClientName = Marshal.PtrToStringAnsi(pClientName);
            }
            return sClientName;
        }

        /// <summary>
        /// GetUserDomainName
        /// </summary>
        /// <returns></returns>
        public static string GetUserDomainName()
        {

            string sDomain = string.Empty;
            uint iReturned = 0;

            IntPtr pUserDomainName = IntPtr.Zero;
            //Get the Domain Name of the Terminal Services User
            if (WTSQuerySessionInformation(IntPtr.Zero, -1, WTS_INFO_CLASS.WTSDomainName, out pUserDomainName, out iReturned) == true)
            {
                sDomain = Marshal.PtrToStringAnsi(pUserDomainName);
            }
            return sDomain;
        }

        ////public static void abc( )
        ////{
        ////    IntPtr pServer = IntPtr.Zero;
        ////    string sUserName = string.Empty;
        ////    string sDomain = string.Empty;
        ////    string sClientApplicationDirectory = string.Empty;
        ////    string sIPAddress = string.Empty;

        ////    WTS_CLIENT_ADDRESS oClientAddres = new WTS_CLIENT_ADDRESS();
        ////    WTS_CLIENT_DISPLAY oClientDisplay = new WTS_CLIENT_DISPLAY();


        ////    uint iReturned = 0;

        ////    //Get the IP address of the Terminal Services User
        ////    IntPtr pAddress = IntPtr.Zero;
        ////    if (WTSQuerySessionInformation(IntPtr.Zero,-1, WTS_INFO_CLASS.WTSClientAddress,out pAddress, out iReturned) == true)
        ////    {
        ////        oClientAddres = (WTS_CLIENT_ADDRESS)Marshal.PtrToStructure(pAddress, oClientAddres.GetType());
        ////        sIPAddress = oClientAddres.Address[2] + "." + oClientAddres.Address[3] + "." + oClientAddres.Address[4] + "." + oClientAddres.Address[5];
        ////    }
        ////    //Get the User Name of the Terminal Services User
        ////    if (WTSQuerySessionInformation(IntPtr.Zero, -1, WTS_INFO_CLASS.WTSUserName,out pAddress, out iReturned) == true)
        ////    {
        ////        sUserName = Marshal.PtrToStringAnsi(pAddress);
        ////    }
        ////    //Get the Domain Name of the Terminal Services User
        ////    if (WTSQuerySessionInformation(IntPtr.Zero, -1, WTS_INFO_CLASS.WTSDomainName,out pAddress, out iReturned) == true)
        ////    {
        ////        sDomain = Marshal.PtrToStringAnsi(pAddress);
        ////    }
        ////    //Get the Display Information  of the Terminal Services User
        ////    if (WTSQuerySessionInformation(IntPtr.Zero, -1, WTS_INFO_CLASS.WTSClientDisplay,out pAddress, out iReturned) == true)
        ////    {
        ////        oClientDisplay = (WTS_CLIENT_DISPLAY)Marshal.PtrToStructure (pAddress, oClientDisplay.GetType());
        ////    }
        ////    //Get the Application Directory of the Terminal Services User
        ////    if (WTSQuerySessionInformation(IntPtr.Zero, -1,WTS_INFO_CLASS.WTSClientDirectory, out pAddress, out iReturned) == true)
        ////    {
        ////        sClientApplicationDirectory = Marshal.PtrToStringAnsi(pAddress);
        ////    }


        ////    NetWorkUtilsLog.WriteLog("IP Address : " + sIPAddress);
        ////    NetWorkUtilsLog.WriteLog("User Name : " + sDomain + @"\" + sUserName);
        ////    NetWorkUtilsLog.WriteLog("Client Display Resolution: " +oClientDisplay.iHorizontalResolution + " x " + oClientDisplay.iVerticalResolution);
        ////    NetWorkUtilsLog.WriteLog("Client Display Colour Depth: " + oClientDisplay.iColorDepth);
        ////    NetWorkUtilsLog.WriteLog("Client Application Directory: " +sClientApplicationDirectory);

        ////}
    }
}
