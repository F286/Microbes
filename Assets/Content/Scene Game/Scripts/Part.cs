using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public abstract class Part : MonoBehaviour
{
    public CONTENT_Microbe microbe
    {
        get
        {
            return GetComponentInParent<CONTENT_Microbe>();
        }
    }
    public NetworkInstanceId id
    {
        get
        {
            return GetComponentInParent<CONTENT_Microbe>().netId;
        }
    }
    public abstract void NetworkUpdate();
}
