const net = require('net');
const uuid = require('uuid');

const messages = new Set();
const ids = new Map();



var client3 = new net.Socket();
client3.connect(3002, "127.0.0.1", () => {
    console.log("client3 Connected");
}) //Connect
client3.on('close', function () {
    client3.connect(3002, "127.0.0.1", () => {})
}) //Reconnect if connection is closed
client3.on('error', (ex) => {
    if (ex.message.includes("client3 ECONNREFUSED")) {}
}); //error
client3.on('data', (data) => {
    if(data.toString() === "Already scanned"){
        console.log("ALREADY SCANNED");
    }
    else if(data.toString() !== "Already scanned"){
      console.log(data.toString());
      messages.add(data.toString());
    }
  })