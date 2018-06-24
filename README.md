# UnityComponentAutoInjector

사용 방법 : 변수, 배열, 리스트의 타입에 맞춰서 자동으로 주입됩니다.
만약 상속 되어있는 클래스도 이 속성이 존재하면 모두 주입됩니다.
  
만약 변수가 private 일때 속성 :
[SerializeField, HideInInspector(변수를 가리고 싶은 경우), GetComponent]

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
	[GetComponentInChildrenName] public ClassExample _objectExample; // ObjectExample 오브젝트가 주입됩니다.
									 // 이름이 없으면 변수이름으로 찾습니다.
									 // 언더바는 자동으로 삭제되고 소문자로 바뀐뒤에 찾습니다.

	[FindGameObject("오브젝트 이름")] public GameObject _gameObject;        // 현재 씬에 존재하는 게임오브젝트를 찾습니다.
	[FindGameObjectWithTag("태그 이름")] public GameObject _gameObject1;    // 현재 씬에서 해당 태그가 설정 되어있는 게임오브젝트를 찾습니
	[FindGameObjectWithTag("태그 이름")] public GameObject[] _gameObjects;  // 현재 씬에서 해당 태그가 붙어있는 게임오브젝트들을 모두 찾습니다.
	[FindGameObjectWithTag("태그 이름")] public List<ClassExample> _gameObjectList;

	[FindObjectOfType] public ClassExample _class3;         // 현재 씬에 존재하는 타입을 찾아서 주입시킵니다.
	[FindObjectOfType] public ClassExample[] _classes3;     // 현재 씬에 존재하는 타입들을 찾아서 모두 주입시킵니다.
	[FindObjectOfType] public List<ClassExample> _classList3;
  
  
  
주의 사항 :
  1. private 변수는 [SerializeField] 직렬화 속성을 무조건 포함해야 합니다.
  2. 자동으로 주입시킬 스크립트를 인스펙터창 화면으로 반드시 확인해야 합니다.
  3. 변수들을 재 주입 시키려면 다른 오브젝트 클릭 후 주입시키려는 오브젝트를 다시 누르세요.
  4. 변수를 재 주입 시키려면 인스펙터 화면에서 변수의 값을 눌러서 Delete 키를 누른 후 3번을 따라하세요.
  5. 배열, 리스트 주입을 초기화 시키려면 인스펙터 창에 배열 갯수를 0개로 설정 후 3번을 따라하세요.
  6. 에디터 상에서만 동작되며 빌드시에는 아무런 문제가 없습니다. 안심하셔도 됩니다.
  
기타 피드백은 및 개선사항은 Issues 에 작성해주세요.
