using System.Diagnostics;
using System.Runtime.InteropServices;

public class ConsoleUtility
{
	[DllImport("kernel32.dll", SetLastError = true)]
	public static extern bool AttachConsole(uint dwProcessId);
	[DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)]
	public static extern bool FreeConsole();
	[DllImport("kernel32.dll")] public static extern bool SetConsoleCtrlHandler(CtrlTypesBoolean HandlerRoutine, bool Add);
	public delegate bool CtrlTypesBoolean(CtrlTypes CtrlType);
	public enum CtrlTypes : uint {
		CTRL_C_EVENT = 0,
		CTRL_BREAK_EVENT,
		CTRL_CLOSE_EVENT,
		CTRL_LOGOFF_EVENT = 5,
		CTRL_SHUTDOWN_EVENT
	}
	[DllImport("kernel32.dll")][return: MarshalAs(UnmanagedType.Bool)]
	public static extern bool GenerateConsoleCtrlEvent(CtrlTypes aDWCtrlEvent, uint aDWProcessGroupId);
	public static void ProcessSendCtrlC(Process aProcess){
		// Send ctrl+c (close)
		ProcessSendCtrlEvent(aProcess, CtrlTypes.CTRL_C_EVENT);
	}
	public static void ProcessSendCtrlEvent(Process aProcess, CtrlTypes aCtrlEvent) {
		if (AttachConsole((uint)aProcess.Id)) {
			SetConsoleCtrlHandler(null, true);
			GenerateConsoleCtrlEvent(aCtrlEvent, 0);
			FreeConsole();
			aProcess.WaitForExit(1010);
			SetConsoleCtrlHandler(null, false);
		}
	}
}
