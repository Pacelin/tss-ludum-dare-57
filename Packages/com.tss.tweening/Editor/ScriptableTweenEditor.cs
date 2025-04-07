using UnityEditor;
using UnityEngine;

namespace TSS.Tweening.Editor
{
    [CustomEditor(typeof(ScriptableTween))]
    public class ScriptableTweenEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            ScriptableTweenItemPropertyDrawer.TargetProvider = serializedObject;

            var presetProp = serializedObject.FindProperty("_preset");
            var itemsProp = serializedObject.FindProperty("_items");
            var cacheProp = serializedObject.FindProperty("_cacheTween");
            var loopsProp = serializedObject.FindProperty("_loops");
            var playOnEnableProp = serializedObject.FindProperty("_playOnEnable");
            var loopTypeProp = serializedObject.FindProperty("_loopType");
            var delayProp = serializedObject.FindProperty("_delay");
            var completeOnKill = serializedObject.FindProperty("_completeOnKill");

            EditorGUILayout.PropertyField(completeOnKill);
            EditorGUILayout.PropertyField(cacheProp);
            EditorGUILayout.PropertyField(playOnEnableProp);
            EditorGUILayout.PropertyField(loopsProp);
            EditorGUILayout.PropertyField(loopTypeProp);
            EditorGUILayout.PropertyField(delayProp);
            EditorGUILayout.PropertyField(presetProp);
            if (presetProp.objectReferenceValue)
            {
                var lastEnabled = GUI.enabled;
                GUI.enabled = false;
                EditorGUILayout.PropertyField(new SerializedObject(presetProp.objectReferenceValue).FindProperty("_items"));
                GUI.enabled = lastEnabled;
                itemsProp.arraySize = 0;
            }
            else
               EditorGUILayout.PropertyField(itemsProp);

            var obj = target as ScriptableTween;
            if (GUILayout.Button("Play"))
            {
                if (!Application.isPlaying)
                {
                    OnDisable();
                    DG.DOTweenEditor.DOTweenEditorPreview.PrepareTweenForPreview(obj!.GetNewTween(), andPlay: false);
                    DG.DOTweenEditor.DOTweenEditorPreview.Start(() => SceneView.lastActiveSceneView.Repaint());
                }
            }
            if (GUILayout.Button("Stop"))
                OnDisable();
            
            serializedObject.ApplyModifiedProperties();
            serializedObject.Update();
        }

        private void OnDisable()
        {
            DG.DOTweenEditor.DOTweenEditorPreview.Stop(true);
            SceneView.lastActiveSceneView.Repaint();
            Canvas.ForceUpdateCanvases();
        }
    }
}