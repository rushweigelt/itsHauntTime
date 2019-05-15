using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Singleton<Player>
{
    public GameObject interactPrompt;

    public SpriteRenderer reflection;

    private SpriteRenderer spriteRenderer;

    /// <summary>
    /// Speed at which transparency changes
    /// </summary>
    [Range(0,2)]
    public float transparencyChangeSpeed;

    /// <summary>
    /// Alpha value used when hatto becomes transparent
    /// </summary>
    [Range(0,1)]
    public float minTransparency;

    /// <summary>
    /// Alpha value used when hatto becomes opaque
    /// </summary>
    [Range(0,1)]
    public float maxTransparency;

    // Start is called before the first frame update
    void Start()
    {
        //turn off interactive prompt at start of scene.
        interactPrompt.SetActive(false);

        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        // Update reflection on sprite
        reflection.sprite = spriteRenderer.sprite;
        reflection.flipX = spriteRenderer.flipX;
        reflection.color = spriteRenderer.color;
    }

    public void SetTransparent(bool transparent)
    {
        // Lerp towards transparent
        if(transparent) {
            Debug.Log("Setting ghost to transparent");
            StartCoroutine(FadeTransparency(minTransparency));
        }
        // Lerp towards opaque
        else {
            Debug.Log("Setting ghost to opaque");
            StartCoroutine(FadeTransparency(maxTransparency));
        }
    }

    private IEnumerator FadeTransparency(float alpha)
    {
        // Get starting alpha
        Color color = spriteRenderer.color;
        float startAlpha = color.a;

        float t = 0;
        while(t < 1) {
            // Lerp alpha towards target value
            color.a = Mathfx.Lerp(startAlpha, alpha, t);
            spriteRenderer.color = color;

            // Increment t and resume next frame
            t += Time.deltaTime * transparencyChangeSpeed;
            yield return null;
        }
        // Manually set alpha at end (in case we overshot our target)
        color.a = alpha;
        spriteRenderer.color = color;
    }
}
