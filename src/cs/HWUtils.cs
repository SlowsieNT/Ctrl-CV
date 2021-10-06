public class HWUtils { 
        public static string GetProcessorName() {
            foreach (ManagementObject vObj in new ManagementObjectSearcher("select * from Win32_Processor").Get())
                try {
                    return vObj["Name"].ToString();
                } catch {}
            return "";
        }
        public static string GetProcessorId() {
            foreach (ManagementObject vObj in new ManagementObjectSearcher("select * from Win32_Processor").Get())
                try {
                    return vObj["ProcessorId"].ToString();
                } catch {}
            return "";
        }
        public static string GetGraphicCardName() {
            foreach (ManagementObject vObj in new ManagementObjectSearcher("Select * from Win32_VideoController").Get())
                try {
                    return vObj["Name"].ToString();
                } catch {}
            return "";
        }
        [DllImport("kernel32.dll")][return: MarshalAs(UnmanagedType.Bool)]
        static extern bool GetPhysicallyInstalledSystemMemory(out long TotalMemoryInKilobytes);
        public static int GetRAMSize() {
            long vMemKiB;
            GetPhysicallyInstalledSystemMemory(out vMemKiB);
            return (int)(vMemKiB / 1024 / 1024);
        }
    }
