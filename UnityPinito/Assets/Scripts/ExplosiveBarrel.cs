using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class ExplosiveBarrel : MonoBehaviour{
    [Range(1f,8f)] public float radius = 1;
    public float damage = 10;
    public Color color = Color.red;

    private MaterialPropertyBlock mpb;
    private static readonly int shPropColor = Shader.PropertyToID("Color");

    public MaterialPropertyBlock Mpb{
        get{
            if (mpb == null) mpb = new MaterialPropertyBlock();
            return mpb;
        }
    }


    void ApplyColor(){
        SpriteRenderer rnd = GetComponent<SpriteRenderer>();
        Mpb.SetColor(shPropColor, color);
        rnd.SetPropertyBlock(Mpb);
    }

    private void OnValidate(){
        ApplyColor();
    }

    private void OnEnable(){
        ApplyColor();
        ExplosiveBarrelManager.explosiveBarrels.Add((this));
    }
    
    private void OnDisable(){
        ExplosiveBarrelManager.explosiveBarrels.Remove((this));
    }

    private void OnDrawGizmosSelected(){
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
