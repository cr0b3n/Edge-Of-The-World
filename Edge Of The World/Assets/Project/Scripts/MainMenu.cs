using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

[DisallowMultipleComponent]
public class MainMenu : MonoBehaviour {

    public GameObject mainOptions;
    public TextMeshProUGUI bestDistance;
    public TextMeshProUGUI bestTime;
    public TextMeshProUGUI bestScore;

    private void Start() {
        Time.timeScale = 1f;
        AudioListener.pause = false;
        AudioManager.PlayBGM(BGMType.Menu);
        mainOptions.SetActive(true);

        bestDistance.text = string.Format("{0:N}", UserData.instance.GetBestMileage());
        bestTime.text = TimeSpan.FromSeconds(UserData.instance.GetBestTime()).ToString(@"hh\:mm\:ss");
        bestScore.text = string.Format("{0:n0}", UserData.instance.GetBestScore());
    }

    public void PlayGame() {
        AudioManager.PlayUIAudio();
        SceneManager.LoadScene(1);
    }

    public void ShowHideDialogue(GameObject obj) {
        AudioManager.PlayUIAudio();
        mainOptions.SetActive(obj.activeSelf);
        obj.SetActive(!obj.activeSelf);
    }


    //public void CloseDialogue(GameObject obj) {
    //    AudioManager.PlayUIAudio();
    //    obj.SetActive(false);
    //    mainOptions.SetActive(true);
    //}
}
