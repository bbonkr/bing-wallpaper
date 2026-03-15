---
title: "Next.js 마이그레이션 작업 로그"
date: "2026-03-15"
project: "bing-wallpaper"
tags:
  - migration
  - nextjs
  - docker
  - cors
---

# 오늘 작업 요약

## 완료
- `src/Bing.Wallpaper/Program.cs`에 CORS 정책 추가
  - `Cors:AllowedOrigins` 설정 기반 허용
  - 개발 환경에서만 `AllowAnyOrigin` fallback
- `src/Bing.Wallpaper/appsettings.json`에 `Cors.AllowedOrigins` 추가
- 루트 `Dockerfile`에서 `ClientApp` 빌드 단계 제거
- `frontend/Dockerfile` 생성 (multi-stage build + `HEALTHCHECK`)
- `docker-compose.fullstack.yml` 생성
  - backend `healthcheck` (`/healthz`)
  - frontend `depends_on.condition=service_healthy`
- `README.md`에 분리 배포 실행 방법 추가
- `.dockerignore`, `frontend/.dockerignore` 추가/보완 (빌드 컨텍스트 경량화)
- 계획 문서 진행 상태 업데이트
  - `docs/plans/migrate-to-nextjs/task-plan.md`
  - `docs/plans/migrate-to-nextjs/task-details.md`
- GitHub Actions 전환 반영
  - `verify-pr.yml`, `pr-completed.yml`의 `ClientApp` 빌드를 `frontend` 빌드로 교체
  - `docker.yml`을 backend/frontend 이미지 분리 빌드/푸시로 확장

## 검증
- `dotnet build` 성공 (오류 없음)
- `frontend/pnpm build` 성공
- `docker compose` 실기동 검증은 로컬 `docker` CLI 부재로 보류

## 결정 사항
- `src/Bing.Wallpaper/ClientApp`은 즉시 삭제하지 않고 read-only legacy로 유지
- 컷오버 안정화 기준 충족 후 제거/이관 재검토

## 다음 작업
1. Docker 가능 환경에서 `docker compose -f docker-compose.fullstack.yml up -d --build` 검증
2. `/`, `/collector`, `/logs`, 404 및 API 연동 스모크 테스트
3. 컷오버 기준 충족 시 `ClientApp` 제거 또는 `legacy/` 이관 확정
