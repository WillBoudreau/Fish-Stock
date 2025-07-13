using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.Rendering;
using UnityEngine.EventSystems;
using UnityEditor;
using Unity.Jobs;
using Unity.Collections;

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
    public GameObject settingsScreen;//The settings screen UI
    public GameObject boatSelectionScreen;//The boat selection screen UI
    [Header("Loading Screen Elements")]
    public float fadeTime = 1.0f;//The time it takes to fade in and out
    public Slider loadingBar;//The loading bar
    public CanvasGroup loadingCanvasGroup;//The loading canvas group
    [Header("Hint UI Elements")]
    [SerializeField] private GameObject hintPanel;//The panel that contains the hint text
    public TextMeshProUGUI hintText;//The text component that displays the hint
    public List<string> hints = new List<string>();//A list of hints to display
    public int currentHintIndex = 0;//The index of the current hint being displayed
    [ReadOnly] public bool isHintActive = false;//Is the hint panel active
    [SerializeField] private float timeBetweenHintText = 0.05f;//The time between each letter being displayed in the hint text
    [Header("Class References")]
    [SerializeField] private GameManager gameManager;//The game manager
    [SerializeField] private LevelManager levelManager;//The level manager
    [SerializeField] private SoundManager soundManager;//The sound manager
    [SerializeField] private MusicManager musicManager;//The music manager
    [Header("UI Elements")]
    [SerializeField] Slider masterSlider;
    [SerializeField] Slider musicSlider;
    [SerializeField] Slider sfxSlider;


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
        settingsScreen.SetActive(false);
        boatSelectionScreen.SetActive(false);
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

        while (timer < fadeTime)
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
        loadingBar.value = 0;
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
        loadingBar.value = 0;
        while (timer < 1)
        {
            timer += Time.deltaTime / 2f;
            loadingBar.value = Mathf.Lerp(0, 1, timer);
            yield return null;
        }
        yield return new WaitForSeconds(fadeTime);
        StartCoroutine(LoadingUIFadeOut());
    }
    /// <summary>
    /// Sets the volume of the music and sound effects based on the sliders.
    /// </summary>
    public void SetVolume()
    {
        if (musicManager.audioMixer.GetFloat("Master", out float masterVolume))
        {
            masterSlider.value = masterVolume;
        }
        if (musicManager.audioMixer.GetFloat("Music", out float musicVolume))
        {
            musicSlider.value = musicVolume;
        }
        if (soundManager.audioMixer.GetFloat("SFX", out float sfxVolume))
        {
            sfxSlider.value = sfxVolume;
        }
    }
    /// <summary>
    /// Sets the volume based on the slider values.
    /// </summary>
    /// param name = "group"></param>
    public void SetSliderVolume(string group)
    {
        switch (group)
        {
            case "Master":
                musicManager.SetVolume(masterSlider.value, "Master");
                break;
            case "Music":
                musicManager.SetVolume(musicSlider.value, "Music");
                break;
            case "SFX":
                soundManager.ChangeVolume(sfxSlider.value, "SFX");
                break;
            default:
                Debug.LogWarning("Invalid volume group: " + group);
                break;
        }
    }
}