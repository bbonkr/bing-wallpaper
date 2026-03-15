# ClientApp -> Next.js 전환 세부 계획

## A. 사전 준비 (Phase 0)
### 목표
기존 동작을 기준선으로 고정해 회귀를 빠르게 탐지한다.

### 작업
1. 현재 라우트/기능 목록 스냅샷 작성
- 기준: `src/Bing.Wallpaper/ClientApp/src/BingImageApp/components/App/App.tsx`
- 핵심 라우트: `/`, `/collector`, `/logs`, `/404`

2. 환경변수 목록 확정
- 프론트엔드: `NEXT_PUBLIC_API_BASE_URL`
- 백엔드: 기존 `ConnectionStrings__Default`, `Collector__*` 유지
- Compose 내부 통신 기준 예시
  - frontend -> backend: `http://bing_wallpaper:5000/api/v1.0`

3. 기존 번들링 경로 기록
- 현재: webpack -> `src/Bing.Wallpaper/wwwroot/js/bingImageApp/bingImageApp.bundle.js`
- 전환 후 제거 대상 후보로 표시

### 체크포인트
- 기존 앱 로컬 실행/핵심 기능 동작 확인 후 진행

---

## B. Next.js 앱 부트스트랩 (Phase 1)
### 목표
`frontend/`에 실행 가능한 Next.js 앱 골격을 만든다.

### 작업
1. `frontend/` 생성 및 초기화
- 패키지 매니저: `pnpm`
- TypeScript + ESLint 활성화
- Next 버전: 작업 시점의 안정 버전으로 고정(major/minor 명시)

2. 구조 정의
- App Router 사용 (`frontend/app`)
- 기본 디렉터리
  - `frontend/app`
  - `frontend/src/components`
  - `frontend/src/store`
  - `frontend/src/api`
  - `frontend/public`

3. 공통 스타일 이전
- Bulma import 위치를 Next 글로벌 스타일 규칙에 맞게 이동
- 기존 CSS 파일은 우선 그대로 복사/참조 후 추후 정리

4. 정적 자산 복사
- `src/Bing.Wallpaper/wwwroot`의 favicon/icon을 `frontend/public`으로 복사

### 체크포인트
- `pnpm dev`, `pnpm build` 성공
- 기본 페이지 렌더링 성공

---

## C. 화면/라우팅/상태 이관 (Phase 2)
### 목표
기존 사용자 경로를 Next 라우트로 동일하게 제공한다.

### 작업
1. 라우팅 매핑
- `app/page.tsx` -> `/`
- `app/collector/page.tsx` -> `/collector`
- `app/logs/page.tsx` -> `/logs`
- `app/not-found.tsx` -> 기존 404 화면

2. 레이아웃 이관
- 기존 `Header`, `Footer`, `Container`, `FullSizeImage`를 Next 구조에 맞게 이동
- 공통 레이아웃은 `app/layout.tsx`에서 제공

3. 상태관리 이관
- 기존 `store/actions`, `reducers`, `epics` 구조 유지
- 브라우저 전용 상태코드는 `use client` 경계 내부에서 초기화
- SSR에서 불필요한 스토어 생성이 일어나지 않도록 Provider 위치 고정

4. 라우터 API 대체
- `react-router-dom` 의존 제거
- 링크는 Next `Link`, 페이지 전환은 Next 라우팅 방식으로 교체

5. 코드 분할 대체
- `React.lazy` 경로는 Next의 동적 import 또는 route-level 분리로 대체

### 체크포인트
- 주요 경로 이동 정상
- 기존 기능 동등성(이미지 목록/수집/로그 조회/404) 확보

---

## D. API 통신 및 백엔드 연동 (Phase 3)
### 목표
분리 배포 환경에서도 안정적으로 API를 호출한다.

### 작업
1. API 클라이언트 이전
- 기존 `src/Bing.Wallpaper/ClientApp/src/api`와 서비스/클라이언트를 `frontend/src/api`로 이관
- OpenAPI generator 스크립트 경로를 frontend 기준으로 수정

2. Base URL 전략 통일
- 클라이언트 직접 호출 방식: `NEXT_PUBLIC_API_BASE_URL`
- 필요시 Next rewrites로 `/api` 프록시 제공(선택)

3. 백엔드 CORS 설정 강화
- 개발/운영 origin을 명시적으로 허용
- 와일드카드 CORS(`AllowAnyOrigin`)는 개발 전용으로 제한

4. 오류 처리 검증
- axios interceptor의 에러 매핑이 Next 환경에서도 동일 동작하는지 확인

### 체크포인트
- 브라우저 네트워크 탭에서 CORS 오류 없음
- 4xx/5xx 시 기존과 동등한 에러 처리 동작

---

## E. Docker/Compose 분리 배포 (Phase 4)
### 목표
프론트엔드/백엔드를 독립 이미지로 빌드 및 배포한다.

### 작업
1. 프론트엔드 Dockerfile 추가 (`frontend/Dockerfile`)
- multi-stage build 권장
- builder: pnpm install + next build
- runner: next start
- 포트: `3000`

2. Compose 구성 추가/개편
- 예시 파일: `docker-compose.fullstack.yml` (신규 권장)
- 서비스
  - `bing_wallpaper` (backend, 5000)
  - `bing_wallpaper_frontend` (next, 3000)
- frontend env
  - `NEXT_PUBLIC_API_BASE_URL=http://localhost:5000/api/v1.0` (외부 접속 기준)
  - 내부 네트워크 통신 필요 시 별도 서버사이드 URL 변수 추가

3. 기존 Dockerfile 정리
- 백엔드 Dockerfile에서 ClientApp 빌드 단계 제거
- 백엔드는 .NET publish만 책임지도록 단순화

4. 실행/배포 Runbook 작성
- 이미지 빌드/푸시/기동/로그확인/재기동 절차를 문서화

### 체크포인트
- `docker compose -f docker-compose.fullstack.yml up -d --build` 성공
- `http://localhost:3000` 접근 + API 연동 정상

---

## F. 정리 및 마무리 (Phase 5)
### 목표
구 버전 의존을 제거하고 운영 가능한 상태로 마감한다.

### 작업
1. 구 ClientApp 정리
- 즉시 삭제 또는 `legacy/` 이동 중 하나 선택
- 최소 요건: 백엔드 빌드가 ClientApp 산출물에 더 이상 의존하지 않음

2. 문서 업데이트
- `README.md`에 frontend 개발/배포 절차 추가
- 환경변수 표 정리(backend/frontend 분리)

3. 검증
- smoke test
  - 홈 목록 조회
  - Collector 실행
  - Logs 조회
  - 존재하지 않는 경로 404

4. 릴리즈 체크
- 태그/이미지 버전 정책 반영
- 롤백 절차(이전 이미지 태그 복원) 명시

### 체크포인트
- 신규 팀원이 문서만으로 로컬/컨테이너 실행 가능

---

## 실행 순서 요약 (의존성 고정)
1. Phase 0
2. Phase 1
3. Phase 2
4. Phase 3
5. Phase 4
6. Phase 5

각 Phase 완료 시점에 빌드/실행 검증을 통과하지 못하면 다음 Phase 진행 금지.
