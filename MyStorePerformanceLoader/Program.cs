using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Threading;

namespace MyStoreTest
{
    internal class Program
    {
        private static readonly Int32 _commandNumber = 10000;
        private static readonly Int32 _instanceNumber = 500;
        private static readonly String _pathToExe = @"D:\projects\MyStore\MyStore.Client\bin\Debug\MyStore.Client.exe";
        private static readonly String _command = "listallcars";
        private static readonly String _loginCommand = "login";
        static void Main()
        {
            LaunchTest2();
        }

        private static void LaunchInstance(object instanceNumber)
        {
            Console.WriteLine("[{0}]\tInstance {1} started work", DateTime.Now, instanceNumber);
            ProcessStartInfo info = new ProcessStartInfo(_pathToExe)
            {
                RedirectStandardInput = true,
                RedirectStandardOutput = true,
                UseShellExecute = false
            };
            using (Process process = new Process())
            {
                process.StartInfo = info;
                process.Start();
                process.StandardOutput.Close();
                process.StandardInput.WriteLine(_loginCommand);

                for (int i = 0; i < _commandNumber; i++)
                {
                    process.StandardInput.WriteLine(_command);
                }
                Console.WriteLine("[{0}]\tInstance {1} finished work", DateTime.Now, instanceNumber);
                process.WaitForExit();
                process.Kill();
            }
        }

        private static void LaunchTest()
        {
            var tasks = new List<Task>();
            for (int i = 0; i < _instanceNumber; i++)
            {
                int j = i;
                tasks.Add(Task.Run(() =>
                {
                    LaunchInstance(j);
                }));
            }
            Task.WaitAll(tasks.ToArray());
        }

        private static void LaunchTest2()
        {
            var threads = new Thread[_instanceNumber];
            for (int i = 0; i < _instanceNumber; i++)
            {
                threads[i] = new Thread(LaunchInstance);
            }
            for (int i = 0; i < _instanceNumber; i++)
            {
                threads[i].Start(i);
            }
        }
    }
}
