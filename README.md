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
  - 싱글톤의 한계 극복이다. 싱글톤은 Static으로 선언하여 어디에서든 접근이 가능하여 오염될 가능성이 있고 코드 또한 스파게티 코드가 될 우려가 있기 때문이다.
  - Find 계열 함수에 대한 비용 문제이다. FindAnyObject 함수가 추가되면서 기존보다는 비용 문제가 개선되었다고 할지라도 작동 원리는 Scene에 배치되어 있는 모든 Object를 검사하는 것이기 때문에 시간 복잡도는 O(N)이 나오지만 Zenject를 활용해 의존성을 주입하면 탐색에 대한 비용이 0에 가깝기 때문이다.
  - 초기화 순서를 보장하기 위해서 이다. Unity에서는 Awake/Start 호출 순서 문제로 NullReferenceException을 방지하기 위함이다. Zenject에는 Initialize함수를 별도로 제공하는데 이 함수는 Unity의 Awake보다 먼저 호출되어 초기화에 대한 순서를 보장할 수 있다.
- 사용 함으로써 얻는 이점
  - 낮은 결합도와 높은 유연성: 인터페이스 기반 설계로 특정 클래스 구현 변경 시 다른 코드에 미치는 영향 최소화
  - 코드 재사용성 증가: 모듈화된 바인딩 구조로 기능 단위의 재사용 및 교체가 용이
  
**Addressables**
- Addressables 사용 이유
  - 게임에 필요한 모든 에셋을 빌드에 직접 포함시키지 않고 필요한 시점에 동적으로 로드 및 해제하기 위해 사용한다. 만약 사용하지 않으면 인스펙터에 할당을 하여 로드 하여야 하는데 이렇게 되면 사용하지 않는 Prefab과 혼동이 올 수도 있고 일일이 찾아서 인스펙터에 할당을 해야하기 때문에 불편 하기 때문이다.
  - Addressables를 사용하면 Prefab을 포함한 csv, json파일 등등을 외부 서버에 올릴 수 있게 돼서 메모리 및 빌드 용량을 최적화 시킬 수 있다. 그리고 외부 서버에 올려두면 업데이트를 하더라도 갱신된 정보를 Addressables를 이용해 서버에 올려두면 변경된 부분에 대한 용량만 받으면 패치가 되기 때문에 유저들이 매 업데이트마다 대용량의 다운로드를 할 필요가 없어진다.
  
- AssetBundle 대신 Addressables를 사용한 이유
  - 기존의 AssetBundle도 좋은것은 맞으나 관리에 있어서 큰 불편함이 있기 때문이다. AssetBundle은 다른 두 대의 번들이 동일한 데이터를 참조할 때 종속성을 수동으로 처리하지 않으면 파일의 크기가 불필요하게 증가 하지만, Addressables는 에디터가 자동으로 분석하여 에셋 간의 종속성을 관리해준다.
  - 에셋의 실제 물리적 위치나 번들 파일명이 변경되어도 주소(Addressables Name / Key)만 변경되지 않는다면 코드를 수정하지 않아도 되기 때문이다.
  - 메모리 해제를 AssetBundle에 비해 간소화 시켰기 때문이다. Addressables는 번들의 참조 카은팅을 시스템 내부에서 처리하므로 해제로직 없이도 안전하게 메모리를 관리할 수 있다. 
- 사용 시 얻는 이점
  - 비동기 로딩의 코드를 표준화 하여 코드를 일관되고 깔끔하게 관리할 수 있게된다.
  - 로컬 에셋 로딩과 원격 에셋 로딩 간의 전환이 설정 변경만으로 가능하여 라이브 서비스 대응에 용이하다.
  - 에셋의 생명주기와 참조 카운트가 시각적으로 관리되어 메모리 누수 위험이 감소한다

**ObjectPool**
- ObjectPool을 사용한 이유는 빈번한 객체 생성/파괴를 짧은 주기 동안 대량으로 생성되고 파괴되면서 메모리 할당 비용과 GC 부하가 지속적으로 증가하기 때문. 그리고 작은 동적 할당(new)으로 인해 GC 오버헤드로 인한 프레임 드랍 및 메모리 파편화 현상이 발생하기 때문.
- ObjectPool을 사용함 으로써 얻는 이점은 크게 2가지로 볼 수 있다.
  - GC 오버헤드 최적화: 객체를 파괴하는 대신 풀에 반환하여 재사용 함으로써 GC 호출 빈도를 크게 감소
  - 생성 및 메모리 할당 비용 절감: 초기화 시점에 필요한 만큼의 객체를 미리 생성해 두고 재활용하여 런타임 시점의 메모리 할당 오버헤드를 제거

# 수정사항 or 버그<br>
- TraceCamera 이동시 Player에 잔상이 생기는 현상 수정하기<br>
- 점프 후 OneWayPlatform에 걸쳤을 때 겹쳐져서 안내려 오는 현상<br>
  <img width="722" height="405" alt="image" src="https://github.com/user-attachments/assets/bab5a854-db86-44db-a74b-160302eff513" />
- 특정 상황에서 AttackState로 고정되는 버그(원인은 아직 모름)<br>



# 해결된 버그
- <img width="965" height="42" alt="image" src="https://github.com/user-attachments/assets/26e28207-3e76-423a-a364-9ba29893379e" />
  - 해결한 방법: OnDestory => OnDisable로 변경
- GazeVector의 x가 -1일 때 DownArrow를 누르면 GazeVector의 x가 +1로 변경됨<br>
  - 해결한 방법: GazeVector를 MoveInput의 x값이 0이상일 때만 수정하도록 변경
- **런타임 중 혹은 플레이 종료 시 Presenter Object가 파괴되는 버그<br>**
  
# 해야 할 일<br>
**진행중:UI 만들기(7월 22일 ~ 7월 28일)<br>**

~~- Player 리팩토링(6월29일 ~ 7월 5일)~~ 7월 12일 완료<br>
~~- Enemy 행동 로직 및 FSM(7월 6일 ~ 7월 19일)~~ 8월 17일 완료<br>
~~- 전투 시스템(7월 20일 ~ 7월 21일)~~ 8월 23일 완료<br>
- 오브젝트 만들기(후 순위)<br>
- UI 만들기(7월 22일 ~ 7월 28일)<br>
  - Pause UI버튼에 기능 바인딩 하기<br>
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
NormalEnemy load시 맵에 배치를 하되 플레이어를 기준으로 영역 밖에 있는 NormalEnemy의 Active를 false로 설정하여 연산부하를 줄인다
# 사용된 외부 툴 및 Unity Package
- zenject
- Addressables
