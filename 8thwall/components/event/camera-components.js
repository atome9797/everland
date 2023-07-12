import {setCaptureMode, captureMode} from '../../components/record/record-button'
import {loadingStart, loadingEnd} from '../common/loading'
const wrapMode = {
  video: 0,
  default: 1,
}
const recordMode = {
  before: 0,
  after: 1,
}
// 다운로드 결과 애니메이션 창
function downloadResultBar() {
  $('.toast_msg').css('display', 'block')
  $('.toast_msg').fadeOut(5000)
}
const getTimestamp = () => {
  const now = new Date()
  return [now.getHours(), now.getMinutes(), now.getSeconds()]
    .map(n => n.toString().padStart(2, '0')).join('')
}
let timer
let hour = 0
let minute = 0
let second = 0
function setTimerText() {
  let secondstr
  let minutestr
  let hourstr
  if (second < 10) {
    secondstr = `0${second}`
  } else {
    secondstr = second
  }
  if (minute < 10) {
    minutestr = `0${minute}`
  } else {
    minutestr = minute
  }
  if (hour < 10) {
    hourstr = `0${hour}`
  } else {
    hourstr = hour
  }
  for (let i = 0; i < $('.timer_wrap').length; i++) {
    $('.timer_wrap').eq(`${i}`).text(`${hourstr}:${minutestr}:${secondstr}`)
  }
}
function videoTimer() {
  // 1초마다 시간 갱신 되도록 실행
  second += 1
  if (second >= 60) {
    minute += 1
    second = 0
  }
  if (minute >= 60) {
    hour += 1
    minute = 0
  }
  setTimerText()
}
function resetTimer() {
  hour = 0
  minute = 0
  second = 0
  setTimerText()
  console.log('초기화')
  $('.timer_wrap').eq('1').css('visibility', 'hidden')
}
// 엘리먼트 활성화 비활성화
const displaySetting = (className, type) => {
  for (let i = 0; i < $(`.${className}`).length; i++) {
    $(`.${className}`).eq(`${i}`).css('display', type)
  }
}
// 비디오 이미지 캡처 스왑 버튼
const swapVideoCatureMode = (check) => {
  if (check) {
    setCaptureMode('fixed')
    displaySetting('pictureMode', 'none')
    displaySetting('videoMode', 'block')
  } else {
    setCaptureMode('photo')
    displaySetting('pictureMode', 'block')
    displaySetting('videoMode', 'none')
  }
}
// 이미지 다운로드
function downloadURIPicture(uri) {
  loadingStart()
  const link = document.createElement('a')
  link.download = `capture-${getTimestamp()}.jpg`
  link.href = uri
  document.body.appendChild(link)
  link.addEventListener('click', () => {
    document.body.removeChild(link)
    loadingEnd()
    $('#toast_msg_text').text('사진이 저장되었습니다.')
    downloadResultBar()
  })
  link.click()
}
// 동영상 다운로드
function downloadURIVideo(uri) {
  loadingStart()
  const xhr = new XMLHttpRequest()
  xhr.open('GET', uri, true)
  xhr.responseType = 'blob'
  xhr.onload = function (e) {
    if (this.status === 200) {
      const a = document.createElement('a')
      a.href = document.getElementById('videoPreview').src
      a.download = `capture-${getTimestamp()}.mp4`
      document.body.appendChild(a)
      a.addEventListener('click', () => {
        document.body.removeChild(a)
        loadingEnd()
        // 다운로드 완료시 처리
        $('#toast_msg_text').text('동영상이 저장되었습니다.')
        downloadResultBar()
      })
      a.click()
    }
  }
  xhr.send()
}
// 녹화 모드 활성화 비활성화
const swapRecordMode = (index) => {
  if (wrapMode.video === index) {
    $('.captureShareMode').css('display', 'none')
    $('#toast_msg_text').text('15초간 촬영됩니다.')
    downloadResultBar()
  } else {
    $('.captureShareMode').css('display', 'block')
  }
  // 동영상 촬영 페이지로 이동
  for (let i = 0; i < $('.wrapMode').length; i++) {
    $('.wrapMode').eq(`${i}`).css('display', 'none')
    if (index === i) {
      $('.wrapMode').eq(`${i}`).css('display', 'block')
    }
  }
}
// 캡처 이벤트
const pictureShoot = () => {
  // 캡처
  XR8.CanvasScreenshot.takeScreenshot().then(
    (data) => {
      // myImage is an <img> HTML element
      const image = document.getElementById('captureImg')
      image.src = `data:image/jpeg;base64,${data}`
      window.imageUrl = image.src
      $('#captureImg').css('display', 'block')
    },
    (error) => {
      console.log(error)
      // Handle screenshot error.
    }
  )
  swapRecordMode(wrapMode.default)
}
// 비디오/ 사진 저장
const SaveMode = () => {
  if (captureMode === 'fixed') {
    downloadURIVideo(window.videoRecUrl)
  } else if (captureMode === 'photo') {
    downloadURIPicture(window.imageUrl)
  }
}
// 비디오 공유하기
async function shareCanvasAsImage() {
  let dataUrl = ''
  let filename = ''
  if ($('#captureImg').css('display') === 'none') {
    dataUrl = window.videoRecUrl
    filename = 'UXStory.mp4'
  } else {
    dataUrl = window.imageUrl
    filename = 'UXStory.png'
  }
  // Convert dataUrl into blob using browser fetch API
  const blob = await (await fetch(dataUrl)).blob()
  // Create file form the blob
  const dataFile = new File([blob], filename, {type: blob.type})
  // Check if the device is able to share these files then open share dialog
  if (navigator.canShare && navigator.canShare({files: [dataFile]})) {
    try {
      await navigator.share({
        files: [dataFile],
      })
    } catch (error) {
      console.log('Sharing failed!', error)
    }
  } else {
    console.log('This device does not support sharing files.')
  }
}
// 공유하기 이벤트
const ShareMode = () => {
  shareCanvasAsImage()
}
// 뒤로가기 이벤트
const ReturnMode = () => {
  // 1. 이미지 비활성화 및 비디오 비활성화
  // document.getElementById('captureImg').style.display = 'none';
  // document.getElementById('captureVideo').style.display = 'none';
  // 2. 버튼 ui 변경 (이전 모드를 기억하고 있어야 함)
  // 촬영모드로 전환
  swapRecordMode(wrapMode.default)
}
let recModeCheck = false
// 녹화 이벤트
const recMode = () => {
  // 1. 녹화 버튼 눌렀을때 타이머 설정
  if (!recModeCheck) {
    timer = setInterval(videoTimer, 1000)
    recModeCheck = true
  }
}
// 녹화 종료 이벤트
const stopRecMode = () => {
  recModeCheck = false
  // 1. 녹화 정지 버튼 눌렀을때 타이머 정지
  clearInterval(timer)
  $('.timer_wrap').eq('1').css('visibility', 'visible')
}
// 카메라 이벤트 제어
const cameraComponent = (SceneData) => {
  const scene = SceneData
  // 촬영 전환 버튼 클릭 이벤트
  $('.video_chg_btn').on('click', () => {
    swapVideoCatureMode(true)
  })
  $('.picture_chg_btn').on('click', () => {
    swapVideoCatureMode(false)
  })
  // 녹화 버튼 눌렀을때 뜨는 페이지
  $('.video_shoot_btn').on('click', () => {
    swapRecordMode(wrapMode.video)
    resetTimer()
  })
  // 공유하기 이벤트 버튼
  // $('.share_btn').on('click', ShareMode)
  // 뒤로가기 이벤트 버튼
  $('#closePreviewButton').on('click', ReturnMode)
}
export {cameraComponent, recMode, stopRecMode, SaveMode, downloadResultBar}