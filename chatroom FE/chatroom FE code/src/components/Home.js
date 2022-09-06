import '../App.css';
import React, { useState, useEffect } from 'react';
import Friends from "./Friends"
import Messages from './Messages';
import WriteMessage from "./WriteMessage"
import api from "../services/api"

const Home = () => {
    const [selectedUserId, setSelectedUserId] = useState(0);
    const [users, setUsers] = useState([]);
    const [messages, setMessages] = useState([]);

    useEffect(() => {
        getAllUsers();
    }, []);

    const getAllUsers = () => {
        api.get("https://localhost:5001/Account/users")
            .then(response => {
                setUsers(response.data);
                const firstUser = response.data[0];
                if(firstUser){
                    selectUserId(firstUser.userId)
                }
            })
            .catch(error => {
                console.error(error.response);
            })
    }

    const selectUserId = (id) => {
        setSelectedUserId(id);
        getMessagesOfSelectedUser(id);
    }

    const sendMessage = (content) => {
        const requestBody = {
            receiverId: selectedUserId,
            content: content
        }

        api.post("https://localhost:5001/Message/send", requestBody)
            .then(response => {
                getMessagesOfSelectedUser(selectedUserId);
            })
            .catch(error => {
                console.error(error.response);
            })
    }

    const getMessagesOfSelectedUser = (userId) => {
        api.get(`https://localhost:5001/Message/messages/${userId}`)
            .then(response => {
                setMessages(response.data);
            })
            .catch(error => {
                console.error(error.response);
            })
    }

    return (
        <div>
            <div className='chatBoard'>

                {<Friends users={users} selectUserId={selectUserId} selectedUserId={selectedUserId} />}
                {messages && selectedUserId && <Messages messages={messages}/>}
                {selectedUserId && <WriteMessage sendMessageToSelectedUser={sendMessage} />}
            </div>
        </div>
    );
}

export default Home;