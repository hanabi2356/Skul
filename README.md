# 개요
프로젝트 명 : Skul 모작
개발 기간 : 2026/05/02 ~ 
사용된 엔진 : Unity 6.3

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
Data파일은 csv파일을 파싱하여 관리
