import React from "react";


const Message = ({message}) => {
    return (
        <div className='myMessage'>
            <img src={message.userImageUrl} alt='poza' className='avatar'/>
            <h2 className='name'>{message.username}</h2>
            <p className='messageText'>
            {message.content}
            </p>
        </div>  
    )
}

export default Message;