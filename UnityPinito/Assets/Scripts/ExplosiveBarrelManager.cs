using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

#if UNITY_EDITOR
using UnityEngine;
#endif

using UnityEngine.Rendering.VirtualTexturing;

public class ExplosiveBarrelManager : MonoBehaviour{
    public static List<ExplosiveBarrel> explosiveBarrels = new List<ExplosiveBarrel>();

    #if UNITY_EDITOR
    void OnDrawGizmos(){
        foreach (ExplosiveBarrel barrel in explosiveBarrels){
            Vector2 managerPos = transform.position;
            Vector2 barrelPos = barrel.transform.position;
            float halfHeight = (managerPos.y - barrelPos.y) * 0.5f;
            Vector2 offset = Vector2.up * halfHeight;
            //Handles.DrawAAPolyLine(transform.position, barrel.transform.position);
            //Gizmos.DrawLine(transform.position, barrel.transform.position);
            Handles.DrawBezier(managerPos, barrelPos, 
                managerPos-offset, barrelPos+offset, Color.white,
                EditorGUIUtility.whiteTexture, 1f);
            
        }
    }
    #endif
}
