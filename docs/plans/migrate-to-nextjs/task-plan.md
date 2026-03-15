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

## 9) 권장 브랜치/커밋 전략
- 브랜치: `feature/373-migrate-clientapp-to-nextjs`
- 커밋 단위
1. `chore(frontend): bootstrap nextjs app`
2. `feat(frontend): migrate routes and layout`
3. `feat(frontend): migrate store and api integration`
4. `chore(docker): add frontend image and compose integration`
5. `docs: add migration notes and runbook`
