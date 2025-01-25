using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace game
{
    public class OxygenTestPlayerController : MonoBehaviour
    {

        [SerializeField]
        private float MaxOxygenLevel;
        [SerializeField]
        private float OxygenRechargeRate = .5f;
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
            UpdateOxygenText();
        }

        private void UpdateOxygenText() {
           HUDManager.Instance.UpdateOxygen(CurrentOxygenLevel);
        }

        private void OnCollisionStay(Collision other) {
            if (other.gameObject.CompareTag("Bubble")){
                other.gameObject.GetComponent<BubbleController>().Deflate();
                if (CurrentOxygenLevel < MaxOxygenLevel){
                    CurrentOxygenLevel += OxygenRechargeRate * Time.deltaTime;
                }else{
                    CurrentOxygenLevel = MaxOxygenLevel;
                }
            }
        }
    }
}