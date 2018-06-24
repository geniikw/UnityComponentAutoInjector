﻿#region Header
/* ============================================ 
 *	작성자 : KJH
   ============================================ */
#endregion Header

using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class CGetComponentExtends
{
	public static UnityEngine.Object GetComponent(this Component component, Type type)
	{
		if (IsGameObjectType(type))
			return component.GetGameObject();

		return component.GetComponent(type);
	}

	public static UnityEngine.Object GetComponentInChildrenOnly(this Component component, Type type)
	{
		if (IsGameObjectType(type))
			return component.GetGameObjectInChildrenName(null);

		Component[] components = component.GetComponentsInChildren(type, true);

		int len = components.Length;
		for (int i = 0; i < len; i++)
		{
			Component compo = components[i];
			if (compo.transform == component.transform) continue;

			return compo;
		}

		Debug.LogError(type.Name + " 컴퍼넌트를 찾을 수 없습니다.", component);

		return null;
	}

	public static UnityEngine.Object GetComponentInChildrenName(this Component component, Type type, string objectName = null)
	{
		if (IsGameObjectType(type))
			return component.GetGameObjectInChildrenName(objectName);

		Component[] components = component.GetComponentsInChildren(type, true);

		int len = components.Length;
		for (int i = 0; i < len; i++)
		{
			Component compo = components[i];

			string currentName = compo.name;

			if (string.IsNullOrEmpty(objectName))
				return compo;

			else if (currentName.EqualsLower(objectName))
				return compo;
		}

		Debug.LogError(objectName + " 컴퍼넌트를 찾을 수 없습니다.", component);

		return null;
	}

	public static GameObject GetGameObject(this Component component)
	{
		Transform transform = component.GetComponent<Transform>();

		return transform.gameObject;
	} 

	public static GameObject GetGameObjectInChildrenName(this Component component, string objectName = null)
	{
		Transform[] transforms = component.GetComponentsInChildrenOnly<Transform>();

		int len = transforms.Length;
		for (int i = 0; i < len; i++)
		{
			Transform transform = transforms[i];
			GameObject gameObject = transform.gameObject;

			string currentName = transform.name;

			if (string.IsNullOrEmpty(objectName))
				return gameObject;

			else if (currentName.EqualsLower(objectName))
				return gameObject;
		}

		Debug.LogError(objectName + " 게임 오브젝트를 찾을 수 없습니다.", component);

		return null;
	}

	public static T[] GetComponentsInChildrenOnly<T>(this Component component)
		where T : Component
	{
		T[] components = component.GetComponentsInChildren<T>(true);

		int len = components.Length;

		List<T> newComponentList = null;

		for (int i = 0; i < len; i++)
		{
			T currentComponent = components[i];
			if (currentComponent.transform == component.transform) continue;

			if (newComponentList == null)
				newComponentList = new List<T>();

			newComponentList.Add(currentComponent);
		}

		if (newComponentList == null)
		{
			Debug.LogError(typeof(T).Name + " 컴퍼넌트 배열을 찾을 수 없습니다.", component);
			return null;
		}


		return newComponentList.ToArray();
	}

	private static bool IsGameObjectType(Type type)
	{
		return (type == typeof(GameObject));
	}
}