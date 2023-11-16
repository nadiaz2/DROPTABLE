window.onload = async function (e) {
    e.preventDefault();
      // Request permission for iOS 13+ devices

  if (
    DeviceMotionEvent &&
    typeof DeviceMotionEvent.requestPermission === "function"
  ) {
    DeviceMotionEvent.requestPermission();
  }

    let webSocket = new WebSocket('wss://' + window.location.host);

    //receiving
    webSocket.onmessage = (event) => {
        let pc_message = JSON.parse(event.data);
        console.log(pc_message);
    };

    await until(() => webSocket.readyState === WebSocket.OPEN);

    const obj = { name: "PhoneFaceUp", message: 1 };
    webSocket.send(JSON.stringify(obj));
    console.log("1");
    window.addEventListener("deviceorientation", handleOrientation);
    console.log("2");
}

function handleOrientation(event) {
    console.log(event.absolute);
    if (event.beta > 100 || event.beta < -100) {
        window.location.href = 'pause.html';
    }
}

function until(conditionFunction) {

    const poll = resolve => {
        if (conditionFunction()) resolve();
        else setTimeout(_ => poll(resolve), 100);
    }

    return new Promise(poll);

}