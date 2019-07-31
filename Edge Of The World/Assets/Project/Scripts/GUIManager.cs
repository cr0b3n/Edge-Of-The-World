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

    [Header("Main GUI")]
    public TextMeshProUGUI distanceText;
    public TextMeshProUGUI totalTimeText;
    public TextMeshProUGUI scoreText;
    public GameObject pauseMenu;
    public TraveledDistance traveledDistance;

    [Header("Effect Section")]
    public Sprite[] effectSprites;
    public Image effectImage;
    public GameObject uiLootEffect;

    [Header("End Credits")]
    public GameObject gameplayUI;
    public GameObject endCredits;
    public TextMeshProUGUI creditsDistance;
    public TextMeshProUGUI creditsTime;
    public TextMeshProUGUI creditsScore;

    [HideInInspector]
    public float gameTime;
    [HideInInspector]
    public bool gameOver;    
    private int currentScore;   
    private bool isPause;

    #region Unity Executions
    private void Start() {
        pauseMenu.SetActive(false);
        isPause = false;
        gameOver = false;
        uiLootEffect.SetActive(false);
        gameTime = 0f;
        //traveledDistance = FindObjectOfType<TraveledDistance>();
    }

    private void Update() {

#if UNITY_EDITOR

        if (Input.GetKeyDown(KeyCode.R))
            ResetGame();
#endif

        if (gameOver) return;


        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.KeypadEnter))
            PlayPauseGame();

        if (isPause) return;

        SetTextDisplay();
    }

    #endregion /Unity Executions

    #region Custom Methods

    private void SetTextDisplay() {

        float speed = traveledDistance.TotalTraveledDistance / gameTime;
        int finalScore = (int)Mathf.Round(speed) * currentScore;
        distanceText.text = "Mileage: " + string.Format("{0:N}", traveledDistance.TotalTraveledDistance);
        scoreText.text = "Score: " + string.Format("{0:n0}", finalScore);//traveledDistance.TotalTraveledDistance.ToString("0.#") + "m";
        //Debug.Log(string.Format("{0:N}", traveledDistance.TotalTraveledDistance) + " m");
        //var ts = TimeSpan.FromSeconds(Time.time);
        gameTime += Time.deltaTime;
        totalTimeText.text = TimeSpan.FromSeconds(gameTime).ToString(@"hh\:mm\:ss");//string.Format("{0:00}:{1:00}", ts.Minutes, ts.Seconds);
    }

    public void ActivateUIEffect(int index, int score, bool hasEffect = true) {

        currentScore = score;

        if (!hasEffect)
            return;

        uiLootEffect.SetActive(false);
        index = Mathf.Clamp(index, 0, effectSprites.Length - 1);

        effectImage.sprite = effectSprites[index];
        uiLootEffect.SetActive(true);
    }

    public void SetCreditsDetails(int score) {

        gameplayUI.SetActive(false);
        endCredits.SetActive(true);
        creditsDistance.text = string.Format("{0:N}", traveledDistance.TotalTraveledDistance);
        creditsTime.text = TimeSpan.FromSeconds(gameTime).ToString(@"hh\:mm\:ss");

        float speed = traveledDistance.TotalTraveledDistance / gameTime;
        int finalScore = (int)Mathf.Round(speed) * currentScore;

        creditsScore.text = string.Format("{0:n0}", finalScore);

        UserData.instance.CheckScore(finalScore);
        UserData.instance.CheckMileage(traveledDistance.TotalTraveledDistance);
        UserData.instance.CheckTime(gameTime);
        //Debug.Log(traveledDistance.TotalTraveledDistance);
    }

    public void ResetGame() {
        AudioManager.PlayUIAudio();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void PlayPauseGame() {

        isPause = !isPause;
        AudioManager.PlayUIAudio();

        pauseMenu.SetActive(isPause);
        AudioListener.pause = isPause;

        Time.timeScale = (isPause) ? 0f : 1f;
    }

    public void MenuScreen() {
        AudioManager.PlayUIAudio();
        SceneManager.LoadScene(0);
    }

    #endregion /Custom Methods
}
