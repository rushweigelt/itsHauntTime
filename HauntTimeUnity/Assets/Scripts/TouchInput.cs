using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TouchInput : MonoBehaviour {

	public Transform player;

	/// <summary>
	/// Threshold after which player input will be registered as movement
	/// </summary>
	public float touchThreshold;

	/// <summary>
	/// Control scheme used for testing
	/// </summary>
	public enum ControlScheme {
		FOLLOW_TOUCH
	}

	/// <summary>
	/// Selected control scheme
	/// </summary>
	public ControlScheme controlScheme;

	/// <summary>
	/// Get input from touch screen
	/// </summary>
	public Vector2 GetInput()
	{
		// Touch input
		Vector2 touchDelta = Vector2.zero;

		// Handle using selected control scheme
		switch(controlScheme) {
			
			// Follow touch
			case ControlScheme.FOLLOW_TOUCH:
				// Track mouse if being used
				if(Input.GetMouseButton(0)) {
					return GetFollowClickInput();
				}
				else return GetFollowTouchInput();

			default:
				break;
		}

		return touchDelta;
	}

	// ------------------------------------------------------------------------------------
    // --------------------------------- FOLLOW TOUCH INPUT -------------------------------

	/// <summary>
	/// Gets touch input for FOLLOW_TOUCH control scheme
	/// </summary>
    private Vector2 GetFollowTouchInput()
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
				Vector2 touchDelta = currentTouchPos - (Vector2)player.position;

				// Clamp within square with width touchRadius
				touchInput = touchDelta.normalized;
			}
		}
		
		return touchInput;
    }

	/// <summary>
	/// Gets mouse input for FOLLOW_TOUCH control scheme
	/// </summary>
	private Vector2 GetFollowClickInput()
	{
		Vector2 touchInput = Vector2.zero;
		if(Input.GetMouseButton(0)) {
			// Get mouse position (in world space)
			Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

			// Get touch direction relative to player
			Vector2 touchDelta = mousePosition - (Vector2)player.position;

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
