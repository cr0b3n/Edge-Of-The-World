using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class Projectile : MonoBehaviour {

    public Rigidbody myRB;
    public float speed = 30f;

    private Player player;

    private void Start() {

        player = GameObject.FindObjectOfType<Player>();
        //myRB.AddForce(transform.forward * speed, ForceMode.Impulse);
    }

    private void Update() {
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        //myRB.MovePosition(player.transform.position * speed * Time.deltaTime);
        //transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
}
