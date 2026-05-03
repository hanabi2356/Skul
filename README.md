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

# 할 일
PlayerPhysicsHandler 만들기
SkulStatData 주입 로직
