public class BaseConv {
	private const string m_Chars = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
	public static string ToBase(long aValue, int aBase) {
		string vOut = "";
		while (aValue > 0) {
			int vIdx = (int)(aValue % aBase);
			vOut = m_Chars[vIdx] + vOut; // use StringBuilder for better performance
			aValue /= aBase;
		}
		return vOut;
	}
}
public class Utilities {
	public static bool MakeDir(string aDirName) {
		aDirName = Environment.ExpandEnvironmentVariables(aDirName);
		bool vExists = Directory.Exists(aDirName);
		if (!vExists)
			try {
				Directory.CreateDirectory(aDirName);
				return true;
			} catch {}
		return vExists;
	}
	public static string GetUTCDateAsBase36Number() {
		Int32 vUnixTS = (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
		return BaseConv.ToBase(vUnixTS, 36);
	}
	public static string DecodeBase64Modified(string aModifiedBase64) {
		string vPadded = aModifiedBase64.PadRight(aModifiedBase64.Length + (4 - aModifiedBase64.Length % 4) % 4, '=');
		return Encoding.UTF8.GetString(Convert.FromBase64String(vPadded));
	}
	public static string URIDecode(string aStr) { // Uri.Unescape doesn't work
		// No evil RegEx; Behold the Legacy code unfold
		string vOut = "";
		// Define vLen to reduce needless cpu 'usage' (in JS is useful)
		for (int vI = 0, vLen = aStr.Length; vI < vLen; vI++) {
			char vChar = aStr[vI];
			// Indentify encoded parts unless it's end of String
			if ('%' == vChar && 1 + vI < vLen) {
				// (Definitions)
				bool vInvalidEncSeq = false; // flag: invalid encoded sequence
				string vHexValue = "", vInvalidEncSeqPrefix = "%";
				int vUpperBound = 1, vOffset2 = 0;
				// If encoding is prefixed with 'u', length is 4
				if ('u' == aStr[1 + vI]) {
					vUpperBound = 3; // means: length 4
					vOffset2 = 1; // due 'u' offset2 exists
					vInvalidEncSeqPrefix = "%u"; // due 'u' vTemp exists
				}
				// loop through until upperbound reached
				for (int vI2 = 1 + vOffset2 + vI; !vInvalidEncSeq && vI2 < vLen; vI2++) {
					char vChar2 = aStr[vI2], vCC = vChar2;
					// break if buffer is already at len 2
					if (vHexValue.Length > vUpperBound) break;
					// accept range: [a-fA-F0-9]
					if (vCC > 47 && vCC < 58 || vCC > 96 && vCC < 103 || vCC > 64 && vCC < 71) {
						vHexValue += vChar2;
					} else vInvalidEncSeq = true; // invalid char found during parsing
				}
				if (vInvalidEncSeq) { // allocate invalid sequence to output
					vOut += vInvalidEncSeqPrefix + vHexValue;
					vI += 1 + vHexValue.Length; // skip index by invalid sequence's length + 1
				} else if (vHexValue.Length > 3) {
					vI += 5; // 5 because of prefix 'u' (eg: %u0061)
					vOut += (char)(Convert.ToInt32(vHexValue, 16));
				} else if (vHexValue.Length > 1) {
					vI += 2; // 2 (eg: %61)
					vOut += (char)(Convert.ToInt32(vHexValue, 16));
				}
			} else vOut += vChar;
		}
		return vOut;
	}
}
