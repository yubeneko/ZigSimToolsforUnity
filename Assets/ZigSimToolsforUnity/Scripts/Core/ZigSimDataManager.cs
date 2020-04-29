using System;
using UnityEngine;

namespace ZigSimTools
{
    public class ZigSimDataManager : SingletonMonoBehaviour<ZigSimDataManager>
    {
        [SerializeField]
        private int portNumber = 50000;
        private UdpReceiver udpReceiver;
        private bool isInitialized;

        public Action<Device, string> BasicDataCallBack;
        public Action<Accel> AccelCallBack;
        public Action<Gravity> GravityCallBack;
        public Action<Gyro> GyroCallBack;
        public Action<Quaternion> QuaternionCallBack;
        public Action<Compass> CompassCallBack;
        public Action<Pressure> PressureCallBack;
        public Action<Gps> GpsCallBack;
        public Action<MicLevel> MicLevelCallBack;
        public Action<Touch[]> TouchCallBack;

        public void StartReceiving ()
        {
            void Initialize ()
            {
                udpReceiver = new UdpReceiver (portNumber);
                var context = System.Threading.SynchronizationContext.Current;

                udpReceiver.MessageReceived += (s, e) =>
                {
                    context.Post (_ =>
                    {
                        var zigsimData = JsonUtility.FromJson<ZigSimData> (e.Message);
                        //Debug.Log (zigsimData.sensordata.ToString ());

                        BasicDataCallBack?.Invoke (zigsimData.device, zigsimData.timestamp);
                        AccelCallBack?.Invoke (zigsimData.sensordata.accel);
                        GravityCallBack?.Invoke (zigsimData.sensordata.gravity);
                        GyroCallBack?.Invoke (zigsimData.sensordata.gyro);
                        QuaternionCallBack?.Invoke (zigsimData.sensordata.quaternion);
                        CompassCallBack?.Invoke (zigsimData.sensordata.compass);
                        PressureCallBack?.Invoke (zigsimData.sensordata.pressure);
                        GpsCallBack?.Invoke (zigsimData.sensordata.gps);
                        MicLevelCallBack?.Invoke (zigsimData.sensordata.miclevel);
                        TouchCallBack?.Invoke (zigsimData.sensordata.touch);
                    }, null);
                };

                udpReceiver.Disconnected += (s, e) =>
                {
                    Debug.Log ("udp client was closed.");
                };
            }

            if (!isInitialized)
            {
                Initialize ();
                isInitialized = true;
            }
            udpReceiver.StartReceiving ();
        }

        public async void StopReceiving ()
        {
            await udpReceiver.StopReceiving ();
        }

        protected override async void OnDestroy ()
        {
            await udpReceiver.StopReceiving ();
            base.OnDestroy ();
        }

        private async void OnApplicationQuit ()
        {
            await udpReceiver.StopReceiving ();
        }
    }
}