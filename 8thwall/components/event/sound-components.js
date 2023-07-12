import {TrackingParser} from '../common/category'
// 사운드 설정
function soundControl(menuItemName, AudioSource) {
  if (AudioSource) {
    $('#backgroundSound').removeAttr('src')
    $('#backgroundSound').attr('src', `#${menuItemName}Audio`)
    $('#backgroundSound').attr('volume', '1')
  } else {
    $('#backgroundSound').attr('volume', '0')
  }
}
const setSound = (item) => {
  soundControl($(`#item-menu${item}`).attr('value'), TrackingParser[$(`#item-menu${item}`).attr('value')].Sound)
}
export {setSound}
