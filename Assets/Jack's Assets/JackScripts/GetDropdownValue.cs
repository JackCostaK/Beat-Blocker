using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GetDropdownValue : MonoBehaviour
{
    [SerializeField]
    private TMP_Dropdown dropdown;
    [SerializeField]
    private AudioSource MLaudioSource;
    [SerializeField]
    private AudioSource JTaudioSource;


    public void Start()
    {
        MLaudioSource.Play();

    }
    public void HandleInputData()
    {
        int val = dropdown.value;
        Debug.Log(val);
        if (val == 0)
        {
            SongManager.SongNum = 0;
            MLaudioSource.Play();
            JTaudioSource.Stop();
        }
        if (val == 1)
        {
            SongManager.SongNum = 1;
            JTaudioSource.Play();
            MLaudioSource.Stop();
        }
    }
}
