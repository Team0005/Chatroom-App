import React, { useState, useEffect } from "react";
import api from "../services/api";
import Table from '@mui/material/Table';
import TableBody from '@mui/material/TableBody';
import TableCell from '@mui/material/TableCell';
import TableContainer from '@mui/material/TableContainer';
import TableHead from '@mui/material/TableHead';
import TableRow from '@mui/material/TableRow';
import Paper from '@mui/material/Paper';

const ActivityLog = () => {
    const [logs, setLogs] = useState();
    useEffect(() => {
        getLoginLogs();
    }, []);

    const getLoginLogs = () => {
        api.get("https://localhost:5001/LoginLog/getAll")
            .then(response => {
                console.log(response.data);
                setLogs(response.data);
            })
            .catch(error => {
                console.error(error.response);
            })
    }

    return (
        <div className="activityTable">
            <TableContainer component={Paper}>
                <Table sx={{ minWidth: 650 }} aria-label="simple table">
                    <TableHead>
                        <TableRow>
                            <TableCell>Last Login Date</TableCell>
                            <TableCell align="right">IP Adress</TableCell>
                            <TableCell align="right">Succeded</TableCell>
                        </TableRow>
                    </TableHead>
                    <TableBody>
                        {logs && logs.map((log, index) => (
                            <TableRow
                                key={index}
                                sx={{ '&:last-child td, &:last-child th': { border: 0 } }}
                            >
                                <TableCell component="th" scope="row">
                                    {log.timeStamp}
                                </TableCell>
                                <TableCell align="right">{log.ipAddress}</TableCell>
                                <TableCell align="right">{log.succeeded == true ? `Login Succeeded` : `Login Failed`}</TableCell>
                            </TableRow>
                        ))}
                    </TableBody>
                </Table>
            </TableContainer>
        </div>
    );
}

export default ActivityLog;