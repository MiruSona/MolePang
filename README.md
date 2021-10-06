# MolePang

## 프로젝트 설명
* 대학원에서 진행한 프로젝트 "치매 노인 예방을 위한 VR 미니게임" 중 하나
* VR장비로 플레이하는 두더지 잡기 게임

## 영상(이미지 클릭 시 유투브로 이동)
[![MolePang VR](https://img.youtube.com/vi/KFwt_NotHz0/0.jpg)](https://youtu.be/KFwt_NotHz0 "MolePang VR")

## 개발 환경
- Unity 2017.3.1f1
- SteamVR Plugin
- HTC Vive

## 프로젝트 팀원 및 역할
* 그래픽 1인, 프로그래머 1인 개발
* 나의 역할 : 프로그래머

# 개발 내역

## 20180406

### v1.0 
* Mole
	+ 위 / 아래 움직임
	+ 피격 판정
	+ 일정 시간 뒤 돌아가기
	+ 되살리기(Init)

* 테스트용 플레이어

### v1.1
* Mole
	+ 이펙트 추가
	+ 공격 당하는 중 표정 변경

* MoleManager
	+ R누르면 게임 시작
	+ 5 스테이지 마다 두더지 추가

## 20180409

### v1.2
* Bomb
	+ Mole 코드 복붙
	+ 판정이 좀더 위에 있게 설정

* MoleManager
	+ NextStage + EnemySummon으로 변경
	+ 폭탄 / 두더지 / 모자쓴 두더지가 랜덤으로 소환되게 변경
	+ 5 스테이지 부터 폭탄 등장
	+ 15스테이지 부터 모자쓴 두더지 등장
	+ 두더지가 최소 2마리 이상 나오도록 설정
	+ 랜덤한 구멍에서 두더지가 나오도록 설정

* MoleHat
	+ Mole 코드 복붙만 해둠

## 20180410

### v1.3
* MoleHat -> MoleHelmat
	+ Hp = 2
	+ 첫번째 타격 시 모자 날아가는 애니메이션 + 맞는 이펙트
	+ 대기 시간 다르게 설정

* Bomb
	+ 이펙트 변경

* EnemyBase
	+ Bomb 코드를 베이스로 적군 베이스 코드 작성
	+ 사운드 / 이펙트 추가
	+ 대기시간 감소 함수 추가(DecreaseWaitTimer)

* SFXManager
	+ SFX 관리하는 스크립트
	+ SFXEffect Prefab을 생성하는 식으로 효과음 발생
	+ 볼륨 조절도 가능
	+ 일부 사운드에 쿨타임 적용

* SFXAutoDestroy
	+ 사운드가 멈추면 자동으로 파괴되는 코드

* MoleManager -> SFXManager 로 이름 변경

## 20180411

### v1.4
* SteamPlugin 에셋 넣음

* PostProcess 에러 해결

* 색감 조절 -> SteamPlugin 임포트 하면서 색 재조정
	+ Mole Body Material 조절
	+ Helmat Material 조절
	+ Face Materail 조절
	+ Postprocess 조절
	+ Light 조절

* 일부 파티클 이펙트가 제거되지 않고 쌓이는 문제 해결

## 20180412

### v1.5
* DataManager
	+ Score 처리
	+ HP 처리

* SystemManager
	+ 게임 상태 처리(GameState)

* EnemyBase
	+ 적의 HP, Score 처리

* HPUI
	+ HP 처리 UI

* ScoreUI
	+ Score 각 숫자 다 받음
	+ 각 숫자 UI로 표시

* 에러
	+ Stage 재시작 시 초기화가 안됨 -> 아마 Stage값 초기화 안된듯?
	+ 모자쓴 두더지 / 폭탄은 나오는데 3마리부터 나옴

## 20180412

### v1.6
* 에러 고침

* 난이도 별로 시작(스테이값 다르게)

* ResultUI
	+ Result 표시
	+ 최종 Score 표시

* UIManager
	+ Title, InGame, End UI를 게임 상태에 따라 표시

* Bomb
	+ 폭탄 WaitTime 수정
	+ 올라오는 도중 구멍을 막고 있다면 터져버리게 설정

* EnemyBase
	+ 적들 피격 판정 높이 올림
	+ 적들 올라오는 속도 빠르게

## 20180421

### v1.7
* UIButtons
	+ 버튼 종류별로 처리
	+ Start / BackToMenu

* DifficultBtns
	+ 난이도 처리용 버튼
	+ Easy / Normal / Hard 있다
	+ 난이도에따라 시작 스테이지가 다름

* EnemyManager
	+ GameStart 수정 -> 스테이지 지정해서 시작하도록
	+ 난이도 상승 단위를 3으로 변경

* VR(CameraRig) 높이 조절

* 게임 전체적으로 돌아감

* 버그 / 수정해야될 것
	+ 컨트롤러로 두더지 / 폭탄을 들어올릴 수 있음
	+ 컨트롤러 / 트레커 모델이 큐브 -> 손으로 바꿀 예정
	+ 쓸어서 두더지 누르는게 가능

## 20180424

### v1.8
* PlayerController
	+ 플레이어 버그 방지(두더지 / 폭탄 들어올릴 없게 만듬)
	+ 경계 Trigger에 닿으면 플레이어 Collider -> Trigger로 바꿔 못들어 올리게 만듬

* 손 모델 임포트 및 큐브를 손으로 대체