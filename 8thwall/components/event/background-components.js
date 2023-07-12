import {TrackingParser} from '../common/category'
import {loadingEnd, buttonClickEventFunc} from '../common/loading'
let eventData = false
let eventDataActive = false
let categoryMenu = false
let itemData = 0
let myReq
function rand() {
  return Math.floor(Math.random() * 10) % 4
}
function rendering(canvas, videoData) {
  function render() {
    canvas.width = videoData.videoWidth
    canvas.height = videoData.videoHeight
    canvas.getContext('2d').clearRect(0, 0, canvas.width, canvas.height)  // 캔버스 지우기
    canvas.getContext('2d').drawImage(videoData, 0, 0, canvas.width, canvas.height)  // 비디오 프레임 그리기
    myReq = requestAnimationFrame(render)
  }
  render()
}
// 랜덤 박스 알림 창
function randomBoxBar() {
  $('.randomBox_toast_msg').css('display', 'block')
  $('.randomBox_toast_msg').fadeOut(5000)
}
// 랜덤 박스 알림 창
function summerEventBar() {
  $('.summerEvent_toast_msg').css('display', 'block')
  $('.summerEvent_toast_msg').fadeOut(5000)
}
// 캔버스에 배경 추가
const DrawBackgound = (item) => {
  // 배경이 없을때는 실행안함
  cancelAnimationFrame(myReq)
  const canvastest = document.querySelector('#unity-canvas')
  canvastest.getContext('2d').clearRect(0, 0, canvastest.width, canvastest.height)  // 캔버스 지우기
  if (TrackingParser[$('.DefaultCategoryMenu').eq(item).attr('value')].Background) {
    const canvas = document.getElementById('unity-canvas')
    const video = document.getElementById('BackgroundVideo')
    let MenuName = $('.DefaultCategoryMenu').eq(item).attr('value')
    video.setAttribute('loop', 'loop')
    if (TrackingParser[$('.DefaultCategoryMenu').eq(item).attr('value')].Random) {
      const random = ['MDshop', 'Zootopia', 'Texpress', 'Bombman']
      MenuName = random[rand()]
      video.removeAttribute('loop')
      randomBoxBar()
    }
    // 새로운 비디오 url 추가
    video.src = null
    let videoType = 'webm'
    if (window.XR8.XrDevice.deviceEstimate().os === 'iOS') {
      videoType = 'mov'
    }
    if (eventDataActive) {
      // video.src = `https://uxstory.github.io/everland/${MenuName}Event.${videoType}`
      console.log('여기')
      const videoEvent = document.getElementById('BackgroundVideoEvent')
      videoEvent.currentTime = 0.1
      videoEvent.play()
      rendering(canvas, videoEvent)
    } else {
      video.src = `https://uxstory.github.io/everland/${MenuName}.${videoType}`
    }
  }
}
// 캔버스 초기화
const InitCanvas = () => {
  // 캔버스 초기화는 init으로 옮김
  if ($('#unity-canvas')) {
    {
      $('.konvajs-content').empty()
    }
    $('.konvajs-content').append('<canvas id = "unity-canvas" style="padding: 0px;margin: 0px;border: 0px;background: transparent;top: 0px;left: 0px;width: 100%;height: 100%;display: block;"></canvas>')
    console.log('캔버스 초기화 완료')
  }
  const canvas = document.querySelector('#unity-canvas')
  // 캔버스 레코더 초기화
  XR8.CanvasScreenshot.setForegroundCanvas(canvas)
  XR8.MediaRecorder.configure({
    maxDurationMs: 15000,
    foregroundCanvas: canvas,
    enableEndCard: false,
    requestMic: XR8.MediaRecorder.RequestMicOptions.AUTO,
  })
  const video = document.getElementById('BackgroundVideo')
  video.addEventListener('loadeddata', () => {
    console.log('로드됨')
    video.play()
    rendering(canvas, video)
    if (categoryMenu) {
      eventDataActive = true
      summerEventBar()
    } else {
      eventDataActive = false
    }
    setTimeout(() => {
      buttonClickEventFunc(true)
      loadingEnd()
    }, 1000)
  })
}
// 오른쪽눈 윙크하는 이벤트
const righteyewinkEvent = (Scene) => {
  Scene.addEventListener('xrrighteyewinked', () => {
    if (eventData && eventDataActive) {
      console.log('왼쪽 윙크')
      eventData = false
      DrawBackgound(itemData)
    }
  })
}
// 오른쪽눈 윙크하는 이벤트
const lefteyewinkEvent = (Scene) => {
  Scene.addEventListener('xrlefteyewinked', () => {
    if (eventData && eventDataActive) {
      console.log('오른쪽 윙크')
      eventData = false
      DrawBackgound(itemData)
    }
  })
}
// 입여는 이벤트
const mouthOpenEvent = (Scene) => {
  Scene.addEventListener('xrmouthopened', () => {
    if (eventData && eventDataActive) {
      console.log('입 열었을때 이벤트')
      eventData = false
      DrawBackgound(itemData)
    }
  })
}
let updating = false  // if the options are updating
let count = 0
let shakeForward = false
const shakeHeadEvent = (Scene) => {
  // 고개 흔드는 이벤트
  const update = () => {
    setTimeout(() => {
      // update state
      updating = false
      count = 0
    }, 2000)
  }
  // handle option selection
  const show = ({detail}) => {
    const xRot = detail.transform.rotation.w
    if (xRot < -0.1 && shakeForward) {
      shakeForward = !shakeForward
      count++
      console.log(count)
    } else if (xRot > 0.1 && !shakeForward) {
      shakeForward = !shakeForward
      count++
      console.log(count)
    }
    if (count >= 4) {
      count = 0
      // 이벤트 발동
      if (eventData && eventDataActive) {
        console.log('고개 흔들었을때 이벤트')
        eventData = false
        DrawBackgound(itemData)
      }
    }
    if (!updating) {
      updating = true
      // 2초 마다 업데이트 됨
      update()
    }
    // detail.transform.rotation = {w: 0, x: 0, y: 0, z: 0}
  }
  // setinterval로 2초안에 고개 흔들기 2번 왕복이면 처리되도록 하기
  Scene.addEventListener('xrfacefound', show)
  Scene.addEventListener('xrfaceupdated', show)
}
const setBackground = (item) => {
  eventDataActive = false
  if (TrackingParser[$('.DefaultCategoryMenu').eq(item).attr('value')].Type === 'Event') {
    eventData = true
    categoryMenu = true
  } else {
    eventData = false
    categoryMenu = false
  }
  DrawBackgound(item)
  itemData = item
}
const Eventload = () => {
  let videoType = 'webm'
  if (window.XR8.XrDevice.deviceEstimate().os === 'iOS') {
    videoType = 'mov'
  }
  const videoEvent = document.getElementById('BackgroundVideoEvent')
  videoEvent.src = `https://uxstory.github.io/everland/SummerEverlandEvent.${videoType}`
}
export {setBackground, mouthOpenEvent, shakeHeadEvent, InitCanvas, Eventload}