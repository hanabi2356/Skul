# 개요
프로젝트 명 : Skul 모작<br>
개발 기간 : 2026/05/02 ~ <br>
사용된 엔진 : Unity 6.3<br>

# 기능
**Input**<br>
이동 : 방향키<br>
대시 : Z<br>
점프 : C<br>
공격 : X<br>
스킬 : A,S<br>
상호작용 : F<br>
인벤토리 : TAB<br>
교체 : Space<br>



# Architecture<br>
**InputSystem**<br>
- PlayerInput을 GlobalInputManager라는 빈 오브젝트에 넣어 관심사를 분리<br>

**DataManagement**<br>
- Data파일은 csv파일을 파싱하여 관리<br>
- EditorUtility.SetDirty를 사용하여 물리적 디스크에 저장<br>
- AssetDatabase를 사용하여 에셋을 코드를 사용하여 자동 생성<br>

**공용 Singleton**<br>
- DataManager, SoundManager등을 만들 때 Singleton을 적용하여 만들거기 때문데 코드의 중복을 막기 위해 공용 스크립트 제작<br>

**FSM패턴**<br>
- FSM패턴을 상태별 스크립트를 만들어 상태별 스크립트 내부에서 상태를 전환하는 로직을 구성하여 PlayerBase의 코드가 비대해지고 GodClass가 되는것을 방지함<br>
- PlayerBase Awake에서 상태 스크립트들을 미리 캐싱하여 매번 new를 하여 생기는 메모리 부담을 줄인다<br>
- Enum형도 같이 선언하여 Boolean형식 대신 사용하여 불필요한 Boolean형식 선언을 방지<br>

**LabelAttribute**<br>
- 변수 명은 개인적인 견해가 많이 들어가기 때문에 LabelAttribute를 이용하여 한글로 인스펙터에 노출시켜 어떤 거에 대한 변수인지 기획자에게 명확히 인지 시킨다<br>

**맵 정보 저장**<br>
- 모든 맵 파일을 무거운 prefab원본을 저장하기 보다는 prefab을 직렬화 하여 json 파일로 저장하여 용량을 최소화함<br>
- 런타임 중 맵 로드 시에 역직렬화를 하여 tilemap이 있는 prefab을 직접적으로 참조하지 않고 json파일을 참조하여 맵 로드 시 기존 prefab 파일 오염 방지<br>
- 맵 정보를 추출하기 전에 맵의 유효성을 검사하는 코드를 추가하여 맵 파일의 데이터 무결성을 보장한다<br>
- 원본 맵을 만드는 prefab의 레이어를 기능 별로 나누어서 맵에 대한 데이터가 단일 레이어로 묶이는 것을 방지하고 파일을 열였을 때의 가독성을 보장함<br>
- 맵의 정보를 파일로 들고 있기 때문에 개발 파트를 거치지 않고 기획 파트에서 바로 추가 및 수정을 지원한다<br>

**MVP 패턴**
- mvp 패턴을 적용하여 데이터를 받아오는 view와 오브젝트에게 필요한 데이터는 model로 관리함으로 써 관심사를 분리해 model의 데이터 오염을 막는다<br>
- presenter/controller라는 view와 model을 중개 하는 객체를 두어 view와 model이 서로 간섭하는 것을 방지한다<br>
- controller는 순수 C# 클래스로 구성하여 불필요한 Component를 줄였다<br>

**Zenject**
- Zenject를 도입한 이유는 크게 3가지 이다.
  -  1. 싱글톤의 한계 극복이다. 싱글톤은 Static으로 선언하여 어디에서든 접근이 가능하여 오염될 가능성이 있고 코드 또한 스파게티 코드가 될 우려가 있기 때문이다.
  -  2. Find 계열 함수에 대한 비용 문제이다. FindAnyObject 함수가 추가되면서 기존보다는 비용 문제가 개선되었다고 할지라도 작동 원리는 Scene에 배치되어 있는 모든 Object를 검사하는 것이기 때문에 시간 복잡도는 O(N)이 나오지만 Zenject를 활용해 의존성을 주입하면 탐색에 대한 비용이 0에 가깝기 때문이다.
  -  3. 초기화 순서를 보장하기 위해서 이다. Unity에서는 Awake/Start 호출 순서 문제로 NullReferenceException을 방지하기 위함이다. Zenject에는 Initialize함수를 별도로 제공하는데 이 함수는 Unity의 Awake보다 먼저 호출되어 초기화에 대한 순서를 보장할 수 있다.
- 사용 함으로써 얻는 이득
  
**Addressables**
- Addressables 사용 이유
- AssetBundle 대신 사용한 이유
- 사용 시 얻는 이점 

# 수정사항 or 버그<br>
- TraceCamera 이동시 Player에 잔상이 생기는 현상 수정하기<br>
- 점프 후 OneWayPlatform에 걸쳤을 때 겹쳐져서 안내려 오는 현상<br>
  <img width="722" height="405" alt="image" src="https://github.com/user-attachments/assets/bab5a854-db86-44db-a74b-160302eff513" />
- 특정 상황에서 AttackState로 고정되는 버그(원인은 아직 모름)<br>
- GazeVector의 x가 -1일 때 DownArrow를 누르면 GazeVector의 x가 +1로 변경됨<br>
- **런타임 중 혹은 플레이 종료 시 Presenter Object가 파괴되는 버그<br>**

# 해결된 버그
- <img width="965" height="42" alt="image" src="https://github.com/user-attachments/assets/26e28207-3e76-423a-a364-9ba29893379e" />
  - 해결한 방법: OnDestory => OnDisable로 변경
  
# 해야 할 일<br>
**진행중:Enemy 행동 로직 및 FSM<br>**

~~- Player 리팩토링(6월29일 ~ 7월 5일)<br>~~ 7월 12일 완료
- Enemy 행동 로직 및 FSM(7월 6일 ~ 7월 19일)<br>
- 전투 시스템(7월 20일 ~ 7월 21일)<br>
- 오브젝트 만들기(후 순위)<br>
- UI 만들기(7월 22일 ~ 7월 28일)<br>
- 재화 시스템(7월 29일 ~ 7월 30일)<br>
- 아이템 시스템(7월 31일 ~ 8월 1일)<br>
- 스컬 교체 시스템(8월 2일 ~ 8월 3일)<br>
- 스테이지 시스템(스폰 위치, 클리어 여부)(8월 4일 ~ 8월 6일)<br>
- 실제 빌드 시 맵을 랜덤으로 불러 올 수 있는 맵 로더 로직(8월 7일 ~ 8월 9일)<br>
    1. 실제 맵 구성도 (일반 맵 => 일반 맵 => 상점 => 중간 보스 => 일반 맵 => 스테이지 최종 보스)<br>
- QA용 Cheat/Debug Tool(맵 즉시 클리어, 스테이지 강제 이동, 재화 강제 추가...)(8월 10일 ~ 8월 19일)<br>

# Class Diagram
- [https://lucid.app/lucidchart/48678b1b-6ea4-42b7-8a84-4a580e97bc66/edit?viewport_loc=-1773%2C-916%2C2690%2C1357%2C8Fl9sWhwAIUX&invitationId=inv_3435e6d8-a0d4-40ea-a246-153f193e9eb5]
 
# 피드백
xml 적용

# 사용된 외부 툴 및 Unity Package
- zenject
- Addressables
