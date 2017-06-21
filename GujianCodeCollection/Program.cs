using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

using System.IO;
using System.Threading;
using System.Net;

namespace GujianCodeCollection
{
    class Program
    {

        static HashSet<string> codelist = new HashSet<string>();

        static void Main(string[] args)
        {




            ServicePointManager.DefaultConnectionLimit = Int32.MaxValue;
            // Duowan();


            var list = File.ReadAllLines("usedcode.log").ToList();

            codelist = new HashSet<string>(list);
            // Official();
            
          //  File.WriteAllLines("dump.log", list.OrderBy(c => c.IndexOf("323XY")).ThenBy(c => c).ToArray());

            Thread offi = new Thread(Official);

            offi.Start();

            Thread duowan = new Thread(Duowan);

            duowan.Start();

            Thread hao_17173 = new Thread(Hao_17173);

            hao_17173.Start();

            Thread sina = new Thread(Sina);

            sina.Start();


            //   File.WriteAllLines("dumpcode.log",codelist.ToArray());

            while (true)
            {

                Console.ReadKey();
            }



        }

        public static void Sina()
        {
            while (true)
            {
                try
                {
                    string result = HttpRequestUtil.GET("http://ka.sina.com.cn/innerapi/pick?itemId=25705&gid=7859", "", "http://ka.sina.com.cn/25705", Encoding.UTF8);


                    Entity.Code_Sina code = JsonConvert.DeserializeObject<Entity.Code_Sina>(result);

                    if (code.K)
                    {
                        foreach (var vcode in code.Data.Cards)
                        {
                            if (codelist.Add(vcode))
                            {

                                Console.WriteLine(vcode);

                                result = HttpRequestUtil.GET(string.Format("http://gameactivate.game.yy.com/do/2021/activate/{0}", vcode), "", Encoding.UTF8);

                                Entity.Result res = JsonConvert.DeserializeObject<Entity.Result>(result);


                                Console.WriteLine(res.reason);






                                if (res.result != 17)
                                {
                                    Console.WriteLine(result);


                                    try
                                    {
                                        using (StreamWriter sw = new StreamWriter(new FileStream("okcode.log", FileMode.Append)))
                                        {

                                            sw.WriteLine(vcode);
                                            sw.WriteLine(result);
                                            sw.WriteLine("Sina");
                                            sw.WriteLine("------------------------");
                                        }

                                    }
                                    catch
                                    {

                                    }



                                   // break;
                                }
                                else
                                {
                                    try
                                    {
                                        using (StreamWriter sw = new StreamWriter(new FileStream("usedcode.log", FileMode.Append)))
                                        {

                                            sw.WriteLine(vcode);

                                        }

                                    }
                                    catch
                                    {

                                    }
                                }

                            }
                        }
                    }


                }
                catch
                {

                }

            }
        }



        public static void Hao_17173()
        {
            while (true)
            {
                try
                {
                    string result = HttpRequestUtil.GET("http://hao.17173.com/gift-tao-38754.html", "", Encoding.UTF8);


                    Entity.Code_17173 code = JsonConvert.DeserializeObject<Entity.Code_17173>(result);

                    if (codelist.Add(code.CardInfo.CardNumber))
                    {

                        Console.WriteLine(code.CardInfo.CardNumber);

                        result = HttpRequestUtil.GET(string.Format("http://gameactivate.game.yy.com/do/2021/activate/{0}", code.CardInfo.CardNumber), "", Encoding.UTF8);

                        Entity.Result res = JsonConvert.DeserializeObject<Entity.Result>(result);


                        Console.WriteLine(res.reason);






                        if (res.result != 17)
                        {
                            Console.WriteLine(result);


                            try
                            {
                                using (StreamWriter sw = new StreamWriter(new FileStream("okcode.log", FileMode.Append)))
                                {

                                    sw.WriteLine(code.CardInfo.CardNumber);
                                    sw.WriteLine(result);
                                    sw.WriteLine("17173");
                                    sw.WriteLine("------------------------");

                                }

                            }
                            catch
                            {

                            }



                          //  break;
                        }
                        else
                        {
                            try
                            {
                                using (StreamWriter sw = new StreamWriter(new FileStream("usedcode.log", FileMode.Append)))
                                {

                                    sw.WriteLine(code.CardInfo.CardNumber);

                                }

                            }
                            catch
                            {

                            }
                        }

                    }
                }
                catch
                {

                }

            }
        }



        public static void Duowan()
        {
            while (true)
            {
                try
                {
                    string result = HttpRequestUtil.GET("http://ka.duowan.com/gift/tao", "gift_id=200730", Encoding.UTF8);


                    Entity.Code code = JsonConvert.DeserializeObject<Entity.Code>(result);

                    if (codelist.Add(code.data))
                    {

                        Console.WriteLine(code.data);

                        result = HttpRequestUtil.GET(string.Format("http://gameactivate.game.yy.com/do/2021/activate/{0}", code.data), "", Encoding.UTF8);

                        Entity.Result res = JsonConvert.DeserializeObject<Entity.Result>(result);


                        Console.WriteLine(res.reason);

                        if (res.result != 17)
                        {
                            Console.WriteLine(result);


                            try
                            {
                                using (StreamWriter sw = new StreamWriter(new FileStream("okcode.log", FileMode.Append)))
                                {

                                    sw.WriteLine(code.data);
                                    sw.WriteLine(result);
                                    sw.WriteLine("Duowan");
                                    sw.WriteLine("------------------------");

                                }

                            }
                            catch
                            {

                            }



                          //  break;
                        }
                        else
                        {
                            try
                            {
                                using (StreamWriter sw = new StreamWriter(new FileStream("usedcode.log", FileMode.Append)))
                                {

                                    sw.WriteLine(code.data);

                                }

                            }
                            catch
                            {

                            }
                        }

                    }
                }
                catch
                {

                }

            }
        }


        public static void Official()
        {
            while (true)
            {
                try
                {
                    string result = HttpRequestUtil.GET("http://api.activityboard.game.yy.com/amoy/getGiftCode", "code=GJOLTAOHAO06", Encoding.UTF8);


                    Entity.Code code = JsonConvert.DeserializeObject<Entity.Code>(result);

                    if (codelist.Add(code.data))
                    {

                        Console.WriteLine(code.data);

                        result = HttpRequestUtil.GET(string.Format("http://gameactivate.game.yy.com/do/2021/activate/{0}", code.data), "", Encoding.UTF8);

                        Entity.Result res = JsonConvert.DeserializeObject<Entity.Result>(result);


                        Console.WriteLine(res.reason);

                        if (res.result != 17)
                        {

                            Console.WriteLine(result);

                            try
                            {
                                using (StreamWriter sw = new StreamWriter(new FileStream("okcode.log", FileMode.Append)))
                                {

                                    sw.WriteLine(code.data);
                                    sw.WriteLine(result);
                                    sw.WriteLine("Official");
                                    sw.WriteLine("------------------------");

                                }

                            }
                            catch
                            {

                            }


                           // break;
                        }
                        else
                        {
                            try
                            {
                                using (StreamWriter sw = new StreamWriter(new FileStream("usedcode.log", FileMode.Append)))
                                {

                                    sw.WriteLine(code.data);

                                }

                            }
                            catch
                            {

                            }
                        }

                    }

                }
                catch
                {

                }

            }
        }

    }
}
