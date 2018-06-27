#region Header
/* ============================================ 
 *	작성자 : KJH
   ============================================ */
#endregion Header

using System.Text;

public static class CStringExtends
{
	private static readonly StringBuilder _builder = new StringBuilder();

	public static string GetGCSafeString(params object[] appends)
	{
		_builder.Length = 0;

		int len = appends.Length;
		for (int i = 0; i < len; i++)
		{
			object current = appends[i];
			_builder.Append(current);
		}

		return _builder.ToString();
	}

	public static string ToMMSS(this int sec)
	{
		return string.Format("{0:D2}:{1:D2}", (int)((sec / 60f) % 60f), (int)(sec % 60f));
	}

	public static string ToHHMMSS(this int sec)
	{
		return string.Format("{0:D2}:{1:D2}:{2:D2}", sec / 3600f, (sec / 60f) % 60f, sec % 60f);
	}

	public static bool EqualsLower(this string x, string y)
	{
		return x.ToLower().Equals(y.ToLower());
	}

	public static string TrimMemberVarName(this string value)
	{
		if (value.StartsWith("m_"))
			value = value.TrimStart('m', '_');

		else if (value.StartsWith("_"))
			value = value.TrimStart('_');

		return value;
	}
}