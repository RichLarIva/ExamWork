import './App.css';
import { ReactComponent as ClockIcon } from './icons/clock.svg';
import React, {useState, useEffect} from 'react';
import NameList from './NameList';
import io from 'socket.io-client';


function App() {
  const [socket, setSocket] = useState(null);

  useEffect(() => {
    const newSocket = io(`http://localhost:3002`);
    setSocket(newSocket);
    return () => newSocket.close();
  }, [setSocket]);
  const [names, setNames] = useState([
  ])

  function addNames(name){
    setNames([...names, {id: Date.now(), ...name}]);
  }
  useEffect(() => {
    async function getData(){
      const response = await fetch("http://localhost:3030/scannedNames");
      let actualData = await response.json();
      setNames([...names, {id: Date.now(), ...actualData.response.FullName}])
    }
    getData();
  }, []);

  const [dateState, setDateState] = useState(new Date());
  const [dayState, setDayState] = useState(new Date());
  useEffect(() => {
    setInterval(() => setDateState(new Date()), 1000);
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
        {names && names.map(({id, name}) => (
          <li key={id}>
            <h3>{name}</h3>
          </li>
        ))}
      </ul>
      <NameList names={names}/>
    </div>
  );
}

export default App;
