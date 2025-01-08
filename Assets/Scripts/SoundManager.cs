using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;


public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [SerializeField] private AudioSource _bgmSource = null, _sfxSource = null;
    [SerializeField] private AudioClip[] _audios;
    [SerializeField] public AudioClip _sfxClip;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        _bgmSource.loop = true;
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        AudioClip newClip = null;
        if (scene.name == "Start" || scene.name == "FileSelect" || scene.name == "GameSelect") newClip = _audios[0];
        if (scene.name == "Settings") newClip = _audios[1];
        if (scene.name == "Useless Shop") newClip = _audios[2];

        if (scene.name == "Jacks Game" || scene.name == "Jack Song Select" || scene.name == "Tyler Wen Game" || scene.name == "Shrimp Swiper" || scene.name == "Rustin's game") _bgmSource.Pause();
        else if (!_bgmSource.isPlaying) _bgmSource.UnPause();

        if (newClip != _bgmSource.clip && newClip != null)
        {
            _bgmSource.enabled = false;
            _bgmSource.clip = newClip;
            _bgmSource.enabled = true;
        }
    }

    public void Pause()
    {
        _bgmSource.Pause();
    }

    public void UnPause()
    {
        _bgmSource.UnPause();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Return))
        {
            _sfxSource.clip = _sfxClip;
            _sfxSource.PlayOneShot(_sfxSource.clip);
        }
    }

    public void PlaySFX(AudioClip audioClip)
    {
        _sfxSource.clip = audioClip;
        _sfxSource.PlayOneShot(audioClip);
        _sfxSource.clip = null;
    }

    public void ChangeBGMVolume(float value)
    {
        _bgmSource.volume = value;
    }

    public void ChangeSFXVolume(float value)
    {
        _sfxSource.volume = value;
    }

    public void ToggleBGM(bool isOn)
    {
        _bgmSource.mute = !isOn;
    }

    public void ToggleSFX(bool isOn)
    {
        _sfxSource.mute = !isOn;
    }
}
