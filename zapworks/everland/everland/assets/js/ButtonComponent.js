const ButtonPosition = {
    up: 0,
    down: 1,
}

const MenuPosition = {
    up:0,
    middle:1,
    down:2,
}

const captureShareSwap = {
    capture:0,
    share:1,
}

const wrapMode = {
    default:0,
    video:1,
}

const recordMode = {
    before:0,
    after:1,
}


function captureShareModeSwap(mode){
    const captureShareMode = document.querySelectorAll('.captureShareMode')
    for(let i = 0; i< 2; i++)
    {
        captureShareMode[i].style.display = 'none';
        if(i === mode)
        {
            captureShareMode[i].style.display = 'block';
        }
    }
}

function swapButtonStyle(){
    const selectTouchType = document.getElementById('selectTouchType')
    const camBtn2 = document.querySelector('.camera_chg_btn_type2')
            
    if(selectTouchType === 'PlaceTracking')
    {
        camBtn2.style.display = 'none'
    }
    else{
        camBtn2.style.display = 'block'
    }
}

function recordStyle(recordCheck)
{
    const recordBefore = document.querySelector('.recordBefore')
    const recordAfter = document.querySelector('.recordAfter')
    const recordBeforeTimer = document.querySelector('.recordBeforeTimer')
    const recordAfterTimer = document.querySelector('.recordAfterTimer')
    
    recordBefore.style.display = 'none';
    recordAfter.style.display = 'none';
    recordBeforeTimer.style.visibility = 'hidden';
    recordAfterTimer.style.visibility = 'hidden';

    if(recordMode.before === recordCheck)
    {
        recordBefore.style.display = 'block';
        recordBeforeTimer.style.visibility = 'visible';
    }
    else
    {
        recordAfter.style.display = 'block';
        recordAfterTimer.style.visibility = 'visible';
    }

}


const defaultCategoryMenuActive = () => {
    for(let i = 0; i < 3; i++)
    {
        document.getElementById(`select_menu${i}`).style.display = 'block';
        if(MenuPosition.down === i)
        {
            document.getElementById(`select_menu${i}`).style.display = 'none';
        }
    }

    for(let i = 0; i < 2; i++)
    {
        document.querySelector(`.button_position${i}`).style.display = 'none';
        if(ButtonPosition.down === i)
        {
            document.querySelector(`.button_position${i}`).style.display = 'block';
        }
    }
}

const arrowDownBtnActive = () => {
    for(let i = 0; i < 3; i++)
    {
        document.getElementById(`select_menu${i}`).style.display = 'none';
        if(MenuPosition.down === i)
        {
            document.getElementById(`select_menu${i}`).style.display = 'block';
        }
    }
    for(let i = 0; i < 2; i++)
    {
        document.querySelector(`.button_position${i}`).style.display = 'none';
        if(ButtonPosition.up === i)
        {
            document.querySelector(`.button_position${i}`).style.display = 'block';
        }
    }

    //카메라 전환 버튼 활성화 / 비활성화
    const camBtn = document.querySelector('.camera_chg_btn')
    const selectTouchType = document.getElementById('selectTouchType').getAttribute('value')

    if(selectTouchType === 'PlaceTracking')
    {
        camBtn.style.display = 'none'
    }else{
        camBtn.style.display = 'block'
    }
}

const swapVideoCatureMode = () => {
    const pictureMode = document.querySelectorAll('.pictureMode')
    const videoMode = document.querySelectorAll('.videoMode')

    for(let i = 0; i <2; i++)
    {
        pictureMode[i].style.display = 'none';
        videoMode[i].style.display = 'block';
    }
}

const swapImgCatureMode = () => {
    const pictureMode = document.querySelectorAll('.pictureMode')
    const videoMode = document.querySelectorAll('.videoMode')

    for(let i = 0; i <2; i++)
    {
        pictureMode[i].style.display = 'block';
        videoMode[i].style.display = 'none';
    }
}

const pictureShoot = () => {

    myInstance.SendMessage('GameManager', 'StartCapture');

    //공유모드로 전환
    captureShareModeSwap(captureShareSwap.share);
    
}

const videoShoot = (index) => {

    //동영상 촬영 페이지로 이동
    for(let i = 0; i< 2; i++)
    {
        document.querySelector(`.wrapMode${i}`).style.display = 'none';
        if(index === i)
        {
            document.querySelector(`.wrapMode${i}`).style.display = 'block';
        }
    }

    //카메라 전환 버튼 비활성화
    if(index === wrapMode.default)
    {
        swapButtonStyle();
    }
    else
    {
        document.querySelector('.camera_chg_btn_type2').style.display = 'none';
    }
}

const SaveMode = () => {

    console.log(document.getElementById('captureImg').style.display);
    //비디오 일때
    if($('#captureImg').css("display") == 'none')
    {
        var a = $("<a>")
            .attr("href", window.videoRecUrl)
            .attr("download", "UXStory.mp4")
            .appendTo("body");

            a[0].click();

            a.remove();
    }
    else
    {
        const zapparSaveButton = document.getElementById('zapparSaveButton');
        if(zapparSaveButton)
        {
            zapparSaveButton.click();
        }
    }
}


async function shareCanvasAsImage() {

    // Get canvas as dataURL
    const dataUrl = window.videoRecUrl;
  
    // Convert dataUrl into blob using browser fetch API
    const blob = await (await fetch(dataUrl)).blob()
  
    // Create file form the blob
    const videoFile = new File([blob], 'uxstory.mp4', { type: blob.type })
  
    // Check if the device is able to share these files then open share dialog
    if (navigator.canShare && navigator.canShare({ files: [videoFile] })) {
      try {
        await navigator.share({
          files: [videoFile]
        })
      } catch (error) {
        console.log('Sharing failed!', error)
      }
    } else {
      console.log('This device does not support sharing files.')
    }
  }

const ShareMode = () => {

    //비디오일때
    if($('#captureImg').css("display") == 'none')
    {
        shareCanvasAsImage();
    }
    else
    {
        const zapparShareButton = document.getElementById('zapparShareButton');
        if(zapparShareButton)
        {
            zapparShareButton.click();
        }
    }

}


const ReturnMode = () => {
    //1. 뒤로가기 버튼 이벤트
    const zapparCloseAref = document.getElementById('zapparCloseAref');

    if(zapparCloseAref)
    {
        zapparCloseAref.click();
    }
    
    //2. 이미지 비활성화 및 비디오 비활성화
    document.getElementById('captureImg').style.display = 'none';
    document.getElementById('captureVideo').style.display = 'none';

    //3. 버튼 ui 변경 (이전 모드를 기억하고 있어야 함)
    //촬영모드로 전환
    captureShareModeSwap(captureShareSwap.capture);
}


var timer;
var hour = 0;
var minute = 0;
var second = 0;

function setTimerText()
{
    let secondstr;
    let minutestr;
    let hourstr;

    const timer_wrap = document.querySelectorAll('.timer_wrap')

    if(second < 10)
    {
        secondstr = `0${second}`;
    }
    else
    {
        secondstr = second;
    }

    if(minute < 10)
    {
        minutestr = `0${minute}`;
    }
    else
    {
        minutestr = minute;
    }
    if(hour < 10)
    {
        hourstr = `0${hour}`;
    }
    else
    {
        hourstr = hour;
    }

    for(let i = 0; i< 2; i++)
    {
        timer_wrap[i].innerText = `${hourstr}:${minutestr}:${secondstr}`; 
    }
}

function videoTimer(){
    //1초마다 시간 갱신 되도록 실행
    second = second + 1;
    if(second >= 60)
    {
        minute = minute + 1;
        second = 0;
    }

    if(minute >= 60)
    {
        hour =  hour + 1;
        minute = 0;
    }


    setTimerText();
    
}

function resetTimer()
{
    hour = 0;
    minute = 0;
    second = 0;
    setTimerText();
}

var recoderTest;


function recordCanvasUsingRecordRTC(canvas) {

    let device;
    if( /Android/i.test(navigator.userAgent)) {
        device = 'video/webm;codecs=vp8'
    } else if (/iPhone|iPad|iPod/i.test(navigator.userAgent)) {
        device = 'video/webm;codecs=vp9'
    } else {
        device = 'video/webm;codecs=vp9'
    }


    var recorder = RecordRTC(canvas, {
        type:'canvas',
        mimeType : device,
        audioBitsPerSecond: 128000,
        videoBitsPerSecond: 128000
    });
    recorder.startRecording();

    return recorder;
    // recoderTest = await ZapparVideoRecorder.createCanvasVideoRecorder(canvas, {
    //     quality: 25,
    //     speed: 10,
    //     halfSample: true,
    // });
    // recoderTest.start();
}

function stopRecordCanvasUsingRecordRTC(recorder){

    recorder.stopRecording(function(url) {
        var blob = recorder.getBlob();
        console.log('blob', blob);

        var video = document.getElementById('captureVideo');
        video.src = URL.createObjectURL(blob);
        video.style.display = 'block';
        video.controls = true;
        video.play();
        
        window.videoRecUrl = URL.createObjectURL(blob);
        window.recInitialized = true;
        
        //myInstance.SendMessage('GameManager', 'RTCStopRecord');
    });
    // recoderTest.stop();

    // recoderTest.onComplete.bind(async (res) => {
    //     window.videoRecUrl = await res.asDataURL();
    //     window.recInitialized = true;
    //     myInstance.SendMessage('GameManager', 'RTCStopRecord');
    // });
}




const recMode = () => {
    
    //1. 녹화 버튼 눌렀을때 타이머 설정
    timer = setInterval(videoTimer,1000);

    //2. 녹화 이벤트 실행
    //myInstance.SendMessage('GameManager', 'StartRecord');
    recoderTest = recordCanvasUsingRecordRTC(canvas);

    //3. 일시정지 버튼 활성화
    recordStyle(recordMode.after);
}   

const stopRecMode = () => {
    //1. 녹화 정지 버튼 눌렀을때 타이머 정지
    clearInterval(timer);

    //2. 시간 초기화
    resetTimer();

    //3. 공유하기 페이지로 이동
    //myInstance.SendMessage('GameManager', 'StopRecord');
    stopRecordCanvasUsingRecordRTC(recoderTest);

    //4. 공유 모드로 전환
    videoShoot(wrapMode.default);
    captureShareModeSwap(captureShareSwap.share);

    //5. 카메라 전환버튼 활성화
    swapButtonStyle();

    //6. 일시 정지 버튼 비활성화
    recordStyle(recordMode.before);
}


//카테고리 클릭했을때
const defaultCategoryMenu = document.querySelectorAll('.DefaultCategoryMenu')
//화살표 아래 버튼 눌렀을때
const arrowDownBtn = document.querySelector('.arrow_down_btn')
//촬영 전환버튼 클릭했을때
const swapVideoCatureModeBtn = document.querySelector('.video_chg_btn')
const swapImgCatureModeBtn = document.querySelector('.picture_chg_btn')
//캡처 버튼 눌렀을때
const pictureShootBtn = document.querySelector('.picture_shoot_btn') 
//동영상 버튼 눌렀을때
const videoShootBtn = document.querySelector('.video_shoot_btn') 
//저장 버튼 눌렀을때
const SaveBtn = document.querySelector('.save_btn') 
//공유하기 버튼 눌렀을때
const shareBtn = document.querySelector('.share_btn')
//뒤로 가기 버튼 눌렀을때
const returnBtn = document.querySelector('.return_btn')
//녹화 버튼 눌렀을때
const recBtn = document.querySelector('.rec_btn')
//정지 버튼 눌렀을때
const stopBtn = document.querySelector('.stop_btn')

defaultCategoryMenu.forEach((target) => target.addEventListener('click', defaultCategoryMenuActive));
arrowDownBtn.addEventListener('click', arrowDownBtnActive)
swapVideoCatureModeBtn.addEventListener('click', swapVideoCatureMode)
swapImgCatureModeBtn.addEventListener('click', swapImgCatureMode)
pictureShootBtn.addEventListener('click', pictureShoot)
videoShootBtn.addEventListener('click',  () => {videoShoot(wrapMode.video)})
SaveBtn.addEventListener('click', SaveMode)
shareBtn.addEventListener('click', ShareMode)
returnBtn.addEventListener('click', ReturnMode)
recBtn.addEventListener('click', recMode)
stopBtn.addEventListener('click', stopRecMode)
