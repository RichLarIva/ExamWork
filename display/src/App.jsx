import './App.css';
import { ReactComponent as ClockIcon } from './icons/clock.svg';
import React, {useState, useEffect} from 'react';
import NameList from './NameList';
import axios from "axios";

function App() {
  const [user, setUser] = useState([]);
  let i = 1;
  // eslint-disable-next-line react-hooks/exhaustive-deps
  async function fetchData(){
    try{
      const response = await axios.get("http://localhost:3030");
      if(response.data.response.toString() !== ""){
        setUser(response.data.response);
        addNames(response.data.response.toString());
      }
    }
    catch (error){
      console.error(error);
    }
  }
  useEffect(() => {
    setInterval(() => fetchData(), 1000);
  }, [fetchData])
  const [names, setNames] = useState([
    {name: "test", id: 1}
  ])
  function addNames(name){
    if(name !== "")
    {
      setNames([...names, {id: i+=1, name: name}]);
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
        <div>
        <ClockIcon/><time className="clock">{dateState.toLocaleString('en-us', {
          hour: 'numeric',
          minute: 'numeric',
          hour12: false,
        })}</time>
        </div>
      </header>
      <ul>
      </ul>
      <NameList names={names}/>
    </div>
  );
}

export default App;
