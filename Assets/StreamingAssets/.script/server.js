console.log(__dirname)

const express = require('express'),
    app = express(),
    https = require('https'),
    http = require('http'),
    fs = require('fs'),
    { Server } = require('socket.io')

app.use(express.static('public'))

app.get('/', (req, res) => {
    res.redirect('/before.html')
})

const options = {
    pfx: fs.readFileSync('Droptable.pfx'),
    passphrase: '123456',
}

const httpServer = https.createServer(options, app, (req, res) => {
    res.writeHead(200)
    res.end('hello world\n')
})

// ---------- Socket IO ----------
const insecureServer = http.createServer((req, res) => {
    res.writeHead(200)
    res.end('hello world\n')
})
const secureIO = new Server(httpServer)
const insecureIO = new Server(insecureServer)
let unityClient = null
let phoneClient = null

const onConnect = socket => {
    console.log('connection')

    // Pass all messages between phone and unity
    socket.use((packet, next) => {
        //console.log(packet)
        // Handler
        //console.log(socket === phoneClient)

        if(packet[0] !== 'Device') {
            next()
        } 
        
        if((socket === unityClient) && (phoneClient !== null)) {
            phoneClient.emit(packet[0], packet[1])
            console.log('To Phone:', packet)
        } else if((socket === phoneClient) && (unityClient !== null)) {
            unityClient.emit(packet[0], packet[1])
            console.log('To Unity:', packet)
        } else {
            console.log('To Neither:', packet)
        }

        next()
    })

    socket.on('disconnect', () => {
        console.log('user disconnected')
    })

    socket.on('Device', (obj) => {
        console.log('Device:', obj)
        if (obj === 'Phone') {
            phoneClient = socket
            //socket.emit('MiniGameStart')
        } else if (obj === 'Unity') {
            unityClient = socket
        }
    })

    socket.on('hi', (msg) => console.log('yay'))
}


secureIO.on('connection', onConnect)
insecureIO.on('connection', onConnect)

insecureServer.listen(8001)
// -------- End Socket IO --------


httpServer.listen(8000)