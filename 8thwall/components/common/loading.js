let buttonClickEvent = false
const buttonClickEventFunc = (check) => {
  buttonClickEvent = check
}
// 로딩바 나오는 이벤트
const loadingStart = () => {
  $('.load_ani').css({'display': 'block'})
  $('.top_sect').css('pointer-events', 'none')
  $('.btm_sect').css('pointer-events', 'none')
}
// 로딩바 사라지는 이벤트
const loadingEnd = () => {
  $('.load_ani').css({'display': 'none'})
  $('.top_sect').css('pointer-events', 'auto')
  $('.btm_sect').css('pointer-events', 'auto')
}
export {loadingStart, loadingEnd, buttonClickEventFunc, buttonClickEvent}
