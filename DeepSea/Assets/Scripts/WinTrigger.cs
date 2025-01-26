using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinTrigger : MonoBehaviour
{
    private bool Triggered = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") & !Triggered)
        {
            Elevator e = transform.parent.gameObject.GetComponent<Elevator>();
            e.Player = other.gameObject;
            e.Inside = true;
            Triggered = true;
        }
    }
}
