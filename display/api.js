const express = require('express');
const bodyParser = require('body-parser');
const cors = require('cors');
const app = express();
const mysql = require('mysql');

const conn = mysql.createConnection({
    host: "localhost",
    user: "root",
    password: "",
    database: "MatsalApplikation",
    multipleStatements: true
});

conn.connect((err) => {
    if(err) return console.log(err);
    console.log("Connected to the server");
})

app.use(bodyParser.json());
app.use(cors({
    origin: '*'
}));

app.use(express.json());
app.use(express.urlencoded({extended:true}));

app.get("/scannedNames", (req, res) => {
    const q = "SELECT * FROM LunchScans WHERE Date>=CURDATE() and Date < CURDATE() + INTERVAL 1 DAY"
    const sql = conn.query(q, (err, results) => {
        if(err) {
            return console.log(err);
        }
        res.send(apiResponse((results)));
    })
})

function apiResponse(results){
    return JSON.stringify({"status": 200, "error": null, "response": results})
}

app.listen(3030, () =>{
    console.log("Api launched on 3030");
})