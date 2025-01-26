using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundTrigger : MonoBehaviour
{
    private bool Played = false;
    [SerializeField]
    private string AudioToPlay;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") & !Played)
        {
            AudioManager.Instance.SpawnSoundAtLocation(AudioToPlay, transform);
            Played = true;
        }
    }
}
