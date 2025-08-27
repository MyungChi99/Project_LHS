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
   https://miro.com/app/board/uXjVPPJUQsE=/
- **세계관 및 기획 컨셉**  
  - 
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
