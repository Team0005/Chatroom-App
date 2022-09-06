import React, { useState, useEffect } from "react";
import { Button, TextField } from '@mui/material';
import { toast } from "react-toastify";

import api from "../services/api"

const UserInfo = () => {
    const [nationality, setNationality] = useState("");
    const [phone, setPhone] = useState("");
    const [imageUrl, setImageUrl] = useState(localStorage.getItem("imageUrl"));
    const [password, setPassword] = useState("");

    useEffect(() => {
        getUserDetails();
    }, []);

    const getUserDetails = () => {
        api.get("https://localhost:5001/Account/details")
            .then(response => {
                setNationality(response.data.nationality);
                setPhone(response.data.phone);
                setImageUrl(response.data.imageUrl);
                setPassword(response.data.password);
            })
            .catch(error => {
                console.error(error.response);
            })
    }

    const updateUserInfo = () => {
        const requestBody = {
            nationality: nationality,
            phone: phone,
            password: password,
            imageUrl: imageUrl
        }

        api.put("https://localhost:5001/Account/update", requestBody)
            .then((response) => {
                toast.success("Updated Successfully");
                localStorage.setItem("imageUrl", imageUrl)
            })
            .catch(error => {
                toast.error(error);
            })
    }

    return (
        <div className="userInfo">
            <TextField sx={{ m: `1%`, width: `98%` }} label="Nationality *" value={nationality} onChange={(newText) => setNationality(newText.target.value)}></TextField>
            <br />
            <TextField sx={{ m: `1%`, width: `98%` }} label="Phone *" value={phone} onChange={(newText) => setPhone(newText.target.value)}></TextField>
            <br />
            <TextField sx={{ m: `1%`, width: `98%` }} label="Image Url *" value={imageUrl} onChange={(newText) => setImageUrl(newText.target.value)}></TextField>
            <br />
            <TextField sx={{ m: `1%`, width: `98%` }} label="Password *" value={password} type="password" onChange={(newText) => setPassword(newText.target.value)}></TextField>
            <br />
            <Button sx={{ m: `1%`, width: `98%` }} onClick={updateUserInfo}>Save</Button>
        </div>
    );
}

export default UserInfo;
