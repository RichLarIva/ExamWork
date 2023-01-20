import React from 'react';

function NameDisplay({name, id}){
    return (
        <div className='name'>
            <h3>{id}</h3>
            <h3>{name}</h3>
        </div>
    )
}

export default NameDisplay;