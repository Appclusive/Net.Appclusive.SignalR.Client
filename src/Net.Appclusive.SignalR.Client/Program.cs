/**
 * Copyright 2017 d-fens GmbH
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 * http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System.Threading;
using Microsoft.AspNet.SignalR.Client;
using Net.Appclusive.Public.SignalR;

namespace Net.Appclusive.SignalR.Client
{
    public class Program
    {
        private const string APPCLUSIVE_BASE_URI = "http://appclusive/";

        public static void Main(string[] args)
        {
            // initialise connection & proxy
            var hubConnection = new HubConnection(APPCLUSIVE_BASE_URI);
            var workerHubProxy = hubConnection.CreateHubProxy(nameof(IWorkerHub).TrimStart('I'));
            
            var worker = new Worker();
            var workerHub = new WorkerHubBase(workerHubProxy);

            // register for an event
            workerHubProxy.On<string>(nameof(IWorker.ProcessWorkItem), worker.ProcessWorkItem);

            hubConnection.Start().Wait();
            
            while (true)
            {
                // call method on server
                workerHub.NotifyServer("Hi Server");

                Thread.Sleep(20 * 1000);
            }
        }
    }
}
