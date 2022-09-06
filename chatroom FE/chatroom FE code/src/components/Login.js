import { Button, TextField } from '@mui/material';
import React, { useState } from 'react';
import api from "../services/api"
import { toast } from 'react-toastify';

const Login = () => {
  const [username, setUsername] = useState("");
  const [password, setPassword] = useState("");

  const logIn = () => {
    //simulate IP address
    if (username !== "" && password !== "") {
      const ipAddress = `${Math.round(1 + Math.random() * (255 - 1))}.${Math.round(1 + Math.random() * (255 - 1))}.${Math.round(1 + Math.random() * (255 - 1))}.${Math.round(1 + Math.random() * (255 - 1))}`;
      const requestBody = {
        username: username,
        password: password,
        ipAddress: ipAddress
      }

      api.post("https://localhost:5001/Account/login", requestBody)
        .then(response => {
          localStorage.setItem("jwt", response.data.jwt);
          localStorage.setItem("username", response.data.username);
          localStorage.setItem("userRole", response.data.userRole);
          localStorage.setItem("userId", response.data.userId);
          localStorage.setItem("imageUrl", response.data.imageUrl);
          window.location.href = "/home";
        })
        .catch(error => {
          console.error(error);
        })
    }
    else {
      toast("Fill Username and Password in")
    }
  }

  const enterTextArea = (event) => {
    if(event.key === "Enter"){
        event.preventDefault(); //cancels the event to happen, when we press enter, a new line in will not be added in the text area
        logIn();
    }
}

  return (
    <div>
      <TextField value={username} label="Username" onChange={(e) => setUsername(e.target.value)} />
      <TextField value={password} label="Password" type="password" onChange={(e) => setPassword(e.target.value)} />
      <br />
      <Button variant="text" onClick={logIn} onKeyDown={enterTextArea}>Log In</Button>
      <br />
    </div>
  );
}

export default Login;