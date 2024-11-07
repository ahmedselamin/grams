import { useState, useEffect, useContext } from "react";
import {
    Card,
    CardContent,
    Typography,
    Stack,
    Box,
    Button,
    Dialog,
    DialogActions,
    DialogContent,
    DialogTitle,
    TextField,
    Container,
} from "@mui/material";
import axiosInstance from "../utils/axiosInstance";
import { AuthContext } from "../authContext";

const Home = () => {
    const [isAuthenticated] = useContext(AuthContext);
    const [post, setPosts] = useState([]);
    const [open, setOpen] = useState(false);
    const [formData, setFormData] = useState({
        caption: "",
        photo: null,
    });

    const handleInputChange = (e) => {
        setFormData({
            ...formData,
            [e.target.name]: e.target.value
        });
    }

    const clearForm = () => {
        setFormData({
            caption: "",
            photo: null,
        })
    };

    const fetchGrams = async () => {
        try {
            const response = await axiosInstance.get('/Posts/fetch');
            setPosts(response.data.data);
        } catch (error) {
            console.error(error);
        }
    }

    return (
        <h2>Home</h2>
    );

};

export default Home;