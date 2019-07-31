using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class AudioManager : MonoBehaviour {

    static AudioManager current;

    [Header("Gameplay Audio Clips")]
    public AudioClip runClip;
    public AudioClip jumpClip;
    public AudioClip landClip;
    public AudioClip bonusClip;
    public AudioClip penaltyClip;
    public AudioClip catClip;

    [Header("UI Audio Clips")]
    public AudioClip buttonAudio;

    [Header("BGM")]
    public AudioClip gamePlayClip;
    public AudioClip menuBGMClip;
    public AudioClip endCreditClip;

    private AudioSource bgmSource;
    private AudioSource uiSource;
    private AudioSource playerSource;
    private AudioSource effectSource;

    private void Awake() {

        if (current != null && current != this) {
            Destroy(gameObject);
            return;
        }

        current = this;
        DontDestroyOnLoad(gameObject);

        bgmSource = gameObject.AddComponent<AudioSource>() as AudioSource;
        uiSource = gameObject.AddComponent<AudioSource>() as AudioSource;
        playerSource = gameObject.AddComponent<AudioSource>() as AudioSource;
        effectSource = gameObject.AddComponent<AudioSource>() as AudioSource;

        uiSource.ignoreListenerPause = true;
    }

    public static void PlayBGM(BGMType bgmType) {

        switch(bgmType) {

            case BGMType.Gameplay:
                current.bgmSource.clip = current.gamePlayClip;
                break;
            case BGMType.Menu:
                current.bgmSource.clip = current.menuBGMClip;
                break;
            case BGMType.Credits:
                current.bgmSource.clip = current.endCreditClip;
                break;
        }

        //current.bgmSource.clip = (isMenu) ? current.menuBGMClip : current.gamePlayClip;
        current.bgmSource.loop = true;
        current.bgmSource.volume = 0.25f;
        current.bgmSource.Play();
    }

    public static void PlayAudioEffect(EffectAudio effectAudio) {

        //if (current.effectSource.isPlaying)
        //    current.effectSource.Stop();

        switch (effectAudio) {

            case EffectAudio.Bonus:
                current.effectSource.PlayOneShot(current.bonusClip);
                break;
            case EffectAudio.Penalty:
                current.effectSource.PlayOneShot(current.penaltyClip);
                break;
            case EffectAudio.Cat:
                current.effectSource.PlayOneShot(current.catClip);
                break;
        }
    }

    public static void PlayPlayerAudio(PlayerAudio playerAudio) {

        if (current.playerSource.isPlaying)
            current.playerSource.Stop();

        switch (playerAudio) {
            case PlayerAudio.Run:
                current.effectSource.PlayOneShot(current.runClip);
                break;
            case PlayerAudio.Jump:
                current.effectSource.PlayOneShot(current.jumpClip);
                break;
            case PlayerAudio.Land:
                current.effectSource.PlayOneShot(current.landClip);
                break;
        }
    }

    public static void PlayGameOverAudio() {
        //current.uiSource.PlayOneShot(current.gameOverAudio);

    }

    public static void PlayUIAudio() {

        //if (current.uiSource.isPlaying)
        //    current.uiSource.Stop();
        current.uiSource.PlayOneShot(current.buttonAudio);
        //current.uiSource.Play();
    }
}

public enum PlayerAudio {
    Run,
    Jump,
    Land
}

public enum EffectAudio {
    Bonus,
    Penalty,
    Cat
}

public enum BGMType {
    Gameplay,
    Menu,
    Credits
}