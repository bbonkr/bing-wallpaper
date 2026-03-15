# ClientApp -> Next.js 전환 작업 계획 (주 계획)

## 1) 목표
`src/Bing.Wallpaper/ClientApp` 기반 클라이언트 앱을 `frontend/` 디렉터리의 Next.js 앱으로 전환하고, 백엔드(`src/Bing.Wallpaper`)와 프론트엔드를 **별도 Docker 이미지/컨테이너**로 배포 가능하도록 구성한다.

## 2) 확정된 의사결정 (사용자 확인 완료)
- 렌더링/실행: Next 서버 별도 실행(SSR 가능 구조)
- 배포: 백엔드/프론트엔드 분리 이미지 + Docker Compose로 오케스트레이션
- 상태관리: 기존 `redux + redux-observable` 최대한 유지
- 스타일: 기존 `Bulma + 기존 CSS` 최대한 유지
- 프론트엔드 루트: 프로젝트 루트 `frontend/`

## 3) 작업 범위
- 포함
1. `frontend/` Next.js 신규 앱 생성 및 기존 화면/라우팅 이관
2. API 호출 경로를 분리 배포 환경에 맞게 재구성
3. 백엔드 CORS/환경설정 보완
4. 프론트엔드 Dockerfile + Compose 구성 추가
5. 로컬/컨테이너 검증 시나리오 문서화
- 제외
1. 디자인 리뉴얼
2. Redux Toolkit/RTK Query로의 상태관리 대개편
3. 백엔드 도메인/DB 스키마 변경

## 4) 산출물
- 계획 문서(본 문서 + 세부 계획)
- `frontend/` Next.js 앱
- 프론트엔드 전용 Dockerfile
- 분리 배포용 compose 파일(또는 기존 compose 확장)
- 마이그레이션 완료 체크리스트

## 4-1) Docker 관련 생성/수정 대상 파일
- 생성: `frontend/Dockerfile`
- 생성: `docker-compose.fullstack.yml` (권장안)
- 수정: 루트 `Dockerfile` (백엔드 빌드에서 `ClientApp` 빌드 단계 제거)

### Docker HEALTHCHECK 예시 스니펫
```dockerfile
# frontend/Dockerfile (runner stage 예시)
HEALTHCHECK --interval=30s --timeout=5s --start-period=20s --retries=3 \
  CMD wget -qO- http://127.0.0.1:3000/ >/dev/null || exit 1
```

```yaml
# docker-compose.fullstack.yml (예시)
services:
  bing_wallpaper_frontend:
    # ...
    depends_on:
      bing_wallpaper:
        condition: service_healthy
```

## 5) 단계 및 의존관계
1. 기반 셋업
: `frontend/` 생성, Next.js 초기화, 공통 설정 확정
2. 앱 이관
: 라우팅/레이아웃/컴포넌트/스토어 이관
3. API/통신 정리
: API 클라이언트/환경변수/CORS 정리
4. 컨테이너화
: frontend 이미지 빌드, compose 통합
5. 검증 및 전환
: E2E 수준 동작 검증, 기존 ClientApp 제거(또는 read-only 보관)

세부 실행 절차와 파일 단위 작업은 아래 문서를 따른다.
- [세부 계획](./task-details.md)

## 6) 즉시 실행 규칙 (에이전트 작업 시작 기준)
- 순서 고정: 기반 셋업 -> 앱 이관 -> API/통신 -> 컨테이너 -> 검증
- 각 단계 완료 시 `빌드 성공 + 핵심 경로 동작`을 확인해야 다음 단계로 진행
- 단계 중 실패 시 다음 단계로 넘어가지 않고 해당 단계에서 원인 수정

## 7) 완료 기준 (Definition of Done)
- `frontend/`에서 Next.js 앱이 `pnpm build` 성공
- Docker Compose로 백엔드/프론트엔드 동시 기동 성공
- 주요 경로(`/`, `/collector`, `/logs`, 상세 이미지 보기, 404) 정상 동작
- 브라우저에서 API 통신 오류(CORS/경로 오류) 없음
- 기존 `ClientApp` 빌드 파이프라인 의존 제거 또는 비활성화 완료

## 8) 리스크 및 대응
- API base URL 불일치
: `NEXT_PUBLIC_API_BASE_URL` 표준화 + compose 환경변수로 강제
- SSR 시 브라우저 전용 코드 충돌
: 초기 단계는 Client Component 우선 적용(`"use client"`), 필요시 동적 import
- Redux observable 의존성 이관 이슈
: 스토어 초기화 코드를 최소 변경으로 이전하고, 에픽 실행 순서를 테스트로 확인
- 라우팅 충돌
: Next App Router 기준으로 페이지 파일 구조를 URL과 1:1 매핑

## 9) 추가 권장사항
1. 런타임 API 변수 이원화
: `NEXT_PUBLIC_API_BASE_URL`(브라우저) + `INTERNAL_API_BASE_URL`(서버사이드) 분리
2. CORS 최소화
: 가능하면 Next `rewrites` 기반 `/api` 프록시 우선 적용, 직접 호출은 필요 시에만 유지
3. CI 스모크 테스트 추가
: `frontend build` + `docker compose up` + 핵심 라우트/API 헬스 체크 자동화
4. 이미지 태그 전략 고정
: `latest` 단독 대신 `git sha` 또는 `semver` 태그 병행
5. 컷오버 기준 명문화
: `ClientApp` 제거/legacy 전환 트리거(예: 1주 무사고, 핵심 시나리오 100% 통과) 사전 합의
6. OpenAPI 생성 안정화
: generator 버전/옵션 고정 및 CI 드리프트 체크 도입
7. frontend 컨테이너 HEALTHCHECK 적용
: `frontend/Dockerfile`에 health endpoint 기반 `HEALTHCHECK`를 추가하고 compose `depends_on`의 health 조건과 연계

## 10) 권장 브랜치/커밋 전략
- 브랜치: `feature/373-migrate-clientapp-to-nextjs`
- 커밋 단위
1. `chore(frontend): bootstrap nextjs app`
2. `feat(frontend): migrate routes and layout`
3. `feat(frontend): migrate store and api integration`
4. `chore(docker): add frontend image and compose integration`
5. `docs: add migration notes and runbook`

## 11) 진행 현황 (2026-03-15)
- Phase 0~3: 완료
- Phase 4: 파일/설정 작업 완료, 로컬 `docker` 부재로 compose 실기동 검증 보류
- Phase 5: 문서 정리 진행 중

### 완료 항목
1. `frontend/` Next.js 앱 구성 및 주요 라우트(`/`, `/collector`, `/logs`, `not-found`) 이관
2. API 클라이언트/서비스 이관 및 `NEXT_PUBLIC_API_BASE_URL` 반영
3. 백엔드 CORS를 `Cors:AllowedOrigins` 기반 명시 허용 방식으로 변경
4. `frontend/Dockerfile`/`docker-compose.fullstack.yml` 추가
5. 루트 `Dockerfile`에서 `ClientApp` 빌드 단계 제거
6. `README.md`에 분리 배포 실행 절차 추가
7. GitHub Actions에서 `ClientApp` 빌드 단계를 `frontend` 빌드로 전환
8. 릴리즈 Docker workflow를 backend/frontend 이미지 분리 빌드/푸시로 확장

### 보류 항목
1. `docker compose -f docker-compose.fullstack.yml up -d --build` 실기동 검증
2. 브라우저 실환경에서 프론트-백엔드 연동 스모크 테스트 최종 확인

### 컷오버 결정
- `src/Bing.Wallpaper/ClientApp`은 즉시 삭제하지 않고 read-only legacy로 유지
- 컷오버 기준 충족(무사고 기간 + 핵심 시나리오 통과) 후 제거 또는 `legacy/` 이관 재평가
