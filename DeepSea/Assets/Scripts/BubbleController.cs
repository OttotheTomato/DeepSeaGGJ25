using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace game {
    public class BubbleController : MonoBehaviour
    {
        [SerializeField]   
        private float MinimumSize = .5f;

        [SerializeField] 
        private float DeflationSpeed = .01f;

        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            
        }

        public void Deflate() {
            gameObject.transform.localScale -= new Vector3(DeflationSpeed, DeflationSpeed, DeflationSpeed) * Time.deltaTime;
            CheckBubblePop();
        }

        private void CheckBubblePop() {
            if (gameObject.transform.localScale.x <= MinimumSize) {
                Destroy(gameObject);
                // AudioManager.instance.Play("BubblePop");
            }
        }
    }
}