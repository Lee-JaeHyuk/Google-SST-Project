# Google-SST-Project
## 구글의 SST API를 활용한 상지 재활 평가 <br/><br/>

### **1. 프로젝트 목표**

- 한국 몬트리올 인지평가(MoCa-K)는 경도인지장애를 평가하고자 만들어졌다. 주의력, 집중력, 실행력, 기억력, 어휘력, 시각 공간력, 추상력, 계산과 지남력 같은 인지 기능들을 평가한다.
- 이번 음성인식 활용에는 Google API의 Speech To Text를 이용했다. <br/><br/>


### **2. 프로젝트 개발 환경**

S/W 개발 환경
* - OS : Window 10
* - IDE : Visual Studio Community 2019, .NetFramwork
* - Language : WPF(C#, XAML)

H/W 개발 환경
* - Device : Samsung mic  <br/><br/>

### **3. 프로젝트 적용 기술**

#### 3-1. STT API
- Google VS Naver  
웹 기반 콘솔에서 서비스를 등록하여 음성인식을 하는 API중에 대표적인 2개 네이버 클로버와 구글을 비교해보았다.

- 실험 환경  
각기 다른 사람 5명의 목소리 데이터를 갖고 실험  
각 사람마다 총 2번의 실습 내용을 거친다.  
실시간, 녹음 형태로 실험 진행  

- 실험 결과

1. 음절 (한글 14개 음절 입력)  

![1](https://user-images.githubusercontent.com/60414900/107150084-63cc9300-699f-11eb-8ec3-ddce7397c834.JPG)
<br/><br/>

2. 단어 (과일, 탈 것, 운송수단, 측량도구, 사자 5개 단어 입력)  

![2](https://user-images.githubusercontent.com/60414900/107150117-8eb6e700-699f-11eb-8eeb-7353ca7743f9.JPG)
<br/><br/>

3. 문장 (명령어 2개 입력)  

![3](https://user-images.githubusercontent.com/60414900/107150136-a8f0c500-699f-11eb-92a2-52cc163cd6c8.JPG)
<br/><br/>

- Google STT 사용 이유  
당시 프로젝트를 진행 당시 Naver Clova의 경우 HTTP 통신 2.0 이 지원되지 않아 실시간 음성에 대한 처리를 하지 못하였다.  
따라서 실시간 시험으로 진행되는데 사용되는 프로젝트인 Mock-k test의 부적합하여 Google STT APi 를 사용하게 되었다.














