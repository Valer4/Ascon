using System;

namespace WcfClient.Authorization
{
	public class AuthorizationClient : IAuthorizationClient
	{
		private readonly ChannelsManager _channelsManager;

		public AuthorizationClient(ChannelsManager channelsManager)
		{
			_channelsManager = channelsManager;
		}

		public string GetAuthorization() => throw new NotImplementedException();
	}
}
