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
    [SerializeField] private GameObject spawnPoint;//The spawn point for the player
    public List<AsyncOperation> scenesToLoad = new List<AsyncOperation>();//The list of scenes to load
    public string sceneName;//The name of the scene to load
    [SerializeField] private float sceneLoadTime = 2.0f;//The time it takes to load a scene
    [Header("Class References")]
    [SerializeField] private UIManager uIManager;//The UI Manager
    [SerializeField] private GameManager gameManager;//The game manager
    [SerializeField] private PlatformManager platformManager;//The platform manager

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
        playerPrefab = gameManager.playerPrefab;//Get the player prefab from the GameManager
    }
    /// <summary>
    /// Load Scene for the buttons in the UI
    /// </summary>
    /// <param name="sceneName"></param>
    public void LoadScene(string sceneName)
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        uIManager.UILoadingScreen(uIManager.loadingScreen);
        StartCoroutine(WaitForScreenLoad(sceneName));
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        sceneName = SceneManager.GetActiveScene().name;
        spawnPoint = GameObject.FindGameObjectWithTag("SpawnPoint");
        Debug.Log("SpawnPoint found: " + spawnPoint.name);
        if(platformManager == null)
        {
            platformManager = FindObjectOfType<PlatformManager>();
        }
        if(platformManager != null)
        {
            platformManager.SpawnNewPlatform();
        }
        if(spawnPoint != null)
        {
            Debug.Log("SpawnPoint found: " + spawnPoint.name);

            playerPrefab.transform.position = spawnPoint.transform.position;
        }
        if (scene.name == sceneName)
        {
            playerPrefab.SetActive(true);
            Debug.Log("Scene loaded: " + scene.name);
            SceneManager.sceneLoaded -= OnSceneLoaded;
            StartCoroutine(LoadLevel(sceneName));
        }
        else if(scene.name.StartsWith("Main"))
        {
            gameManager.SetGameState(GameManager.gameState.MainMenu);
            Time.timeScale = 1;
            playerPrefab.SetActive(false);
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
        if(platformManager == null)
        {
            Debug.Log("PlatformManager is null, finding it again.");
            platformManager = FindObjectOfType<PlatformManager>();
        }
        if(platformManager != null)
        {
            Debug.Log("PlatformManager found, spawning platforms.");
            platformManager.StartCoroutine(platformManager.CheckDistance());
        }
        //Wait for the scene to load
        yield return new WaitForSeconds(sceneLoadTime);
        //Set the player to the spawn point
        //GameObject player = Instantiate(playerPrefab, spawnPoint.transform.position, Quaternion.identity);
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

