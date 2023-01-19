const net = require('net');
const express = require("express");
const cors = require('cors');
const bodyParser = require('body-parser');
const app = express();

var message = "";

app.use(bodyParser.json());
app.use(cors({
    origin: '*'
}));
app.use(express.json());
app.use(express.urlencoded({extended:true}));


const client = new net.Socket();
client.connect(3002, "127.0.0.1", () => {
    console.log("client3 Connected");
}) //Connect
client.on('close', function () {
    client.connect(3002, "127.0.0.1", () => {})
}) 
// Client gets an error
client.on('error', (ex) => {
    if (ex.message.includes("client3 ECONNREFUSED")) {
        console.log(ex.message.toString());
    }
}); 
// Client recieves data
client.on('data', (data) => {
    if(data.toString() === "Already scanned"){
        console.log("ALREADY SCANNED");
        message = "Already scanned";
    }
    else if(data.toString() !== "Already scanned"){
      console.log(data.toString());
      message = data.toString();
    }
})

app.get("/", (req, res) => {
    res.send(JSON.stringify({"response": message}));
    message = "";
})

app.listen(3030, () =>{
    console.log("Server launched on 3030")
})