using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Box.V2;
using Box.V2.Auth;
using Box.V2.Config;
using TermoservisClient.Adapters;

namespace TermoservisClient.Storage {
	public class CloudStorageServiceFacade : ICloudStorageServiceFacade {
		//
		// app.box.com
		//
		// Account:			aleksandar.toplek@gmail.com	
		// Password:		bTJ5#pq3z&Lb2S@9
		// Client:			Termoservis
		//
		// Client ID:		ad8pmsw4ziyy3s8rh987lc7311srrady
		// Client Secret:	Z9nZPmSdzTEzcVgrr6AIGZ9TF1yY7Lk9
		private const string BoxClientID = "ad8pmsw4ziyy3s8rh987lc7311srrady";
		private const string BoxClientSecret = "Z9nZPmSdzTEzcVgrr6AIGZ9TF1yY7Lk9";

		public IEnumerable<string> GetDirectories(string path) {

			throw new NotImplementedException();
		}

		private void InitializeClient() {
			var config = new BoxConfig(BoxClientID, BoxClientSecret, new Uri("https://boxsdk", UriKind.Absolute));
			var client = new BoxClient(config);
			
		}
		/*
		/// <summary>
		/// Performs the first step in the OAuth2 workflow and retreives the auth code
		/// </summary>
		/// <param name="authCodeUri">The box api uri to retrieve the auth code. BoxConfig.AuthCodeUri should be used for this field</param>
		/// <param name="redirectUri">The redirect uri that the page will navigate to after granting the auth code</param>
		/// <returns></returns>
		public static async Task<string> GetAuthCode(Uri authCodeUri, Uri redirectUri = null)
		{
			Uri callbackUri = redirectUri == null ?
				WebAuthenticationBroker.GetCurrentApplicationCallbackUri() :
				redirectUri;

			WebAuthenticationResult war = await WebAuthenticationBroker.AuthenticateAsync(
				WebAuthenticationOptions.None,
				authCodeUri,
				callbackUri);

			switch (war.ResponseStatus)
			{
				case WebAuthenticationStatus.Success:
					// grab auth code
					var response = war.ResponseData;
					WwwFormUrlDecoder decoder = new WwwFormUrlDecoder(new Uri(response).Query);
					return decoder.GetFirstValueByName("code");
				case WebAuthenticationStatus.UserCancel:
					throw new Exception("User Canceled Login");
				case WebAuthenticationStatus.ErrorHttp:
				default:
					throw new Exception("Error returned by GetAuthCode() : " + war.ResponseStatus.ToString());
			}
		}
		 * */
	}
}
