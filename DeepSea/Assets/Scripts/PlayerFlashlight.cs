using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFlashlight : MonoBehaviour
{
    [SerializeField]
    private Camera m_Camera;

    public float _Range = 3f;

    private AnnoyingFish currentFish;
    void Update()
    {
        RaycastHit hit;

        if (Physics.Raycast(m_Camera.transform.position, m_Camera.transform.forward, out hit))
        {
            if (hit.collider.gameObject.CompareTag("Fish") && hit.distance < _Range && CanSeeTarget(hit.transform))
            {
                currentFish = hit.collider.gameObject.GetComponent<AnnoyingFish>();
                currentFish._Flashed = true;
            }
            else
            {
                if (currentFish != null)
                {
                    currentFish._Flashed = false;
                    currentFish = null;
                }
            }
        }
    }

    private bool CanSeeTarget(Transform _Target)
    {
        RaycastHit[] _Objects = Physics.RaycastAll(transform.position, _Target.position - transform.position, Vector3.Distance(transform.position, _Target.position));

        for (int i = 0; i < _Objects.Length; i++)
        {
            if (_Objects[i].collider.gameObject.CompareTag("Obstruction"))
                return false;
        }
        return true;
    }
}
