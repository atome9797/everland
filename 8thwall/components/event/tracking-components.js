import {TrackingType, TrackingParser} from '../common/category'
import {loadingEnd, buttonClickEventFunc} from '../common/loading'
// 얼굴 트래킹 세팅
function SetXrface(trackingName, faceType) {
  $(`#${faceType}`).empty()
  let innerText = ''
  if (faceType === 'face-paint') {
    innerText += `<xrextras-face-mesh id="face-paint-model" material-resource="#${trackingName}Paint"></xrextras-face-mesh>`
  } else if (faceType === 'face-item') {
    innerText += `<a-entity
        gltf-model="#head-occluder"
        position="0 0 0.02"
        xrextras-hider-material
      ></a-entity>
      <a-entity
        id="face-item-model"
        gltf-model="#${trackingName}Item"
        position="${TrackingParser[trackingName].position}"
        scale="${TrackingParser[trackingName].scale}"
        rotation="${TrackingParser[trackingName].rotation}"
        animation-mixer="clips: [${TrackingParser[trackingName].animation}]; loop: repeat">
      </a-entity>`
  } else if (faceType === 'face-attachment') {
    innerText += `<a-entity
        gltf-model="#head-occluder"
        position="0 0 0.02"
        xrextras-hider-material
      ></a-entity>
      <xrextras-face-attachment id="attachmentPoint" point="${TrackingParser[trackingName].Point}">
        <a-entity
          id="face-attachment-model"
          gltf-model="#${trackingName}Item"
          scale="${TrackingParser[trackingName].scale}"
          position="${TrackingParser[trackingName].position}"
          rotation="${TrackingParser[trackingName].rotation}"
        >
        </a-entity>
      </xrextras-face-attachment>`
  }
  document.getElementById(faceType).insertAdjacentHTML('beforeend', innerText)
  $(`#${faceType}`).attr('visible', 'true')
  document.getElementById(`${faceType}-model`).addEventListener('model-loaded', () => {
    console.log('로드됨')
    setTimeout(() => {
      buttonClickEventFunc(true)
      loadingEnd()
    }, 1000)
  })
  // 로드 실패 원인 파악
  document.getElementById(`${faceType}-model`).addEventListener('model-error', (result) => {
    console.log('실패')
    console.log(result)
  })
}
function SetXrface2(trackingName, trackingType) {
  $('.faceTracking').each((index) => {
    $('.faceTracking').eq(`${index}`).attr('visible', false)
  })
  if (trackingType === TrackingType.NoneTracking) {
    return
  }
  $(`.${trackingName}`).each((index) => {
    $(`.${trackingName}`).eq(`${index}`).attr('visible', true)
  })
  setTimeout(() => {
    buttonClickEventFunc(true)
    loadingEnd()
  }, 1000)
}
const setTracking = (item) => {
  // const positionNameArr = TrackingParser[$('.DefaultCategoryMenu').eq(`${item}`).attr('value')].Type.split('/')
  const trackingType = TrackingParser[$('.DefaultCategoryMenu').eq(`${item}`).attr('value')].TrackingType
  // 트래킹 초기화
  const FaceTrackingType = ['face-paint', 'face-item', 'face-attachment']
  for (let i = 0; i < FaceTrackingType.length; i++) {
    $(`#${FaceTrackingType[i]}`).attr('visible', 'false')
  }
  SetXrface2($('.DefaultCategoryMenu').eq(`${item}`).attr('value'), trackingType)
  // 얼굴 트래킹 세팅
  //
  // for (let i = 0; i < positionNameArr.length; i++) {
  // SetXrface($('.DefaultCategoryMenu').eq(`${item}`).attr('value'), positionNameArr[i])
  // }
  //
}
export {setTracking}