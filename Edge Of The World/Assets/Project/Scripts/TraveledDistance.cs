using UnityEngine;

[DisallowMultipleComponent]
public class TraveledDistance : MonoBehaviour {

    //public float startDelay = 1f;
    [Range(0.0f, 1.0f)]
    public float minimumDistance = 0.08f;

    private Vector3 lastPosition;
    public float TotalTraveledDistance { get; private set; }
    //private float distanceTraveled;
    //private int walkable;
    [HideInInspector]
    public bool isActive;
    //private float stateTimeElapse;

    private void Start() {
        TotalTraveledDistance = 0f;
        isActive = true;
        //walkable = LayerMask.NameToLayer("Walkable");
    }

    private void Update() {

        if (!isActive) return;

        Vector3 newPos = new Vector3(transform.position.x, 0f, transform.position.z);

        float d = Vector3.Distance(newPos, lastPosition);
        lastPosition = newPos;

        if (d > minimumDistance)
            TotalTraveledDistance += d;
    }

    // public bool CheckIfCountDownElapsed(float duration) {

    //    stateTimeElapse += Time.deltaTime;
    //    return stateTimeElapse >= duration;
    //}

    //private void OnCollisionEnter(Collision collision) {

    //    if (isActive) return;

    //    if (collision.gameObject.layer != walkable) return;

    //    lastPosition = new Vector3(transform.position.x, 0f, transform.position.z);       
    //    isActive = true;
    //}

    //public float GetDistanceTraveled() {
    //    return distanceTraveled;
    //}
}
