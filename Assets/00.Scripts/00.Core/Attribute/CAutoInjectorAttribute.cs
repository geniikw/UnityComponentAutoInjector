﻿#region Header
/* ============================================ 
 *	작성자 : KJH
   ============================================ */
#endregion Header

using System;

public interface IAutoInjectable { }

namespace AutoInjector
{
	public class InActiveBase : Attribute, IAutoInjectable
	{
		public readonly bool includeInActive;

		public InActiveBase(bool includeInActive)
		{
			this.includeInActive = includeInActive;
		}
	}

	public class NameBase : Attribute, IAutoInjectable
	{
		public readonly string name;

		public NameBase(string componentName)
		{
			this.name = componentName;
		}

		public string NameOfTrimUnderscore(string variableName)
		{
			string nameOf = name;
			if (string.IsNullOrEmpty(nameOf))
				nameOf = variableName.TrimUnderscore();

			return nameOf;
		} 
	}
}


[AttributeUsage(AttributeTargets.Field)] public class GetComponentAttribute : Attribute, IAutoInjectable { }

[AttributeUsage(AttributeTargets.Field)] public class GetComponentInParentAttribute : AutoInjector.InActiveBase {
	public GetComponentInParentAttribute(bool includeInActive = false) : base(includeInActive) { }
}
[AttributeUsage(AttributeTargets.Field)] public class GetComponentInChildrenAttribute : AutoInjector.InActiveBase {
	public GetComponentInChildrenAttribute(bool includeInActive = false) : base(includeInActive) { }
}

[AttributeUsage(AttributeTargets.Field)] public class GetComponentInChildrenNameAttribute : AutoInjector.NameBase {
	public GetComponentInChildrenNameAttribute(string componentName = null) : base(componentName) { }
}

[AttributeUsage(AttributeTargets.Field)] public class GetComponentInChildrenOnlyAttribute : Attribute, IAutoInjectable { }


[AttributeUsage(AttributeTargets.Field)] public class FindGameObjectAttribute : AutoInjector.NameBase {
	public FindGameObjectAttribute(string gameObjectName) : base(gameObjectName) { }
}
[AttributeUsage(AttributeTargets.Field)] public class FindGameObjectWithTagAttribute : AutoInjector.NameBase {
	public FindGameObjectWithTagAttribute(string tagName) : base(tagName) { }
}
[AttributeUsage(AttributeTargets.Field)] public class FindObjectOfTypeAttribute : Attribute, IAutoInjectable { }