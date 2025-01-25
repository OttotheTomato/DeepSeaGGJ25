using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace game
{
    public class HUDManager : MonoBehaviour
    {
        private static HUDManager _instance;
        public static HUDManager Instance { get { return _instance; } }

        [SerializeField] 
        private TextMeshProUGUI OxygenLevelText;

        void Awake() {
            if (_instance != null && _instance != this)
            {
                Destroy(this.gameObject);
            } else {
                _instance = this;
            }
        }

        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
            
        }

        public void UpdateOxygen(float oxygen) {
            OxygenLevelText.text = "O2: " + oxygen.ToString("F2") + "%";
            if(oxygen >= 60){
                OxygenLevelText.color = new Color32(143, 166, 76, 255);
            }else if (oxygen >= 20){
                OxygenLevelText.color = new Color32(181, 100, 38, 255);
            }else{
                OxygenLevelText.color = new Color32(122, 46, 40, 255);
            }
        }
    }
}