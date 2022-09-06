import React, { useState } from "react";

const WriteMessage = ({sendMessageToSelectedUser}) => {

    const [message, setMessage] = useState("");
    
    const sendMessage = () => {
        if(message !== ""){
        sendMessageToSelectedUser(message);
        setMessage("");
        }
    }
    const enterTextArea = (event) => {
        if(event.key === "Enter"){
            event.preventDefault(); //cancels the event to happen, when we press enter, a new line in will not be added in the text area
            sendMessage();
        }
    }
    return (
        <div className='writeMessage'>
            <textarea className='tarea' placeholder='Write a message...' value={message} onChange={(event) => setMessage(event.target.value)} onKeyDown={enterTextArea}/>
            <button className='sendButton' onClick={sendMessage}>Send!</button>
        </div>
    )
    
    
}

export default WriteMessage;