﻿/**
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

using System;
using Microsoft.AspNet.SignalR.Client;

namespace Net.Appclusive.SignalR.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            var hubConnection = new HubConnection("http://localhost:53344/");
            IHubProxy workerHubProxy = hubConnection.CreateHubProxy("WorkerHub");
            
            // register for an event
            workerHubProxy.On<string>("ProcessWorkItem", ProcessWorkItem);
            hubConnection.Start();

            // call method on server
            workerHubProxy.Invoke("NotifyServer", "Hi server");

            while (true)
            {
                
            }
        }

        private static void ProcessWorkItem(string message)
        {
            Console.WriteLine(message);
        }
    }
}
