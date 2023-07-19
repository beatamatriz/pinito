using System.Collections;
using System.Collections.Generic;
using Melanchall.DryWetMidi.Interaction;
using UnityEngine;
using UnityEngine.Experimental.Rendering;

public class Lane : MonoBehaviour{

    public Melanchall.DryWetMidi.MusicTheory.NoteName noteSnap;

    public KeyCode input;

    public GameObject notePrefab;

    private List<Beat> beats = new List<Beat>();

    public List<double> timeStamps = new List<double>();

    private int spawnIndex = 0;
    private int inputIndex = 0;
    
    // Start is called before the first frame update
    void Start(){
        
    }

    public void SetTimeStamps(Melanchall.DryWetMidi.Interaction.Note[] array){
        foreach (var beat in array){
            if (beat.NoteName == noteSnap){
                var metricTimeSpan = TimeConverter.ConvertTo<MetricTimeSpan>(beat.Time, Conductor.midiFile.GetTempoMap());
                timeStamps.Add((double) metricTimeSpan.Minutes*60f + metricTimeSpan.Seconds 
                                                                   + (double) metricTimeSpan.Milliseconds/1000f);
            }
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        if (spawnIndex < timeStamps.Count){
            if (Conductor.GetAudioSourceTime() >= timeStamps[spawnIndex] - Conductor.Instance.beatTime){
                var beat = Instantiate(notePrefab, transform);
                beats.Add(beat.GetComponent<Beat>());
                beat.GetComponent<Beat>().assignedTime = (float)timeStamps[spawnIndex];
                spawnIndex++;

            }
        }

        if (inputIndex < timeStamps.Count){
            double timeStamp = timeStamps[inputIndex];
            double pefectBegin = Conductor.Instance.perfectBegin;
            double pefectEnd = Conductor.Instance.perfectEnd;

            double audioTime = Conductor.GetAudioSourceTime() - (Conductor.Instance.inputLag_ms / 1000.0);

            if (Input.GetKeyDown(input)){
                if (audioTime > timeStamp - pefectBegin || audioTime < timeStamp + pefectEnd){
                    Hit();
                    Destroy(beats[inputIndex].gameObject);
                    inputIndex++;
                }
                else{
                    //BAD
                }
            }

            if (audioTime >= timeStamp + pefectEnd){
                Miss();
            }
        }
        
    }

    private void Hit(){
        throw new System.NotImplementedException();
    }

    private void Miss(){
        throw new System.NotImplementedException();
    }
}
