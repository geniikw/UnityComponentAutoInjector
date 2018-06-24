using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClassExample : MonoBehaviour
{
	[GetComponent] public Transform _class; // GameObject 지원
	[GetComponent] public ClassExample[] _classes;
	[GetComponent] public List<ClassExample> _classList;

	[GetComponentInChildren] public ClassExample _class1;
	[GetComponentInChildren] public ClassExample[] _classes1;
	[GetComponentInChildren(true)] public List<ClassExample> _classList1; // 꺼져있는 오브젝트도 주입됩니다.

	[GetComponentInChildrenOnly] public ClassExample _class2;           // 자식에서만 찾습니다. 꺼져있는 오브젝트도 주입됩니다. GameObject 지원
	[GetComponentInChildrenOnly] public ClassExample[] _classes2;       // 이것도 마찬가지
	[GetComponentInChildrenOnly] public List<ClassExample> _classList2; // 이것도 마찬가지

	[GetComponentInChildrenName("ObjectExample")] public ClassExample _variableName; // ObjectExample 오브젝트가 주입됩니다. GameObject 지원
	[GetComponentInChildrenName] public ClassExample _objectExample;                 // ObjectExample 오브젝트가 주입됩니다.
																					 // 이름이 없으면 변수이름으로 찾습니다.
																					 // 언더바는 자동으로 삭제되고 소문자로 바뀐뒤에 찾습니다.

	[FindGameObject("오브젝트 이름")] public GameObject _gameObject;      // 현재 씬에 존재하는 게임오브젝트를 찾습니다.
	[FindGameObjectWithTag("태그 이름")] public GameObject _gameObject1;    // 현재 씬에서 해당 태그가 설정 되어있는 게임오브젝트를 찾습니다. [FindGameObjectWithTag("태그 이름")] public GameObject[] _gameObjects; // 현재 씬에서 해당 태그가 붙어있는 게임오브젝트들을 모두 찾습니다.
	[FindGameObjectWithTag("태그 이름")] public List<ClassExample> _gameObjectList;

	[FindObjectOfType] public ClassExample _class3;         // 현재 씬에 존재하는 타입을 찾아서 주입시킵니다.
	[FindObjectOfType] public ClassExample[] _classes3;     // 현재 씬에 존재하는 타입들을 찾아서 모두 주입시킵니다.
	[FindObjectOfType] public List<ClassExample> _classList3;
}
