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
    public int totalCollection = 5;


    private float totalDistance;
    private float totalTime;

    private bool isDistanceAchieved;
    private bool isCollectionAchieved;
    private int totalScore;
    private int collection;

    #region Unity Methods

    private void Start() {
        isDistanceAchieved = false;
        isCollectionAchieved = false;
        Time.timeScale = 1f;
        AudioManager.PlayBGM(BGMType.Gameplay);
        AudioListener.pause = false;
    }


    private void Update() {

        CheckEndGameReady();
    }

    #endregion /Unity Methods

    #region Custom Method

    public Transform GetPlanet() {
        return planet;
    }

    public void CheckBonus(Vector3 pos, Quaternion rot, bool isBonus, LootType lootType) {

        if (isBonus) {

            collection++;
            totalScore += 100;
            GUIManager.Instance.ActivateUIEffect((int)lootType, totalScore);

            if (collection >= totalCollection)
                isCollectionAchieved = true;

            AudioManager.PlayAudioEffect(EffectAudio.Bonus);
        } else {
            totalScore -= 100;
            GUIManager.Instance.ActivateUIEffect((int)lootType, totalScore, false);
            AudioManager.PlayAudioEffect(EffectAudio.Penalty);
        }

        pickupEffectPool.GetPooledObject(pos, rot);
        //Debug.Log("Collected: " + collection);
    }


    private void CheckEndGameReady() {

        if (!isCollectionAchieved) return;

        if (isDistanceAchieved) return;

        if (traveledDistance.TotalTraveledDistance > distanceRequirement) {
            isDistanceAchieved = true;
            Instantiate(worldEdge, worldEdgeSpawnPoints[Random.Range(0, worldEdgeSpawnPoints.Length)].position, Quaternion.identity);
            //Debug.Log("Activate endgame portal");
        }
    }

    public void ActivateEnding() {
        AudioManager.PlayBGM(BGMType.Credits);
       GUIManager.Instance.SetCreditsDetails(totalScore);       
    }

    #endregion /Custom Method
}

public enum LootType {
    Illu,
    Alien,
    Eye,
    Mason,
    Moon,
    Gov,
    Media
}