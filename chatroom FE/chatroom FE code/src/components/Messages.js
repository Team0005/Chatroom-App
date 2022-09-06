import React from "react";
import Message from "./Message";

const Messages = ({messages}) => {
    return (
        <div className='messages'>
            {messages && messages.map((message, index) =>{
                return(
                    <Message message={message} key={index}/> //react recomanda ca fiecare component sa aibe setat un key unic pentru a nu avea probleme la randare
                )
                
            })}
        </div>
    )
}

export default Messages;