using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public bool Seen;

    private void Update()
    {
        if (Seen)
        {
            GetComponent<SpriteRenderer>().enabled = true;
            transform.Translate(Vector3.right * Time.deltaTime * 20f);
        }

    }
}
