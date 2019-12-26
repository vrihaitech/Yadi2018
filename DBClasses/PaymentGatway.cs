using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web.Script.Serialization;

namespace Yadi.DBClasses
{
    public class PaymentGatway
    {
        public bool Sendsms(string Api,SendSmsLink Link)
        {
                WebRequest myReq = WebRequest.Create(Api);
                myReq.Method = "POST";
                myReq.ContentType = "application/json";
                myReq.Headers.Add("X-CHANNEL", "API");
                myReq.Headers.Add("ORGID", "GI01");
                myReq.Headers.Add("X-EMAIL", "cashlesstechnologies123@gmail.com");
                myReq.Headers.Add("AuthorizationKey", "eyJhbGciOiJIUzI1NiJ9.eyJpc3MiOiJodHRwczovL21vYmlsZXBheW1lbnRzLmJlbm93LmluLyIsInN1YiI6ImNhc2hsZXNzdGVjaG5vbG9naWVzMTIzQGdtYWlsLmNvbSIsImRhdGEiOnsibWVyY2hhbnRJZCI6IjExNDYwIiwibWNjQ29kZSI6IjU0OTkiLCJtb2JpbGVOdW1iZXIiOiI5MDI5MDA1NDc1IiwiZGlzcGxheU5hbWUiOiJJTlRFR1JBVElPTiBURVNUIE1FUkNIQU5UIiwibWVyY2hhbnRDb2RlIjoiQUY4WTEiLCJwcml2YXRlSWQiOiI3NjUifSwiaWF0IjoxNTAyNzI3NDMxfQ.xkq3So8Z3P1R3AePal_lb7tIi0hEWD3Dmb5y1ddohYM");

                Encrypt obj = new Encrypt();//This class contains the encryption logic that was written in APIs document.
                byte[][] bytkeys = obj.GetHashKeys("abcd");
                String strPayload;
                String Encrypted_Payload;
                JavaScriptSerializer serializer = new JavaScriptSerializer();

                strPayload = serializer.Serialize(Link);
                Encrypted_Payload = obj.EncryptData(strPayload, bytkeys);
                ApiParam objAPIParam = new ApiParam(Encrypted_Payload, strPayload);

                Stream dataStream;

                using (var streamWriter = new StreamWriter(myReq.GetRequestStream()))
                {
                    //initiate the request
                    JavaScriptSerializer serializer1 = new JavaScriptSerializer();
                    var resToWrite = serializer1.Serialize(objAPIParam);
                    streamWriter.Write(resToWrite);
                    streamWriter.Flush();
                    streamWriter.Close();
                    WebResponse response1 = myReq.GetResponse();
                    dataStream = response1.GetResponseStream();
                    StreamReader reader1 = new StreamReader(dataStream);
                    string responseFromServer1 = reader1.ReadToEnd();
                    reader1.Close();
                    dataStream.Close();
                    response1.Close();
                }

                var response = (HttpWebResponse)myReq.GetResponse();
                if(response.StatusCode == HttpStatusCode.OK)
                {
                    return true;
                }
                else
                {
                    return false;
                }
        }

        public string TransactionStatus(string Api,GetTransactionStatus tGetWayReturn)
        {
                WebRequest myReq = WebRequest.Create(Api);
                myReq.Method = "POST";
                myReq.ContentType = "application/json";
                myReq.Headers.Add("X-CHANNEL", "API");
                myReq.Headers.Add("ORGID", "GI01");
                myReq.Headers.Add("X-EMAIL", "cashlesstechnologies123@gmail.com");
                myReq.Headers.Add("AuthorizationKey", "eyJhbGciOiJIUzI1NiJ9.eyJpc3MiOiJodHRwczovL21vYmlsZXBheW1lbnRzLmJlbm93LmluLyIsInN1YiI6ImNhc2hsZXNzdGVjaG5vbG9naWVzMTIzQGdtYWlsLmNvbSIsImRhdGEiOnsibWVyY2hhbnRJZCI6IjExNDYwIiwibWNjQ29kZSI6IjU0OTkiLCJtb2JpbGVOdW1iZXIiOiI5MDI5MDA1NDc1IiwiZGlzcGxheU5hbWUiOiJJTlRFR1JBVElPTiBURVNUIE1FUkNIQU5UIiwibWVyY2hhbnRDb2RlIjoiQUY4WTEiLCJwcml2YXRlSWQiOiI3NjUifSwiaWF0IjoxNTAyNzI3NDMxfQ.xkq3So8Z3P1R3AePal_lb7tIi0hEWD3Dmb5y1ddohYM");

                Encrypt obj = new Encrypt();//This class contains the encryption logic that was written in APIs document.
                byte[][] bytkeys = obj.GetHashKeys("abcd");
                String strPayload;
                String Encrypted_Payload;
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                strPayload = serializer.Serialize(tGetWayReturn);
                Encrypted_Payload = obj.EncryptData(strPayload, bytkeys);
                ApiParam objAPIParam = new ApiParam(Encrypted_Payload, strPayload);

                Stream dataStream;


                using (var streamWriter = new StreamWriter(myReq.GetRequestStream()))
                {
                    //initiate the request
                    JavaScriptSerializer serializer1 = new JavaScriptSerializer();

                    var resToWrite = serializer1.Serialize(objAPIParam);
                    streamWriter.Write(resToWrite);
                    streamWriter.Flush();
                    streamWriter.Close();

                    WebResponse response1 = myReq.GetResponse();
                    dataStream = response1.GetResponseStream();
                    StreamReader reader1 = new StreamReader(dataStream);
                    string responseFromServer1 = reader1.ReadToEnd();
                    reader1.Close();
                    dataStream.Close();
                    response1.Close();
                return responseFromServer1;
                }
            
        }
    }

    public class SendSmsLink
    {
        public string merchantCode { get; set; }
        public string customerName { get; set; }
        public string mobileNumber { get; set; }

        public string amount { get; set; }

        public string refNumber { get; set; }

        public SendSmsLink(String _merchantCode, String _customerName, String _mobileNumber, String _amount, String _refNumber)
        {
            merchantCode = _merchantCode;
            customerName = _customerName;
            mobileNumber = _mobileNumber;
            amount = _amount;
            refNumber = _refNumber;
            //paymentMethod = _paymentMethod;
        }
        public SendSmsLink()
        {

        }
    }

    public class ApiParam
    {
        public string encryptedString { get; set; }
        public string jsonString { get; set; }
        public ApiParam(String _content, String _jsonString)
        {
            encryptedString = _content;
            jsonString = _jsonString;
        }
        public ApiParam()
        {

        }
    }

    public class Encrypt
    {
        public string EncryptData(string textToEncrypt, byte[][] keys)
        {
            byte[] encrypted;
            try
            {
                RijndaelManaged rijndaelCipher = new RijndaelManaged();
                rijndaelCipher.Mode = CipherMode.CBC;
                rijndaelCipher.Padding = PaddingMode.PKCS7;
                rijndaelCipher.KeySize = 0x80; // 256bit key
                rijndaelCipher.BlockSize = 0x80;
                rijndaelCipher.Key = keys[0];
                rijndaelCipher.IV = keys[1];
                ICryptoTransform transform = rijndaelCipher.CreateEncryptor();
                byte[] plainText = Encoding.UTF8.GetBytes(textToEncrypt);
                return Convert.ToBase64String(transform.TransformFinalBlock(plainText,
               0, plainText.Length));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public byte[][] GetHashKeys(string key)
        {
            byte[][] result = new byte[2][];
            // Get SHA256 hash bytes for salt
            byte[] pwdBytes = Encoding.UTF8.GetBytes(key);
            byte[] keyBytes = new byte[0x10];
            SHA256 sha2 = new SHA256CryptoServiceProvider();
            byte[] hashKey = sha2.ComputeHash(pwdBytes);
            int len = hashKey.Length;

            if (len > keyBytes.Length)
            {
                len = keyBytes.Length;
            }
            Array.Copy(hashKey, keyBytes, len);
            result[0] = keyBytes;

            // Get IV bytes
            string iv = "xxxxyyyyzzzzwwww";
            byte[] ivBytes = Encoding.UTF8.GetBytes(iv);
            byte[] iBytes = new byte[0x10];
            int ilen = ivBytes.Length;
            if (ilen > iBytes.Length)
            {
                ilen = ivBytes.Length;
            }
            Array.Copy(ivBytes, iBytes, ilen);
            result[1] = ivBytes;
            return result;
        }
    }


    public class GetTransactionStatus
    {
        public string merchantCode { get; set; }
        public string refNumber { get; set; }
        public string txnId { get; set; }

        public GetTransactionStatus(String _merchantCode, String _refNumber, String _txnId)
        {
            merchantCode = _merchantCode;
            txnId = _txnId;
            refNumber = _refNumber;
        }
        public GetTransactionStatus()
        {

        }
    }

   
}
