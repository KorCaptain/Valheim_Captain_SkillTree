# MCP (Model Context Protocol) 프로젝트 설치 가이드

## 📚 글로벌 MCP 통합 참조
본 가이드는 프로젝트 특화 MCP 설치 절차입니다. MCP 통합 전략 및 서버 선택 방법은 `~/.claude/MCP.md`를 참조하세요.

---

## 🔧 공통 주의사항

1. **현재 사용 환경 확인**: OS(윈도우, 리눅스, 맥) 및 환경(WSL, 파워셸, 명령 프롬프트) 파악
2. **OS별 적절한 설정**: 모르면 사용자에게 물어볼 것
3. **User 스코프 설치**: mcp-installer 사용 시 user 스코프로 설치
4. **공식 사이트 우선**: WebSearch로 MCP 공식 사이트 확인 후 설치
5. **Context7 재확인**: 공식 사이트 확인 후 Context7 MCP로 추가 검증
6. **작동 검증 필수**: 설치 후 디버그 모드로 실제 작동 여부 확인
7. **API 키 처리**: 가상 키로 설치 후 사용자에게 실제 키 입력 안내
8. **서버 의존성**: MySQL 등 특정 서버 구동 필요 시 조건 안내
9. **선택적 설치**: 요청받은 MCP만 설치, 기존 MCP 오류 무시
10. **WSL 패스워드**: qsc1555 (윈도우 네이티브 환경은 제외)

---

## 💻 OS별 주의사항

### Windows
- 경로 구분자: 백슬래시(`\`)
- JSON 이스케이프: `\\\\` (두 번)
- Node.js PATH 등록 확인
- npx -y 옵션 사용 권장

### Linux/macOS/WSL
- 경로 구분자: 슬래시(`/`)
- Node.js 버전: v18 이상
- sudo 권한 필요 시 사용자 확인

---

## 📋 MCP 서버 설치 순서

### 1. 기본 설치
```bash
# mcp-installer 사용
mcp-installer를 사용해 설치
```

### 2. 설치 후 정상 작동 확인
```bash
# 설치 목록 확인
claude mcp list

# 디버그 모드 검증 (2분 관찰)
# Task tool로 디버그 모드 서브 에이전트 구동
claude --debug

# /mcp 명령으로 실제 작동 확인
echo "/mcp" | claude --debug
```

### 3. 문제 발생 시 직접 설치

**User 스코프 설정 예시:**
```bash
claude mcp add --scope user youtube-mcp \
  -e YOUTUBE_API_KEY=$YOUR_YT_API_KEY \
  -e YOUTUBE_TRANSCRIPT_LANG=ko \
  -- npx -y youtube-data-mcp-server
```

### 4. 재검증
설치 후 2번 단계 반복 (claude mcp list → claude --debug → /mcp 확인)

### 5. 고급 설치 방법

**npm/npx 패키지 찾을 수 없는 경우:**
```bash
# npm 전역 설치 경로 확인
npm config get prefix

# npm, pip, uvx 등으로 직접 설치
```

**uvx 명령어 없는 경우:**
```bash
# uv 설치 (Python 패키지 관리자)
curl -LsSf https://astral.sh/uv/install.sh | sh
```

**터미널 작동 검증 후 JSON 설정:**
- 성공 시 인자 및 환경 변수 활용
- 올바른 위치의 JSON 설정 파일에 직접 설정

---

## 📂 설정 파일 위치

### Linux, macOS, WSL
- **User 설정**: `~/.claude/` 디렉토리
- **Project 설정**: 프로젝트 루트/.claude

### Windows 네이티브
- **User 설정**: `C:\Users\{사용자명}\.claude` 디렉토리
- **Project 설정**: 프로젝트 루트\.claude

---

## 🔧 JSON 설정 예시

### 1. npx 사용
```json
{
  "youtube-mcp": {
    "type": "stdio",
    "command": "npx",
    "args": ["-y", "youtube-data-mcp-server"],
    "env": {
      "YOUTUBE_API_KEY": "YOUR_API_KEY_HERE",
      "YOUTUBE_TRANSCRIPT_LANG": "ko"
    }
  }
}
```

### 2. cmd.exe 래퍼 (Windows)
```json
{
  "mcpServers": {
    "mcp-installer": {
      "command": "cmd.exe",
      "args": ["/c", "npx", "-y", "@anaisbetts/mcp-installer"],
      "type": "stdio"
    }
  }
}
```

### 3. PowerShell
```json
{
  "command": "powershell.exe",
  "args": [
    "-NoLogo", "-NoProfile",
    "-Command", "npx -y @anaisbetts/mcp-installer"
  ]
}
```

### 4. node 직접 지정
```json
{
  "command": "node",
  "args": [
    "%APPDATA%\\npm\\node_modules\\@anaisbetts\\mcp-installer\\dist\\index.js"
  ]
}
```

---

## ✅ args 배열 설계 체크리스트

### 토큰 단위 분리
```json
// ✅ 안전 (분리)
"args": ["/c", "npx", "-y", "pkg"]

// ❌ 위험 (통합)
"args": ["/c", "npx -y pkg"]
```

### 경로 포함
```json
// JSON 이스케이프 필수
"C:\\tools\\mcp\\server.js"
```

### 환경 변수 전달
```json
"env": {
  "UV_DEPS_CACHE": "%TEMP%\\uvcache"
}
```

### 타임아웃 조정
```json
// 느린 PC: MCP_TIMEOUT 환경변수 (ms)
"MCP_TIMEOUT": "10000"  // 10초
```

---

## 🔍 설치 검증 프로토콜

**모든 설치/설정 후 필수 검증:**
1. `claude mcp list` - 설치 목록 확인
2. `claude --debug` - 디버그 모드 2분 관찰
3. `/mcp` 명령 - 실제 작동 여부 확인

---

## 🗑️ MCP 서버 제거

```bash
# 예시: youtube-mcp 제거
claude mcp remove youtube-mcp
```

---

## 📚 관련 문서
- **글로벌 MCP 통합**: `~/.claude/MCP.md` - Context7, Sequential, Magic, Playwright 전략
- **메인 규칙**: [CLAUDE.md](CLAUDE.md) - 프로젝트 개발 규칙
- **SuperClaude 플래그**: [CLAUDE.md#페르소나](CLAUDE.md#🎭-superclaude-페르소나-자동-활성화) - --c7, --seq 플래그 활용
