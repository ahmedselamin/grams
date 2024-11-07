import { createContext, useState } from 'react';
import { jwtDecode } from 'jwt-decode';
import axiosInstance from './utils/axiosInstance'; 


//create context
export const AuthContext = createContext();

export const AuthProvider = ({ children }) => {
    const [isAuthenticated, setIsAuthenticated] = useState(false);

    const register = async (formData) => {
        try {
            const response = await axiosInstance.post('/Auth/register', {
                username: formData.username,
                password: formData.password
            });

            return response.status === 201 ? true : false;

        } catch (error) {
            console.error(error)
            return false;
        }
    }

    const login = async (formData) => {
        try {
            const response = await axiosInstance.post('/Auth/login', {
                username: formData.username,
                password: formData.password
            });

            const token = response.data.token;

            localStorage.setItem('token', token);

            setIsAuthenticated(true);
            return true;
            
        } catch (error) {
            console.error(error)
            return false;
        }
    }

    const logout = () => {
        setIsAuthenticated(false);
        localStorage.removeItem('token');
    }

    return (
        <AuthContext.Provider>
            {children}
        </AuthContext.Provider>
    )
}