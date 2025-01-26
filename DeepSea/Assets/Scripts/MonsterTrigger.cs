using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterTrigger : MonoBehaviour
{
    private bool Triggered = false;

    [SerializeField]
    private Monster M;

    private void OnTriggerEnter(Collider other)
    {
        if (!Triggered & other.gameObject.CompareTag("Player"))
        {
            M.Seen = true;
        }
    }
}
