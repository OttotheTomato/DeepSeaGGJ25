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
        private float OxygenDrainRate = .1f;
        [SerializeField]
        private float StartingOxygenLevel;
        [SerializeField]
        private float CurrentOxygenLevel;

        private bool isInBubble;

        // Start is called before the first frame update
        void Start()
        {
            isInBubble = false;
            CurrentOxygenLevel = StartingOxygenLevel;
        }

        // Update is called once per frame
        void Update()
        {
            if (!isInBubble){
                CurrentOxygenLevel -= OxygenDrainRate * Time.deltaTime;
            }
            UpdateOxygenText();
        }

        private void UpdateOxygenText() {
           HUDManager.Instance.UpdateOxygen(CurrentOxygenLevel);
        }

        private void OnCollisionStay(Collision other) {
            if (other.gameObject.CompareTag("Bubble")){
                isInBubble = true;
                other.gameObject.GetComponent<BubbleController>().Deflate();
                if (CurrentOxygenLevel < MaxOxygenLevel){
                    CurrentOxygenLevel += OxygenRechargeRate * Time.deltaTime;
                }else{
                    CurrentOxygenLevel = MaxOxygenLevel;
                }
            }
        }

        private void OnCollisionExit(Collision other) {
            isInBubble = false;
        }
    }
}