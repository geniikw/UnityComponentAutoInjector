#region Header
/* ============================================ 
 *	작성자 : KJH
   ============================================ */
#endregion Header

using System.Collections.Generic;

public static class CCollectionExtends
{
	public static T[] ToArray<T>(this ICollection<T> collection)
	{
		int count = collection.Count;

		T[] array = new T[count];

		int i = 0;

		var iter = collection.GetEnumerator();
		while (iter.MoveNext())
			array[i++] = iter.Current;

		return array;
	}

	public static bool TryGetValueThrow<T_KEY, T_VALUE>(this IDictionary<T_KEY, T_VALUE> map, T_KEY tKey, out T_VALUE tValueOut, string strHeader, string strSub)
	{
		if (map.TryGetValue(tKey, out tValueOut) == false)
			ThrowDebug(tKey, strHeader, strSub);

		return true;
	}

	public static bool ContainKeyThrow<T_KEY, T_VALUE>(this IDictionary<T_KEY, T_VALUE> map, T_KEY tKey, string strHeader, string strSub)
	{
		if (map.ContainsKey(tKey) == false)
			ThrowDebug(tKey, strHeader, strSub);

		return true;
	}

	public static void ThrowDebug<T_KEY>(T_KEY tKey, string strHeader = "Header", string strSub = "Sub")
	{
		throw new System.Exception(string.Format("{0} -> {1} : [{2}][null]...\n\n", strHeader, strSub, tKey));
	}
}