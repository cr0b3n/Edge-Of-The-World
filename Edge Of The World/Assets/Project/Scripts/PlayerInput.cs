using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class PlayerInput : MonoBehaviour {

    public bool testTouchControlsInEditor = false;  //Should touch controls be tested?
    public float verticalDPadThreshold = .5f;       //Threshold touch pad inputs
    //public Thumbstick thumbstick;					//Reference to Thumbstick

    [HideInInspector]
    public float horizontal = 0f;
    [HideInInspector]
    public float vertical = 0f;

    private bool readyToClear;
    //private bool isActive = false;

    #region Unity Execusions

    private void Update() {

        //if (!isActive)
        //    return;

        ClearInputs();
        ProcessInputs();
    }

    private void FixedUpdate() {
        //In FixedUpdate() we set a flag that lets inputs to be cleared out during the 
        //next Update(). This ensures that all code gets to use the current inputs
        readyToClear = true;
    }

    #endregion /Unity Execusions

    #region Custom Methods
    private void ClearInputs() {

        if (!readyToClear)
            return;

        horizontal = 0f;
        vertical = 0f;

        readyToClear = false;
    }

    private void ProcessInputs() {
        KeyboardInputs();
        //TouchInputs();
        //Debug.Log("updating player input");
    }

    private void TouchInputs() {
        //If this isn't a mobile platform AND we aren't testing in editor, exit
        if (!Application.isMobilePlatform && !testTouchControlsInEditor)
            return;

        //Record inputs from screen thumbstick
        //Vector2 thumbstickInput = thumbstick.GetDirection();

        ////Accumulate horizontal input
        //horizontal += thumbstickInput.x;
        //vertical += thumbstickInput.y;
    }

    private void KeyboardInputs() {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
    }

    //public void UpdateActivity() {
    //    isActive = !isActive;
    //}

    #endregion /Custom Methods

}
