using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// TouchInput - subscribe to the OnReceiveTouch() events to get user input
/// </summary>
public class TouchInput : MonoBehaviour {

	/// <summary>
	/// Threshold after which player input will be registered as movement
	/// </summary>
	public float touchThreshold;

    public Vector2 lastTouchPos;

    /// <summary>
    /// Control scheme used for testing
    /// </summary>
    public enum ControlScheme {
		FOLLOW_DRAG,
        FOLLOW_TAP
	}

	/// <summary>
	/// Selected control scheme
	/// </summary>
	public ControlScheme controlScheme;

    /// <summary>
    /// Used to show position of player tap
    /// </summary>
    public GameObject tapIndicator;

    public delegate void OnReceiveTouch(Vector2 position);

    /// <summary>
    /// Touch event that fires immediately after detecting touch input
    /// </summary>
    public OnReceiveTouch onGetTouchDown;
    
    /// <summary>
    /// Touch event that fires immediately after detecting user lifted finger
    /// </summary>
    public OnReceiveTouch onGetTouchEnded;

    public OnReceiveTouch onGetTouch;

    /// <summary>
    /// Speed at which tap indicator animates
    /// </summary>
    [Range(0,2)]
    public float tapIndicatorSpeed;

    IEnumerator showTapIndicatorCoroutine;

    private void Start()
    {
        lastTouchPos = Player.Instance.transform.position;
    }

    private void Update()
    {
        // First frame of user touch
        if(Input.GetMouseButtonDown(0)) {
            // Update last touch position
            lastTouchPos = GetFollowTapInput();
            Debug.Log("Received tap at " + lastTouchPos);

            // Show tap indicator here
            StartCoroutine(ShowTapIndicator(lastTouchPos));

            // Fire delegate event
            if (onGetTouchDown != null) {
                onGetTouchDown.Invoke(lastTouchPos);
            }
        }

        // User holds finger on screen
        else if (Input.GetMouseButton(0)) {
            // Update last touch position
            lastTouchPos = GetFollowTapInput();

            // Fire delegate event
            if (onGetTouch != null) {
                onGetTouch.Invoke(lastTouchPos);
            }
        }

        // User lifts finger
        else if(Input.GetMouseButtonUp(0)) {
            // Update last touch position
            lastTouchPos = GetFollowTapInput();

            if (onGetTouchEnded != null) {
                onGetTouchEnded.Invoke(lastTouchPos);
            }
        }

        // TODO: update project solution to C# 6 so we can use this fancy null check operator:
        // onGetTouch?.Invoke(lastTouchPos);
    }

    // ------------------------------------------------------------------------------------
    // --------------------------------- FOLLOW TAP INPUT ---------------------------------

    /// <summary>
    /// Returns position of most recent touch
    /// </summary>
    /// <returns></returns>
    private Vector2 GetFollowTapInput()
    {
        // Get position of touch in worldspace
        Vector2 touchPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        //// Show tap indicator here
        //StartCoroutine(ShowTapIndicator(lastTouchPos));

        return touchPos;
    }


    // TODO: consider optimizing this to use an object pool
    /// <summary>
    /// Coroutine animation that indicates position tapped by user
    /// </summary>
    /// <param name="tapPosition"></param>
    /// <returns></returns>
    private IEnumerator ShowTapIndicator(Vector2 tapPosition)
    {
        // Set tap indicator to initial state (active = true, at tap position, scale = 0)
        GameObject tapIndicator = Instantiate(this.tapIndicator);
        tapIndicator.transform.position = tapPosition;
        tapIndicator.transform.localScale = Vector3.zero;
        
        // Set start color to full alpha
        SpriteRenderer spriteRenderer = tapIndicator.GetComponent<SpriteRenderer>();
        Color color = spriteRenderer.color;
        color.a = 1;
        spriteRenderer.color = color;

        float t = 0;
        while(t < 1) {
            // Move scale value up the curve (grow from 0 to 1)
            Vector3 scale = Mathfx.Hermite(Vector3.zero, Vector3.one, t);

            // Move color alpha value down the curve (shrink from 1 to 0)
            color.a = Mathfx.Hermite(1, 0, t);

            // Update scale/color values
            tapIndicator.transform.localScale = scale;
            spriteRenderer.color = color;

            // Increment time
            t += Time.deltaTime * (tapIndicatorSpeed);
           yield return null;
        }

        // Destroy when animation finished
        Destroy(tapIndicator);
    }


    // ------------------------------------------------------------------------------------
    // --------------------------------- FOLLOW DRAG INPUT --------------------------------

    /// <summary>
    /// Gets touch input for FOLLOW_TOUCH control scheme
    /// </summary>
    private Vector2 GetFollowDragInput()
    {
        Vector2 touchInput = Vector2.zero;

		if(Input.touchCount > 0) {
		// if(Input.GetMouseButton(0)) {
			// Get touch position (in world space)
			Vector2 currentTouchPos = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
			
			// If still touching
			if(Input.GetTouch(0).phase != TouchPhase.Ended || Input.GetTouch(0).phase != TouchPhase.Canceled) {
				Debug.Log("TouchInput | user touching at " + currentTouchPos);
				
				// Get touch direction relative to player
				Vector2 touchDelta = currentTouchPos - (Vector2)Player.Instance.transform.position;

				// Clamp within square with width touchRadius
				//touchInput = touchDelta.normalized;
			}
		}
		
		return touchInput;
    }

	/// <summary>
	/// Gets mouse input for FOLLOW_TOUCH control scheme
	/// </summary>
	private Vector2 GetFollowClickDragInput()
	{
		Vector2 touchInput = Vector2.zero;
		if(Input.GetMouseButton(0)) {
			// Get mouse position (in world space)
			Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

			// Get touch direction relative to player
			Vector2 touchDelta = mousePosition - (Vector2)Player.Instance.transform.position;

			// Ignore input if below threshold
			if(touchDelta.magnitude < touchThreshold) {
				return Vector2.zero;
			}
			// Return normalized vector in direction of movement
			else {
				touchInput = touchDelta.normalized;
			}
		}
		
		return touchInput;
	}
}
