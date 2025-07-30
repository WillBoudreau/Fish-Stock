using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.Rendering;


public class LevelManager : MonoBehaviour
{
    [Header("Level Manager")]
    [Header("Level variables")]
    [SerializeField] private int currentLevelIndex = 0;//The current level index
    private GameObject playerPrefab;//The player prefab
    private GameObject player2Prefab;//The player prefab for player 2
    [SerializeField] private GameObject spawnPoint;//The spawn point for the player
    [SerializeField] private GameObject spawnPoint2;//The spawn point for player 2
    public List<AsyncOperation> scenesToLoad = new List<AsyncOperation>();//The list of scenes to load
    public string sceneName;//The name of the scene to load
    [SerializeField] private float sceneLoadTime = 2.0f;//The time it takes to load a scene
    [Header("Class References")]
    [SerializeField] private UIManager uIManager;//The UI Manager
    [SerializeField] private GameManager gameManager;//The game manager
    [SerializeField] private MusicManager musicManager;//The music manager

    void Start()
    {
        //If the UIManager is null, find the UIManager
        if(uIManager == null)
        {
            uIManager = FindObjectOfType<UIManager>();
        }
        //If the GameManager is null, find the GameManager
        if(gameManager == null)
        {
            gameManager = FindObjectOfType<GameManager>();
        }
        if(musicManager == null)
        {
            musicManager = FindObjectOfType<MusicManager>();
        }
        playerPrefab = gameManager.playerPrefab;//Get the player prefab from the GameManager
        player2Prefab = gameManager.player2Prefab;//Get the player prefab for player 2 from the GameManager
    }
    /// <summary>
    /// Load Scene for the buttons in the UI
    /// </summary>
    /// <param name="sceneName"></param>
    public void LoadScene(string sceneName)
    {
        uIManager.UILoadingScreen(uIManager.loadingScreen);
        SceneManager.sceneLoaded += OnSceneLoaded;
        StartCoroutine(WaitForScreenLoad(sceneName));
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        sceneName = SceneManager.GetActiveScene().name;
        spawnPoint = GameObject.FindGameObjectWithTag("SpawnPoint");
        spawnPoint2 = GameObject.FindGameObjectWithTag("SpawnPoint2");
        Debug.Log("SpawnPoint found: " + spawnPoint.name);
        Debug.Log("SpawnPoint2 found: " + spawnPoint2.name);
        if (spawnPoint != null && spawnPoint2 != null)
        {
            Debug.Log("SpawnPoint found: " + spawnPoint.name);
            Debug.Log("SpawnPoint2 found: " + spawnPoint2.name);

            playerPrefab.transform.position = spawnPoint.transform.position;
            player2Prefab.transform.position = spawnPoint2.transform.position;
            playerPrefab.GetComponent<PlayerController>().enabled = true;
            player2Prefab.GetComponent<PlayerController>().enabled = true;
        }
        if (scene.name == sceneName)
        {
            Debug.Log("Scene loaded: " + scene.name);
            SceneManager.sceneLoaded -= OnSceneLoaded;
            StartCoroutine(LoadLevel(sceneName));
            if (scene.name != "MainMenuScene")
            {
                musicManager.PlayMusic(true, "Gameplay");
                gameManager.SetGameState(GameManager.gameState.InGame);
                gameManager.SetPlayerState(true);
                Time.timeScale = 1;
                uIManager.SwitchUI(uIManager.hUD);//Switch to the HUD UI
            }
            else if (scene.name == "MainMenuScene")
            {
                gameManager.SetGameState(GameManager.gameState.MainMenu);
                Time.timeScale = 1;
                Debug.Log("Main Menu Scene loaded: " + scene.name);
            }
        }
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    /// <summary>
    /// Load the specified level based on the sceneName variable
    /// </summary>
    /// <param name="sceneName">The name of the scene to load</param>
    IEnumerator LoadLevel(string sceneName)
    {
        //Load the scene asynchronously
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
        asyncLoad.allowSceneActivation = false;
        scenesToLoad.Add(asyncLoad);
        while (!asyncLoad.isDone)
        {
            if (asyncLoad.progress >= 0.9f)
            {
                asyncLoad.allowSceneActivation = true;
            }
            yield return null;
        }
        //Wait for the scene to load
        yield return new WaitForSeconds(sceneLoadTime);
    }
    /// <summary>
    /// Wait for the screen to load before loading the scene
    /// </summary>
    /// <param name="sceneName">The name of the scene to load</param>
    /// <returns></returns>
    IEnumerator WaitForScreenLoad(string sceneName)
    {
        //Wait for the screen to load
        yield return new WaitForSeconds(uIManager.fadeTime);
        //Load the scene
        yield return new WaitForSeconds(sceneLoadTime);
        
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
        operation.completed += OperationCompleted;
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }
    /// <summary>
    /// Gets average progress for Loading bar.
    /// </summary>
    /// <returns></returns>
    public float GetLoadingProgress()
    {
        float totalProgress = 0;

        foreach (AsyncOperation operation in scenesToLoad)
        {
            totalProgress += operation.progress;
        }

        return totalProgress / scenesToLoad.Count;
    }
    /// <summary>
    /// Event for when load operation is finished.
    /// </summary>
    /// <param name="operation"></param>
    private void OperationCompleted(AsyncOperation operation)
    {
        scenesToLoad.Remove(operation);
        operation.completed -= OperationCompleted;
    }
}

