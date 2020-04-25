## ZigSimについて

https://zig-project.com

## 本アセットについて
本アセットはZigSimからUDP通信で送られてくるJsonフォーマットのセンサーデータをUnityで受信して利用するためのアセットです。Json Utilityを使うことで高速でJsonのパースを行い、ZigSimアプリから送られてくる全てのデータを格納できるようにしました。

注意: 無料版で使用できるデータに限られます。

## ObjectRotationSampleシーンを動かす
ObjectRotationSampleシーンはUnity内のオブジェクトをスマホ(タブレット)の回転と同期させて回転させることができるサンプルシーンです。モバイルデバイスでZigSimアプリをダウンロードし、各種設定を確認します。

Settings
- DATA DESTINATION: OTHER APP
- PROTOCOL: UDP
- IP ADDRESS: PCのIPアドレス
- PORT NUMBER: 50000
- MESSAGE FORMAT: JSON
- MESSAGE RATE: 30
- DEVICE UUID: そのままで
- COMPASS ANGLE: そのままで
- BEACON: そのままで

Sensor
- QUATERNION にチェックを入れる

以上で準備は完了です。Unityで ObjectRotationSample シーンを再生し、モバイルアプリでデータの送信を開始するとUnityシーン内のオブジェクトがモバイルデバイスの回転と同期して回転するようになります。

## センサーデータの使い方
`ZigSimDataManager`クラスはそれぞれのセンサーデータを引数にとるコールバック関数を持っています。

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

このコールバック関数を使って受け取ったセンサーデータを他のクラスで利用することができます。なお、`ZigSimDataManager`クラスはシングルトンなクラスであるため、同一シーンであればどのクラスからでもアクセスできます。

例) ObjectRotator.cs
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

基本データ
```csharp
ZigSimDataManager.Instance.BasicDataCallBack += (Device d, string timestamp) =>
{
    Debug.Log (d.ToString ());
    Debug.Log (timestamp);
};
```

タッチセンサ
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
