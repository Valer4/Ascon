using CommonHelpers.Any.Interfaces;
using CommonHelpers.CryptoProvider.Interfaces;
using System.IO;
using System.Net;
using System.Security.Cryptography.Pkcs;

namespace CommonHelpers.Any
{
    public class RestApi : IRestApi
    {
        private IStreamHelper _streamHelper;
        private INoticeError _noticeError;
        private ICryptoProvider _cryptoProvider;
        public RestApi(IStreamHelper streamHelper, INoticeError noticeError, ICryptoProvider cryptoProvider)
        {
            _streamHelper = streamHelper;
            _noticeError = noticeError;
            _cryptoProvider = cryptoProvider;
        }

        public byte[] GetHttpData(string url, string method, string contentType, byte[] sentData, string accessToken = null, bool useCertificate = false, string msgBadStatusCode = null)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url); // Двухфакторная аутентификация на бою.

            if (useCertificate) // Блок добавляет двухфакторную аутентификацию к отправке сообщения.
                request.ClientCertificates.Add(_cryptoProvider.GetCertificate());

            request.Method = method;
            request.ContentType = contentType;
            request.ContentLength = sentData.Length;

            if ( ! string.IsNullOrEmpty(accessToken))
                request.Headers.Add(HttpRequestHeader.Authorization, "Bearer " + accessToken);

            if ("POST" == method)
                using (Stream requestStream = request.GetRequestStream())
                    requestStream.Write(sentData, 0, sentData.Length); // Отправили.

            byte[] array = null;
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse()) // Получили.
                switch (response.StatusCode)
                {
                    case HttpStatusCode.OK: array = _streamHelper.StreamToByteArray(response.GetResponseStream()); break;
                    default:
                        if ( ! string.IsNullOrWhiteSpace(msgBadStatusCode))
                            _noticeError.Throw(string.Format(msgBadStatusCode, response.StatusCode));
                        else
                            _noticeError.Throw();
                        break;
                }

            if (useCertificate) // CMS/PKCS #7.
            {
                SignedCms signedCms = new SignedCms();
                signedCms.Decode(array);
                signedCms.CheckSignature(true);

                return signedCms.ContentInfo.Content;
            }

            return array;
        }
    }
}
