public class JSON { // System.Web.Extensions.dll
	public static System.Web.Script.Serialization.JavaScriptSerializer m_JSS = new System.Web.Script.Serialization.JavaScriptSerializer();
	public static T Parse<T>(string aString) {
		try { return m_JSS.Deserialize<T>(aString); } catch { return default(T); }
	}
	public static string ToString<T>(T aObject) {
		try { return m_JSS.Serialize(aObject); } catch { return ""; }
	}
}
