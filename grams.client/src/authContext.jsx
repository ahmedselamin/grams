import { createContext, useState } from 'react';
import { jwtDecode } from 'jwt-decode';
import axiosInstance from './utils/axiosInstance'; 


//create context
export const AuthContext = createContext();

export const AuthProvider = ({ children }) => {

    return (
        <AuthContext.Provider>
            {children}
        </AuthContext.Provider>
    )
}