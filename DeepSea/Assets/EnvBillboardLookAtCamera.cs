using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvBillboardLookAtCamera : MonoBehaviour
{
    [SerializeField] Transform CameraTransform;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.forward = CameraTransform.forward;
    }
}
