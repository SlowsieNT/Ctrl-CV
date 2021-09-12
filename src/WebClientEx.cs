public class WebClientEx : WebClient {
	public CookieContainer m_CookieContainer;
	public Uri Uri;

	public WebClientEx() { m_CookieContainer = new CookieContainer();}

	public WebClientEx(CookieContainer aCookies) { m_CookieContainer = aCookies; }
	public List<string[]> Headers = new List<string[]>();
	
	public string Accept = "";
	public string Referer = "";
	public string UserAgent = "";
	public string ContentType = "";
	protected override WebRequest GetWebRequest(Uri aAddress) {
		WebRequest vReq = base.GetWebRequest(aAddress);
		if (vReq is HttpWebRequest)
			(vReq as HttpWebRequest).CookieContainer = this.m_CookieContainer;
		HttpWebRequest vHttpReq = (HttpWebRequest)vReq;
		vHttpReq.Accept = Accept;
		vHttpReq.Referer = Referer;
		vHttpReq.UserAgent = UserAgent;
		vHttpReq.ContentType = ContentType;
		foreach (var vHeader in Headers) {
			try {
				vHttpReq.Headers[vHeader[0]] = vHeader[1];
			} catch {}
		}
		vHttpReq.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
		return vHttpReq;
	}

	protected override WebResponse GetWebResponse(WebRequest aReq) {
		WebResponse vResp = base.GetWebResponse(aReq);
		string vSetCookieHdr = vResp.Headers[HttpResponseHeader.SetCookie];
		if (vSetCookieHdr != null)
			m_CookieContainer.SetCookies(aReq.RequestUri, vSetCookieHdr);
		return vResp;
	}
}
