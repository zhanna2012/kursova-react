import React, {useContext, useState} from 'react';
import { TextField, Button } from '@mui/material';
import {UserContext} from "../../providers/UserProvider";
import {useNavigate} from "react-router-dom";
import styles from './Login.module.scss';
import axios from "axios";

const Login = () => {
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');
    const [emailError, setEmailError] = useState('');
    const [passwordError, setPasswordError] = useState('');
    const { user, login, updateToken, updateRefreshToken } = useContext(UserContext);
    const navigate = useNavigate();

    const validateEmail = (email) => {
        const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
        return emailRegex.test(email);
    };

    const handleLogin = () => {
        if (!email) {
            setEmailError('Email is required');
        } else if (!validateEmail(email)) {
            setEmailError('Invalid email address');
        } else {
            setEmailError('');
        }

        if (!password) {
            setPasswordError('Password is required');
        } else {
            setPasswordError('');
        }

        if (email && password && !emailError && !passwordError) {
            const url = 'https://localhost:7207/Authorization';
            const headers = {
                'accept': '*/*',
                'Content-Type': 'application/json-patch+json'
            };
            axios.post(url, {
                email,
                password
            }, {headers})
                .then(response => {
                    updateToken(response.data.accessToken);
                    updateRefreshToken(response.data.refreshToken);
                    login({email: email, password: password});
                    navigate("/");
                })
                .catch(error => {
                    console.error('Authorization failed', error);
                });
        }
    };

    return (
        <div style={{ margin: '70px' }}>
            <TextField
                label="Email"
                variant="outlined"
                value={email}
                onChange={(e) => setEmail(e.target.value)}
                error={!!emailError}
                helperText={emailError}
                fullWidth
                margin="normal"
            />

            <TextField
                label="Password"
                variant="outlined"
                value={password}
                onChange={(e) => setPassword(e.target.value)}
                error={!!passwordError}
                helperText={passwordError}
                type="password"
                fullWidth
                margin="normal"
            />

            <Button variant="contained" color="primary" onClick={handleLogin}>
                Login
            </Button>
        </div>
    );
};

export default Login;
