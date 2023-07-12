import {setBackground, mouthOpenEvent, shakeHeadEvent, InitCanvas, Eventload} from './background-components'
import {setTracking} from './tracking-components'
import {loadingStart, buttonClickEventFunc, buttonClickEvent} from '../common/loading'
const positionMode = {
  top: 0,
  down: 1,
}
const cameraWide = (position) => {
  if (position === positionMode.top) {
    $('.select_menu').css('display', 'block')
  } else {
    $('.select_menu').css('display', 'none')
  }
  for (let i = 0; i < $('.button_position').length; i++) {
    $('.button_position').eq(`${i}`).css('display', 'none')
    if (i === position) {
      $('.button_position').eq(`${i}`).css('display', 'block')
    }
  }
}
// 씬 로드시 버튼에 대한 동적 생성을 위해 필요한 컴포넌트
const menuSettingComponent = (SceneData) => {
  // 버튼 누를수 있는지 여부
  buttonClickEventFunc(true)
  // 입 열었을때 이벤트
  // mouthOpenEvent(SceneData)
  // lefteyewinkEvent(SceneData)
  // 얼굴 쉐이킹 이벤트 구독
  shakeHeadEvent(SceneData)
  // 동영상 로드 되면 로딩창 없어지게 설정
  Eventload()
  // 캔버스 초기화
  InitCanvas()
  // 하단 카테고리 메뉴 클릭시 이벤트
  $('.DefaultCategoryMenu').each((index) => {
    $('.DefaultCategoryMenu').eq(`${index}`).on('click', () => {
      if (buttonClickEvent) {
        buttonClickEventFunc(false)
        loadingStart()
        setTracking(index)
        setBackground(index)
        cameraWide(positionMode.down)
        // setSound(i)
      }
    })
  })
  // 씬 캔버스 위치 수정 => 메뉴 버튼에 따라 퍼센트로 높이 달라지게 설정 => fixed라 수정이 안됨
  $('#unity-container').append($('.a-canvas'))
  document.querySelector('.a-canvas').style.cssText = 'position:absolute !important'
  $('.a-canvas').css({'z-index': '-10'})
  // 화살표 버튼 누르면 메뉴 display none
  $('.arrow_down_btn').on('click', () => {
    cameraWide(positionMode.down)
  })
  $('.menu_chg_btn_bottom').on('click', () => {
    cameraWide(positionMode.top)
  })
}
export {menuSettingComponent}