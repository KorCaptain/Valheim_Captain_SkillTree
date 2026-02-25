---
name: cst-mcp-setup
description: Use when setting up or configuring MCP servers for this project. Triggers: MCP setup, MCP 설정, project MCP, MCP 프로젝트 설치
---

## 핵심 규칙 요약

- 프로젝트 특화 MCP 설치 절차 - 글로벌 설정은 `~/.claude/MCP.md` 참조
- 환경 확인 우선: OS(Windows), 환경(WSL/PowerShell/CMD) 파악
- user 스코프로 설치: `claude mcp add --scope user {name} -- npx -y {pkg}`
- Windows 경로: JSON 내 `\\\\` 이스케이프 필수
- 설치 검증: `claude mcp list` → `echo "/mcp" | claude --debug`

**전체 문서**: `C:\home\ssunyme\.npm-global\bin\CaptainSkillTree\md\MCP_PROJECT_SETUP.md`
