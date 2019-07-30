using UnityEngine;

[DisallowMultipleComponent]
public class UIAnimEvent : MonoBehaviour {

    public void AnimationComplete() {
        gameObject.SetActive(false);
    }
}
