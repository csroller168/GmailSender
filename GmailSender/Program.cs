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
using System.IO;
using System.Threading;

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
            //var contentPath = args[0];
            var symbols = args[0];
            var recipients = args[1].Split(',');

            UserCredential credential;
            var credentialsPath = Path.Combine(
                Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location),
                "credentials.json");
            using (var stream =
                new FileStream(credentialsPath, FileMode.Open, FileAccess.Read))
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

            var msg = GetMessage(symbols, recipients);

            service.Users.Messages.Send(msg, "me").Execute();
        }

        private static Message GetMessage(string symbols, string[] recipients)
        {
            using (var ms = new MemoryStream())
            {
                var mime = GetMimeMessage(symbols, recipients);
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

        private static MimeMessage GetMimeMessage(string symbols, string[] recipients)
        {
            var msg = new MimeMessage();
            msg.From.Add(new MailboxAddress("", "chrisshort168@gmail.com"));
            foreach(var to in recipients)
            {
                msg.To.Add(new MailboxAddress("", to));
            }
            msg.Subject = "Trading app notification";
            msg.Body = new TextPart("plain") { Text = GetContent(symbols) };
            return msg;
        }

        private static string GetContent(string symbols)
        {
            //return File.ReadAllText(contentPath);
            return "This is an automated email from Chris's trading app."
                + Environment.NewLine
                + $"The app is long {symbols}";
        }
    }
}
// [END gmail_quickstart]