#region Header
/* ============================================ 
 *	작성자 : KJH
   ============================================ */
#endregion Header

public static class CStringExtends
{
	public static bool EqualsLower(this string x, string y)
	{
		return x.ToLower().Equals(y.ToLower());
	}

	public static string TrimUnderscore(this string value)
	{
		if (value.StartsWith("_"))
			value = value.TrimStart('_');

		return value;
	}
}