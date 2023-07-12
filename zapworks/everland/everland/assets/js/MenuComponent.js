
    const itemActive = document.querySelectorAll('.item')
    
    function resetMenuActive()
    {
        for(let i = 0; i < 10; i++)
        {
            itemActive[i].classList.remove('active');
        }
    }
    

    const categoryList = {}

    const TrackingType = {
        FaceTracking: 'FaceTracking',
        NoneTracking: 'NoneTracking',
        PlaceTracking: 'PlaceTracking',
    }

    // json 파싱 faceTracking NoneTracking PlaceTracking
    const consertAndShow = [{'MonsterCleanup': TrackingType.FaceTracking}, {'Parade': TrackingType.FaceTracking},
    {'Fireworks': TrackingType.NoneTracking}, {'LennyAndFriends': TrackingType.PlaceTracking}]
    const zootopia = [{'PandaWithPose': TrackingType.PlaceTracking}, {'RedPandaWithPose': TrackingType.PlaceTracking}, {'WalkingTiger': TrackingType.PlaceTracking}]
    const attraction = [{'AmazonFilter': TrackingType.FaceTracking}, {'TexpressFilter': TrackingType.FaceTracking}]
    const fairyTown = [{'FairyTownFilter': TrackingType.FaceTracking}]
    const all = [{'MonsterCleanup': TrackingType.FaceTracking}, {'Parade': TrackingType.FaceTracking},
    {'Fireworks': TrackingType.NoneTracking}, {'LennyAndFriends': TrackingType.PlaceTracking},
    {'PandaWithPose': TrackingType.PlaceTracking}, {'RedPandaWithPose': TrackingType.PlaceTracking}, {'WalkingTiger': TrackingType.PlaceTracking},
    {'AmazonFilter': TrackingType.FaceTracking}, {'TexpressFilter': TrackingType.FaceTracking},
    {'FairyTownFilter': TrackingType.FaceTracking}]

    categoryList.consertAndShow = consertAndShow
    categoryList.zootopia = zootopia
    categoryList.attraction = attraction
    categoryList.fairyTown = fairyTown
    categoryList.all = all

    const category = ['consertAndShow', 'zootopia', 'attraction', 'fairyTown', 'all']

    const menuListComponent = (item, index) => {
        // 최대인 5개로 일단 구성

        const selectCategory = document.getElementById('selectCategory');
        selectCategory.setAttribute('value', item);

        for (let i = 0; i < 10; i++) {
            const menuItem = document.getElementById(`item-menu${i}`);
            const menuItemChild = document.querySelector(`#item-menu${i} .item`).getAttribute('value');
            menuItem.style.display = 'none'
            menuItem.classList.remove('swiper-slide');
            if (!(document.getElementById('cameraPosition').value === 'front' && menuItem.getAttribute('name') === 'PlaceTracking') && (menuItemChild == item || item === 'all')) {
                menuItem.style.display = 'block'
                menuItem.classList.add('swiper-slide')
            }
        }

        swiperContainer.update(); 

        // 카테고리 버튼 활성화 비활성화
        for (let i = 0; i < category.length - 1; i++) {
            if (index === i || index === category.length - 1) {
            document.getElementById(`category${i}`).setAttribute('class', 'active')
            } else {
            document.getElementById(`category${i}`).setAttribute('class', '')
            }
        }
        
        resetMenuActive();
    }

    for (let i = 0; i < category.length; i++) {
        const menuItem = document.querySelectorAll(`.${category[i]}`)
        menuItem.forEach((target) => target.addEventListener('click', () => {
                menuListComponent(category[i], i)}));
    }


    //소메뉴 클릭시 실행되는 컴포넌트
    const menuComponent = (item) => {
        const menuItemName = document.getElementById(`item-menu${item}`).getAttribute('value')
        const ItemType = document.getElementById(`item-menu${item}`).getAttribute('name')
        const selectTouchMenu = document.getElementById('selectTouchMenu')
        const selectTouchType = document.getElementById('selectTouchType')

        selectTouchMenu.setAttribute('value', menuItemName)
        selectTouchType.setAttribute('value', ItemType)

        myInstance.SendMessage('GameManager', 'SelectMenuEvent', menuItemName);

        //placetracking일땐 스왑버튼 비활성화
        const camBtn2 = document.querySelector('.camera_chg_btn_type2')
        
        if(ItemType === 'PlaceTracking')
        {
            camBtn2.style.display = 'none'
        }
        else{
            camBtn2.style.display = 'block'
        }

        //해당 버튼 active
        resetMenuActive();

        itemActive[item].classList.add('active');

    }


    for (let i = 0; i < 10; i++) {
        const menuItem = document.getElementById(`item-menu${i}`)
        menuItem.addEventListener('click', () => {
            menuComponent(i)
        })
    }

