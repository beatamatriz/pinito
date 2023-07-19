using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Interaction;
using System.IO;
using UnityEngine.Networking;
public class Conductor : MonoBehaviour{
    public static Conductor Instance;
    public AudioSource audioSource;
    public float offset_sec;
    public int inputLag_ms;
    public string filePath;
    public float beatTime;
    public Vector2 beatSpawnPos;
    public Vector2 beatTapPos;

    public double perfectBegin; //in seconds
    public double perfectEnd;
    
    public static MidiFile midiFile;

    void Start(){
        Instance = this;
        ReadFromFile();
    }

    private void ReadFromFile(){
        midiFile = MidiFile.Read(Application.streamingAssetsPath + "/" + filePath);
    }

    //TODO: manipulate, gatekeep, girlboss, gaslight
    public void GetDataFromMidi(){
        var notes = midiFile.GetNotes();
        var array = new Note[notes.Count];
        notes.CopyTo(array, 0);
        
        Invoke(nameof(StartSong), offset_sec);
    }

    public void StartSong(){
        audioSource.Play();
    }

    public static double GetAudioSourceTime(){
        return (double)Instance.audioSource.timeSamples / Instance.audioSource.clip.frequency;
    }
}
