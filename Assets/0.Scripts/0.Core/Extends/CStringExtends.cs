#region Header
/* ============================================ 
 *	작성자 : KJH
   ============================================ */
#endregion Header

using System.Text;

public static class CStringExtends
{
	private static StringBuilder _builder = new StringBuilder();

	public static StringBuilder GetBuilder()
	{
		_builder.Length = 0;

		return _builder;
	}

	public static string ToMMSS(this int iSec)
	{
		return string.Format("{0:D2}:{1:D2}", (int)((iSec / 60f) % 60f), (int)(iSec % 60f));
	}

	public static string ToHHMMSS(this int iSec)
	{
		return string.Format("{0:D2}:{1:D2}:{2:D2}", iSec / 3600f, (iSec / 60f) % 60f, iSec % 60f);
	}

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