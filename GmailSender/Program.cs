// Copyright 2018 Google LLC
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

// [START gmail_quickstart]
using Google.Apis.Auth.OAuth2;
using Google.Apis.Gmail.v1;
using Google.Apis.Gmail.v1.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using MimeKit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GmailQuickstart
{
    class Program
    {
        // If modifying these scopes, delete your previously saved credentials
        // at ~/.credentials/gmail-dotnet-quickstart.json
        static string[] Scopes =
        {
            GmailService.Scope.MailGoogleCom
        };
        static readonly char[] padding = { '=' };
        static string ApplicationName = "Gmail API .NET Quickstart";

        static void Main(string[] args)
        {
            UserCredential credential;

            using (var stream =
                new FileStream("credentials.json", FileMode.Open, FileAccess.Read))
            {
                // The file token.json stores the user's access and refresh tokens, and is created
                // automatically when the authorization flow completes for the first time.
                string credPath = "token.json";
                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    Scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;
                Console.WriteLine("Credential file saved to: " + credPath);
            }

            // Create Gmail API service.
            var service = new GmailService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });

            var msg = GetMessage();

            for(int i = 0; i < 99; i++)
            {
                service.Users.Messages.Send(msg, "me").Execute();
                System.Threading.Thread.Sleep(15000);
            }
        }

        private static Message GetMessage()
        {
            using (var ms = new MemoryStream())
            {
                var mime = GetMimeMessage();
                mime.WriteTo(ms);
                var bytes = ms.ToArray();
                var encodedStr = Convert
                    .ToBase64String(bytes)
                    .TrimEnd(padding)
                    .Replace('+', '-')
                    .Replace('/', '_');

                var msg = new Message { Raw = encodedStr };
                return msg;
            }
        }

        private static MimeMessage GetMimeMessage()
        {
            var msg = new MimeMessage();
            msg.From.Add(new MailboxAddress("", "from@gmail.com"));
            msg.To.Add(new MailboxAddress("", "to@gmmail.com"));
            msg.Subject = "subject";
            msg.Body = new TextPart("plain") { Text = GetContent() };
            return msg;
        }

        private static string GetContent()
        {
            return System.IO.File.ReadAllText("content.txt");
        }
    }
}
// [END gmail_quickstart]