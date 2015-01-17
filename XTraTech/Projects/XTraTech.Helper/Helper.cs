using System;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
namespace XTraTech.Helper
{
    public class Helper
    {
        public static object XMLToObject(Type objType, string xml)
        {
            XmlSerializer serializer = new XmlSerializer(objType);
            StringReader stream = new StringReader(xml);
            XmlTextReader reader = new XmlTextReader(stream);
            return serializer.Deserialize(reader);
        }
        public static string ObjectToXML(object obj)
        {
            XmlSerializer serializer = new XmlSerializer(obj.GetType());
            StringWriter writer = new StringWriter();
            serializer.Serialize(writer, obj);
            return writer.ToString();
        }
        public static double AnewCurrencyFormat(double suplierCurrency)
        {
            string stringFormat = string.Format("{0:0.00}", suplierCurrency);
            return double.Parse(stringFormat, CultureInfo.InvariantCulture);
        }
        public static byte[] Zip(string str)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(str);
            byte[] result;
            using (MemoryStream msi = new MemoryStream(bytes))
            {
                using (MemoryStream mso = new MemoryStream())
                {
                    using (GZipStream gs = new GZipStream(mso, CompressionMode.Compress))
                    {
                        Helper.CopyTo(msi, gs);
                    }
                    result = mso.ToArray();
                }
            }
            return result;
        }
        public static void CopyTo(Stream src, Stream dest)
        {
            byte[] bytes = new byte[4096];
            int cnt;
            while ((cnt = src.Read(bytes, 0, bytes.Length)) != 0)
            {
                dest.Write(bytes, 0, cnt);
            }
        }
        public static string Unzip(byte[] bytes)
        {
            string @string;
            using (MemoryStream msi = new MemoryStream(bytes))
            {
                using (MemoryStream mso = new MemoryStream())
                {
                    using (GZipStream gs = new GZipStream(msi, CompressionMode.Decompress))
                    {
                        Helper.CopyTo(gs, mso);
                    }
                    @string = Encoding.UTF8.GetString(mso.ToArray());
                }
            }
            return @string;
        }
        public static string CreatePurchaseReference(int purchaseOrderID, DateTime createdOn)
        {
            return string.Concat(new object[]
			{
				"BeT",
				createdOn.ToString("yy"),
				createdOn.ToString("MM"),
				createdOn.ToString("dd"),
				purchaseOrderID
			});
        }
        public static int GetPurchaseOrderID(string purchaseOrderRef)
        {
            string str;
            if (purchaseOrderRef.Length > 9)
            {
                str = purchaseOrderRef.Substring(9);
            }
            else
            {
                str = "0";
            }
            return Convert.ToInt32(str);
        }
        public static XmlDocument StripDocumentNamespace(XmlDocument oldDom)
        {
            // Remove all xmlns:* instances from the passed XmlDocument
            // to simplify our xpath expressions.
            XmlDocument newDom = new XmlDocument();
            newDom.LoadXml(System.Text.RegularExpressions.Regex.Replace(
            oldDom.OuterXml, @"(xmlns:?[^=]*=[""][^""]*[""])", "",
            System.Text.RegularExpressions.RegexOptions.IgnoreCase | System.Text.RegularExpressions.RegexOptions.Multiline));
            return newDom;
        }
    }
}
