using UnityEngine;
using System.Collections;

public class SCR_Forward : MonoBehaviour 
{
    public GameObject Target;

    void OnTriggerEnter2D(Collider2D other) 
    {
        Target.SendMessage("OnTriggerEnter2D", other);
    }
    void OnTriggerExit2D(Collider2D other) 
    {
        Target.SendMessage("OnTriggerExit2D", other);
    }
}
