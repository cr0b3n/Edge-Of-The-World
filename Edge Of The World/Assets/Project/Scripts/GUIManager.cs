using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

[DisallowMultipleComponent]
public class GUIManager : MonoBehaviour {

    #region Singleton

    public static GUIManager Instance { get; private set; }

    private void Awake() {

        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }
    #endregion /Singleton

    public TextMeshProUGUI distanceText;
    public TextMeshProUGUI totalTimeText;

    private TraveledDistance traveledDistance;
    private double gameTime;

    private void Start() {
        gameTime = 0d;
        traveledDistance = FindObjectOfType<TraveledDistance>();
    }

    private void Update() {

        distanceText.text = traveledDistance.TotalTraveledDistance.ToString("0.#") + "m";

        //var ts = TimeSpan.FromSeconds(Time.time);
        gameTime += Time.deltaTime;
        totalTimeText.text = TimeSpan.FromSeconds(gameTime).ToString(@"hh\:mm\:ss");//string.Format("{0:00}:{1:00}", ts.Minutes, ts.Seconds);

        if (Input.GetKeyDown(KeyCode.R)) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }    
        
        if(Input.GetKeyDown(KeyCode.Escape)) {

            if(Time.timeScale == 0) {
                Time.timeScale = 1f;
                Debug.Log("Game Unpaused!");
            } else {
                Time.timeScale = 0f;
                Debug.Log("Game Paused!");
            }            
        }
    }

}
