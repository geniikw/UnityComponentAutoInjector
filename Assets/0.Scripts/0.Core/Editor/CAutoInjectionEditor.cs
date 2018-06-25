﻿#region Header
/* ============================================ 
 *	작성자 : KJH
   ============================================ */
#endregion Header

#if UNITY_EDITOR

namespace UnityEditor
{
	using System.Collections.Generic;
	using UnityEngine;

	[CustomEditor(typeof(MonoBehaviour), true), CanEditMultipleObjects]
	public class CAutoInjectionEditor : Editor
	{
		public static List<SerializedObject> _serializedObjectList = new List<SerializedObject>();

		[MenuItem("CONTEXT/MonoBehaviour/Force Auto Injection")]
		private static void ForceAutoInjection()
		{
			int count = _serializedObjectList.Count;
			if (count == 0)
				Debug.LogWarning("직렬화된 오브젝트가 없습니다.");

			for (int i = 0; i < count; i++)
				AutoInjection(_serializedObjectList[i], true);
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
			AutoInjectionWithForceList(serializedObject);
		}

		private void OnDisable()
		{
			Clear();
		}
	}
}

#endif