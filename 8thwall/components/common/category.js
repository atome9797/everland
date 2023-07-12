// 트래킹 타입
const TrackingType = {
    FaceTracking: 'FaceTracking',
    NoneTracking: 'NoneTracking',
    PlaceTracking: 'PlaceTracking',
  }
  const TrackingParser = {
    'Pubao': {'TrackingType': TrackingType.FaceTracking, 'Sound': false, 'Background': false, 'Type': 'face-attachment', 'Point': 'noseBridge', 'scale': '1.8 1.8 1.8', 'position': '0 0.05 0.8', 'rotation': '0 180 0'},
    'LennyAndFriends': {'TrackingType': TrackingType.FaceTracking, 'Sound': false, 'Background': false, 'Type': 'face-item', 'scale': '0.2 0.2 0.2', 'position': '1 1 -0.5', 'rotation': '10 180 0', 'animation': 'JackAction'},
    'SummerEverland': {'TrackingType': TrackingType.NoneTracking, 'Sound': false, 'Background': true, 'Type': 'Event'},
    'Random': {'TrackingType': TrackingType.NoneTracking, 'Sound': false, 'Background': true, 'Type': 'None', 'Random': true},
    'Bombman': {'TrackingType': TrackingType.FaceTracking, 'Sound': false, 'Background': false, 'Type': 'face-attachment', 'Point': 'noseBridge', 'scale': '1.8 1.8 1.8', 'position': '0 -4 1.3', 'rotation': '0 180 0'},
  }
  export {TrackingType, TrackingParser}