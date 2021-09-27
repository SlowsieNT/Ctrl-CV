using System.Web.Script.Serialization;
// Newtonsoft JSON?? No no no, we dont use needless things
public class JSON { // ref dll: System.Web.Extensions.dll
	static JavaScriptSerializer m_JSS = new JavaScriptSerializer();
	public static string Stringify<T>(T aObject) {
		try {
			return m_JSS.Serialize(aObject);
		} catch {
			return "";
		}
	}
	public static T Parse<T>(string aString) {
		try {
			return m_JSS.Deserialize<T>(aString);
		} catch {
			return default(T);
		}
	}
}
