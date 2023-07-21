
# Audio Recording Devices Monitoring App

This project is a C# application built using the NAudio library. Its purpose is to monitor the status of audio recording devices connected to Windows and check if a microphone is currently recording.



## Installation

This project uses the NAudio library. Therefore, you need to install NAudio in the directory where your project files are located using the NuGet Package Manager.

Open the directory where your project files are located in Visual Studio.
Go to Tools > NuGet Package Manager > Package Manager Console.
In the opened console, enter the following command to install NAudio package:

```package-manager
Install-Package NAudio
```

  
## Features

- List active audio recording devices in Windows.
- Monitor sessions for each audio recording device and check recording status.

  
## How To Use


```csharp
using console_test.Class;

class Program
{
    static void Main(string[] args)
    {
        // To start by listing audio recording devices:
        MicrophoneManager manager = new MicrophoneManager();
        manager.CaptureDeviceSessionStatus();

        // To check if a microphone is currently recording:
        bool isRecording = MicrophoneManager.IsMicrophoneRecording();
        Console.WriteLine("Is the microphone currently recording? " + isRecording);
    }
}
```

  
## License
This project is licensed under the [MIT License](https://choosealicense.com/licenses/mit/). For more information, see the [MIT License](https://choosealicense.com/licenses/mit/) file.

  
