using UnityEngine;

[DisallowMultipleComponent]
public class PickUp : MonoBehaviour {

    public bool isBonus = true;
    [Range(26.0f, 28.5f)]
    public float maxDistance = 26f;
    public float speed = 10f;

    //public Transform fakeShadow;

    private Transform planet;
    private int playerLayer;
    private bool positionReach;
    //private Quaternion shadowRot;

    private void Start() {
        //shadowRot = fakeShadow.rotation;
        playerLayer = LayerMask.NameToLayer("Player");
        planet = GameManager.Instance.GetPlanet();

    }

    private void Update() {
        SetPositionOrientation();
    }

    private void OnTriggerEnter(Collider other) {

        if (other.gameObject.layer != playerLayer)
            return;


        Debug.Log("Player Detected");

        //Check if bonus or not and do action
    }

    private void SetPositionOrientation() {

        if (positionReach)
            return;

        float d = Vector3.Distance(transform.position, planet.position);

        if (d <= maxDistance) {        
            positionReach = true;
            return;
        }

        transform.position = Vector3.MoveTowards(transform.position, planet.position, speed * Time.deltaTime);
        Quaternion rot = Quaternion.FromToRotation(Vector3.forward, planet.position - transform.position);
        transform.rotation = rot;
        //fakeShadow.rotation = shadowRot;
    }
}
