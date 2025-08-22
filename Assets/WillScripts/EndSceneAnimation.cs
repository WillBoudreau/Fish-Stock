using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndSceneAnimation : MonoBehaviour
{
    [Header("End Scene Animation")]
    [Header("Animation Properties")]
    [SerializeField] private float animationDuration = 2.0f; // Duration of the animation in seconds
    [SerializeField] private Vector3 endPosition = new Vector3(0, 0, 0); // Final position of the object
    [SerializeField] private float scaleFactor = 1.0f; // Scale factor for the object
    [SerializeField] private bool playOnStart = true; // Whether to play the animation on start
    [Header("Animation References")]
    [SerializeField] private AnimationCurve animationCurve; // Animation curve to control the animation
    [SerializeField] private List<GameObject> objectsToAnimate = new List<GameObject>(); // The object to animate



    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (playOnStart)
        {
            playOnStart = false; // Ensure it only plays once
            AnimateObjects();
        }
    }

    /// <summary>
    /// Have each of the objects animate to the end position and scale.
    /// </summary>
    public void AnimateObjects()
    {
        foreach (GameObject obj in objectsToAnimate)
        {
            StartCoroutine(AnimateObject(obj));
        }
    }
    /// <summary>
    /// Animate a single object to the end position and scale.
    /// </summary>
    /// <param name="obj">The object to animate.</param>
    private IEnumerator AnimateObject(GameObject obj)
    {
        Vector3 startPosition = obj.transform.position;
        Vector3 startScale = obj.transform.localScale;
        float elapsedTime = 0f;

        while (elapsedTime < animationDuration)
        {
            float t = elapsedTime / animationDuration;
            float curveValue = animationCurve.Evaluate(t);
            obj.transform.position = Vector3.Lerp(startPosition, endPosition, curveValue);
            obj.transform.localScale = Vector3.Lerp(startScale, startScale * scaleFactor, curveValue);

            // Lower the scale as the animation progresses
            obj.transform.localScale = new Vector3(
                obj.transform.localScale.x * (1 - curveValue),
                obj.transform.localScale.y * (1 - curveValue),
                obj.transform.localScale.z * (1 - curveValue)
            );

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure the final position and scale are set
        obj.transform.position = endPosition;
        //obj.transform.localScale = startScale * scaleFactor;
    }

}
