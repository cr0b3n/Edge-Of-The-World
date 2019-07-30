using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[DisallowMultipleComponent]
public class PlanetMovingObject : MonoBehaviour {

    private Transform planet;
    private Rigidbody myRB;
    private float gravity = 100;
    private bool OnGround = false;
    private float distanceToGround;
    private Vector3 Groundnormal;

    private void Start() {
        planet = GameManager.Instance.GetPlanet();
        myRB = GetComponent<Rigidbody>();
        myRB.freezeRotation = true;
    }

    private void Update() {

        //MOVEMENT
        //float x = Input.GetAxis("Horizontal") * Time.deltaTime * speed;
        //float z = Input.GetAxis("Vertical") * Time.deltaTime * speed;

        //transform.Translate(new Vector3(playerInput.horizontal, 0, playerInput.vertical) * Time.deltaTime * speed);

        //Local Rotation

        //if (Input.GetKey(KeyCode.E)) {

        //    transform.Rotate(0, 150 * Time.deltaTime, 0);
        //}
        //if (Input.GetKey(KeyCode.Q)) {

        //    transform.Rotate(0, -150 * Time.deltaTime, 0);
        //}

        //GroundControl
        RaycastHit hit = new RaycastHit();
        if (Physics.Raycast(transform.position, -transform.up, out hit, 10)) {

            distanceToGround = hit.distance;
            Groundnormal = hit.normal;

            if (distanceToGround <= 0.2f) {
                OnGround = true;
            } else {
                OnGround = false;
            }
        }

        //GRAVITY and ROTATION
        Vector3 gravDirection = (transform.position - planet.position).normalized;

        if (OnGround == false) {
            myRB.AddForce(gravDirection * -gravity);
        }

        //
        Quaternion toRotation = Quaternion.FromToRotation(transform.up, Groundnormal) * transform.rotation;
        transform.rotation = toRotation;
    }
}
