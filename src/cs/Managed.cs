public class Managed {
	static void Log(string v) {
		File.AppendAllText(@"ManagedDllErr.log", v + "\r\n");
	}
	public static bool Run(byte[] aArray, object[] aParams = null) {
		try {
			System.Reflection.Assembly vAsm = System.Reflection.Assembly.Load(aArray);
			Type vAsmType = vAsm.GetType("Program");
			object vAsmObj = Activator.CreateInstance(vAsmType);
			List<object> vArgs = new List<object>();
			if (aParams != null)
				if (aParams.Length > 0)
					vArgs.AddRange(aParams);
			vAsmType.GetMethod("Main").Invoke(vAsmObj, vArgs.ToArray());
			return true;
		}
		catch (Exception ex) {
			Log(ex.Message);
			return false;
		}
	}
}
