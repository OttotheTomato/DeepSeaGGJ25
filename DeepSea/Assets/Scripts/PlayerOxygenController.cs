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
            AudioManager.Instance.PlayerSounds(AS, "breathing02");
        }

        private void UpdateOxygenText()
        {
            HUDManager.Instance.UpdateOxygen(CurrentOxygenLevel);
        }

        private void OnTriggerEnter(Collider other){
            if (other.gameObject.CompareTag("Bubble")){
                isInBubble = true;
            }
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.gameObject.CompareTag("Bubble") && other.gameObject.activeSelf)
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
            else{
                isInBubble = false;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.CompareTag("Bubble")){
                isInBubble = false;
            }
        }

        public void OnBubbleDestroyed()
        {
            isInBubble = false;
        }

        private void PlayerDie()
        {
            // AudioManager.Instance.Play("PlayerDie");
            RagdollPlayer();
            HUDManager.Instance.GameOver();
        }

        public void PlayerWin()
        {
            // AudioManager.Instance.Play("PlayerWin");
            HUDManager.Instance.GameWin();
        }

        public void RagdollPlayer()
        {
            float deathForce = 10f;
            float deathTorque = 10f;

            Rigidbody rb = gameObject.GetComponent<Rigidbody>();

            rb.freezeRotation = false;

            rb.AddForce(Vector3.down * deathForce, ForceMode.Impulse);
            rb.AddTorque(Vector3.right * deathTorque, ForceMode.Impulse);

            GetComponent<Collider>().enabled = false;

        }
    }
}