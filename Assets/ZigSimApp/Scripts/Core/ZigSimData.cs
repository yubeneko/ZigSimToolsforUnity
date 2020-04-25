using System;

namespace ZigSimTools
{
    [Serializable]
    public class ZigSimData
    {
        public Device device;
        public string timestamp;
        public SensorData sensordata;

        public override string ToString ()
        {
            return "ZigSimData: \n" +
                $"{device.ToString()}\n" +
                $"timestamp: {timestamp}\n\n" +
                $"{sensordata.ToString ()}\n";
        }
    }

    /// <summary>
    /// Basic information
    /// </summary>
    [Serializable]
    public class Device
    {
        public string name;
        public string uuid;
        public string os;
        public string osversion;
        public int displaywidth;
        public int displayheight;

        public override string ToString ()
        {
            return "device: \n" +
                $"name: {name}\n" +
                $"uuid: {uuid}\n" +
                $"os: {os}\n" +
                $"osversion: {osversion}\n" +
                $"displaywidth: {displaywidth}\n" +
                $"displayheight: {displayheight}\n";
        }
    }

    [Serializable]
    public class SensorData
    {
        public Accel accel;
        public Gravity gravity;
        public Gyro gyro;
        public Quaternion quaternion;
        public Compass compass;
        public Pressure pressure;
        public Gps gps;
        public MicLevel miclevel;
        public Proximitymonitor proximitymonitor;
        public RemoteControl remotecontrol;
        public Touch[] touch;
        public Beacon[] beacon;

        public override string ToString ()
        {
            return "sensordata: \n" +
                $"{accel.ToString ()}\n" +
                $"{gravity.ToString ()}\n" +
                $"{gyro.ToString ()}\n" +
                $"{quaternion.ToString ()}\n" +
                $"{compass.ToString ()}\n" +
                $"{pressure.ToString ()}\n" +
                $"{gps.ToString ()}\n" +
                $"{miclevel.ToString ()}\n" +
                $"{proximitymonitor.ToString ()}\n" +
                $"{remotecontrol.ToString ()}\n" +
                GetTouchArrayString () + "\n" +
                GetBeaconArrayString () + "\n";
        }

        public string GetTouchArrayString ()
        {
            var res = "";
            var count = 0;

            if (touch == null) return res;

            foreach (var t in touch)
            {
                count++;
                var s = $"{count} " +
                    $"{t.ToString ()}\n";
                res += s;
            }
            return res;
        }

        public string GetBeaconArrayString ()
        {
            var res = "";
            var count = 0;

            if (beacon == null) return res;

            foreach (var b in beacon)
            {
                count++;
                var s = $"{count} " +
                    $"{b.ToString ()}\n";
                res += s;
            }
            return res;
        }
    }

    /// <summary>
    /// Output when "ACCEL" is selected
    /// </summary>
    [Serializable]
    public class Accel
    {
        public double x;
        public double y;
        public double z;

        public override string ToString ()
        {
            return "accel: \n" +
                $"x: {x.ToString ()}\n" +
                $"y: {y.ToString ()}\n" +
                $"z: {z.ToString ()}\n";
        }
    }

    /// <summary>
    /// Output when "GRAVITY" is selected
    /// </summary>
    [Serializable]
    public class Gravity
    {
        public double x;
        public double y;
        public double z;

        public override string ToString ()
        {
            return "gravity: \n" +
                $"x: {x.ToString ()}\n" +
                $"y: {y.ToString ()}\n" +
                $"z: {z.ToString ()}\n";
        }
    }

    /// <summary>
    /// Output when "GYRO" is selected
    /// </summary>
    [Serializable]
    public class Gyro
    {
        public double x;
        public double y;
        public double z;

        public override string ToString ()
        {
            return "gyro: \n" +
                $"x: {x.ToString ()}\n" +
                $"y: {y.ToString ()}\n" +
                $"z: {z.ToString ()}\n";
        }
    }

    /// <summary>
    /// Output when "QUATERNION" is selected
    /// </summary>
    [Serializable]
    public class Quaternion
    {
        public double x;
        public double y;
        public double z;
        public double w;

        public override string ToString ()
        {
            return "quaternion: \n" +
                $"x: {x.ToString ()}\n" +
                $"y: {y.ToString ()}\n" +
                $"z: {z.ToString ()}\n" +
                $"w: {w.ToString ()}\n";
        }
    }

    /// <summary>
    /// Output when "COMPASS" is selected
    /// </summary>
    [Serializable]
    public class Compass
    {
        public bool faceup;
        public double compass;

        public override string ToString ()
        {
            return "compass: \n" +
                $"faceup: {faceup.ToString ()}\n" +
                $"compass: {compass.ToString ()}\n";
        }
    }

    /// <summary>
    /// Output when "PRESSURE" is selected
    /// </summary>
    [Serializable]
    public class Pressure
    {
        public double pressure;
        public double altitude;

        public override string ToString ()
        {
            return "pressure: \n" +
                $"pressure: {pressure.ToString ()}\n" +
                $"altitude: {altitude.ToString ()}\n";
        }
    }

    /// <summary>
    /// Output when "GPS" is selected
    /// </summary>
    [Serializable]
    public class Gps
    {
        public double latitude;
        public double longitude;

        public override string ToString ()
        {
            return "gps: \n" +
                $"latitude: {latitude.ToString ()}\n" +
                $"longitude: {longitude.ToString ()}\n";
        }
    }

    /// <summary>
    /// // Output when any of "2D Touch", "TOUCH RADIUS", or "3D TOUCH" is selected
    /// </summary>
    [Serializable]
    public class Touch
    {
        // Output when "2D Touch" is selected
        public float x;
        // Output when "2D Touch" is selected
        public float y;
        // Output when "Touch RADIUS" is selected
        public float radius;
        // Output when "3D Touch" is selected
        public float force;

        public override string ToString ()
        {
            return "Touch: \n" +
                $"x: {x.ToString ()}\n" +
                $"y: {y.ToString ()}\n" +
                $"radius: {radius.ToString ()}\n" +
                $"force: {force.ToString ()}\n";
        }
    }

    [Serializable]
    public class Beacon
    {
        public string uuid;
        public int rssi;
        public int major;
        public int minor;

        public override string ToString ()
        {
            return "Beacon: \n" +
                $"uuid: {uuid}\n" +
                $"rssi: {rssi.ToString ()}\n" +
                $"major: {major.ToString ()}\n" +
                $"minor: {minor.ToString ()}\n";
        }
    }

    [Serializable]
    public class Proximitymonitor
    {
        public bool proximitymonitor;

        public override string ToString ()
        {
            return "proximitymonitor: \n" +
                $"proximitymonitor: {proximitymonitor.ToString ()}\n";
        }
    }

    /// <summary>
    /// Output when "MIC LEVEL" is selected
    /// </summary>
    [Serializable]
    public class MicLevel
    {
        public double average;
        public double max;

        public override string ToString ()
        {
            return "miclevel: \n" +
                $"average: {average.ToString ()}\n" +
                $"max: {max.ToString ()}\n";
        }
    }

    [Serializable]
    public class RemoteControl
    {

        public bool volumeup;
        public bool playpause;
        public bool volumedown;

        public override string ToString ()
        {
            return "remotecontrol: \n" +
                $"volumeup: {volumeup.ToString ()}\n" +
                $"playpause: {playpause.ToString ()}\n" +
                $"volumedown: {volumedown.ToString ()}\n";
        }
    }
}