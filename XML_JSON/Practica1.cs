using Newtonsoft.Json;
using System;
using System.Xml;
using System.Net;

namespace XML_JSON
{
    internal class Practica1
    {
        internal static void Test()
        {
            string xml = @"<?xml version='1.0' standalone='no'?>
                            <root>
                              <person id='1'>
                              <name>Alan</name>
                              <url>http://www.google.com</url>
                              </person>
                              <person id='2'>
                              <name>Louis</name>
                              <url>http://www.yahoo.com</url>
                              </person>
                            </root>";

            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml);

            string json = JsonConvert.SerializeXmlNode(doc);
            Console.WriteLine(json);

            Test1();

            //dynamic data = JavaScriptSerializers.Get<dynamic>(json);
            var argumento = 1;
            dynamic data = null;
            switch (argumento)
            {
                case 1: data = Jsons.Get<dynamic>(json);break;
                case 2: data = Jsons.Get<dynamic>(json); break;
            }


            dynamic data = Jsons.Get<dynamic>(json);

            data = JsonConverts.Get<dynamic>(json);

            data = JavaScriptSerializers.Get<dynamic>(json);

            data = Jsons.Get<dynamic>(json);
            Console.WriteLine(data["root"]["person"][0]["@id"]);
            Console.WriteLine(data.GetType());

            // https://json2csharp.com/?fbclid=IwAR0dTc_nbuo0LEec4gsqo9Ur8aEeyDiN4BVUmVfQ3O2b-e3CA2wJoyGNZyM
            // https://devblogs.microsoft.com/dotnet/paste-json-as-classes-in-asp-net-and-web-tools-2012-2-rc/

            data = JavaScriptSerializers.Get<Root>(json);
            //      Console.WriteLine(data["root"]["person"][0]["@id"]);
            //    Console.WriteLine(data.GetType());
            // Console.WriteLine(data.root.person[0].name);

            data = JsonConverts.Get<Root>(json);

            //Console.WriteLine(data.root.person[0].name);
            //Console.WriteLine(data.root.person[0]["@id"]);
            //Console.WriteLine(data.GetType());

            data = Jsons.Get<Root>(json);
            Console.WriteLine(data["root"]["person"][0]["@id"]);
            Console.WriteLine(data.GetType());


        }

        private static void Test1()
        {
            WebClient client = new WebClient();
            var cadena = System.IO.File.ReadAllText(@"C:\Carga\Archivo.txt");
            string getString = client.DownloadString(cadena);

            dynamic data = JavaScriptSerializers.Get<dynamic>(getString);

            //data["track"]["streamable"]["fulltrack"]

            data = JObjects.Get(getString);
            // data["track"]["streamable"].fulltrack
            // data["track"].streamable.fulltrack
            //data.track.streamable.fulltrack

            data = JsonConverts.Get<dynamic>(getString);

            data = Jsons.Get<dynamic>(getString);
        }
    }
}
