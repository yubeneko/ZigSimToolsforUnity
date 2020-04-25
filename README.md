# ZigSimToolsforUnity

Provides easy way to use Zig Sim on Unity.

## About ZigSim

https://zig-project.com

## Features
Using this assets, Unity receives Json data from Zig Sim over the UDP protocol. This asset uses Json Utility. So you can deserialize quickly.

## Attention
The data received is limited to the free version of ZigSim.

## Getting Started
To start using this assets, you might try to execute ObjectRotationSample Scene. Before run this scene, you have to confirm some settings.

1. Download ZigSim App for your mobile device from App store or Google Play.

2. Launch the app. Press the Settings button and set as follows:

    Settings
   - DATA DESTINATION: OTHER APP
   - PROTOCOL: UDP
   - IP ADDRESS: Your PC's IP address.
   - PORT NUMBER: 50000
   - MESSAGE FORMAT: JSON
   - MESSAGE RATE: 30
   - DEVICE UUID: Don't edit
   - COMPASS ANGLE: Don't edit
   - BEACON: Don't edit

3. Press the Sensor button and check only QUATERNION.

4. Press Start button on Zig Sim and Run Unity scene. The Object starts rotating same as your mobile device.

## How to use sensor data.
The `ZigSimDataManager` class has a callback function that takes each sensor data as an argument.

```csharp
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
```

The sensor data received using this callback function can be used by other classes. Since the `ZigSimDataManager` class is a singleton class, it can be accessed from any class in the same scene.

ex) ObjectRotator.cs
```csharp
using UnityEngine;
using ZigSim;
using Quaternion = UnityEngine.Quaternion;

public class ObjectRotator : MonoBehaviour
{
    private Quaternion targetRotation;

    void Start ()
    {
        ZigSimDataManager.Instance.StartReceiving ();
        ZigSimDataManager.Instance.QuaternionCallBack += (ZigSim.Quaternion q) =>
        {
            // Debug.Log (q.ToString ());
            var newQut = new Quaternion ((float) - q.x, (float) - q.z, (float) - q.y, (float) q.w);
            var newRot = newQut * Quaternion.Euler (90f, 0, 0);
            targetRotation = newRot;
        };
    }

    void Update ()
    {
        transform.localRotation = targetRotation;

        if (Input.GetKeyDown (KeyCode.Escape))
        {
            ZigSimDataManager.Instance.StopReceiving ();
        }
    }
}
```

Basic Data
```csharp
ZigSimDataManager.Instance.BasicDataCallBack += (Device d, string timestamp) =>
{
    Debug.Log (d.ToString ());
    Debug.Log (timestamp);
};
```

Touch Sensor
```csharp
ZigSimDataManager.Instance.TouchCallBack += (ZigSim.Touch[] touches) =>
{
    var count = 0;
    foreach (var t in touches)
    {
        Debug.Log ($"{count} : {t.ToString()}");
        count++;
    }
};
```

## License
This software is released under the MIT License, see LICENSE.txt.