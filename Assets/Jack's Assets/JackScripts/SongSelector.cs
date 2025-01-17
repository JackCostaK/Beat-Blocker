using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SongSelector : MonoBehaviour
{
    [SerializeField]
    private int song_num;
    [SerializeField]
    private AudioSource song;

    // Start is called before the first frame update
    public void SelectSong()
    {
        SongManager.SongNum = song_num;
        song.Play();
    }
    public void DeselectSong()
    {
        song.Stop();
    }
}