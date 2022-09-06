import { Button, TextField } from '@mui/material';
import React, { useState, useEffect } from 'react';
import api from "../services/api"
import Table from '@mui/material/Table';
import TableBody from '@mui/material/TableBody';
import TableCell from '@mui/material/TableCell';
import TableContainer from '@mui/material/TableContainer';
import TableHead from '@mui/material/TableHead';
import TableRow from '@mui/material/TableRow';
import Paper from '@mui/material/Paper';
import { toast } from 'react-toastify';

const Users = () => {
  const [registerUsername, setRegisterUsername] = useState("");
  const [registerPassword, setRegisterPassword] = useState("");
  const [registerUserRole, setRegisterUserRole] = useState("User");
  const [nationality, setNationality] = useState("");
  const [phone, setPhone] = useState("");
  const [birthDate, setBirthDate] = useState("2000-01-01");
  const [imageUrl, setImageUrl] = useState("");
  const [users, setUsers] = useState(null);

  useEffect(() => {
    getAllUsers();
  }, []);

  const registerUser = () => {
    const requestBody = {
      username: registerUsername,
      password: registerPassword,
      userRole: registerUserRole,
      nationality: nationality,
      phone: phone,
      birthDate: birthDate,
      imageUrl: imageUrl
    };
    console.log(requestBody);
    api.post("https://localhost:5001/Account/register", requestBody)
      .then(response => {
        toast.success("User added successfully");
        getAllUsers();
      })
      .catch(error => {
        toast.error(error);
      })
  }

  const getAllUsers = () => {
    api.get("https://localhost:5001/Account/users")
        .then(response => {
          console.log(response.data);
          setUsers(response.data);
        })
        .catch(error => {
          console.error(error.response);
        })
  }

  const deleteUser = (userId) => {
    console.log("delete user id " + userId);
    api.delete(`https://localhost:5001/Account/delete/${userId}`)
        .then(response => {
          getAllUsers();
          toast.success("User Deleted Successfully");
        })
        .catch(error => {
          console.error(error.response);
        })
  }

  return (
    <div>
      <div>
        <br />
        <TextField sx={{ m: 1 }} value={registerUsername} label="Register Username *" onChange={(e) => setRegisterUsername(e.target.value)} />
        <TextField sx={{ m: 1 }} value={registerPassword} label="Register Password *" type="password" onChange={(e) => setRegisterPassword(e.target.value)} />
        <TextField sx={{ m: 1 }} value={registerUserRole} label="Register Role *" onChange={(e) => setRegisterUserRole(e.target.value)} />
        <TextField sx={{ m: 1 }} value={imageUrl} label="Image Url *" onChange={(e) => setImageUrl(e.target.value)} />
        <br />
        <TextField sx={{ m: 1 }} value={nationality} label="Nationality *" onChange={(e) => setNationality(e.target.value)} />
        <TextField sx={{ m: 1 }} value={phone} label="Phone *" onChange={(e) => setPhone(e.target.value)} />
        <TextField sx={{ m: 1 }} value={birthDate} label="Birth Date *" type="date" onChange={(e) => setBirthDate(e.target.value)} />
        <br />
        <Button variant="text" onClick={registerUser}>Register User</Button>
      </div>
      <div className="activityTable">
            <TableContainer component={Paper}>
                <Table sx={{ minWidth: 650 }} aria-label="simple table">
                    <TableHead>
                        <TableRow>
                            <TableCell>Username</TableCell>
                            <TableCell align="right">Role</TableCell>
                            <TableCell align="right">Nationality</TableCell>
                            <TableCell align="right">Phone</TableCell>
                            <TableCell align="right">Actions</TableCell>
                        </TableRow>
                    </TableHead>
                    <TableBody>
                        {users && users.map((user, index) => (
                            <TableRow
                                key={index}
                                sx={{ '&:last-child td, &:last-child th': { border: 0 } }}
                            >
                                <TableCell component="th" scope="row">
                                    {user.username}
                                </TableCell>
                                <TableCell align="right">{user.userRoleString}</TableCell>
                                <TableCell align="right">{user.nationality}</TableCell>
                                <TableCell align="right">{user.phone}</TableCell>
                                <TableCell align="right"><Button variant="text" onClick={(e) => deleteUser(user.userId)}>Delete</Button></TableCell>
                            </TableRow>
                        ))}
                    </TableBody>
                </Table>
            </TableContainer>
        </div>
    </div>
  );
}

export default Users;
