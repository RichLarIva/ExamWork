import './App.css';
import { ReactComponent as ClockIcon } from './icons/clock.svg';
import React, {useState, useEffect} from 'react';
import NameList from './NameList';
import ScannedPopup from './ScannedPopup';
import axios from "axios";

function App() {
  const [count, setCount] = useState(1);
  const [id, setID] = useState(1);
  // eslint-disable-next-line react-hooks/exhaustive-deps
  async function fetchData() 
  {  
    try{
      const response = await axios.get("http://localhost:3030");
        setCount(count + 1);
        addNames(response.data.response.toString());
    }
    catch (error){
      console.error(error);
    }
  }
  useEffect(() => {
    const interval = setInterval(() => {
      fetchData();
    }, 1000);
    return () => clearInterval(interval);
  }, []);
  fetchData();

  const [names, setNames] = useState([])
  const [isOpen, setIsOpen] = useState(false);
  function addNames(name){
    if(name !== "")
    {
      if(name !== "Already scanned"){
        setID(id + 1);
        console.log(name);
        const date = new Date();
        const time = date.getHours() + ":" + date.getMinutes() + ":" + date.getSeconds();
        setNames([{id: id, name: name, timeStamp: time}, ...names]);
        
      }
      if(name === "Already scanned"){
        setIsOpen(true);
        setTimeout(() => {
          setIsOpen(false);
        }, 15000);
      }
    }
    
  }

  const [dateState, setDateState] = useState(new Date());
  const [dayState, setDayState] = useState(new Date());

  useEffect(() => {
    setInterval(() => setDateState(new Date()), 60000);
  }, []);
  const weekDay = ['Söndag', 'Måndag', 'Tisdag', 'Onsdag', 'Torsdag', 'Fredag', 'Lördag'];
  useEffect(() => {
    setInterval(() => setDayState(new Date()), 25000);
  }, []);


  return (
    <div className="App">
      <header className="App-header">
        <div>
          <p className='currentDay'>{weekDay[dayState.getDay()]}</p>
        </div>
        {isOpen && <ScannedPopup/>}
        <div>
        <ClockIcon/><time className="clock">{dateState.toLocaleString('en-us', {
          hour: 'numeric',
          minute: 'numeric',
          hour12: false,
        })}</time>
        </div>
      </header>

      <NameList names={names}/>
    </div>
  );
}

export default App;
