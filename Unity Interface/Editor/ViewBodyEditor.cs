using UnityEngine;
using UnityEditor;

using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using Suffixian.ViewMaster.Core;
using Suffixian.ViewMaster.Unity;
using Suffixian.Unity.Editor;

namespace Suffixian.ViewMaster.Unity
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(ViewBody), true)]
    public class ViewBodyEditor : Editor
    {
        ViewBody body;

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            body = target as ViewBody;

            GUI.changed = false;

            DrawConfigInterface(body);

            if (GUISuffixian.DrawToggledHeader("References (Base iGUI)", "references", false))
            {
                GUISuffixian.BeginFrame();
                base.OnInspectorGUI();
                GUISuffixian.EndFrame();
            }

            serializedObject.ApplyModifiedProperties();
        }

        public void DrawConfigInterface(UnityEngine.Object undoObject)
        {
            if (GUISuffixian.DrawToggledHeader("Config", "configinterface", false))
            {
                GUISuffixian.BeginFrame();

                if (body == null)
                {
                    EditorGUILayout.LabelField(string.Format("This View Body is inaccessible (null)."));
                }
                else
                {
                    //TODO Edit ViewBody info
                }

                GUISuffixian.EndFrame();
            }
        }

        static private void RegisterUndo(string name, params UnityEngine.Object[] objects)
        {
            if (objects != null && objects.Length > 0)
            {
                UnityEditor.Undo.RecordObjects(objects, name);
                foreach (UnityEngine.Object obj in objects)
                {
                    if (obj != null)
                    {
                        EditorUtility.SetDirty(obj);
                    }
                }
            }
        }
    }
}