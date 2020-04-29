using UnityEngine;
using ZigSimTools;
using Quaternion = UnityEngine.Quaternion;

public class ObjectRotator : MonoBehaviour
{
    private Quaternion targetRotation;

    void Start ()
    {
        ZigSimDataManager.Instance.StartReceiving ();
        ZigSimDataManager.Instance.QuaternionCallBack += (ZigSimTools.Quaternion q) =>
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

        if (Input.GetKeyDown (KeyCode.S))
        {
            ZigSimDataManager.Instance.StartReceiving ();
        }

        if (Input.GetKeyDown (KeyCode.Escape))
        {
            ZigSimDataManager.Instance.StopReceiving ();
        }
    }
}