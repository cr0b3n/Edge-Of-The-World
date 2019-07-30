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

    [Header("Basic Section")]
    [SerializeField]
    private Transform planet;
    public ObjectPooler pickupEffectPool;

    [Header("Goal Section")]
    public TraveledDistance traveledDistance;
    public float distanceRequirement = 250f;
    public GameObject worldEdge;
    public Transform[] worldEdgeSpawnPoints;

    private float totalDistance;
    private float totalTime;

    private bool isEndGameReady;
    private int totalScore;

    #region Unity Methods

    private void Start() {
        Time.timeScale = 1f;
    }


    private void Update() {

        CheckEndGameReady();       
    }

    #endregion /Unity Methods

    #region Custom Method

    public Transform GetPlanet() {
        return planet;
    }

    public void CheckBonus(Vector3 pos, Quaternion rot, bool isBonus) {

        if (isBonus)
            totalScore++;
        else
            totalScore--;

        pickupEffectPool.GetPooledObject(pos, rot);
        Debug.Log("Score: " + totalScore);
    }


    private void CheckEndGameReady() {

        if (isEndGameReady) return;

        if (traveledDistance.TotalTraveledDistance > distanceRequirement) {
            isEndGameReady = true;
            Instantiate(worldEdge, worldEdgeSpawnPoints[Random.Range(0, worldEdgeSpawnPoints.Length)].position, Quaternion.identity);            
            Debug.Log("Activate endgame portal");
        }
    }

    #endregion /Custom Method
}