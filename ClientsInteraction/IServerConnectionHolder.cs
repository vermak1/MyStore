using System;

namespace MyStore.Server
{
    internal interface IServerConnectionHolder : IDisposable
    {
        void OpenConnection();
        void CloseConnection();
    }
}
