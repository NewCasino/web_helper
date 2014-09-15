﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;



class BrowserProxy
{
    struct Struct_INTERNET_PROXY_INFO
    {
        public int dwAccessType;
        public IntPtr proxy;
        public IntPtr proxyBypass;
    };

    [DllImport("wininet.dll", SetLastError = true)]
    private static extern bool InternetSetOption(IntPtr hInternet, int dwOption, IntPtr lpBuffer, int lpdwBufferLength);

    private void RefreshIESettings(string strProxy)
    {
        const int INTERNET_OPTION_PROXY = 38;
        const int INTERNET_OPEN_TYPE_PROXY = 3;

        Struct_INTERNET_PROXY_INFO struct_IPI;

        // Filling in structure
        struct_IPI.dwAccessType = INTERNET_OPEN_TYPE_PROXY;
        struct_IPI.proxy = Marshal.StringToHGlobalAnsi(strProxy);
        struct_IPI.proxyBypass = Marshal.StringToHGlobalAnsi("local");

        // Allocating memory
        IntPtr intptrStruct = Marshal.AllocCoTaskMem(Marshal.SizeOf(struct_IPI));

        // Converting structure to IntPtr
        Marshal.StructureToPtr(struct_IPI, intptrStruct, true); 
        bool iReturn = InternetSetOption(IntPtr.Zero, INTERNET_OPTION_PROXY, intptrStruct, Marshal.SizeOf(struct_IPI));
    }

    void test()
    {
        RefreshIESettings("192.168.1.200:1010"); 
        System.Object nullObject = 0;
        string strTemp = String.Empty;
        System.Object nullObjStr = strTemp;
        //axWebBrowser1.Navigate("http://www.baidu.com", ref nullObject, ref nullObjStr, ref nullObjStr, ref nullObjStr);
    }
}
