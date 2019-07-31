using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class UserData : MonoBehaviour {

    #region Singleton

    public static UserData instance;

    private void Awake() {

        if (instance != null && instance != this) {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    #endregion /Singleton

    private int bestScore;
    private float bestTime;
    private float bestMileage;

    private void Start() {
        bestScore = 0;
        bestTime = 1000000f;
        bestMileage = 1000000f;
    }

    public int GetBestScore() {
        return bestScore;
    }

    public void CheckScore(int score) {

        if (score < bestScore)
            return;

        bestScore = score;
    }

    public float GetBestTime() {
        return bestTime;
    }

    public void CheckTime(float time) {
        if (time > bestTime)
            return;

        bestTime = time;
    }

    public float GetBestMileage() {
        return bestMileage;
    }

    public void CheckMileage(float mileage) {
        if (mileage > bestMileage)
            return;

        bestMileage = mileage;
    }
}
