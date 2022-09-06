import React from "react";



const Friends = ({users, selectUserId, selectedUserId}) => {
    return (

        <div className='friends'  >
            {users.map((user, index) => {
                return(
                        <center key={index} onClick={() => selectUserId(user.userId)} style={selectedUserId === user.userId ? {backgroundColor: '#b3b2b2'} : null}>
                           <img src={user.imageUrl} alt='poza' className='friendAvatar' />
                           <h3 >{user.username}</h3>
                       </center> 
                )
                           
            })}
        </div>
    )

}


export default Friends;