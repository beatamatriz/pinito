using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beat : MonoBehaviour{
    private double instanceTime;
    public float assignedTime;

    void Start(){
        instanceTime = Conductor.GetAudioSourceTime();
    }

    void Update(){
        double aliveTime = Conductor.GetAudioSourceTime() - instanceTime;
        float t = (float)(aliveTime) / (Conductor.Instance.beatTime);

        if (t > 1){
            Destroy(gameObject);
        }
        else{
            transform.localPosition = Vector2.Lerp(Conductor.Instance.beatSpawnPos, Conductor.Instance.beatTapPos, t);
        }
        
    }
}
