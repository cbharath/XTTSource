namespace Adapters
{
    using System.Collections.Generic;
    using System.IO;
    using System.IO.Compression;
    using System.Net;
    using System;

    /// <summary>
    /// The URL invocation method. Used by the HttpHelper class
    /// </summary>
    public enum HttpMethod
    {
        /// <summary>
        /// HTTP POST method
        /// </summary>
        Post,
        /// <summary>
        /// HTTP GET method
        /// </summary>
        Get
    }
    /// <summary>
    /// The content type used in the url. Used by the HttpPostApi class.
    /// </summary>
    public enum ContentType
    {
        /// <summary>
        /// content type = application/x-www-form-urlencoded
        /// </summary>
        wwwFormUrlencoded,
        /// <summary>
        /// content type = text/xml; charset=UTF-8
        /// </summary>
        textXml
    }
    /// <summary>
    /// The encoding type. Used by the HttpPostApi class.
    /// </summary>
    public enum EncodingType
    {
        /// <summary>
        /// Ascii encoding
        /// </summary>
        ASCII,
        /// <summary>
        /// Unicode encoding
        /// </summary>
        Unicode,
        /// <summary>
        /// UTF7 encoding
        /// </summary>
        UTF7,
        /// <summary>
        /// UTF8 encoding
        /// </summary>
        UTF8
    }
    [Serializable]
    /// <summary>
    /// An generic HTTP Helper class that can be used for calling api methods using the HTTP protocol
    /// Use this class when you want to call any url & get back a response.
    /// </summary>
    public class HttpPostApi
    {
        /// <summary>
        /// Data to send as post data
        /// </summary>
        string _postData = null;
        /// <summary>
        /// boolean field indicating if compression should be used during communication
        /// </summary>
        bool _enableCompression;
        /// <summary>
        /// Custom headers that the user wants to send
        /// </summary>
        public Dictionary<string, string> _headers = new Dictionary<string, string>();
        /// <summary>
        /// Gets or sets the post data.
        /// </summary>
        /// <value>
        /// The post data.
        /// </value>
        public string PostData { set { _postData = value; } get { return _postData; } }
        /// <summary>
        /// Gets or sets a value indicating whether [enable compression].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [enable compression]; otherwise, <c>false</c>.
        /// </value>
        public bool EnableCompression { set { _enableCompression = value; } get { return _enableCompression; } }
        /// <summary>
        /// Invokes the URL.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <param name="method">The HTTP method (POST or GET).</param>
        /// <param name="contentType">The content type based on the enum.</param>
        /// <returns>The response as a string</returns>
        public string invokeURL(string url, HttpMethod method, ContentType contentType)
        {
            string retval = "";
            HttpWebRequest Request = (HttpWebRequest)WebRequest.Create(url);
            Request.Method = (method == HttpMethod.Post) ? "POST" : "GET";
            if (_enableCompression)
                Request.Headers.Add("Accept-Encoding", "gzip,deflate");
            switch (contentType)
            {
                case ContentType.wwwFormUrlencoded:
                    Request.ContentType = "application/x-www-form-urlencoded";
                    break;
                case ContentType.textXml:
                    Request.ContentType = "text/xml; charset=UTF-8";
                    break;
                default:
                    Request.ContentType = "application/x-www-form-urlencoded";
                    break;
            }
            if (_headers.Count != 0)
            {
                foreach (KeyValuePair<string, string> item in _headers)
                {
                    Request.Headers.Add(item.Key, item.Value);
                }
            }
            Request.ContentLength = 0;

            if (_postData != null)
            {
                System.Text.Encoding encoding = new System.Text.UTF8Encoding();
                byte[] data = encoding.GetBytes(_postData);
                Request.ContentLength = data.Length;
                Stream stream = Request.GetRequestStream();
                stream.Write(data, 0, data.Length);
                stream.Close();
            }

            WebResponse response = Request.GetResponse();
            if (response.Headers["Content-Encoding"] == "gzip")
                retval = unZipStream(response.GetResponseStream());
            else
                retval = StreamToString(response.GetResponseStream());
            return retval;
        }
        #region Compression
        /// <summary>
        /// Unzip the stream.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <returns>The stream unzipped</returns>
        private string unZipStream(Stream stream)
        {
            GZipStream unzipstream = new GZipStream(stream, CompressionMode.Decompress);
            return StreamToString(unzipstream);
        }
        /// <summary>
        /// Convert a Stream to string.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <returns>The contents of the stream as string</returns>
        private string StreamToString(Stream stream)
        {
            MemoryStream ret = new MemoryStream();
            stream.CopyTo(ret);
            return ByteArrayToString(ret.ToArray());
        }
        /// <summary> 
        /// Converts a byte array to a string using specified encoding. 
        /// </summary> 
        /// <param name="bytes">Array of bytes to be converted.</param> 
        /// <param name="encodingType">EncodingType enum.</param> 
        /// <returns>The byte array converted to string</returns> 
        public static string ByteArrayToString(byte[] bytes, EncodingType encodingType = EncodingType.UTF8)
        {
            System.Text.Encoding encoding = null;
            switch (encodingType)
            {
                case EncodingType.ASCII:
                    encoding = new System.Text.ASCIIEncoding();
                    break;
                case EncodingType.Unicode:
                    encoding = new System.Text.UnicodeEncoding();
                    break;
                case EncodingType.UTF7:
                    encoding = new System.Text.UTF7Encoding();
                    break;
                case EncodingType.UTF8:
                    encoding = new System.Text.UTF8Encoding();
                    break;
            }
            return encoding.GetString(bytes);
        }
        #endregion Compression
    }
}
