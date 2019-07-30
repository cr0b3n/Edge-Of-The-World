using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class AnimationEvent : MonoBehaviour {

    public ObjectPooler stepsPool;

    public void SpawnStepEffect() {
        stepsPool.GetPooledObject(transform.position, transform.rotation);
    }
}
