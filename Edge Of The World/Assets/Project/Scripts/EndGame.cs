using UnityEngine;

[DisallowMultipleComponent]
public class EndGame : PickUp {

    public GameObject[] catPrefabs;


    protected override void OnTriggerEnter(Collider other) {

        if (!isActive)
            return;

        if (other.gameObject.layer != playerLayer)
            return;

        Instantiate(catPrefabs[Random.Range(0, catPrefabs.Length)], transform.position, transform.rotation);
        //Debug.Log("Game ended roll credits");

        Player player = other.GetComponent<Player>();
        isActive = false;
        player?.ActivateEndGame();
    }
}
