using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OxygenTestPlayerController : MonoBehaviour
{

    [SerializeField]
    private float MaxOxygenLevel;

    [SerializeField]
    private float StartingOxygenLevel;
    private float CurrentOxygenLevel;

    // Start is called before the first frame update
    void Start()
    {
        CurrentOxygenLevel = StartingOxygenLevel;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionStay(Collision other) {
        Debug.Log("Collision detecte");
        if (other.gameObject.CompareTag("Bubble")){
            other.gameObject.GetComponent<BubbleController>().Deflate();
            CurrentOxygenLevel += 1;
        }
    }
}
