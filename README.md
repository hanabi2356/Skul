# 개요
프로젝트 명 : Skul 모작<br>
개발 기간 : 2026/05/02 ~ <br>
사용된 엔진 : Unity 6.3<br>

# 기능
***Input***<br>
이동 : 방향키<br>
대시 : Z<br>
점프 : C<br>
공격 : X<br>
스킬 : A,S<br>
상호작용 : F<br>
인벤토리 : TAB<br>
교체 : Space<br>
***LabelAttribute***<br>
변수 명은 개인적인 견해가 많이 들어가기 때문에 LabelAttribute를 이용하여 한글로 인스펙터에 노출시켜 어떤 거에 대한 변수인지 기획자에게 명확히 인지 시킨다<br>


# Architecture<br>
***InputSystem***<br>
PlayerInput을 GlobalInputManager라는 빈 오브젝트에 넣어 관심사를 분리<br>
***DataManagement***<br>
Data파일은 csv파일을 파싱하여 관리<br>
EditorUtility.SetDirty를 사용하여 물리적 디스크에 저장<br>
AssetDatabase를 사용하여 에셋을 코드를 사용하여 자동 생성<br>
***공용 Singleton***<br>
DataManager, SoundManager등을 만들 때 Singleton을 적용하여 만들거기 때문데 코드의 중복을 막기 위해 공용 스크립트 제작<br>
***FSM패턴***<br>
FSM패턴을 상태별 스크립트를 만들어 상태별 스크립트 내부에서 상태를 전환하는 로직을 구성하여 PlayerBase의 코드가 비대해지고 GodClass가 되는것을 방지함<br>
PlayerBase Awake에서 상태 스크립트들을 미리 캐싱하여 매번 new를 하여 생기는 메모리 부담을 줄인다<br>
Enum형도 같이 선언하여 Boolean형식 대신 사용하여 불필요한 Boolean형식 선언을 방지<br>

# 수정사항 or 버그<br>
TraceCamera 이동시 Player에 잔상이 생기는 현상 수정하기<br>
방향키를 안누르고 대시 실행 시 대시 애니메이션이 안나오는 버그<br>
점프 후 OneWayPlatform에 걸쳤을 때 겹쳐져서 안내려 오는 현상<br>
  
# 해야 할 일<br>
맵 제작 및 로컬에 저장하기 위한 Export 툴<br>
  맵 데이터 유효성 검사 툴(스폰 위치 지정, 레이어 지정, 클리어 후 들어갈 문의 여부...)<br>
실제 빌드 시 맵을 랜덤으로 불러 올 수 있는 맵 로더 로직<br>
  실제 맵 구성도 [일반 맵 => 일반 맵 => 상점 => 중간 보스 => 일반 맵 => 스테이지 최종 보스]<br>
전투 시스템<br>
스테이지 시스템(스폰 위치, 클리어 여부)<br>

QA용 Cheat/Debug Tool(맵 즉시 클리어, 스테이지 강제 이동, 재화 강제 추가...)<br>
Enemy용 csv 및 importer 툴<br>
Enemy 행동 로직 및 FSM<br>
오브젝트 만들기<br>
UI 만들기<br>
재화 시스템<br>
아이템 시스템<br>
스컬 교체 시스템<br>


