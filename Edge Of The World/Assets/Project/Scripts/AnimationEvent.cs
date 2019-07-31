using UnityEngine;

[DisallowMultipleComponent]
public class AnimationEvent : MonoBehaviour {

    public ObjectPooler stepsPool;

    public void SpawnStepEffect() {
        stepsPool.GetPooledObject(transform.position, transform.rotation);
        AudioManager.PlayPlayerAudio(PlayerAudio.Run);
    }

    //public void PlayWalkAudio() {
    //    AudioManager.PlayPlayerAudio(PlayerAudio.Walk);
      
    //}
}
