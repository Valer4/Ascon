namespace CommonHelpers.Any.Interfaces
{
	public interface IRestApi
    {
        IStreamHelper StreamHelper { get; }

        byte[] GetHttpData(string url, string method, string contentType, byte[] sentData, string accessToken = null, bool useCertificate = false, string msgBadStatusCode = null);
    }
}
