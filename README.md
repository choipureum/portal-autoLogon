# portal-autoLogon
[![made-with-Markdown](https://img.shields.io/badge/Made%20with-Markdown-1f425f.svg)](http://commonmark.org)
[![Maintenance](https://img.shields.io/badge/Maintained%3F-yes-green.svg)](https://github.com/choipureum/CommitChecker/graphs/commit-activity) 
[![PRs Welcome](https://img.shields.io/badge/PRs-welcome-brightgreen.svg?style=flat-square)](http://makeapullrequest.com)
> 회사에서 매일 종료되는 
> 윈도우 및 포탈 때문에 
> 내가 열 받아서 만든
> 다중 url 기록 저장 && open, login 자동화

- 자주 사용하는 Chrome url을 저장 & 로그인 정보를 기록한 뒤 auto-login을 통해 process를 생성하고 kill을 할 수 있습니다.
- * 네이버 및 구글은 정책으로 인해 '2020.9'부터 캡챠에 걸려 자동 로그인 지원하지 않습니다.

## 핵심 기능  Key Feature
- url, id, pw를 저장해 하나의 chrome창에 여러개의 탭을 생성해 저장해둔 여러 url 로그인 또는 url process를 생성합니다. 
- 자주 사용하는 url을 마구마구 등록한 뒤 사용하세요.

- 모든 기록은 "C:/Users/autoLogin/key.txt"에 저장됩니다. 자동 디렉토리 
## 사용 How To Use
- 관리자 권한 자동 획득합니다.(c드라이브 접근 후 폴더생성)

- <b>'+'</b> : url, id, pw를 입력한 뒤 저장합니다. id, pw없이 url만 입력할 경우 로그인 로직은 실행하지 않습니다.
 
- <b>'-'</b> : listBox에서 특정 항목 선택한 뒤 클릭시 해당 정보를 삭제합니다.
 
- <b>'kill'</b> : 본 프로그램을 통해 생성된 모든 process를 kill합니다. * 단순 종료시에도 같은 로직 실행함.
 
- <b>'How to use'</b> : 사용방법

- <b>'로그인'</b> : auto 실행 * 끝날때까지 기다리세요 제발

## Contributing
- [@choipureum](https://github.com/choipureum)

## Reference
```
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Interactions;
```
## issues
- 자동 로그인의 경우 Mportal만 지원합니다.(selector)
- 향후 추가예정

## Links
- Repository: https://github.com/choipureum/portal-autoLogon
- Issue tracker: https://github.com/choipureum/portal-autoLogon/issues
  - 보안 취약점 등의 민감한 이슈인 경우 poo1994.imbc.com 로 연락주십시오. 
## Updates
- <b>version_1</b> : Mportal 자동 로그인 지원
- <b>version_2</b> : 경로 설정 디버깅,프로세스 오류 디버깅, 관리자권한 자동 취득, 넷플릭스 자동 로그인 지원 및 설치파일  
## Testing
### 자동 로그인
![image](https://user-images.githubusercontent.com/55127127/112962103-fb20bc00-9180-11eb-847b-4138b4b759fb.png)
### GUI
![image](https://user-images.githubusercontent.com/55127127/112961948-d2002b80-9180-11eb-8384-ab157446e541.png)
