public class TextUtils {
		public static string URIEncode(string aStr) {
			// No evil RegEx; Behold the Legacy code unfold
			string vOut = "";
			// Define vLen to reduce needless cpu 'usage' (in JS is useful)
			for (int vI = 0, vLen = aStr.Length; vI < vLen; vI++) {
				int vChar = (int)aStr[vI];
				if (vChar < 33) vOut += "%" + vChar.ToString("X");
				else if (vChar > 33 && vChar < 39) vOut += "%" + vChar.ToString("X");
				else if (vChar > 42 && vChar < 45) vOut += "%" + vChar.ToString("X");
				else if (vChar > 46 && vChar < 48) vOut += "%" + vChar.ToString("X");
				else if (vChar > 57 && vChar < 65) vOut += "%" + vChar.ToString("X");
				else if (vChar > 90 && vChar < 95) vOut += "%" + vChar.ToString("X");
				else if (vChar > 95 && vChar < 97) vOut += "%" + vChar.ToString("X");
				else if (vChar > 122 && vChar < 126) vOut += "%" + vChar.ToString("X");
				else if (vChar > 126 && vChar < 128) vOut += "%" + vChar.ToString("X");
				else if (vChar > 127) vOut += "%u" + vChar.ToString("X4");
				else vOut += (char)vChar;
			}
			return vOut;
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
						vInvalidEncSeqPrefix = "%u"; // due 'u' vInvalidEncSeqPrefix exists
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
