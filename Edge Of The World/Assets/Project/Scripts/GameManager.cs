using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class GameManager : MonoBehaviour {

    #region Singleton

    public static GameManager Instance { get; private set; }

    private void Awake() {

        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }
    #endregion /Singleton

    [SerializeField]
    private GameObject planet;
    public TraveledDistance traveledDistance;
    public float distanceRequirement = 250f;

    private float totalDistance;
    private float totalTime;

    private bool isEndGameReady;

    #region Unity Methods

    private void Start() {
        
    }


    private void Update() {

        CheckEndGameReady();       
    }

    #endregion /Unity Methods

    #region Custom Method

    public GameObject GetPlanet() {
        return planet;
    }

    private void CheckEndGameReady() {

        if (isEndGameReady) return;

        if (traveledDistance.TotalTraveledDistance > distanceRequirement) {
            isEndGameReady = true;
            Debug.Log("Activate endgame portal");
        }
    }

    #endregion /Custom Method
}
