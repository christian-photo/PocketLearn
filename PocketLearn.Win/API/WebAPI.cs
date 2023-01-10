#region "copyright"

/*
    Copyright © 2023 Christian Palm (christian@palm-family.de)
    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at http://mozilla.org/MPL/2.0/.
*/

#endregion "copyright"

using EmbedIO;
using EmbedIO.WebApi;
using PocketLearn.Public.Core.Config;
using System.Threading;

namespace PocketLearn.Win.API
{
    public class WebAPI
    {
        public WebServer ActiveServer;
        private CancellationTokenSource apiToken;
        public readonly int Port;
        private Thread serverThread;

        public WebAPI(WinConfig config)
        {
            Port = config.Port;
            Start();
        }

        private WebServer CreateServer()
        {
            ActiveServer = new WebServer(o => o
               .WithUrlPrefix($"http://*:{Port}")
               .WithMode(HttpListenerMode.EmbedIO)
               );

            ActiveServer.WithWebApi($"/api", m => m.WithController<APIHandler>());
            return ActiveServer;
        }

        public void Start()
        {
            try
            {
                serverThread = new Thread(APITask);
                serverThread.Name = "API Thread";
                serverThread.SetApartmentState(ApartmentState.STA);
                serverThread.Start();
            }
            catch { }
        }

        public void Stop()
        {
            try
            {
                if (ActiveServer != null)
                {
                    if (ActiveServer.State != WebServerState.Stopped)
                    {
                        apiToken.Cancel();
                        ActiveServer.Dispose();
                        ActiveServer = null;
                    }
                }

                if (serverThread != null && serverThread.IsAlive)
                {
                    serverThread.Abort();
                    serverThread = null;
                }
            }
            catch { }
        }

        private void APITask()
        {
            try
            {
                using (WebServer webServer = CreateServer())
                {
                    apiToken = new CancellationTokenSource();
                    webServer.RunAsync(apiToken.Token).Wait();
                }
            }
            catch
            {
                serverThread.Abort();
            }
        }
    }
}
