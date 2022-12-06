using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Gmail.v1;
using Google.Apis.Gmail.v1.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class GmailController : BaseApiController
    {
        [HttpGet]
        public async Task<ActionResult<IList<Message>>> GetEmailsList()
        {
            string[] Scopes = { GmailService.Scope.MailGoogleCom };
            UserCredential credential;
            using (var stream =
                        new FileStream(Environment.CurrentDirectory + "/../gmailapi/client_secret.json",
                        FileMode.Open, FileAccess.Read))
            {
                string credPath = Environment.CurrentDirectory + "/../gmailapi/token.json";
                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.FromStream(stream).Secrets,
                    Scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;
            }
            var service = new GmailService(new BaseClientService.Initializer
                {
                    HttpClientInitializer = credential,
                    ApplicationName = "CrmWebApp"
                });
            UsersResource.MessagesResource.ListRequest request = service.Users.Messages.List("me");
            IList<Message> messages = request.Execute().Messages;
            return Ok(messages);
        }
    }
}