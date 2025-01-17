using Melanchall.DryWetMidi.Interaction;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lane : MonoBehaviour
{
    public static Lane Instance;
    public Melanchall.DryWetMidi.MusicTheory.NoteName noteRestriction;
    public KeyCode input;
    public GameObject notePrefab;
    public GameObject shinyPrefab;
    public float noteSpawnY;
    public float noteDespawnY
    {
        get
        {
            return SongManager.Instance.noteTapY - (noteSpawnY - SongManager.Instance.noteTapY);
        }
    }

    public float noteSpawnX;
    public float noteDespawnX
    {
        get
        {
            return SongManager.Instance.noteTapX - (noteSpawnX - SongManager.Instance.noteTapX);
        }
    }
    List<Rock> notes = new List<Rock>();
    public List<double> timeStamps = new List<double>();

    int spawnIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
    }
    public void SetTimeStamps(Melanchall.DryWetMidi.Interaction.Note[] array)
    {
        foreach (var note in array)
        {
            if (note.NoteName == noteRestriction)
            {
                var metricTimeSpan = TimeConverter.ConvertTo<MetricTimeSpan>(note.Time, SongManager.midiFile.GetTempoMap());
                timeStamps.Add((double)metricTimeSpan.Minutes * 60f + metricTimeSpan.Seconds + (double)metricTimeSpan.Milliseconds / 1000f);
            }

        }
    }
 
    // Update is called once per frame
    void Update()
    {
        if (spawnIndex < timeStamps.Count)
        {
            if (SongManager.GetAudioSourceTime() >= timeStamps[spawnIndex] - SongManager.Instance.noteTime)
            {

                if (UnityEngine.Random.Range(0, 80) != 1)
                {
                    var note = (Instantiate(notePrefab, transform));
                    notes.Add(note.GetComponent<Rock>());
                    note.GetComponent<Rock>().assignedTime = (float)timeStamps[spawnIndex];
                }
                else
                {
                    var note = (Instantiate(shinyPrefab, transform));
                    notes.Add(note.GetComponent<Rock>());
                    note.GetComponent<Rock>().assignedTime = (float)timeStamps[spawnIndex];
                }


                spawnIndex++;
            }
        }

        //if (inputIndex < timeStamps.Count)
        //{
        //    double timeStamp = timeStamps[inputIndex];
        //    double marginOfError = SongManager.Instance.marginOfError;
        //    double audioTime = SongManager.GetAudioSourceTime() - (SongManager.Instance.inputDelayInMilliseconds / 1000.0);

        //    if (Input.GetKeyDown(input))
        //    {
        //        if (Math.Abs(audioTime - timeStamp) < marginOfError)
        //        {
        //            Hit();
        //            print($"Hit on {inputIndex} note");
        //            Destroy(notes[inputIndex].gameObject);
        //            inputIndex++;
        //        }
        //        else
        //        {
        //            print($"Hit inaccurate on {inputIndex} note with {Math.Abs(audioTime - timeStamp)} delay");
        //        }
        //    }
        //    if (timeStamp + marginOfError <= audioTime)
        //    {
        //        Miss();
        //        print($"Missed {inputIndex} note");
        //        inputIndex++;
        //    }
        //}

    }
    //private void Hit()
    //{
    //    ScoreManager.Hit();
    //}
    //private void Miss()
    //{
    //    ScoreManager.Miss();
    //}
}