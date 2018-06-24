#region Header
/* ============================================ 
 *	작성자 : KJH
   ============================================ */
#endregion Header

#if UNITY_EDITOR

using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MonoBehaviour), true), CanEditMultipleObjects]
public class CAutoInjectionEditor : Editor
{
	private void OnEnable()
	{
		if (EditorApplication.isPlayingOrWillChangePlaymode) return;

		SerializedObject obj = serializedObject;

		Object target = obj.targetObject;
		if (target == null) return;

		obj.Update();
			CAutoInjector.Inject(obj, target);
		obj.ApplyModifiedProperties();
	}
}

#endif