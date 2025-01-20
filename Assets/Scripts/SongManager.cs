using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Interaction;
using System.IO;
using UnityEngine.Networking;
using System;

public class SongManager : MonoBehaviour
{

    public static SongManager Instance;
    public static int SongNum = 0;
    public AudioSource MLaudioSource;
    public AudioSource AlaudioSource;
    public AudioSource LOaudioSource;
    private AudioSource audioSource;

    public Lane[] lanes;
    public float songDelayInSeconds;
    //public double marginOfError; // in seconds

    //public int inputDelayInMilliseconds;


    public string MLfileLocation;
    public string AlfileLocation;
    public string LOfileLocation;
    public string fileLocation;
    public float noteTime;
   
    public float noteTapY; 
    public float noteTapX;


    public static MidiFile midiFile;


    // Start is called before the first frame update
    void Start()
    {

        Instance = this;
        if (SongNum == 0)
            audioSource = MLaudioSource;
        if (SongNum == 1)
            audioSource = AlaudioSource;
        if (SongNum == 2)
            audioSource = LOaudioSource;
        ReadFromFile();
        
    }

    private void ReadFromFile()
    {
        if (SongNum == 0)
            fileLocation = MLfileLocation;
        if (SongNum == 1)
            fileLocation = AlfileLocation;
        if (SongNum == 2)
            fileLocation = LOfileLocation;

        midiFile = MidiFile.Read(Application.streamingAssetsPath + "/" + fileLocation);
        GetDataFromMidi();
    }

    private void GetDataFromMidi()
    {
        var notes = midiFile.GetNotes();
        var array = new Melanchall.DryWetMidi.Interaction.Note[notes.Count];
        notes.CopyTo(array, 0);

        foreach (var lane in lanes) lane.SetTimeStamps(array);

        Invoke(nameof(StartSong), songDelayInSeconds);
    }
    public void StartSong()
    {
        audioSource.Play();

    }
    public static double GetAudioSourceTime()
    {   
        return (double)Instance.audioSource.timeSamples / Instance.audioSource.clip.frequency;
        
    }



    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale == 0)
        {
            audioSource.Pause();
        }
        if (Time.timeScale == 1)
        {
            audioSource.UnPause();
        }
        if (audioSource.isPlaying == false && GetAudioSourceTime()  > 10 && Time.timeScale == 1)
        {

            ScoreManager.Instance.ShowResults = true;
        }
    }
}
