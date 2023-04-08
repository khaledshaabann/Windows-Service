using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Runtime.InteropServices;
using System.Net.NetworkInformation;
using System.IO;
using System.Net.Mail;
using System.Net;
using System.Net.Mime;

namespace MyNewService
{
    public partial class MyNewService : ServiceBase
    {
        public MyNewService()
        {
            InitializeComponent();
        }


        protected override void OnStart(string[] args)
        {

            int cpuVal = GetCpuVal();
            int memVal = GetMemVal();
            long netSent = GetNetSent();
            long netRec = GetNetRec();
            double driveValue = GetDriveVal();

            using (StreamWriter writeText = new StreamWriter("C://Temp//service.txt"))
            {

                writeText.WriteLine("Workloads:");

                writeText.WriteLine(String.Format("CPU Usage: {0}", cpuVal));
                writeText.WriteLine(String.Format("Memory Usage: {0}", memVal));
                writeText.WriteLine(String.Format("Bytes Sent on Network: {0}", netSent));
                writeText.WriteLine(String.Format("Bytes Received on Network: {0}", netRec));
                writeText.WriteLine(String.Format("Drive Usage: {0}", driveValue));

            }

            



        }


        

        protected override void OnStop()
        {
        }


   
       
        
        private static int GetCpuVal()
        {
            var CpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
            CpuCounter.NextValue();
            System.Threading.Thread.Sleep(1000);
            int val = (int)CpuCounter.NextValue();
            return val;
        }

        private static int GetMemVal()
        {
            var MemCounter = new PerformanceCounter("Memory", "% Committed Bytes in Use");
            int val = (int)(MemCounter.NextValue());
            return val;
        }

        private static long GetNetSent()
        {
            if (!NetworkInterface.GetIsNetworkAvailable())
                return 0;
            NetworkInterface[] interfaces = NetworkInterface.GetAllNetworkInterfaces();
            long netSent = 0;
            foreach(NetworkInterface intf in interfaces)
            {

                 netSent = intf.GetIPv4Statistics().BytesSent;
                
            }
            return netSent;
        }
        private static long GetNetRec()
        {
            if (!NetworkInterface.GetIsNetworkAvailable())
                return 0;
            NetworkInterface[] interfaces = NetworkInterface.GetAllNetworkInterfaces();
            long netRec = 0;
            foreach (NetworkInterface intf in interfaces)
            {
                netRec = intf.GetIPv4Statistics().BytesReceived;
                
            }
            return netRec;

        }

        private static double GetDriveVal()
        {
            DriveInfo gDrive = new DriveInfo("G");
            double DriveVal = 0;
            if (gDrive.IsReady)
            {
                DriveVal = 100 - ((gDrive.AvailableFreeSpace / (float)gDrive.TotalSize) * 100);
                
            }
            return DriveVal;
        }
        
    }
        
}
