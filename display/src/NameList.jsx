import React from 'react';
import NameDisplay from './RegisteredName';


function NameList({names}){
    return (
        <div className="names">
            {names.map(name =>(
                <div key={name.id}>
                <NameDisplay name={name.name}/>
                </div>
            ))}
        </div>
    )
}

export default NameList;