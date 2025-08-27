# 🎮 [Project LHS]

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
   https://miro.com/app/board/uXjVPPJUQsE=/(
- **세계관 및 기획 컨셉**  
  - 
  - 주요 상호작용(엘리베이터, 환경 오브젝트)  
  - 게임의 톤앤매너 (예: 어두운 분위기의 SF, 감성적 픽셀 아트 등)

---

## 🖼️ 컨셉 아트
| 씬/캐릭터 | 이미지 |
|-----------|--------|
| 주인공 컨셉 | ![](./docs/art/player.png) |
| 배경 컨셉 | ![](./docs/art/background.png) |
| 엘리베이터 씬 | ![](./docs/art/elevator.png) |

---

## ⚙️ 구현 기능
- **Player Movement**  
  - 기본 이동 (WASD, 점프)  
  - 카메라 추적 및 시야 전환  
- **Elevator Event Interaction**  
  - 플레이어 접근 시 이벤트 발생  
  - 상호작용 키 입력에 따른 상태 변화  
- **기타 코드 실험**  
  - 카메라 전환 로직  
  - 레벨 이동 기초 구현  

코드 예시:  
```csharp
// Camera follow logic (간단 발췌 예시)
void LateUpdate() {
    Vector3 targetPosition = player.position + offset;
    transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.deltaTime);
}
