﻿using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace CoreLibrary
{
    //.net core 引用 webservice 服务
    //https://www.cnblogs.com/amuro/p/9430015.html

    //.Net Core调用WebService
    //https://blog.csdn.net/xiaouncle/article/details/100164452

    //https://devblogs.microsoft.com/dotnet/custom-asp-net-core-middleware-example/
    /////// <summary>
    /////// WebService代理类  
    /////// </summary>  
    ////public class WebServiceAgent
    ////{
    ////    public readonly object agent;
    ////    private Type agentType;
    ////    private const string CODE_NAMESPACE = "Winform.WebServiceAgent.Dynamic";

    ////    /// <summary>
    ////    /// 构造函数  
    ////    /// </summary>
    ////    /// <param name="url"<</param<  
    ////    public WebServiceAgent(string url)
    ////    {
    ////        XmlTextReader reader = new XmlTextReader(url + "?wsdl");

    ////        //创建和格式化 WSDL 文档  
    ////        ServiceDescription sd = ServiceDescription.Read(reader);

    ////        //创建客户端代理代理类  
    ////        ServiceDescriptionImporter sdi = new ServiceDescriptionImporter();
    ////        sdi.AddServiceDescription(sd, null, null);

    ////        //使用 CodeDom 编译客户端代理类  
    ////        CodeNamespace cn = new CodeNamespace(CODE_NAMESPACE);
    ////        CodeCompileUnit ccu = new CodeCompileUnit();
    ////        ccu.Namespaces.Add(cn);
    ////        sdi.Import(cn, ccu);
    ////        Microsoft.CSharp.CSharpCodeProvider icc = new Microsoft.CSharp.CSharpCodeProvider();
    ////        CompilerParameters cp = new CompilerParameters();
    ////        CompilerResults cr = icc.CompileAssemblyFromDom(cp, ccu);
    ////        agentType = cr.CompiledAssembly.GetTypes()[0];
    ////        agent = Activator.CreateInstance(agentType);
    ////    }

    ////    ///<summary>  
    ////    ///调用指定的方法  
    ////    ///</summary>  
    ////    ///<param name="methodName"<方法名，大小写敏感</param<  
    ////    ///<param name="args"<参数，按照参数顺序赋值</param<  
    ////    ///<returns<Web服务的返回值</returns<  
    ////    public object Invoke(string methodName, params object[] args)
    ////    {
    ////        MethodInfo mi = agentType.GetMethod(methodName);
    ////        return this.Invoke(mi, args);
    ////    }
    ////    ///<summary>
    ////    ///调用指定方法  
    ////    ///</summary>
    ////    ///<param name="method"<方法信息</param<  
    ////    ///<param name="args"<参数，按照参数顺序赋值</param<  
    ////    ///<returns<Web服务的返回值</returns<  
    ////    public object Invoke(MethodInfo method, params object[] args)
    ////    {
    ////        return method.Invoke(agent, args);
    ////    }
    ////    public MethodInfo[] Methods
    ////    {
    ////        get
    ////        {
    ////            return agentType.GetMethods();
    ////        }
    ////    }
    ////}

    /////////////// <summary>
    ///////////////  利用WebRequest/WebResponse进行WebService调用的类
    /////////////// </summary>
    ////////////public class WebSvcCaller
    ////////////{
    ////////////    //<webServices>
    ////////////    //  <protocols>
    ////////////    //    <add name="HttpGet"/>
    ////////////    //    <add name="HttpPost"/>
    ////////////    //  </protocols>
    ////////////    //</webServices>
    ////////////    private static Hashtable _xmlNamespaces = new Hashtable();//缓存xmlNamespace，避免重复调用GetNamespace

    ////////////    /// <summary>
    ////////////    /// 需要WebService支持Post调用
    ////////////    /// </summary>
    ////////////    public static XmlDocument QueryPostWebService(String URL, String MethodName, Hashtable Pars)
    ////////////    {
    ////////////        HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(URL + "/" + MethodName);
    ////////////        request.Method = "POST";
    ////////////        request.ContentType = "application/x-www-form-urlencoded";
    ////////////        SetWebRequest(request);
    ////////////        byte[] data = EncodePars(Pars);
    ////////////        WriteRequestData(request, data);
    ////////////        return ReadXmlResponse(request.GetResponse());
    ////////////    }

    ////////////    /// <summary>
    ////////////    /// 需要WebService支持Get调用
    ////////////    /// </summary>
    ////////////    public static XmlDocument QueryGetWebService(String URL, String MethodName, Hashtable Pars)
    ////////////    {
    ////////////        HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(URL + "/" + MethodName + "?" + ParsToString(Pars));
    ////////////        request.Method = "GET";
    ////////////        request.ContentType = "application/x-www-form-urlencoded";
    ////////////        SetWebRequest(request);
    ////////////        return ReadXmlResponse(request.GetResponse());
    ////////////    }


    ////////////    /// <summary>
    ////////////    /// 通用WebService调用(Soap),参数Pars为String类型的参数名、参数值
    ////////////    /// </summary>
    ////////////    public static XmlDocument QuerySoapWebService(String URL, String MethodName, Hashtable Pars)
    ////////////    {
    ////////////        if (_xmlNamespaces.ContainsKey(URL))
    ////////////        {
    ////////////            return QuerySoapWebService(URL, MethodName, Pars, _xmlNamespaces[URL].ToString());
    ////////////        }
    ////////////        else
    ////////////        {
    ////////////            return QuerySoapWebService(URL, MethodName, Pars, GetNamespace(URL));
    ////////////        }
    ////////////    }
    ////////////    private static XmlDocument QuerySoapWebService(String URL, String MethodName, Hashtable Pars, string XmlNs)
    ////////////    {
    ////////////        _xmlNamespaces[URL] = XmlNs;//加入缓存，提高效率
    ////////////        HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(URL);
    ////////////        request.Method = "POST";
    ////////////        request.ContentType = "text/xml; charset=utf-8";
    ////////////        request.Headers.Add("SOAPAction", "\"" + XmlNs + (XmlNs.EndsWith("/") ? "" : "/") + MethodName + "\"");
    ////////////        SetWebRequest(request);
    ////////////        byte[] data = EncodeParsToSoap(Pars, XmlNs, MethodName);
    ////////////        WriteRequestData(request, data);
    ////////////        XmlDocument doc = new XmlDocument(), doc2 = new XmlDocument();
    ////////////        doc = ReadXmlResponse(request.GetResponse());
    ////////////        XmlNamespaceManager mgr = new XmlNamespaceManager(doc.NameTable);
    ////////////        mgr.AddNamespace("soap", "http://schemas.xmlsoap.org/soap/envelope/");
    ////////////        String RetXml = doc.SelectSingleNode("//soap:Body/*/*", mgr).InnerXml;
    ////////////        doc2.LoadXml("<root>" + RetXml + "</root>");
    ////////////        AddDelaration(doc2);
    ////////////        return doc2;
    ////////////    }
    ////////////    private static string GetNamespace(String URL)
    ////////////    {
    ////////////        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(URL + "?WSDL");
    ////////////        SetWebRequest(request);
    ////////////        WebResponse response = request.GetResponse();
    ////////////        StreamReader sr = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
    ////////////        XmlDocument doc = new XmlDocument();
    ////////////        doc.LoadXml(sr.ReadToEnd());
    ////////////        sr.Close();
    ////////////        return doc.SelectSingleNode("//@targetNamespace").Value;
    ////////////    }
    ////////////    private static byte[] EncodeParsToSoap(Hashtable Pars, String XmlNs, String MethodName)
    ////////////    {
    ////////////        XmlDocument doc = new XmlDocument();
    ////////////        doc.LoadXml("<soap:Envelope xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:soap=\"http://schemas.xmlsoap.org/soap/envelope/\"></soap:Envelope>");
    ////////////        AddDelaration(doc);
    ////////////        XmlElement soapBody = doc.CreateElement("soap", "Body", "http://schemas.xmlsoap.org/soap/envelope/");
    ////////////        XmlElement soapMethod = doc.CreateElement(MethodName);
    ////////////        soapMethod.SetAttribute("xmlns", XmlNs);
    ////////////        foreach (string k in Pars.Keys)
    ////////////        {
    ////////////            XmlElement soapPar = doc.CreateElement(k);
    ////////////            soapPar.InnerXml = ObjectToSoapXml(Pars[k]);
    ////////////            soapMethod.AppendChild(soapPar);
    ////////////        }
    ////////////        soapBody.AppendChild(soapMethod);
    ////////////        doc.DocumentElement.AppendChild(soapBody);
    ////////////        return Encoding.UTF8.GetBytes(doc.OuterXml);
    ////////////    }
    ////////////    private static string ObjectToSoapXml(object o)
    ////////////    {
    ////////////        XmlSerializer mySerializer = new XmlSerializer(o.GetType());
    ////////////        MemoryStream ms = new MemoryStream();
    ////////////        mySerializer.Serialize(ms, o);
    ////////////        XmlDocument doc = new XmlDocument();
    ////////////        doc.LoadXml(Encoding.UTF8.GetString(ms.ToArray()));
    ////////////        if (doc.DocumentElement != null)
    ////////////        {
    ////////////            return doc.DocumentElement.InnerXml;
    ////////////        }
    ////////////        else
    ////////////        {
    ////////////            return o.ToString();
    ////////////        }
    ////////////    }
    ////////////    private static void SetWebRequest(HttpWebRequest request)
    ////////////    {
    ////////////        request.Credentials = CredentialCache.DefaultCredentials;
    ////////////        request.Timeout = 10000;
    ////////////    }
    ////////////    private static void WriteRequestData(HttpWebRequest request, byte[] data)
    ////////////    {
    ////////////        request.ContentLength = data.Length;
    ////////////        Stream writer = request.GetRequestStream();
    ////////////        writer.Write(data, 0, data.Length);
    ////////////        writer.Close();
    ////////////    }
    ////////////    private static byte[] EncodePars(Hashtable Pars)
    ////////////    {
    ////////////        return Encoding.UTF8.GetBytes(ParsToString(Pars));
    ////////////    }
    ////////////    private static String ParsToString(Hashtable Pars)
    ////////////    {
    ////////////        StringBuilder sb = new StringBuilder();
    ////////////        foreach (string k in Pars.Keys)
    ////////////        {
    ////////////            if (sb.Length > 0)
    ////////////            {
    ////////////                sb.Append("&");
    ////////////            }
    ////////////            //sb.Append(HttpUtility.UrlEncode(k) + "=" + HttpUtility.UrlEncode(Pars[k].ToString()));
    ////////////        }
    ////////////        return sb.ToString();
    ////////////    }
    ////////////    private static XmlDocument ReadXmlResponse(WebResponse response)
    ////////////    {
    ////////////        StreamReader sr = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
    ////////////        String retXml = sr.ReadToEnd();
    ////////////        sr.Close();
    ////////////        XmlDocument doc = new XmlDocument();
    ////////////        doc.LoadXml(retXml);
    ////////////        return doc;
    ////////////    }
    ////////////    private static void AddDelaration(XmlDocument doc)
    ////////////    {
    ////////////        XmlDeclaration decl = doc.CreateXmlDeclaration("1.0", "utf-8", null);
    ////////////        doc.InsertBefore(decl, doc.DocumentElement);
    ////////////    }
    ////////////}

}
