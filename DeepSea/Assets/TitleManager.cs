using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TitleManager : MonoBehaviour
{

    [SerializeField] 
    private GameObject creditsPanel;

    private bool isLerping;

    // Start is called before the first frame update
    void Start()
    {
        isLerping = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("LightingTest");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ShowCredits()
    {
        creditsPanel.SetActive(true);
        if(!isLerping){
            isLerping = true;
            Vector2 startPosition = new Vector2(creditsPanel.GetComponent<RectTransform>().anchoredPosition.x, -Screen.height);
            Vector2 targetPosition = new Vector2(creditsPanel.GetComponent<RectTransform>().anchoredPosition.x, Screen.height + 100);
        
            // Vector2 startPosition = creditsPanel.GetComponent<RectTransform>().anchoredPosition;
            // Vector2 targetPosition = -startPosition;
            StartCoroutine(LerpPosition(creditsPanel.GetComponent<RectTransform>(), targetPosition, 5f, () => {
                creditsPanel.GetComponent<RectTransform>().anchoredPosition = startPosition;
                isLerping = false;
                }));
        }
    }

    private IEnumerator LerpPosition(RectTransform rectTransform, Vector2 targetPosition, float duration, System.Action onComplete)
    {
        float elapsedTime = 0;
        Vector2 startPosition = rectTransform.anchoredPosition;
        while (elapsedTime < duration)
        {
            rectTransform.anchoredPosition = Vector2.Lerp(startPosition, targetPosition, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        rectTransform.anchoredPosition = targetPosition;
        onComplete?.Invoke();

    }
}
