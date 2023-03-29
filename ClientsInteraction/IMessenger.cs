﻿using System;
using System.Threading.Tasks;

namespace MyStore.Server
{
    internal interface IMessenger
    {
        Task<String> ReceiveMessageAsync();

        Task SendMessageAsync(String message);
    }
}
