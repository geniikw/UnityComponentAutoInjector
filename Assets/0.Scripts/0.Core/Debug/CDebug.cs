#region Header
/* ============================================ 
 *	작성자 : KJH
   ============================================ */
#endregion Header

using UnityEngine;

public static class CDebug
{
	public static void Log(Object obj, params object[] objs)
	{
		Debug.Log(CStringExtends.GetGCSafeString(objs), obj);
	}

	public static void LogWarning(Object obj, params object[] objs)
	{
		Debug.LogWarning(CStringExtends.GetGCSafeString(objs), obj);
	}

	public static void LogError(Object obj, params object[] objs)
	{
		Debug.LogError(CStringExtends.GetGCSafeString(objs), obj);
	}

	public static void Log(params object[] objs)
	{
		Debug.Log(CStringExtends.GetGCSafeString(objs));
	}

	public static void LogWarning(params object[] objs)
	{
		Debug.LogWarning(CStringExtends.GetGCSafeString(objs));
	}

	public static void LogError(params object[] objs)
	{
		Debug.LogError(CStringExtends.GetGCSafeString(objs));
	}
}