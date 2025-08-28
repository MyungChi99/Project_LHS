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

### 🎯 주요 특징  

#### 1. 정교한 점프 메커니즘  
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



#### 2. 코요테 타임 (Coyote Time) & 점프 버퍼링 (Jump Buffering)  
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


#### 3. 점프 힘 보정 (Jump Force Compensation)  
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
---

### 🔌 모듈식 입력 시스템 (Modular Input System)  
캐릭터의 행동 로직(Move, Jump)과 입력 소스(키보드, AI 등)를 분리하기 위해 **ScriptableObject 기반 전략 패턴**을 적용했습니다.  
이를 통해 입력 방식을 손쉽게 교체하고 확장할 수 있는 구조를 구현했습니다.  

---

### 📂 코드 구조  

#### 1. 입력 규격 정의 (InputController.cs)  
```csharp
// 모든 입력 방식이 따라야 하는 추상 클래스
public abstract class InputController : ScriptableObject
{
    public abstract float RetrieveMoveInput();
    public abstract bool RetrieveJumpInput();
    public abstract bool RetrieveJumpHoldInput();
}
```

#### 2. 키보드 입력 구현 (PlayerController.cs)  
```csharp
[CreateAssetMenu(menuName = "InputController/PlayerController")]
public class PlayerController : InputController
{
    public override float RetrieveMoveInput() => Input.GetAxisRaw("Horizontal");
    public override bool RetrieveJumpInput() => Input.GetButtonDown("Jump");
    public override bool RetrieveJumpHoldInput() => Input.GetButton("Jump");
}
```

#### 3. 실제 사용 (Move.cs)  
```csharp
public class Move : MonoBehaviour
{
    [SerializeField] private InputController _input; // 인스펙터에서 할당

    private void Update()
    {
        _direction.x = _input.RetrieveMoveInput();
        if (_input.RetrieveJumpInput()) Jump();
    }
}
```

---

### ✅ 주요 장점  
- **확장성**: AI, Gamepad 등 새로운 입력 방식을 클래스 추가만으로 적용 가능  
- **독립성**: 입력 소스 교체만으로 플레이어 → AI 전환, 리플레이 구현 가능  
- **테스트 용이성**: TestInputController를 통해 디버깅·자동화 테스트 지원  

---

### ⚙️ 데이터 중심 설계를 위한 스크립터블 오브젝트 시스템
게임 로직(MonoBehaviour)과 핵심 데이터(체력, 속도 등)를 분리하여 디자이너와 프로그래머 간의 협업을 원활하게 하고, 유지보수와 밸런싱이 용이한 구조를 만들기 위해 스크립터블 오브젝트 기반의 데이터 시스템을 설계했습니다.

이 시스템의 핵심은 FloatVariable과 FloatReference, 두 개의 클래스로 구성됩니다.

---

#### 1. FloatVariable: 중앙 데이터 컨테이너  
FloatVariable은 체력, 공격력, 속도 등 게임 내의 주요 수치 데이터를 담는 공유 가능한 애셋입니다.  
ScriptableObject로 만들어져, 여러 씬과 오브젝트에서 직접 참조하여 동일한 데이터를 공유할 수 있습니다.  

```csharp
// 여러 게임 오브젝트가 참조할 수 있는 공유 데이터 애셋
[CreateAssetMenu]
public class FloatVariable : ScriptableObject
{
    public float Value;

    public void SetValue(float value)
    {
        Value = value;
    }

    public void ApplyChange(float amount)
    {
        Value += amount;
    }
}
```


#### 2. FloatReference: 스마트하고 유연한 데이터 접근자  
FloatReference는 이 시스템의 핵심으로, 데이터 사용의 편의성과 유연성을 극대화하는 '스마트 포인터' 역할을 합니다.  

- **디자이너를 위한 유연성**: 인스펙터 창에서 `UseConstant` 토글을 통해, 상수 값 또는 공유 데이터(FloatVariable) 참조를 선택 가능  
- **프로그래머를 위한 우아함**: `implicit operator`를 구현해 `.Value` 없이 일반 float처럼 사용 가능  

```csharp
[Serializable]
public class FloatReference
{
    public bool UseConstant = true;
    public float ConstantValue;
    public FloatVariable Variable;

    // UseConstant 값에 따라 고정값 또는 Variable의 값을 반환
    public float Value
    {
        get { return UseConstant ? ConstantValue : Variable.Value; }
    }

    // 이 클래스를 일반 float처럼 사용할 수 있게 해주는 핵심 로직
    public static implicit operator float(FloatReference reference)
    {
        return reference.Value;
    }
}
```

---

### 📊 적용 전후 코드 비교  

- 적용 전:  
```csharp
if (playerHealth.Value > 50) { ... }
```

- 적용 후:  
```csharp
if (playerHealth > 50) { ... }
```

---

### ✅ 주요 장점  
- **데이터와 로직의 분리**: 밸런스 수정 시 코드 수정·컴파일 없이 데이터 애셋만 변경  
- **협업 효율성**: 디자이너가 직접 안전하게 데이터 수정 및 테스트 가능  
- **가독성·유지보수성 향상**: 시스템 간 결합도를 낮추고, 코드 구조를 단순화  

---

## 🧑‍💻 프로젝트 회고

### 얻은 경험
- **버전 관리:** Git을 활용한 개인 프로젝트 버전 관리 및 커밋 메시지 작성 습관을 형성했습니다. 아래와 같은 Git-flow 전략을 기반으로 브랜치를 관리하며 협업과 기능 개발의 효율성을 높였습니다.

  <p align="center">
    <img width="250" alt="Git Branch Strategy" src="https://github.com/user-attachments/assets/c2b553d3-3618-4c11-a033-109964ae354e" />
    <br>
    <em>Project LHS의 Git 브랜치 전략 도식화</em>
  </p>

- **설계 능력:** 시작은 하드코딩을 통해 직관성을 키우고 빠르게 다양한 기능을 구현하였습니다. 점점 많은 기능들이 추가 되면서 재미도 있었고 가령 wall jump 와 double jump 를 합쳐서 삼단 슈퍼 점프 등을
  가능하게 하는 것을 보고 더욱 흥미를 얻었습니다. 허나 정말 많은 버그들이 생겨날 수 있었으며 수많은 collision test, animation 등을 엮다 보니 점점 프로젝트가 난해해져서 스파게티 코드가 되어가는 것을 느꼇습니다.
  결국 새로운 아키텍쳐를 위해 코어 시스템은 바꾸기로 결심하고 scriptable object 를 통해 종속도를 낮추었습니다. 또한 새로 구현한 기능 가령 체력바를 테스트 하는 환경을 만들고 싶을때 프로젝트가 커지면 커질수록 분리해서 테스트
  할 수 있는 환경이 필요하다는 것을 느꼇습니다. 솔직히 체력바 오르고 내리는걸 테스트 해보려고 3gb 이상의 exe 파일을 만들어 테스트 해보는건 정말 비효율적일 수 없었습니다. 체력바를 상호작용 하는 object로 설계하면 테스트 환경에서
  캐릭터, 체력바, 적만 구현해서 테스트 할 수 있습니다. 이렇듯 확장성과 개별 테스트를 고려한 환경을 구현 해야 오히려 더 빠르고 안정적인 프로젝트로 완성시킬 수 있음을 깨달았습니다.

---
