using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class CONTENT_Camera : MonoBehaviour 
{
    public float Smooth = 0.2f;
    public float offset = 2;
    public Transform target;
    Vector2 Velocity;

	public void LateUpdate () 
    {
        if (target == null)
        {
            var f = GameObject.FindGameObjectsWithTag("microbe");
            foreach (var item in f)
            {
                if (item.GetComponent<NetworkBehaviour>().isLocalPlayer)
                {
                    target = item.GetComponentInChildren<CONTENT_CameraFollow>().transform;
                    break;
                }
            }
        }
        if (target)
        {
            var o = Vector2.Scale(Input.mousePosition, new Vector2(1f / Screen.width, 1f / Screen.height)) * offset;

            Vector3 set = Vector2.SmoothDamp(
                          (Vector2)transform.position, (Vector2)target.position + o, ref Velocity, Smooth);
            set.z = -10;
            transform.position = set;
        }
	}
}
