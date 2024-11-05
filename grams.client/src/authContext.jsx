import { createContext, useState } from "react";
import axiosInstance from "./utils/axiosInstance";

//create auth context 
export const AuthContext = createContext();

export const AuthProvider = ({ children }) => {
    const [isAuthenticated, setIsAuthenticated] = useState(false);
    const [username, setUsername] = useState('');

    const register = async (formData) => {
        try {
            const response = await axiosInstance.post('/auth/register', {
                username: formData.username,
                password: formData.password
            });

            return response.status === 201 ? true : false;

        } catch (error) {
            console.error(error);

            return false;
        }
    }
}