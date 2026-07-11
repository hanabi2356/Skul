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

# 수정사항 or 버그<br>
- TraceCamera 이동시 Player에 잔상이 생기는 현상 수정하기<br>
- 점프 후 OneWayPlatform에 걸쳤을 때 겹쳐져서 안내려 오는 현상<br>
  <img width="722" height="405" alt="image" src="https://github.com/user-attachments/assets/bab5a854-db86-44db-a74b-160302eff513" />

- 특정 상황에서 AttackState로 고정되는 버그(원인은 아직 모름)<br>


# 해결된 버그
- <img width="965" height="42" alt="image" src="https://github.com/user-attachments/assets/26e28207-3e76-423a-a364-9ba29893379e" /><br>
  - 해결한 방법: OnDestory => OnDisable로 변경
  
# 해야 할 일<br>
**진행중:Player 리팩토링<br>**

- Player 리팩토링(6월29일 ~ 7월 5일)<br>
- Enemy 행동 로직 및 FSM(7월 6일 ~ 7월 19일)<br>
- 전투 시스템(7월 20일 ~ 7월 21일)<br>
- 오브젝트 만들기(후순위)<br>
- UI 만들기(7월 22일 ~ 7월 28일)<br>
- 재화 시스템(7월 29일 ~ 7월 30일)<br>
- 아이템 시스템(7월 31일 ~ 8월 1일)<br>
- 스컬 교체 시스템(8월 2일 ~ 8월 3일)<br>
- 스테이지 시스템(스폰 위치, 클리어 여부)(8월 4일 ~ 8월 6일)<br>
- 실제 빌드 시 맵을 랜덤으로 불러 올 수 있는 맵 로더 로직(8월 7일 ~ 8월 9일)<br>
    1. 실제 맵 구성도 (일반 맵 => 일반 맵 => 상점 => 중간 보스 => 일반 맵 => 스테이지 최종 보스)<br>
- QA용 Cheat/Debug Tool(맵 즉시 클리어, 스테이지 강제 이동, 재화 강제 추가...)(8월 10일 ~ 8월 19일)<br>
 
# 피드백
xml 적용

# 사용된 외부 툴
- zenject
