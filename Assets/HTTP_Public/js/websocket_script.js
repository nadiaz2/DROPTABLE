window.onload = async function() {
	let webSocket = new WebSocket('ws://'+window.location.host)

	webSocket.onmessage = (event) => {
		console.log(event.data);
	};

	await until(() => webSocket.readyState == WebSocket.OPEN)
	//webSocket.send("Hello!")

	document.querySelector('button').onclick = () => webSocket.send("Hello!")
}

const currentUrl = window.location.href
const ipAddress = currentUrl.split("//")[1].split(':')[0]
console.log(currentUrl)
console.log(ipAddress)
console.log(window.location.host)

const simpleIPAddress = window.location.host.split(':')[0]
console.log(simpleIPAddress)

function until(conditionFunction) {

	const poll = resolve => {
		if(conditionFunction()) resolve();
		else setTimeout(_ => poll(resolve), 100);
	}

	return new Promise(poll);

}