using System.Collections;
using UnityEngine;

public class SlidingSprite : MonoBehaviour
{
    public RectTransform spriteRectTransform; // Assign the RectTransform of your sprite in the inspector
    public Vector2 onScreenPosition; // Position when the sprite is visible on screen
    public Vector2 offScreenPosition; // Position when the sprite is hidden off screen
    public float slideSpeed = 1.0f; // Speed at which the sprite slides

    private bool isOnScreen = false;

    public void ToggleSprite()
    {
        Debug.Log("ToggleSprite called");
        StopAllCoroutines();
        if (isOnScreen)
        {
            StartCoroutine(SlideSprite(offScreenPosition));
        }
        else
        {
            StartCoroutine(SlideSprite(onScreenPosition));
        }
        isOnScreen = !isOnScreen;
    }

    private IEnumerator SlideSprite(Vector2 targetPosition)
    {
        Debug.Log("SlideSprite started");
        while (Vector2.Distance(spriteRectTransform.anchoredPosition, targetPosition) > 0.1f)
        {
            Debug.Log("Sliding...");
            spriteRectTransform.anchoredPosition = Vector2.Lerp(spriteRectTransform.anchoredPosition, targetPosition, slideSpeed * Time.deltaTime);
            yield return null;
        }
        spriteRectTransform.anchoredPosition = targetPosition;
        Debug.Log("SlideSprite finished");
    }
}
