import React from 'react';
import {
  BrowserRouter,
  Routes,
  Route
} from "react-router-dom";
import Login from './components/Login';
import Home from './components/Home';
import Users from './components/Users';
import Navigation from './components/Navigation';
import UserInfo from './components/UserInfo';
import ActivityLog from './components/ActivityLog';
import { ToastContainer, toast } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';

const App = () => {
  return (
    <div className="App">
      <ToastContainer
        position="top-right"
        autoClose={5000}
        hideProgressBar={false}
        newestOnTop={false}
        closeOnClick
        rtl={false}
        pauseOnFocusLoss
        draggable
        pauseOnHover
      />
      <BrowserRouter>
        <Navigation />
        <Routes>
          {localStorage.getItem("username") &&
            <Route exact path="/" element={<Home />} />
          }
          {localStorage.getItem("username") &&
            <Route exact path="/home" element={<Home />} />
          }

          {localStorage.getItem("username") === null &&
            <Route exact path="/login" element={<Login />} />
          }

          {localStorage.getItem("username") !== null &&
            <Route exact path="/home" element={<Home />} />
          }

          {localStorage.getItem("username") &&
            <Route exact path="/userInfo" element={<UserInfo />} />
          }

          {localStorage.getItem("username") &&
            <Route exact path="/activityLog" element={<ActivityLog />} />
          }

          {localStorage.getItem("userRole") === "Administrator" &&
            <Route exact path="/users" element={<Users />} />
          }

          <Route
            path="*"
            element={
              <div>
                <p>There's nothing here!</p>
              </div>
            }
          />
        </Routes>
      </BrowserRouter>
    </div>
  );
}

export default App;