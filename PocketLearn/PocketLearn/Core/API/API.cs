using EmbedIO;
using EmbedIO.WebApi;
using PocketLearn.Public.Core.Config;
using Swan.Logging;
using System;
using System.Threading;

namespace PocketLearn.Win.API
{
    public class API
    {
        public WebServer ActiveServer;
        private CancellationTokenSource apiToken;
        public readonly int Port;
        private Thread serverThread;

        public API(WinConfig config)
        {
            Port = config.Port;
            Start();
        }

        public WebServer CreateServer()
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
            catch (Exception ex)
            {
                Logger.Error($"failed to start web server: {ex}");
            }
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
            catch (Exception ex)
            {
                Logger.Error($"failed to stop API: {ex}");
            }
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
            catch (Exception ex)
            {
                serverThread.Abort();
            }
        }
    }
}
