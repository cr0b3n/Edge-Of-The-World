using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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

    public Sprite[] effectSprites;
    public Image effectImage;
    public GameObject uiLootEffect;

    private TraveledDistance traveledDistance;
    private double gameTime;

    #region Unity Executions
    private void Start() {
        uiLootEffect.SetActive(false);
        gameTime = 0d;
        traveledDistance = FindObjectOfType<TraveledDistance>();
    }

    private void Update() {

        SetTextDisplay();

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

    #endregion /Unity Executions

    #region Custom Methods

    private void SetTextDisplay() {
        distanceText.text = traveledDistance.TotalTraveledDistance.ToString("0.#") + "m";

        //var ts = TimeSpan.FromSeconds(Time.time);
        gameTime += Time.deltaTime;
        totalTimeText.text = TimeSpan.FromSeconds(gameTime).ToString(@"hh\:mm\:ss");//string.Format("{0:00}:{1:00}", ts.Minutes, ts.Seconds);
    }

    public void ActivateUIEffect(int index) {

        index = Mathf.Clamp(index, 0, effectSprites.Length - 1);

        effectImage.sprite = effectSprites[index];
        uiLootEffect.SetActive(true);
    }

    #endregion /Custom Methods
}
