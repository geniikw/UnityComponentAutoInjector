# UnityComponentAutoInjector

사용 방법 : 변수, 배열, 리스트의 타입에 맞춰서 자동으로 주입됩니다.
만약 상속 되어있는 클래스도 이 속성이 존재하면 모두 주입됩니다.

주입되는 방식은 에디터상의 오브젝트를 누르면 인스펙터 창이 활성화되면서 주입됩니다.
에디터 상에서만 적용되는거라 플레이 버튼 누를시에 아무 지장이 없습니다.

```csharp
///만약 변수가 private 일때 속성 :
[SerializeField, HideInInspector(변수를 가리고 싶은 경우), GetComponent]

[GetComponent] public Transform _class; // GameObject 지원
[GetComponent] public ClassExample[] _classes;
[GetComponent] public List<ClassExample> _classList;

[GetComponentInChildren] public ClassExample _class1;
[GetComponentInChildren] public ClassExample[] _classes1;
[GetComponentInChildren(true)] public List<ClassExample> _classList1; // 꺼져있는 오브젝트도 주입됩니다.

[GetComponentInParent] public ClassExample _class4;
[GetComponentInParent] public ClassExample[] _classes4;
[GetComponentInParent(true)] public List<ClassExample> _classList4; // 꺼져있는 오브젝트도 주입됩니다.

[GetComponentInChildrenOnly] public ClassExample _class2;           // 자식과 자식 계층구조 모두 찾습니다. 꺼져있는 오브젝트도 주입됩니다. GameObject 지원
[GetComponentInChildrenOnly] public ClassExample[] _classes2;       // 이것도 마찬가지
[GetComponentInChildrenOnly] public List<ClassExample> _classList2; // 이것도 마찬가지
[GetComponentInChildrenOnly(false)] public List<ClassExample> _classList3; //false 로 설정하면 계층구조를 제외한 자식만 찾습니다.

[GetComponentInChildrenName("ObjectExample")] public ClassExample _objectExample; // ObjectExample 오브젝트가 주입됩니다. GameObject 지원
[GetComponentInChildrenName] public ClassExample _objectExample;  // ObjectExample 오브젝트가 주입됩니다.
								  // 이름이 없으면 변수이름으로 찾습니다.
								  // 언더바는 자동으로 삭제되고 소문자로 바뀐뒤에 찾습니다.

[FindGameObject("오브젝트 이름")] public GameObject _gameObject;         // 현재 씬에 존재하는 게임오브젝트를 찾습니다.
[FindGameObjectWithTag("태그 이름")] public GameObject _gameObjectTag;     // 현재 씬에서 해당 태그가 설정 되어있는 게임오브젝트를 찾습니
[FindGameObjectWithTag("태그 이름")] public GameObject[] _gameObjectsTag;   // 현재 씬에서 해당 태그가 붙어있는 게임오브젝트들을 모두 찾습니다.
[FindGameObjectWithTag("태그 이름")] public List<GameObject> _gameObjectListTag;

[FindObjectOfType] public ClassExample _classType;         // 현재 씬에 존재하는 타입을 찾아서 주입시킵니다.
[FindObjectOfType] public ClassExample[] _classesType;     // 현재 씬에 존재하는 타입들을 찾아서 모두 주입시킵니다.
[FindObjectOfType] public List<ClassExample> _classListType;
```

  
주의 사항 :
  1. private 변수는 [SerializeField] 직렬화 속성을 무조건 포함해야 합니다.
  2. 자동으로 주입시킬 스크립트를 인스펙터창 화면으로 반드시 확인해야 합니다.
  3. 해당 컴퍼넌트의 변수들을 재 주입 시키려면 톱니바퀴를 누른 후 [Force Auto Injection] 을 누릅니다.
  4. 에디터 상에서만 동작되며 빌드시에는 아무런 문제가 없습니다. 안심하셔도 됩니다.
  5. 이 에셋을 사용전에 이미 Prefab 이 되어있다면 다시 에디터로 옮겨서 재 주입을 시켜야 됩니다. (에러가 날 경우에만)
  6. 동적 오브젝트 생성(new GameObject(name)) 에서의 자동주입은 당연히 미지원 입니다. 대신 유니티 기본 내장 되어있는 GetComponent 를 사용하세요.
  7. 다른 커스텀 에디터를 사용하는 경우에는 해당 에디터 코드의
  OnEnable() 에는 CAutoInjectionEditor.AutoInjectionWithForceList(serializedObject); OnDisable() 에는 CAutoInjectionEditor.Clear();
  를 호출하면 됩니다. 만약 에디터 코드를 상속받게 될 경우에도 동일하게 적용시켜주면 됩니다.
  
  
  
기타 피드백은 및 개선사항은 Issues 에 작성해주세요.


MIT License

Copyright (c) 2018 KJH

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
