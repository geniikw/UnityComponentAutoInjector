#region Header
/* ============================================ 
 *	작성자 : KJH
 *	기  능 : 자동으로 스크립트 변수를 주입합니다.
   ============================================ */
#endregion Header

#if UNITY_EDITOR

using System;
using UnityEngine;
using UnityEditor;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;

public static class CAutoInjector
{
	private static readonly BindingFlags _bindingFlags = (BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

	public static void Inject(SerializedObject serializedObject, object obj, bool isForceInject = false)
	{
		FieldInfo[] fields = obj.GetType().GetFieldInfoWithBaseClass(_bindingFlags);

		bool isInjected = false;

		int len = fields.Length;
		for (int i = 0; i < len; i++)
		{
			FieldInfo fieldInfo = fields[i];

			Type fieldType = fieldInfo.FieldType;
			Type elementType = fieldType.GetElementType();

			object[] attributes = fieldInfo.GetCustomAttributes(true);

			int lenAttributes = attributes.Length;
			for (int j = 0; j < lenAttributes; j++)
			{
				object currentAttribute = attributes[j];
				if ((currentAttribute is IAutoInjectable) == false) continue;

				string variableName = fieldInfo.Name;
				SerializedProperty property = serializedObject.FindProperty(variableName);

				object componentOut = null;

				if (fieldType.IsArray)
				{
					if (isForceInject == false && property.arraySize > 0) continue;

					if (IsGetComponentsAttribute(obj, currentAttribute, fieldInfo, elementType, out componentOut))
					{
						Array array = (componentOut as Array);

						int length = array.Length;
						for (int k = 0; k < length; k++)
						{
							property.InsertArrayElementAtIndex(k);
							SerializedProperty prop = property.GetArrayElementAtIndex(k);

							prop.objectReferenceValue = (array.GetValue(k) as UnityEngine.Object);
						}

						if (length <= 0)
							componentOut = null;
					}
				}
				else if (fieldType.IsGenericType)
				{
					if (fieldType.GetGenericTypeDefinition() == typeof(List<>))
					{
						if (isForceInject == false && property.arraySize > 0) continue;

						if (IsGetComponentsAttribute(obj, currentAttribute, fieldInfo, fieldType.GetGenericArguments()[0], out componentOut))
						{
							ICollection collection = (componentOut as ICollection);

							property.arraySize = collection.Count;

							int length = 0;
							var iter = collection.GetEnumerator();
							while (iter.MoveNext())
							{
								object current = iter.Current;

								SerializedProperty prop = property.GetArrayElementAtIndex(length);
								prop.objectReferenceValue = (current as UnityEngine.Object);

								length++;
							}

							if (length <= 0)
								componentOut = null;
						}
					}
				}
				else
				{
					if (isForceInject == false && property.objectReferenceValue != null) continue;

					if (IsGetComponentAttribute(obj, currentAttribute, fieldInfo, fieldType, out componentOut))
					{
						property.objectReferenceValue = (componentOut as UnityEngine.Object);
					}

				}

				if (isInjected == false && componentOut != null)
					isInjected = true;
			}
		}

		if (isInjected)
		{
			UnityEngine.Object unityObject = (obj as UnityEngine.Object);
			Debug.Log("<b><i><color=black>" + (obj as UnityEngine.Object).name + "</color><color=green> Auto injection complete.</color></i></b>", unityObject);
		}
	}

	private static bool IsGetComponentsAttribute(object obj, object attribute, FieldInfo fieldInfo, Type elementType, out object componentsOut)
	{
		componentsOut = null;

		if (attribute is GetComponentAttribute)
			componentsOut = typeof(MonoBehaviour).InvokeGeneric(obj, "GetComponents", new Type[0], new[] { elementType });

		else if (attribute is GetComponentInChildrenAttribute)
			componentsOut = typeof(MonoBehaviour).InvokeGeneric(obj, "GetComponentsInChildren", new[] { typeof(bool) }, new[] { elementType },
				(attribute as GetComponentInChildrenAttribute).includeInActive);

		else if (attribute is GetComponentInChildrenOnlyAttribute)
			componentsOut = typeof(CGetComponentExtends).InvokeGeneric(obj, "GetComponentsInChildrenOnly", new[] { typeof(Component) }, new[] { elementType }, obj);

		else if (attribute is GetComponentInParentAttribute)
			componentsOut = typeof(MonoBehaviour).InvokeGeneric(obj, "GetComponentsInParent", new[] { typeof(bool) }, new[] { elementType },
				(attribute as GetComponentInParentAttribute).includeInActive);

		else if (attribute is FindGameObjectWithTagAttribute)
			componentsOut = typeof(GameObject).Invoke(obj, "FindGameObjectsWithTag", new[] { typeof(string) },
				(attribute as FindGameObjectWithTagAttribute).NameOfTrimUnderscore(fieldInfo.Name));

		else if (attribute is FindObjectOfTypeAttribute)
			componentsOut = typeof(UnityEngine.Object).Invoke(obj, "FindObjectsOfType", new[] { typeof(Type) }, elementType);

		return (componentsOut != null);
	}

	private static bool IsGetComponentAttribute(object obj, object attribute, FieldInfo fieldInfo, Type fieldType, out object componentOut)
	{
		componentOut = null;

		if (attribute is GetComponentAttribute)
			componentOut = typeof(CGetComponentExtends).Invoke(obj, "GetComponent", new[] { typeof(Component), typeof(Type) }, obj, fieldType);

		else if (attribute is GetComponentInParentAttribute)
			componentOut = typeof(MonoBehaviour).Invoke(obj, "GetComponentInParent", new[] { typeof(Type) }, fieldType);

		else if (attribute is GetComponentInChildrenAttribute)
			componentOut = typeof(MonoBehaviour).Invoke(obj, "GetComponentInChildren", new[] { typeof(Type), typeof(bool) }, fieldType, true);

		else if (attribute is GetComponentInChildrenOnlyAttribute)
			componentOut = typeof(CGetComponentExtends).Invoke(obj, "GetComponentInChildrenOnly", new[] { typeof(Component), typeof(Type) }, obj, fieldType);

		else if (attribute is GetComponentInChildrenNameAttribute)
			componentOut = typeof(CGetComponentExtends).Invoke(obj, "GetComponentInChildrenName", new[] { typeof(Component), typeof(Type), typeof(string) }, obj, fieldType,
				(attribute as GetComponentInChildrenNameAttribute).NameOfTrimUnderscore(fieldInfo.Name));


		else if (attribute is FindGameObjectAttribute)
			componentOut = typeof(GameObject).Invoke(obj, "Find", new[] { typeof(string) },
				(attribute as FindGameObjectAttribute).NameOfTrimUnderscore(fieldInfo.Name));

		else if (attribute is FindGameObjectWithTagAttribute)
			componentOut = typeof(GameObject).Invoke(obj, "FindGameObjectWithTag", new[] { typeof(string) },
				(attribute as FindGameObjectWithTagAttribute).NameOfTrimUnderscore(fieldInfo.Name));

		else if (attribute is FindObjectOfTypeAttribute)
			componentOut = typeof(UnityEngine.Object).Invoke(obj, "FindObjectOfType", new[] { typeof(Type) }, fieldType);

		return (componentOut != null);
	}
}

#endif