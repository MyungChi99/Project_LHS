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
### ✨ Responsive 2D Platformer Controller  
단순한 이동을 넘어, 플레이어에게 **최상의 조작감(Game Feel)**을 제공하는 것을 목표로 설계한 컨트롤러입니다.  

---

## 🎯 주요 특징  

### 1. 정교한 점프 메커니즘  
- **가변 점프 높이 (Variable Jump Height)**  
  점프 버튼 입력 시간을 기반으로 점프 높이가 달라집니다.  
  → 플레이어가 섬세하게 점프를 조절할 수 있도록 구현했습니다.  

```csharp
// FixedUpdate() 내부
if (_controller.input.RetrieveJumpHoldInput() && _body.velocity.y > 0)
{
    // 점프 버튼 유지 & 상승 중 → 중력 약화
    _body.gravityScale = _upwardMovementMultiplier;
}
else if (!_controller.input.RetrieveJumpHoldInput() || _body.velocity.y < 0)
{
    // 버튼 뗌 or 하강 중 → 중력 강화
    _body.gravityScale = _downwardMovementMultiplier;
}
```

---

### 2. 코요테 타임 (Coyote Time) & 점프 버퍼링 (Jump Buffering)  
- 발판에서 떨어진 직후, 또는 착지 직전의 애매한 순간에도 입력을 수용  
- 점프 타이밍의 관대함을 통해 **조작 만족도**를 높임
- 성공적인 게임에는 반드시 있는 기능이다.

```csharp
// FixedUpdate() 내부

// 땅에 있을 때 → 코요테 시간 초기화
if (_onGround && _body.velocity.y == 0)
    _coyoteCounter = _coyoteTime;
else
    _coyoteCounter -= Time.deltaTime;

// 점프 입력 → 버퍼 초기화
if (_desiredJump)
{
    _desiredJump = false;
    _jumpBufferCounter = _jumpBufferTime;
}
else if (_jumpBufferCounter > 0)
{
    _jumpBufferCounter -= Time.deltaTime;
}

// 버퍼 시간 안에 있으면 점프 실행
if (_jumpBufferCounter > 0)
    JumpAction();
```

```csharp
// JumpAction() 내부
if (_coyoteCounter > 0f || (_jumpPhase < _maxAirJumps && _isJumping))
{
    // 점프 실행 로직...
}
```

---

### 3. 점프 힘 보정 (Jump Force Compensation)  
- 공중에서 점프 시 **현재 y축 속도**를 고려하여 점프 힘을 보정  
- 일관된 점프 경험을 제공
- 슈퍼마리오에 있는 기능을 참고

```csharp
// JumpAction() 내부
private void JumpAction()
{
    // (기본 점프 로직...)

    if (_velocity.y > 0f)
    {
        // 상승 중 공중 점프 → 현재 상승 속도만큼 보정
        _jumpSpeed = Mathf.Max(_jumpSpeed - _velocity.y, 0f);
    }
    else if (_velocity.y < 0f)
    {
        // 하강 중 공중 점프 → 하강 속도를 상쇄
        _jumpSpeed += Mathf.Abs(_body.velocity.y);
    }
    
    _velocity.y += _jumpSpeed;
}
```

- **기타 코드 실험**
    - 카메라 전환 로직
    - 레벨 이동 기초 구현
---

## 🧑‍💻 프로젝트 회고

### 얻은 경험
- **버전 관리:** Git을 활용한 개인 프로젝트 버전 관리 및 커밋 메시지 작성 습관을 형성했습니다. 아래와 같은 Git-flow 전략을 기반으로 브랜치를 관리하며 협업과 기능 개발의 효율성을 높였습니다.

  <p align="center">
    <img width="250" alt="Git Branch Strategy" src="https://github.com/user-attachments/assets/c2b553d3-3618-4c11-a033-109964ae354e" />
    <br>
    <em>Project LHS의 Git 브랜치 전략 도식화</em>
  </p>

- **설계 능력:** 추후 확장성을 고려한 인터페이스 및 추상 클래스 설계의 중요성을 깨달았습니다.
- **성능 최적화:** `Profiler`를 활용하여 `Physics` 및 `Garbage Collection`으로 인한 성능 저하를 분석하는 경험을 했습니다.

---

### 아쉬운 점 및 개선 방향
- **코드 효율성:** `Coroutine`으로 작성된 일부 이벤트 로직을 `Async/Await`를 활용하여 더 효율적이고 가독성 높은 비동기 로직으로 개선하고 싶습니다.
- **최신 기술 적용:** 유니티의 새로운 `Input System` 패키지를 도입하여 더 유연하고 확장 가능한 입력 시스템을 구현하고 싶습니다.
