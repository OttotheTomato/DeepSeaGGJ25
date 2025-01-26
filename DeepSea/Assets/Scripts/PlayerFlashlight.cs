using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFlashlight : MonoBehaviour
{
    [SerializeField]
    private Camera m_Camera;

    private AnnoyingFish currentFish;
    void Update()
    {
        RaycastHit hit;

        if (Physics.Raycast(m_Camera.transform.position, m_Camera.transform.forward, out hit))
        {
            if (hit.collider.gameObject.CompareTag("Fish"))
            {
                currentFish = hit.collider.gameObject.GetComponent<AnnoyingFish>();
                currentFish._Flashed = true;
            }
            else
            {
                currentFish._Flashed = false;
                currentFish = null;
            }
        }
    }
}
