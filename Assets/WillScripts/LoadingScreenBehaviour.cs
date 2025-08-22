using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LoadingScreenBehaviour : MonoBehaviour
{
    [Header("Loading Screen Settings")]
    [SerializeField] private List<string> loadingMessages = new List<string>();
    [SerializeField] private List<Sprite> loadingImages = new List<Sprite>();
    [SerializeField] private Sprite loadingImage;
    [SerializeField] private TextMeshProUGUI loadingText;
    [SerializeField] private float fadeDuration = 1f;
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private float displayDuration = 2f;
    [SerializeField] private int messageIndex = 0;
    [SerializeField] private int previousMessageIndex = -1; // To track the previous message index
    [SerializeField] private int randomIndex = 0;
    private float initDisplayDuration = 2f;

    private void Start()
    {
        initDisplayDuration = displayDuration;
        FadeInLoadingMessage();
        DisplayRandomLoadingMessage();
    }
    private void Update()
    {
        // Check if the display duration has elapsed
        if (displayDuration <= 0f)
        {
            FadeOutLoadingMessage();
            DisplayRandomLoadingMessage(); // Display a new message after fading out
            displayDuration = initDisplayDuration; // Reset the display duration for next use
        }
        else
        {
            displayDuration -= Time.deltaTime;
        }
    }
    /// <summary>
    /// Displays a random loading message from the list.
    /// </summary>
    private void DisplayRandomLoadingMessage()
    {
        if (loadingMessages.Count == 0) return;

        randomIndex = Random.Range(0, loadingMessages.Count);
        if (messageIndex >= loadingMessages.Count)
        {
            messageIndex = 0; // Reset index if it exceeds the list size
        }
        else if (randomIndex == previousMessageIndex)
        {
            // If the random index is the same as the previous one, get a new random index
            randomIndex = (randomIndex + 1) % loadingMessages.Count;
        }
        messageIndex = randomIndex; // Update message index to the random one
        previousMessageIndex = messageIndex; // Update previous message index
        loadingText.text = loadingMessages[randomIndex];

    }
    #region Fade out
    /// <summary>
    /// Fades out the loading screen message.
    /// </summary>
    public void FadeOutLoadingMessage()
    {
        StartCoroutine(FadeOutCoroutine());
    }
    private IEnumerator FadeOutCoroutine()
    {
        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);
            yield return null;
        }
        canvasGroup.alpha = 0f; // Ensure it ends at 0
    }
    #endregion
    #region Fade in
    /// <summary>
    /// Fades in the loading screen message.
    /// </summary>
    public void FadeInLoadingMessage()
    {
        StartCoroutine(FadeInCoroutine());

        DisplayRandomLoadingMessage(); // Display a new random message when fading in
    }
    private IEnumerator FadeInCoroutine()
    {
        float elapsedTime = 0f;
        canvasGroup.alpha = 0f; // Ensure it starts at 0
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(0f, 1f, elapsedTime / fadeDuration);
            yield return null;
        }
        canvasGroup.alpha = 1f; // Ensure it ends at 1
    }
    #endregion
    /// <summary>
    /// Loads the matching image to the current loading message.
    /// </summary>
    /// <param name="index">The index of the image to load.</param>
    public void LoadImage(int index)
    {
        if (loadingImages.Count == 0 || index < 0 || index >= loadingImages.Count) return;

        loadingImage = loadingImages[index];
    }
}