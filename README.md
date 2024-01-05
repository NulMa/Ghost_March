# Ghost_March

- 개요

	- 한국 무속 신앙을 기반으로 한
	뱀파이버 서바이벌 라이크 장르의 모바일 게임

	- APK 파일 다운로드 :
   		https://drive.google.com/drive/folders/1qbRwrNemT_Zs7Ug-0bk8C69Psq08Y6i6?usp=drive_link

- 현재 버전

	- Ghost_March_Alpha 0.35.apk (2024. 1. 05)

- 제작기간

	- 클론 코딩을 통해 학습에 3주
	- 스프라이트 교체 및 기능 추가에 2주
	- 현재 진행중...




 구현 목표 리스트

 	- 몹 스폰 시스템 변경
  	  	 현재, Spawner 스크립트에서 몬스터의 스폰간격, 스프라이트 타입, 체력, 속도, 크기, 색, 물리력을 직접 할당하여 사용 중
  	  	이를 담는 데이터 타입을 만들고, 기존 스크립트에는 한 라운드에 여러 몹이 젠될 수 있게 해당 데이터 타입과, 젠 확률을 설정하게 구현
      		(2023.11.14 완)

    	- 경험치 시스템 개편, 처치 시 경험치 오브젝트 드롭으로 변경 및 플레이어가 근처로 다가가야만 습득
		현재 모든 몹이 경험치를 1만 줘서 밸런스 조절이 어렵고, 자칫하면 플레이가 루즈해짐
      			(경험치 드롭 구현 완료, 2023.11.16)
       			(경험치 플레이어 추적 구현 완료, 2023.11.23)

	- 일시정지 메뉴 및 기능 추가 (일시 정지 기능 추가 완료, 2023.12.05)

 	- 몬스터 체력바 및 플레이어 데미지 이펙트 (체력바 완성, 2023.12.07, 데미지 이펙트 완성, 2023. 12. 13)

   	- 플레이어 피격 이펙트 적용(2023. 12. 28)
       
   	- 몹 종류 추가
   	  	단기 목표로 4종 까지 (군집 타입 포함)
       		(군집 타입 및 방향 고정 로직 완성, 2023. 12. 13)

   	- 광고 구현

     	- 무기 및 악세서리 종류 추가(현재 무기 3, 악세서리 2, 소비 아이템1)
   	  	무기 4, 악세서리 4, 소비아이템 2를 단기 목표로
			- 초당 체력 회복 악세서리
			- 아이템 습득 거리 증가 악세서리
			- 활, 성장 시 투사체 수  증가, , 무한관통, 낮은 연사력 (완성, 2024. 01. 05)
			- 작두 근거리, 진행방향 기준 약 120도 범위, 일정 확률로 치명타
   
	  
	- 필드 드롭형 소형 회복제 구현 (2024. 1. 2)
 
   	- 귀행만의 특수 기믹이 필요 함, 개발 필요
		 당장 생각나는 방안으론 리듬게임적 요소를 추가해 버튼 액션을 넣는 방법을 생각중
  		주의할 사항은 왠만 해선 한 손 사용을 지향해야함



	----------------------------------------- 출시 이후 목표 ----------------------------------------

  	- 난이도의 추가(기존의 8분 -> 12분 혹은 시간 제한 없이 버티기 등)

   	- 수익모델이라 할 수 있는 유료재화 스킨과 후원 기능 구현

사용 리소스

	그래픽	- 자체 제작

	BGM	- 음원 제작자(눈솔, https://noonsol.net/) // 라이센스 구매 후 사용

	SFX 	- 음원 사이트에서 CC 1.0, CC 4.0 등의 음원들을 찾아서 사용

	아래는 사용 음원과 파일명 업로더와 링크, CC 버전 등을 기재


	FileName // URL // UserName // CC
	--------------------------------------------------------------------

	649360__sonofxaudio__sword_thunder01

	https://freesound.org/s/649360/

	SonoFxAudio

	CC BY 4.0

	--------------------------------------------------------------------

	HuechCrunch

	https://freesound.org/s/244490/

	zimbot

	CC BY 4.0

	--------------------------------------------------------------------

	270476__littlerobotsoundfactory__stab_knife_00

	https://freesound.org/s/270476/

	LittleRobotSoundFactory

	CC BY 4.0

	--------------------------------------------------------------------

	Squish impact

	https://freesound.org/s/500912/

	Bertsz	

	CC0 1.0 DEED

	--------------------------------------------------------------------

	PowerUp.wav

	https://freesound.org/s/328120/

	kianda

	CC0 1.0 DEED

	--------------------------------------------------------------------

	11228 asian logo.wav

	https://freesound.org/s/638809/

	Robinhood76

	CC BY 4.0

	--------------------------------------------------------------------

	Traditional Asian Percussion 01.wav

	https://freesound.org/s/445633/

	MATRIXXX_

	CC0 1.0 DEED

	--------------------------------------------------------------------

	625687__audacitier__knock-1

	https://freesound.org/s/625687/	

	AUDACITIER

	CC0 1.0 DEED


	--------------------------------------------------------------------

	11562 cartoon group wee.wav

	https://freesound.org/s/703306/

	Robinhood76

	CC BY 4.0

	--------------------------------------------------------------------

