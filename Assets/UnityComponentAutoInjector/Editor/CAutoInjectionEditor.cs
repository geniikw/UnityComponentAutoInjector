#region Header

/* ============================================
 *	작성자 : KJH
   ============================================ */

#endregion Header

#if UNITY_EDITOR

namespace UnityEditor
{
    using System.Collections.Generic;
    using UnityEngine;

    [CustomEditor(typeof(MonoBehaviour), true)/*, CanEditMultipleObjects*/]
    public class CAutoInjectionEditor : Editor
    {
        public static List<SerializedObject> _serializedObjectList = new List<SerializedObject>();

        [MenuItem("CONTEXT/MonoBehaviour/Force Auto Injection")]
        private static void ForceAutoInjection()
        {
            int count = _serializedObjectList.Count;
            if (count == 0)
                CDebug.LogWarning("Multi-object force auto injecting not supported.");

            for (int i = 0; i < count; i++)
                AutoInjection(_serializedObjectList[i], true);
        }

        [InitializeOnLoadMethod]
        private static void OnPostCompile()
        {
            if (EditorApplication.isCompiling || EditorApplication.isTemporaryProject || EditorApplication.isPlayingOrWillChangePlaymode)
            {
                return;
            }

            Debug.Log("InitializeOnLoadMethod");
            var objects = Editor.FindObjectsOfType<MonoBehaviour>();
            if (objects == null)
            {
                return;
            }

            foreach(var obj in objects)
            {
                var so = new SerializedObject(obj);
                if (so == null)
                    return;
                AutoInjection(so);
            }
        }

        public static void AutoInjection(SerializedObject serializedObject, bool isForceInject = false)
        {
            serializedObject.Update();
            CAutoInjector.Inject(serializedObject, isForceInject);
            serializedObject.ApplyModifiedProperties();
        }

        public static void AutoInjectionWithForceList(SerializedObject serializedObject)
        {
            AutoInjection(serializedObject);
            Add(serializedObject);
        }

        public static void Clear()
        {
            _serializedObjectList.Clear();
        }

        public static void Add(SerializedObject serializedObject)
        {
            if (_serializedObjectList.Contains(serializedObject)) return;

            _serializedObjectList.Add(serializedObject);
        }

        private void OnEnable()
        {
            Debug.Log("OnEnable");
            AutoInjectionWithForceList(serializedObject);
        }

        private void OnDisable()
        {
            Debug.Log("OnDisable");
            Clear();
        }
    }
}

#endif