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
    public AudioSource VVaudioSource;
    public AudioSource AlaudioSource;
    public AudioSource LOaudioSource;
    public AudioSource BPaudioSource;
    public AudioSource SCaudioSource;
    private AudioSource audioSource;

    public Lane[] lanes;
    public float songDelayInSeconds;
    //public double marginOfError; // in seconds

    //public int inputDelayInMilliseconds;


    public string VVfileLocation;
    public string AlfileLocation;
    public string LOfileLocation;
    public string BPfileLocation;
    public string SCfileLocation;
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
            audioSource = VVaudioSource;
        if (SongNum == 1)
            audioSource = AlaudioSource;
        if (SongNum == 2)
            audioSource = LOaudioSource;
        if (SongNum == 3)
            audioSource = BPaudioSource;
        if (SongNum == 4)
            audioSource = SCaudioSource;
        ReadFromFile();
        
    }

    private void ReadFromFile()
    {
        if (SongNum == 0)
            fileLocation = VVfileLocation;
        if (SongNum == 1)
            fileLocation = AlfileLocation;
        if (SongNum == 2)
            fileLocation = LOfileLocation;
        if (SongNum == 3)
            fileLocation = BPfileLocation;
        if (SongNum == 4)
            fileLocation = SCfileLocation;
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
        if (audioSource.isPlaying == false && GetAudioSourceTime()  > 120 && Time.timeScale == 1)
        {

            ScoreManager.Instance.ShowResults = true;
        }
    }
}
