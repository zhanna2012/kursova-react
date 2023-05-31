import React, { createContext, useState } from 'react';

const UserContext = createContext();

const UserProvider = ({ children }) => {
    const [user, setUser] = useState(null);
    const [token, setToken] = useState(null);
    const [refreshToken, setRefreshToken] = useState(null);


    const login = (userData) => {
        setUser(userData);

    };

    const logout = () => {
        setUser(null);
        setToken(null);
    };

    const updateToken = (newToken) => {
        setToken(newToken);
    };

    const updateRefreshToken = (newToken) => {
        setRefreshToken(newToken);
    };


    const contextValue = {
        user,
        token,
        refreshToken,
        login,
        logout,
        updateToken,
        updateRefreshToken
    };

    return <UserContext.Provider value={contextValue}>{children}</UserContext.Provider>;
};

export { UserContext, UserProvider };
