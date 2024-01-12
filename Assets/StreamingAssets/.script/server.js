const { RTCPeerConnection, RTCSessionDescription, RTCIceCandidate } = require('wrtc'),
    { io } = require('socket.io-client'),
    { Server } = require('socket.io'),
    UUID = require('crypto').randomUUID(),
    PORT = 3500,
    WEB_URL = ""

// WebRTC variables
let _peer     // The RTCPeerConnection object connected to the other user
let _channel  // The data channel used to communicate to the peer

// ---------- Socket IO Server ----------
const unitySocketIO = new Server(PORT)
unitySocketIO.on('connection', function(socket) {
    console.log('connection')

    // Pass all messages from unity to phone
    socket.use((packet, next) => {
        let mainData = JSON.parse(packet[1])
        mainData.name = packet[0]

        let dataStr = JSON.parse(mainData)
        _channel.send(dataStr)  // send to phone
    })

    socket.on('disconnect', () => {
        console.log('Unity SocketIO disconnected')
    })

    socket.emit('UUID', UUID)
})
// -------- End Socket IO Server --------

// ------- WebRTC Phone Connection ------

// Create Socket.IO connection for WebRTC handshake
const socket = io(WEB_URL, {rejectUnauthorized: false})
socket.on('offer', handleOffer)
socket.on('ice-candidate', function(incoming) {
    const candidate = new RTCIceCandidate(incoming)
    _peer.addIceCandidate(candidate).catch(e => console.log(e))
})
socket.on('connect', function() {
    console.log('Connected!')
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
    const dataObj = JSON.parse(event.data)

    const name = dataObj.name
    delete dataObj.name
    const newDataStr = JSON.stringify(dataObj)

    unitySocketIO.emit(name, newDataStr)
}

function handleChannelOpen(event) {
    console.log('Phone WebRTC connected')
}

function handleChannelClose(event) {
    console.log('Phone connection closed')
}

// ----- End WebRTC Phone Connection ----