﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Helper
{
    /// <summary>
    /// http 操作类，通用的http操作类
    /// </summary>
    public static class NetHelper
    {
        public static string HttpCall(string url, string postData, HttpEnum method)
        {
            HttpWebRequest myHttpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            if (method == HttpEnum.Post)
            {
                myHttpWebRequest.Method = "POST";

                //采用UTF8编码
                UTF8Encoding encoding = new UTF8Encoding();
                byte[] byte1 = encoding.GetBytes(postData);
                myHttpWebRequest.ContentType = "application/x-www-form-urlencoded";
                myHttpWebRequest.ContentLength = byte1.Length;

                /*
                * 写请求体数据
                */

                Stream newStream = myHttpWebRequest.GetRequestStream();
                newStream.Write(byte1, 0, byte1.Length);
                newStream.Close();
            }
            else// get
                myHttpWebRequest.Method = "GET";
            myHttpWebRequest.ProtocolVersion = new Version(1, 1);   //Http/1.1版本

            //发送成功后接收返回的JSON信息
            HttpWebResponse response = (HttpWebResponse)myHttpWebRequest.GetResponse();
            string lcHtml = string.Empty;
            Encoding enc = Encoding.GetEncoding("UTF-8");
            Stream stream = response.GetResponseStream();
            StreamReader streamReader = new StreamReader(stream, enc);
            lcHtml = streamReader.ReadToEnd();
            return (lcHtml);
        }
    }
}