using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace game
{
    public class PlayerOxygenController : MonoBehaviour
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

        private bool Gasped = false;
        private AudioSource AS;

        private bool isInBubble;

        // Start is called before the first frame update
        void Start()
        {
            AS = GetComponent<AudioSource>();
            isInBubble = false;
            CurrentOxygenLevel = StartingOxygenLevel;
        }

        // Update is called once per frame
        void Update()
        {
            if (!isInBubble)
            {
                Gasped = false;
                CurrentOxygenLevel -= OxygenDrainRate * Time.deltaTime;
                if (CurrentOxygenLevel < 0)
                {
                    CurrentOxygenLevel = 0;
                    PlayerDie();
                }
            }
            UpdateOxygenText();
        }

        public void RemoveOxygen(float oxygenToRemove)
        {
            CurrentOxygenLevel -= oxygenToRemove;
        }

        private void UpdateOxygenText()
        {
            HUDManager.Instance.UpdateOxygen(CurrentOxygenLevel);
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.gameObject.CompareTag("Bubble"))
            {
                isInBubble = true;
                other.gameObject.GetComponent<BubbleController>().Deflate();
                if (CurrentOxygenLevel < MaxOxygenLevel)
                {
                    CurrentOxygenLevel += OxygenRechargeRate * Time.deltaTime;
                }
                else
                {
                    CurrentOxygenLevel = MaxOxygenLevel;
                }

                if (!Gasped)
                {
                    AudioManager.Instance.Gasp(AS);
                    Gasped = true;
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            isInBubble = false;
        }

        private void PlayerDie(){
            // AudioManager.Instance.Play("PlayerDie");
            HUDManager.Instance.GameOver();
        }
    }
}