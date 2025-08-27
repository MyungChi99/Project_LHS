# 🎮 [Project LHS]
- **dev/Concept: Chang Hanhae**
- **Art/Concept: Tarphis**
> **2D Platformer/Metroidvania**  
> This project was intended to be a concept game which the main gimmick is interaction with lights.
> Currently Abandoned Project yet sharing the experiences.
> If you like this concept and want to develop further more please contact me.
---

## ✨ Overview
- **Development Period**: (2022.01 – 2022.07)  
- **Engine**: Unity (e.g., 2021.3)  
- **Language**: C#  
- **Platform**: PC (Windows)  
- **Status**: Prototype (discontinued)  

---

## 📖 Story & Concept
- **Story**  
   https://miro.com/app/board/uXjVPPJUQsE=/
- **세계관 및 기획 컨셉**  
  - 인류 멸망 이후 신소재(기억의 모래)
  - 주요 상호작용(엘리베이터, 환경 오브젝트)  
  - 게임의 톤앤매너 (예: 어두운 분위기의 SF, 감성적 픽셀 아트 등)

---

## 🖼️ 컨셉 아트
<!-- 가로형 그룹 -->
<p align="center">
  <img src="https://github.com/user-attachments/assets/99f5b595-5179-4492-82f0-19722886edd6" width="30%" />
  <img src="https://github.com/user-attachments/assets/755b30d8-2aab-4e17-bdfc-e4dfb7288fe7" width="30%" />
  <img src="https://github.com/user-attachments/assets/0412789c-eff8-40cb-a126-9ce1646483b5" width="30%" />
</p>

<p align="center">
  <img src="https://github.com/user-attachments/assets/d804a759-8db4-462f-ae3a-c09e5a87e142" width="30%" />
  <img src="https://github.com/user-attachments/assets/7682b76a-0dc3-44ef-b8c1-06320220013f" width="30%" />
  <img src="https://github.com/user-attachments/assets/af8be3ff-b147-409e-abda-4d1669a79838" width="30%" />
</p>

<p align="center">
  <img src="https://github.com/user-attachments/assets/1bdc1171-70a3-4672-a131-bd8ee5514384" width="30%" />
  <img src="https://github.com/user-attachments/assets/4b7825e6-5407-46d2-a8f4-16f362826344" width="30%" />
  <img src="https://github.com/user-attachments/assets/58a84e00-f7ea-49ec-987a-43665e0247a2" width="30%" />
</p>

<p align="center">
  <img src="https://github.com/user-attachments/assets/7c7e2a34-8d60-4871-8d94-d87cde748cec" width="30%" />
  <img src="https://github.com/user-attachments/assets/70ef8182-9e08-4240-8a34-d1a9bd3588e9" width="30%" />
  <img src="https://github.com/user-attachments/assets/370dda27-bcf2-43e7-b65d-beada4084fb5" width="30%" />
</p>

<!-- 세로형 그룹 -->
<p align="center">
  <img src="https://github.com/user-attachments/assets/8f4712b9-e7c0-4a4d-b387-d4130c74d481" width="30%" />
  <img src="https://github.com/user-attachments/assets/03752080-5fa3-437d-a5f2-8a3551494414" width="30%" />
  <img src="https://github.com/user-attachments/assets/70790fa0-346a-47b9-89ae-04fc066dacda" width="30%" />
</p>

<p align="center">
  <img src="https://github.com/user-attachments/assets/75fa9163-7a6c-42d9-b202-966a25d78013" width="30%" />
</p>


---

## ⚙️ 구현 기능
- **Player Movement**
    - 기본 이동(WASD, 점프)
    - **기술적 고민:** `Vector3.Lerp`를 활용한 부드러운 카메라 추적 및 시야 전환 구현. 플레이어의 빠른 이동에 따른 카메라 지연 문제를 해결하기 위해 smoothSpeed 변수 최적화 및 `LateUpdate()` 활용.

- **Elevator Event Interaction**
    - `OnTriggerEnter`를 활용해 플레이어 접근 감지
    - 상호작용 키 입력(E키)에 따른 엘리베이터 상태 변화(상승/하강)
    - **기술적 고민:** `IInteractive`라는 **인터페이스(Interface)**를 설계하여 엘리베이터 외 다른 상호작용 오브젝트에도 동일한 로직을 적용할 수 있도록 확장성을 고려함.

- **기타 코드 실험**
    - 카메라 전환 로직
    - 레벨 이동 기초 구현
---

## 🧑‍💻 프로젝트 회고

### 얻은 경험
- **버전 관리:** Git을 활용한 개인 프로젝트 버전 관리 및 커밋 메시지 작성 습관을 형성했습니다. 아래와 같은 Git-flow 전략을 기반으로 브랜치를 관리하며 협업과 기능 개발의 효율성을 높였습니다.

  <p align="center">
    <img width="500" alt="Git Branch Strategy" src="https://github.com/user-attachments/assets/c2b553d3-3618-4c11-a033-109964ae354e" />
    <br>
    <em>Project LHS의 Git 브랜치 전략 도식화</em>
  </p>

- **설계 능력:** 추후 확장성을 고려한 인터페이스 및 추상 클래스 설계의 중요성을 깨달았습니다.
- **성능 최적화:** `Profiler`를 활용하여 `Physics` 및 `Garbage Collection`으로 인한 성능 저하를 분석하는 경험을 했습니다.

---

### 아쉬운 점 및 개선 방향
- **코드 효율성:** `Coroutine`으로 작성된 일부 이벤트 로직을 `Async/Await`를 활용하여 더 효율적이고 가독성 높은 비동기 로직으로 개선하고 싶습니다.
- **최신 기술 적용:** 유니티의 새로운 `Input System` 패키지를 도입하여 더 유연하고 확장 가능한 입력 시스템을 구현하고 싶습니다.
