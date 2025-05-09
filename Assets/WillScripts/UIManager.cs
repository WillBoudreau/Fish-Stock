using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.Rendering;
using UnityEngine.EventSystems;

public class UIManager : MonoBehaviour
{
    [Header("UI Manager")]
    [Header("UI Variables")]
    public GameObject mainMenu;//The main menu UI
    public GameObject hUD;//The HUD UI
    public GameObject pauseMenu;//The pause menu UI
    public GameObject gameOverScreen;//The game over screen UI
    public GameObject winnerScreen;//The winner screen UI
    public GameObject loadingScreen;//The loading screen UI
    public GameObject creditsScreen;//The credits screen UI
    [Header("Loading Screen Elements")]
    public float fadeTime = 1.0f;//The time it takes to fade in and out
    public Image loadingImage;//The loading image
    public CanvasGroup loadingCanvasGroup;//The loading canvas group
    [Header("Class References")]
    [SerializeField] private GameManager gameManager;//The game manager
    [SerializeField] private LevelManager levelManager;//The level manager


    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        levelManager = FindObjectOfType<LevelManager>();
        SetFalse();
        SwitchUI(mainMenu);//Set the main menu to active
    }
    /// <summary>
    /// Set False
    /// </summary>
    public void SetFalse()
    {
        mainMenu.SetActive(false);
        hUD.SetActive(false);
        pauseMenu.SetActive(false);
        gameOverScreen.SetActive(false);
        winnerScreen.SetActive(false);
        loadingScreen.SetActive(false);
        creditsScreen.SetActive(false);
    }

    /// <summary>
    /// Switch the UI to the specified UI
    /// </summary>
    /// <param name="newUI">The new UI to switch to</param>
    public void SwitchUI(GameObject newUI)
    {
        SetFalse();
        newUI.SetActive(true);//Set the new UI to active
    }
    /// <summary>
    /// Starts the loading Screen process
    /// </summary>
    public void UILoadingScreen(GameObject newUI)
    {
        Debug.Log("Loading Screen Started");
       StartCoroutine(LoadingUIFadeIn());
       StartCoroutine(DelayedSwitchUI(0, newUI));
    }

    /// <summary>
    /// Fades the loading screen in
    /// </summary>
    /// <returns></returns>
    public IEnumerator LoadingUIFadeIn()
    {
        Debug.Log("Loading Screen Fade In Started");
        loadingScreen.SetActive(true);
        //SetFalse();
        //loadingScreen.SetActive(true);
        float timer = 0;

        while(timer < fadeTime)
        {
            //loadingCanvasGroup.alpha = Mathf.Lerp(0,1, timer / fadeTime);
            timer += Time.deltaTime;
            yield return null;
        }
        loadingCanvasGroup.alpha = 1;

        StartCoroutine(LoadingBarProgress());
    }
    /// <summary>
    /// Fades the loading screen out
    /// </summary>
    /// <returns></returns>
    public IEnumerator LoadingUIFadeOut()
    {
        float timer = 0;
        while (timer < fadeTime)
        {
            loadingCanvasGroup.alpha = Mathf.Lerp(1, 0, timer / fadeTime);
            timer += Time.deltaTime;
            yield return null;
        }
        loadingCanvasGroup.alpha = 0;
        loadingScreen.SetActive(false);
        loadingImage.fillAmount = 0;
    }
    /// <summary>
    /// Delays the switch to the new UI
    /// /// </summary>
    /// <param name="delay">The delay time</param>
    /// <param name="newUI">The new UI to switch to</param>
    /// <returns></returns>
    public IEnumerator DelayedSwitchUI(float delay, GameObject newUI)
    {
        yield return new WaitForSeconds(delay);
        SetFalse();
        SwitchUI(newUI);
    }
    /// <summary>
    /// Fills the loading bar
    /// </summary>
    /// <returns></returns>
    public IEnumerator LoadingBarProgress()
    {
        float timer = 0;
        loadingImage.fillAmount = 0;
        while (timer < 1)
        {
            timer += Time.deltaTime / 2f;
            loadingImage.fillAmount += Time.deltaTime / 2f;
            yield return null;
        }
        yield return new WaitForSeconds(fadeTime);
        StartCoroutine(LoadingUIFadeOut());
    }
    
}
