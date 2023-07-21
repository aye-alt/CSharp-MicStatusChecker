using NAudio.CoreAudioApi.Interfaces;
using NAudio.CoreAudioApi;
using System;
using System.Runtime.InteropServices;


namespace console_test.Class
{
    public class MicrophoneManager
    {
        public void CaptureDeviceSessionStatus()
        {
            PropVariant friendlyName = new PropVariant();
            try
            {
                MMDeviceEnumerator enumerator = new MMDeviceEnumerator();
                var captureDevices = enumerator.EnumerateAudioEndPoints(DataFlow.Capture, DeviceState.Active);


                foreach (var captureDevice in captureDevices)
                {
                    AudioSessionManager sessionManager = null;

                    try
                    {
                        sessionManager = captureDevice.AudioSessionManager;
                        var sessions = sessionManager.Sessions;

                        for (int z = 0; z < sessions.Count; z++)
                        {
                            AudioSessionControl sessionControl = sessions[z];
                            sessionControl.RegisterEventClient(new AudioEvents());
                        }
                    }
                    finally
                    {
                        sessionManager?.Dispose();
                    }

                }
            }
            catch(Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
            }
            finally
            {
                Marshal.FreeCoTaskMem(friendlyName.pointerValue);
            }
        }

        public class AudioEvents : IAudioSessionEventsHandler
        {
            void IAudioSessionEventsHandler.OnStateChanged(AudioSessionState state)
            {
                IsMicrophoneRecording();
            }

            public static bool IsMicrophoneRecording()
            {
                PropVariant friendlyName = new PropVariant();
                try
                {
                    MMDeviceEnumerator deviceEnumerator = new MMDeviceEnumerator();
                    MMDeviceCollection deviceCollection = deviceEnumerator.EnumerateAudioEndPoints(DataFlow.Capture, DeviceState.Active);

                    int deviceCount = deviceCollection.Count;

                    for (int i = 0; i < deviceCount; i++)
                    {
                        MMDevice captureDevice = deviceCollection[i];
                        PropertyStore propertyStore = captureDevice.Properties;
                        friendlyName = propertyStore.GetValue(PropertyKeys.PKEY_Device_DeviceDesc.propertyId);
                        if (true)
                        {
                            AudioSessionManager sessionManager = captureDevice.AudioSessionManager;
                            if (sessionManager != null)
                            {
                                int sessionCount = (sessionManager.Sessions.Count);
                                for (int z = 0; z < sessionCount; z++)
                                {
                                    AudioSessionControl sessionControl = sessionManager.Sessions[z];

                                    Console.WriteLine("SessionInstanceIdentifier: {0}", sessionControl.GetSessionInstanceIdentifier);
                                    Console.WriteLine("Session state: {0}", sessionControl.State);

                                    if (sessionControl.State == AudioSessionState.AudioSessionStateActive)
                                    {
                                        return true;
                                    }

                                }
                            }

                        }
                    }
                }
                finally
                {
                    Marshal.FreeCoTaskMem(friendlyName.pointerValue);
                }

                return false;
            }

            void IAudioSessionEventsHandler.OnVolumeChanged(float volume, bool isMuted)
            {
            }

            void IAudioSessionEventsHandler.OnDisplayNameChanged(string displayName)
            {
            }

            void IAudioSessionEventsHandler.OnIconPathChanged(string iconPath)
            {
            }

            void IAudioSessionEventsHandler.OnChannelVolumeChanged(uint channelCount, IntPtr newVolumes, uint channelIndex)
            {
            }

            void IAudioSessionEventsHandler.OnGroupingParamChanged(ref Guid groupingId)
            {
            }

            void IAudioSessionEventsHandler.OnSessionDisconnected(AudioSessionDisconnectReason disconnectReason)
            {
            }
        }
    }
}
