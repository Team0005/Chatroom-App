import { Button} from '@mui/material';
import React from 'react';
import { Link } from "react-router-dom";
import { Avatar } from '@mui/material';

const Navigation = () => {

    const logOut = () => {
        localStorage.clear();
        window.location.href = "/login";
    }
    return (
        <div>
            {localStorage.getItem("username") &&
                <div>
                    <Button 
                        variant="text" 
                        startIcon={<Avatar src={localStorage.getItem("imageUrl")} />}
                        onClick={logOut}>{localStorage.getItem("username")}, Log Out</Button>
                    <Link to="/home"><Button variant="text">Home</Button></Link>
                    {localStorage.getItem("userRole") === "Administrator" &&
                        <Link to="/users"><Button variant="text">Users</Button></Link>
                    }
                    <Link to="/userInfo"><Button variant="text">My Account</Button></Link>
                    <Link to="/activityLog"><Button variant="text">My Activity</Button></Link>
                </div>
            }
        </div>
    );
}

export default Navigation;