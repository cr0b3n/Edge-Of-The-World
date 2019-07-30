using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSpawner : MonoBehaviour {

    //public float radius = 25f;
    private Rigidbody myRB;
    //private Vector3 planetLoc;
    private float gravity = 100;
    private bool OnGround = false;
    private float distanceToGround;
    private Vector3 Groundnormal;
    private Transform planet;
    public float distace = 10f;

    private void Start() {
        planet = GameManager.Instance.GetPlanet();
        myRB = GetComponent<Rigidbody>();
        //planetLoc = GameManager.Instance.GetPlanet().transform.position;
    }

    private void Update() {
        //Debug.Log(Vector3.Distance(planetLoc, transform.position));

        RaycastHit hit = new RaycastHit();
        if (Physics.Raycast(transform.position, -transform.up, out hit, distace)) {

            distanceToGround = hit.distance;
            Groundnormal = hit.normal;

            if (distanceToGround <= 0.2f) {
                OnGround = true;
            } else {
                OnGround = false;
            }
        }

        //GRAVITY and ROTATION
        Vector3 gravDirection = (transform.position - planet.transform.position).normalized;

        if (OnGround == false) {
            myRB.AddForce(gravDirection * -gravity);
        }

        //
        Quaternion toRotation = Quaternion.FromToRotation(transform.up, Groundnormal) * transform.rotation;
        transform.rotation = toRotation;

        //if (Input.GetKeyDown(KeyCode.K)) {
        //    //Vector3 pos = RandomCircle(planetLoc, radius);
        //    //Quaternion rot = Quaternion.FromToRotation(Vector3.forward, planetLoc - pos);
        //    //transform.position = pos;
        //    //transform.rotation = rot;
        //    transform.position = Random.onUnitSphere * radius;
        //}
    }

    //private Vector3 RandomCircle(Vector3 center, float radius) {
    //    float ang = Random.value * 360;
    //    Vector3 pos;
    //    //pos.x = center.x + radius * Mathf.Sin(ang * Mathf.Deg2Rad);
    //    //pos.y = center.y + radius * Mathf.Cos(ang * Mathf.Deg2Rad);
    //    //pos.z = center.z;

    //    pos.x = Random.Range(-1f, 1f);
    //    pos.y = Random.Range(-1f, 1f);
    //    pos.z = Random.Range(-1f, 1f);

    //    pos = pos.normalized * radius;

    //    return pos;
    //}
}
