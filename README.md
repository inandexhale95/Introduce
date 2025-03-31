# Simple Blog
> Introduce Develop Blog

</br>

## :pushpin: Intro
개발 지식을 하나하나 쌓아가며 기술들을 적용해가는 개발 블로그 입니다.

.NET의 Web Framework인 ASP.NET Core와 ORM인 EF Core를 사용했습니다.

EF Core의 Fluent API 방식을 활용해 Migration을 생성한 후 각 클래스 모델의 테이블을 생성하였습니다.

<br/>

![화면 캡처 2022-05-20 205339](https://user-images.githubusercontent.com/75681679/169523102-6ef4740d-6fa6-4b69-9479-ef7d6dd8cf86.png)

### 총 3개의 프로젝트로 구성되어 있습니다.
1. Introduce          : ASP.NET Core WebApp 프로젝트입니다. 
2. Introduce.Data     : 웹 프로젝트의 Model과 ViewModel을 담고 있는 클래스 라이브러리 프로젝트 입니다.
3. Introduce.Service  : 웹 프로젝트의 로직을 담고 있는 클래스 라이브러리 프로젝트 입니다.

<br/>

#### 컨트롤러는 4개로 구성되어 있습니다.
1. HomeController
    * Index 메서드 : 메인 페이지를 담당합니다.
3. UserController
    * Login 메서드 : 사용자의 정보와 권한을 가져와 신원보증과 승인권한을 부여합니다.
    * LogOut 메서드 : 사용자의 계정을 로그아웃 합니다.
    * Register 메서드 : 입력 폼으로 회원가입을 처리합니다.
    * Update 메서드 : 사용자의 정보를 수정합니다.
    * MyInfo 메서드 : 사용자의 정보와 권한을 확인할 수 있습니다.
    * Withdrwan 메서드 : 사용자 계정을 탈퇴합니다.
4. FreeBoardController
    * Index 메서드 : 익명 게시판의 리스트를 출력합니다.
    * Write 메서드 : 익명 게시판의 글을 작성합니다.
    * Remove 메서드 : 익명 게시판 글을 지웁니다.
5. ForumController - 회원가입된 사용자만 이용할 수 있는 게시판 입니다. 
    * Index 메서드 : 게시판의 리스트를 출력합니다.
    * Write 메서드 : 게시판의 글을 작성합니다.
    * Detail 메서드 : 게시판 글의 정보를 자세히 볼 수 있습니다.
    * Update 메서드 : 게시판 글을 수정합니다.
    * Remove 메서드 : 게시판 글을 삭제합니다.

</br>

## :pushpin: Contact
- 이메일: inandexhale95@naver.com
- 깃헙: https://github.com/inandexhale95

</br>

## :pushpin: Projects
~~https://introduceportfolio.azurewebsites.net/~~
<br/>
~~http://introduce-develop-blog.iptime.org/~~

**기술 스택**
<br/>
<img src="https://img.shields.io/badge/.NET6-512BD4?style=flat&logo=Dotnet&logoColor=white"/>
<br/>
<img src="https://img.shields.io/badge/Csharp-239120?style=flat&logo=csharp&logoColor=white"/> - ASP.NET Core / Entity Framework Core
<br/>
~~<img src="https://img.shields.io/badge/Azure-0078D4?style=flat&logo=microsoftAzure&logoColor=white"/> - SQL Server, WebApp~~
<br/>
<img src="https://img.shields.io/badge/Bootstrap-7952B3?style=flat&logo=bootstrap&logoColor=white"/>
