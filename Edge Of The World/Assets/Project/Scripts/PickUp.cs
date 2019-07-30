using UnityEngine;

[DisallowMultipleComponent]
public class PickUp : MonoBehaviour {

    public bool isBonus = true;
    [Range(26.0f, 28.5f)]
    public float maxDistance = 26f;
    public float speed = 10f;

    //public Transform fakeShadow;

    protected Transform planet;
    protected int playerLayer;
    protected bool positionReach;
    //private Quaternion shadowRot;

    protected virtual void Start() {
        //shadowRot = fakeShadow.rotation;
        playerLayer = LayerMask.NameToLayer("Player");
        planet = GameManager.Instance.GetPlanet();

    }

    protected virtual void Update() {
        SetPositionOrientation();
    }

    protected virtual void OnTriggerEnter(Collider other) {

        if (other.gameObject.layer != playerLayer)
            return;

        //Check if bonus or not and do action
        GameManager.Instance.CheckBonus(transform.position, transform.rotation, isBonus);

        //Debug.Log("Player Detected");
        Destroy(gameObject);        
    }

    protected virtual void SetPositionOrientation() {

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
