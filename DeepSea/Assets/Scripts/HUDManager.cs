using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;

namespace game
{
    public class HUDManager : MonoBehaviour
    {
        private static HUDManager _instance;
        public static HUDManager Instance { get { return _instance; } }

        [SerializeField] 
        private TextMeshProUGUI OxygenLevelText;

        [SerializeField]
        private Slider OxygenBarNormal;
        [SerializeField]
        private Slider OxygenBarInsane;

        [SerializeField]
        private Image FogImage;
        [SerializeField]
        private Image HelmetPanel;

        private bool HUDEmergencyflash;
        [SerializeField]
        private Image EmergencyFlashHUD;
        public float blinkSpeed = 1f;
        private bool isBlinking = false;
        public Color startColor = new Color(0f, 0f, 0f, 0f);
        public Color targetColor = new Color(66f, 14f, 14f, 1f);
        private Color currentColor;

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
            HUDEmergencyflash = false;
            currentColor = startColor;
            isBlinking = false;
        }

        // Update is called once per frame
        void Update()
        {
            if(HUDEmergencyflash){
                EmergencyFlashHUD.enabled = true;
                isBlinking = true;
                StartCoroutine(BlinkEmergencyHUD());
            }
            else{
                EmergencyFlashHUD.enabled = false;
                isBlinking = false;
                StopCoroutine(BlinkEmergencyHUD());
            }
        }

        IEnumerator BlinkEmergencyHUD()
        {
            while (isBlinking)
            {
                float t = 0f;
                while (t < 1f)
                {
                    t += Time.deltaTime * blinkSpeed;
                    currentColor = Color.Lerp(startColor, targetColor, t);
                    EmergencyFlashHUD.GetComponent<CanvasGroup>().alpha = currentColor.a;
                    yield return null;
                }
                t = 0f;
                while (t < 1f)
                {
                    t += Time.deltaTime * blinkSpeed;
                    currentColor = Color.Lerp(targetColor, startColor, t);
                    EmergencyFlashHUD.GetComponent<CanvasGroup>().alpha = currentColor.a;
                    yield return null;
                }
            }
        }

        public void UpdateOxygen(float oxygen) {
            OxygenLevelText.text = oxygen.ToString("F1") + "%";
            if(oxygen >= 60){
                OxygenLevelText.color = new Color32(143, 166, 76, 255);
                HUDEmergencyflash = false;
            }else if (oxygen >= 20){
                OxygenLevelText.color = new Color32(181, 100, 38, 255);
                HUDEmergencyflash = false;
            }else{
                OxygenLevelText.color = new Color32(122, 46, 40, 255);
                HUDEmergencyflash = true;
            }
            OxygenBarNormal.value = oxygen / 100f;  
            OxygenBarInsane.value = oxygen / 100f;

            UpdateFog(oxygen);
        }

       public void UpdateFog(float oxygen) {
        float fogLevel = (100 - (oxygen / 100f) * 100) * 0.5f;
        float breatheAmount = Mathf.Sin(Time.time * 1f) * 20f;
        fogLevel += breatheAmount;
        FogImage.color = new Color(1, 1, 1, fogLevel / 100f);
       }

       public void GameOver(){
           StartCoroutine(GameOverRoutine(false));
       }
       public void GameWin(){
           StartCoroutine(GameOverRoutine(true));
       }

        private IEnumerator GameOverRoutine(bool winBool){

            float fadeTime = 2f;
            float elapsedTime = 0f;
            Color startColor = new Color32(66, 14, 14, 0);
            Color endColor = new Color32(66, 14, 14, 120);
            if(winBool){
                startColor = new Color32(0, 0, 0, 0);
                endColor = new Color32(0, 0, 0, 255);
            }else{
                 startColor = new Color32(66, 14, 14, 0);
                 endColor = new Color32(66, 14, 14, 120);
            }

            while (elapsedTime < fadeTime) {
                float alpha = Mathf.Lerp(0f, 120f, elapsedTime / fadeTime);
                HelmetPanel.color = new Color32(66, 14, 14, (byte)alpha);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            HelmetPanel.color = endColor;
            UnityEngine.SceneManagement.SceneManager.LoadScene("TitleScene");
        }
    }
}