import React from 'react';
import NameDisplay from './RegisteredName';


function NameList({names}){
    return (
        <div className="names">
            {names.map(name =>(
                <div key={name.id}>
                <NameDisplay id={name.id} name={name.name} timeStamp={name.timeStamp}/>
                </div>
            ))}
        </div>
    )
}

export default NameList;