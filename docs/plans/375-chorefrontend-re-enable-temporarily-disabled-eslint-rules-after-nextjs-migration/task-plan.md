# Task Plan

## Issue
- GitHub Issue: #375
- Title: `chore(frontend): re-enable temporarily disabled ESLint rules after Next.js migration`

## Goal
Next.js 마이그레이션 중 임시 비활성화된 ESLint 규칙을 단계적으로 재활성화하고, 기능 회귀 없이 CI에서 강제되도록 구성한다.

## Scope
- `@typescript-eslint/no-explicit-any`
- `@typescript-eslint/no-empty-object-type`
- `react-hooks/set-state-in-effect`
- `@next/next/no-img-element`
- `jsx-a11y/alt-text`

## Current Baseline
- 비활성 규칙 위치: `frontend/eslint.config.mjs`
- 현재 CI(`.github/workflows/verify-pr.yml`)에 frontend lint 단계 없음
- 규칙별 잠정 위반 수(규칙만 임시 error로 강제 실행):
  - `@typescript-eslint/no-explicit-any`: 3
  - `@typescript-eslint/no-empty-object-type`: 4
  - `react-hooks/set-state-in-effect`: 3
  - `@next/next/no-img-element`: 4
  - `jsx-a11y/alt-text`: 4

## Execution Plan
1. Baseline 고정
- `frontend` 기준 `pnpm lint`, `pnpm build` 결과를 기준선으로 기록한다.
- 이슈 범위 외 기존 경고(`no-unused-vars`, `react-hooks/exhaustive-deps`)는 이번 작업의 직접 대상이 아님을 명시한다.

2. 1차 재활성화 (Type 규칙)
- `@typescript-eslint/no-empty-object-type`, `@typescript-eslint/no-explicit-any`를 먼저 처리한다.
- 위반 코드 수정 후 두 규칙을 `error`로 상향한다.
- 생성 코드(`frontend/src/api/*`)는 필요 시 예외 범위를 최소화하고 사유를 문서화한다.

3. 2차 재활성화 (UI/접근성)
- `@next/next/no-img-element`, `jsx-a11y/alt-text`를 함께 처리한다.
- 일반 이미지 렌더링은 `next/image` 사용으로 전환한다.
- 장식용 이미지 alt 정책을 명확히 적용하고, 불가피한 예외는 라인 단위로 제한한다.

4. 3차 재활성화 (Hook 상태 업데이트)
- `react-hooks/set-state-in-effect` 위반 지점을 리팩터링한다.
- `useMemo`, 초기화 시점 조정, 이벤트 기반 업데이트 등으로 `effect` 내부 직접 상태 업데이트를 제거한다.
- 동작 민감도가 높은 지점은 작은 단위로 분리 적용한다.

5. CI lint 강제
- `.github/workflows/verify-pr.yml`에 frontend lint step(`pnpm lint`)을 추가한다.
- 필요 시 `.github/workflows/pr-completed.yml`에도 동일 정책을 반영해 일관성을 유지한다.

6. 회귀 검증
- 수동 검증 경로:
  - `/`
  - `/collector`
  - `/logs`
  - 이미지 모달 열기/닫기/키보드 인터랙션
- lint/build 성공 및 핵심 화면 동작 확인 후 마무리한다.

## Done Criteria
- 대상 규칙이 `error`로 재활성화됨 (또는 예외가 최소 범위로 문서화됨)
- CI lint 단계에서 업데이트된 규칙 세트를 강제함
- `/`, `/collector`, `/logs`, 이미지 모달 플로우에서 기능 회귀 없음

## Risks & Mitigations
- Risk: `next/image` 전환 시 레이아웃 변화 가능
- Mitigation: 컴포넌트별 시각 확인 및 사이즈/우선순위 속성 명시

- Risk: `set-state-in-effect` 리팩터링 과정에서 렌더 타이밍 변경
- Mitigation: 작은 단위 적용 + 핵심 플로우 수동 검증

## Deliverables
- ESLint 규칙 재활성화 반영 (`frontend/eslint.config.mjs`)
- 위반 코드 수정 커밋
- CI lint 단계 반영 (`.github/workflows/verify-pr.yml`, 필요 시 `pr-completed.yml`)
- 검증 결과 기록
