import '../../css/index.css'
import '../../css/common.css'
import {cameraComponent} from '../../components/event/camera-components'
import {menuSettingComponent} from '../../components/event/menu-setting-components'
import {initMediaPreview} from '../../components/record/media-preview'
import {initRecordButton} from '../../components/record/record-button'
import * as htmlContent from './main.html'
const create = (SceneData) => {
  cameraComponent(SceneData)
  menuSettingComponent(SceneData)
  initMediaPreview()
  initRecordButton()
}
const MediaRecorder = () => ({
  init() {
    document.body.insertAdjacentHTML('beforeend', htmlContent)
    create(this.el.sceneEl)
  },
})
export {
  MediaRecorder,
}
