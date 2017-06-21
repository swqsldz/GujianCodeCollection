using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.IO.Compression;
using System.Threading;
using System.Configuration;

namespace GujianCodeCollection
{
    public class HttpRequestUtil
    {
        public static string GET(string requestUrl, string paramData, Encoding dataDecode)
        {
            return GET(requestUrl, paramData ,"", dataDecode , new CookieContainer());
        }
        public static string GET(string requestUrl, string paramData,string urlRefer, Encoding dataDecode)
        {
            return GET(requestUrl, paramData, urlRefer, dataDecode, new CookieContainer());
        }


        public static string GET(string requestUrl, string paramData ,string urlRefer , Encoding dataDecode, CookieContainer cc)
        {
            int errCount = 0;

            ServicePointManager.DefaultConnectionLimit = Int32.MaxValue;

            RETRY:

            string ret = string.Empty;
            try
            {
                string reqURL = requestUrl + (paramData == "" ? "" : "?") + paramData;

                //byte[] byteArray = dataEncode.GetBytes(paramData); //转化
                HttpWebRequest webReq = (HttpWebRequest)WebRequest.Create(new Uri(reqURL));
                webReq.Method = "GET";
                webReq.Proxy = null;
                webReq.AllowAutoRedirect = true;
               // webReq.CookieContainer = InitCookie(cc, new Uri(reqURL).Host);
                webReq.KeepAlive = true;
                webReq.UserAgent = "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/45.0.2454.101 Safari/537.36";


               string cookie = ConfigurationManager.AppSettings["COOKIES"].ToString();


                webReq.Headers.Add("Cookie", cookie);

                if (!string.IsNullOrEmpty(urlRefer))
                {
                    webReq.Referer = urlRefer;
                }

                //webReq.ContentType = "application/x-www-form-urlencoded";

                //webReq.ContentLength = byteArray.Length;
                //Stream newStream = webReq.GetRequestStream();
                //newStream.Write(byteArray, 0, byteArray.Length);//写入参数
                //newStream.Close();



                using (HttpWebResponse response = (HttpWebResponse)webReq.GetResponse())
                {

                    foreach (Cookie c in response.Cookies)
                    {
                        cc.Add(c);
                    }

                    using (StreamReader sr = new StreamReader(response.GetResponseStream(), dataDecode))
                    {
                        ret = sr.ReadToEnd();
                        sr.Close();
                    }

                    response.Close();
                }

            }
            catch (WebException ex)
            {
                using (HttpWebResponse response = (HttpWebResponse)ex.Response)
                {

                    if ((int)response.StatusCode == 429)
                    {

                        errCount++;

                        if (!string.IsNullOrEmpty(response.GetResponseHeader("Retry-After")))
                        {
                            try
                            {

                                int interval = 0;

                                if (!int.TryParse(response.GetResponseHeader("Retry-After"), out interval))
                                {
                                    interval = (DateTime.Parse(response.GetResponseHeader("Retry-After")) - DateTime.Now).Milliseconds;
                                }
                                else
                                {
                                    interval = interval * 1000;
                                }


                                Thread.Sleep(interval);

                                if (errCount < 3)
                                {

                                    goto RETRY;
                                }
                            }
                            catch
                            {

                            }


                        }

                    }

                    using (StreamReader sr = new StreamReader(response.GetResponseStream(), dataDecode))
                    {
                        ret = sr.ReadToEnd();
                        sr.Close();
                    }
                }

            }
            catch (Exception ex)
            {

            }
            return ret;
        }


        public static string GET(string requestUrl, string paramData, Encoding dataDecode, ref CookieContainer cc)
        {
            string ret = string.Empty;
            try
            {
                string reqURL = requestUrl + (paramData == "" ? "" : "?") + paramData;

                //byte[] byteArray = dataEncode.GetBytes(paramData); //转化
                HttpWebRequest webReq = (HttpWebRequest)WebRequest.Create(new Uri(reqURL));
                webReq.Method = "GET";
                webReq.Proxy = null;
                webReq.AllowAutoRedirect = true;
                webReq.CookieContainer = InitCookie(cc, new Uri(reqURL).Host);

                //webReq.ContentType = "application/x-www-form-urlencoded";

                //webReq.ContentLength = byteArray.Length;
                //Stream newStream = webReq.GetRequestStream();
                //newStream.Write(byteArray, 0, byteArray.Length);//写入参数
                //newStream.Close();
                using (HttpWebResponse response = (HttpWebResponse)webReq.GetResponse())
                {

                    foreach (Cookie c in response.Cookies)
                    {
                        cc.Add(c);
                    }

                    using (StreamReader sr = new StreamReader(response.GetResponseStream(), dataDecode))
                    {
                        ret = sr.ReadToEnd();
                        sr.Close();
                    }

                    response.Close();
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                //MessageBox.Show(ex.Message);
            }
            return ret;
        }


        public static string GET(string requestUrl, string paramData, Encoding dataDecode, string cookies)
        {
            string ret = string.Empty;
            try
            {
                string reqURL = requestUrl + (paramData == "" ? "" : "?") + paramData;

                //byte[] byteArray = dataEncode.GetBytes(paramData); //转化
                HttpWebRequest webReq = (HttpWebRequest)WebRequest.Create(new Uri(reqURL));
                webReq.Method = "GET";
                webReq.Proxy = null;
                webReq.AllowAutoRedirect = true;
                webReq.Headers.Add("Cookie", cookies);

                //webReq.ContentType = "application/x-www-form-urlencoded";

                //webReq.ContentLength = byteArray.Length;
                //Stream newStream = webReq.GetRequestStream();
                //newStream.Write(byteArray, 0, byteArray.Length);//写入参数
                //newStream.Close();
                using (HttpWebResponse response = (HttpWebResponse)webReq.GetResponse())
                {

                    using (StreamReader sr = new StreamReader(response.GetResponseStream(), dataDecode))
                    {
                        ret = sr.ReadToEnd();
                        sr.Close();
                    }

                    response.Close();
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                //MessageBox.Show(ex.Message);
            }
            return ret;
        }


        public static string GET(string requestUrl, string paramData, Encoding dataDecode, Dictionary<string, string> headers)
        {
            string ret = string.Empty;
            try
            {
                string reqURL = requestUrl + (paramData == "" ? "" : "?") + paramData;

                //byte[] byteArray = dataEncode.GetBytes(paramData); //转化
                HttpWebRequest webReq = (HttpWebRequest)WebRequest.Create(new Uri(reqURL));
                webReq.Method = "GET";
                webReq.Proxy = null;
                webReq.AllowAutoRedirect = true;
                webReq.Accept = "application/json";
                webReq.KeepAlive = true;

                foreach (var header in headers)
                {

                    webReq.Headers.Add(header.Key, header.Value);

                }

                //webReq.ContentType = "application/x-www-form-urlencoded";

                //webReq.ContentLength = byteArray.Length;
                //Stream newStream = webReq.GetRequestStream();
                //newStream.Write(byteArray, 0, byteArray.Length);//写入参数
                //newStream.Close();
                using (HttpWebResponse response = (HttpWebResponse)webReq.GetResponse())
                {

                    using (StreamReader sr = new StreamReader(response.GetResponseStream(), dataDecode))
                    {
                        ret = sr.ReadToEnd();
                        sr.Close();
                    }

                    response.Close();
                }

            }
            catch (Exception ex)
            {
                // Console.WriteLine(ex.ToString());
                //MessageBox.Show(ex.Message);
            }
            return ret;
        }




        public static Stream GET(string requestUrl, string paramData, CookieContainer cc)
        {

            string reqURL = requestUrl + (paramData == "" ? "" : "?") + paramData;

            //byte[] byteArray = dataEncode.GetBytes(paramData); //转化
            HttpWebRequest webReq = (HttpWebRequest)WebRequest.Create(new Uri(reqURL));
            webReq.Method = "GET";

            webReq.CookieContainer = InitCookie(cc, new Uri(reqURL).Host);

            //webReq.ContentType = "application/x-www-form-urlencoded";

            //webReq.ContentLength = byteArray.Length;
            //Stream newStream = webReq.GetRequestStream();
            //newStream.Write(byteArray, 0, byteArray.Length);//写入参数
            //newStream.Close();
            using (HttpWebResponse response = (HttpWebResponse)webReq.GetResponse())
            {

                using (Stream resstream = response.GetResponseStream())
                {


                    byte[] buffer = new byte[response.ContentLength];


                    resstream.Read(buffer, 0, (int)response.ContentLength);

                    resstream.Close();
                    return new MemoryStream(buffer);
                }

            }


        }



        public static string POST(string requestUrl, string paramData, Encoding dataDecode)
        {
            return POST(requestUrl, paramData, dataDecode, new CookieContainer());
        }

        public static string POST(string requestUrl, string paramData, Encoding dataDecode, CookieContainer cc)
        {
            string ret = string.Empty;
            try
            {
                string reqURL = requestUrl;// + "?" + paramData;

                byte[] byteArray = dataDecode.GetBytes(paramData); //转化
                HttpWebRequest webReq = (HttpWebRequest)WebRequest.Create(new Uri(reqURL));
                webReq.Method = "POST";

                webReq.CookieContainer = InitCookie(cc, new Uri(reqURL).Host);

                webReq.AllowAutoRedirect = true;

                webReq.ContentType = "application/x-www-form-urlencoded";

                webReq.ContentLength = byteArray.Length;
                using (Stream newStream = webReq.GetRequestStream())
                {
                    newStream.Write(byteArray, 0, byteArray.Length);//写入参数
                    newStream.Close();
                }

                using (HttpWebResponse response = (HttpWebResponse)webReq.GetResponse())
                {
                    using (StreamReader sr = new StreamReader(response.GetResponseStream(), dataDecode))
                    {
                        ret = sr.ReadToEnd();
                        sr.Close();
                    }

                    response.Close();
                }

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.ToString());
                //MessageBox.Show(ex.Message);
            }
            return ret;
        }

        public static string POST(string requestUrl, string paramData, Encoding dataDecode, ref CookieContainer cc)
        {
            string ret = string.Empty;
            try
            {
                string reqURL = requestUrl;// + "?" + paramData;

                byte[] byteArray = dataDecode.GetBytes(paramData); //转化
                HttpWebRequest webReq = (HttpWebRequest)WebRequest.Create(new Uri(reqURL));
                webReq.Method = "POST";

                webReq.CookieContainer = InitCookie(cc, new Uri(reqURL).Host);

                webReq.AllowAutoRedirect = true;

                webReq.KeepAlive = true;

                webReq.Headers.Add("Pragma", "no-cache");

                webReq.Headers.Add("Cache-Control", "no-cache");

                webReq.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8";

                webReq.Headers.Add("Origin", "http://user.games-cube.com");

                webReq.Headers.Add("Upgrade-Insecure-Requests", "1");


                webReq.UserAgent = "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/45.0.2454.101 Safari/537.36";

                webReq.Referer = "http://user.games-cube.com/login.aspx";



                webReq.Headers.Add("Accept-Encoding", "gzip, deflate");

                webReq.Headers.Add("Accept-Language", "zh-CN,zh;q=0.8\r\n");

                webReq.ContentType = "application/x-www-form-urlencoded";

                webReq.ContentLength = byteArray.Length;
                using (Stream newStream = webReq.GetRequestStream())
                {
                    newStream.Write(byteArray, 0, byteArray.Length);//写入参数
                    newStream.Close();
                }

                using (HttpWebResponse response = (HttpWebResponse)webReq.GetResponse())
                {
                    using (StreamReader sr = new StreamReader(response.GetResponseStream(), dataDecode))
                    {
                        ret = sr.ReadToEnd();
                        sr.Close();
                    }

                    response.Close();
                }

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.ToString());
                //MessageBox.Show(ex.Message);
            }
            return ret;
        }

        public static string POST(string requestUrl, string paramData, Encoding dataDecode, string cookies)
        {
            string ret = string.Empty;
            try
            {
                string reqURL = requestUrl;// + "?" + paramData;

                byte[] byteArray = dataDecode.GetBytes(paramData); //转化
                HttpWebRequest webReq = (HttpWebRequest)WebRequest.Create(new Uri(reqURL));
                webReq.Method = "POST";

                webReq.Headers.Add("Cookie", cookies);

                webReq.AllowAutoRedirect = true;

                webReq.KeepAlive = true;

                webReq.Headers.Add("Pragma", "no-cache");

                webReq.Headers.Add("Cache-Control", "no-cache");

                webReq.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8";

                webReq.Headers.Add("Origin", "Origin: http://admin.games-cube.com");

                webReq.Headers.Add("Upgrade-Insecure-Requests", "1");


                webReq.UserAgent = "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/45.0.2454.101 Safari/537.36";

                webReq.Referer = "http://admin.games-cube.com/api/LolTentacleToken.aspx";



                webReq.Headers.Add("Accept-Encoding", "gzip, deflate");

                webReq.Headers.Add("Accept-Language", "zh-CN,zh;q=0.8\r\n");

                webReq.ContentType = "application/x-www-form-urlencoded";

                webReq.ContentLength = byteArray.Length;
                using (Stream newStream = webReq.GetRequestStream())
                {
                    newStream.Write(byteArray, 0, byteArray.Length);//写入参数
                    newStream.Close();
                }

                using (HttpWebResponse response = (HttpWebResponse)webReq.GetResponse())
                {

                    switch (response.ContentEncoding)
                    {

                        case "gzip":
                            using (GZipStream gzip = new GZipStream(response.GetResponseStream(), CompressionMode.Decompress))
                            {

                                using (StreamReader sr = new StreamReader(gzip, dataDecode))
                                {
                                    ret = sr.ReadToEnd();
                                    sr.Close();
                                }

                            }

                            break;


                        case "defalte":
                            using (DeflateStream deflate = new DeflateStream(response.GetResponseStream(), CompressionMode.Decompress))
                            {

                                using (StreamReader sr = new StreamReader(deflate, dataDecode))
                                {
                                    ret = sr.ReadToEnd();
                                    sr.Close();
                                }

                            }
                            break;

                        default:
                            using (StreamReader sr = new StreamReader(response.GetResponseStream(), dataDecode))
                            {
                                ret = sr.ReadToEnd();
                                sr.Close();
                            }

                            break;
                    }


                    response.Close();
                }

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.ToString());
                //MessageBox.Show(ex.Message);
            }
            return ret;
        }



        private static CookieContainer InitCookie(CookieContainer cc, string Domain)
        {
            CookieContainer coo = new CookieContainer();
            Hashtable table = (Hashtable)cc.GetType().InvokeMember("m_domainTable", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.GetField | System.Reflection.BindingFlags.Instance, null, cc, new object[] { });
            foreach (object pathList in table.Values)
            {
                SortedList lstCookieCol = (SortedList)pathList.GetType().InvokeMember("m_list", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.GetField | System.Reflection.BindingFlags.Instance, null, pathList, new object[] { });
                foreach (CookieCollection colCookies in lstCookieCol.Values)
                    foreach (Cookie c in colCookies)
                    {
                        c.Domain = Domain;
                        coo.Add(c);

                    }
            }
            return coo;
        }



    }
}
