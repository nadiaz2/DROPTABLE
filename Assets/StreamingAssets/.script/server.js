const { RTCPeerConnection, RTCSessionDescription, RTCIceCandidate } = require('wrtc'),
    { io } = require('socket.io-client'),
    { Server } = require('socket.io'),
    UUID = require('crypto').randomUUID(),
    PORT = 3500,
    WEB_URL = "https://echoes-through-the-screen-8aqb2.ondigitalocean.app/"

// WebRTC variables
let _peer     // The RTCPeerConnection object connected to the other user
let _channel  // The data channel used to communicate to the peer

// ---------- Socket IO Server ----------
const unitySocketIO = new Server(PORT)
unitySocketIO.on('connection', function(socket) {
    console.log('Socket.IO connected to Unity')

    // Pass all messages from unity to phone
    socket.use((packet, next) => {
        if(packet[0] === "SERVER") {
            next()
        }

        // send to phone
        _channel.send(`${packet[0]}-${packet[1]}`)
    })

    socket.on('disconnect', () => {
        console.log('Unity SocketIO disconnected')
    })

    let uuidRecieved = false
    socket.on("SERVER", (msg) => {
        if(msg === "UUID Received") {
            uuidRecieved = true
        }
    })

    //socket.emit('UUID', UUID)
    let uuidInterval = setInterval(() => {
        if(uuidRecieved) {
            clearInterval(uuidInterval)
        }
        socket.emit('UUID', UUID)
        console.log("sending uuid")
    }, 500)
})
// -------- End Socket IO Server --------

// ------- WebRTC Phone Connection ------

// Create Socket.IO connection for WebRTC handshake
const socket = io(WEB_URL, {rejectUnauthorized: false})
socket.on('offer', handleOffer)
socket.on('ice-candidate', function(incoming) {
    const candidate = new RTCIceCandidate(incoming)
    _peer.addIceCandidate(candidate).catch(e => console.error(`Error adding ICE candidate: ${incoming}`))
})
socket.on('connect', function() {
    console.log('Socket.IO connected to Web Server')
    socket.emit('create room', UUID)
})

function handleOffer(incoming) {
    _peer = createPeer(incoming.caller)

    const desc = new RTCSessionDescription(incoming.sdp)
    _peer.setRemoteDescription(desc).then(() => {
        return _peer.createAnswer()
    }).then((answer) => {
        return _peer.setLocalDescription(answer)
    }).then(() => {
        const payload = {
            target: incoming.caller,
            caller: socket.id,
            sdp: _peer.localDescription
        }
        socket.emit('answer', payload)
    })
}

function createPeer(partnerID) {
    const peer = new RTCPeerConnection({
        iceServers: [
            {
                urls: 'stun:stun.stunprotocol.org'
            }
        ]
    })
    peer.onicecandidate = (e) => handleICECandidateEvent(e, partnerID)
    peer.ondatachannel = handleDataChannelEvent // runs when the remote peer calls .createDataChannel()

    return peer
}

function handleICECandidateEvent(e, partnerID) {
    if(e.candidate) {
        const payload = {
            target: partnerID,
            candidate: e.candidate
        }
        socket.emit('ice-candidate', payload)
    }
}


// RTCDataChannel event handlers
function handleDataChannelEvent(e) {
    _channel = e.channel
    _channel.onmessage = handleChannelMessage
    _channel.onopen = handleChannelOpen
    _channel.onclose = handleChannelClose
}

function handleChannelMessage(event) {
    console.log('WebRTC message recieved')
    const str = event.data
    const index = str.indexOf('-')
    const name = str.substring(0, index)
    const command = str.substring(index+1)
    unitySocketIO.emit(name, command)
}

function handleChannelOpen(event) {
    console.log('Phone WebRTC connected')
}

function handleChannelClose(event) {
    console.log('Phone WebRTC connection closed')
}

// ----- End WebRTC Phone Connection ----