using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SurfaceGen))]
public class SurfaceCreatorInspector : Editor
{
    private SurfaceGen _creator;

    private void OnEnable()
    {
        _creator = target as SurfaceGen;
        Undo.undoRedoPerformed += RefreshCreator;

    }
    private void OnDisable () {
        Undo.undoRedoPerformed -= RefreshCreator;
    }
    private void RefreshCreator()
    {
        if (Application.isPlaying)
        {
            _creator.Refresh();
        }
    }
    public override void OnInspectorGUI () {
        EditorGUI.BeginChangeCheck();
        DrawDefaultInspector();
        if (EditorGUI.EndChangeCheck()) {
            RefreshCreator();
        }
    }
    
}
