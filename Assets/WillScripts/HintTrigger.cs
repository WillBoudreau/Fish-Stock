using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HintTrigger : MonoBehaviour
{
    [Header("Hint Trigger")]
    [SerializeField] private UIManager uiManager; // Reference to the UIManager to access hint functionality
    [SerializeField] private bool isTriggered = true; // Flag to enable or disable hint functionality
    [SerializeField] private int hintIndex; // Index of the hint to display when triggered
    [Header("Hint Trigger Settings")]
    [SerializeField] private float hintDisplayDuration = 5f; // Duration to display the hint
    [SerializeField] private float hintFadeDuration = 1f; // Duration for the hint to fade out
    [SerializeField] private float hintDelay = 0.5f; // Delay before the hint is displayed
    [Header("Hint Trigger References")]
    [SerializeField] private GameObject hintPanel; // The panel that contains the hint text
    [SerializeField] private TextMeshProUGUI hintText; // The text component that displays the hint
    // Start is called before the first frame update
    void Start()
    {
        uiManager = FindObjectOfType<UIManager>();
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (!isTriggered)
        {
            DisplayHint(); // Call the method to display the hint
            if (!uiManager.isHintActive)
            {
                
            }
        }
    }
    /// <summary>
    /// Display Hint
    /// </summary>
    private void DisplayHint()
    {
        if (uiManager.currentHintIndex != hintIndex)
        {
            uiManager.currentHintIndex = hintIndex; // Set the current hint index to the hint index to be displayed
        }
        Hint();
        isTriggered = true; // Set the flag to true to prevent re-triggering
    }
    void Hint()
    {
        if (uiManager.hints.Count > 0 && uiManager.currentHintIndex < uiManager.hints.Count)
        {
            hintText.text = uiManager.hints[uiManager.currentHintIndex]; // Set the hint text to the current hint
            uiManager.isHintActive = true; // Set the hint panel to active
            hintPanel.SetActive(true); // Activate the hint panel
            StartCoroutine(DisplayHintCoroutine()); // Start the coroutine to display the hint
        }
    }
    private IEnumerator DisplayHintCoroutine()
    {
        yield return new WaitForSeconds(hintDelay); // Wait for the specified delay before displaying the hint
        float elapsedTime = 0f;
        while (elapsedTime < hintDisplayDuration)
        {
            elapsedTime += Time.deltaTime;
            yield return null; // Wait for the next frame
        }
        // Fade out the hint panel after the display duration
        StartCoroutine(FadeOutHintPanel());
    }
    private IEnumerator FadeOutHintPanel()
    {
        float elapsedTime = 0f;
        CanvasGroup canvasGroup = hintPanel.GetComponent<CanvasGroup>();
        while (elapsedTime < hintFadeDuration)
        {
            elapsedTime += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(1f, 0f, elapsedTime / hintFadeDuration); // Fade out the hint panel
            yield return null; // Wait for the next frame
        }
        canvasGroup.alpha = 0f; // Ensure the alpha is set to 0 after fading out
        uiManager.isHintActive = false; // Set the hint panel to inactive
        hintPanel.SetActive(false); // Deactivate the hint panel
    }
}
