using System;
using console_test.Class;

namespace console_test
{
    public class Program
    {
        static void Main()
        {
            MicrophoneManager microphoneManager = new MicrophoneManager();
            microphoneManager.CaptureDeviceSessionStatus();
            Console.ReadKey();
        }
    }
}
