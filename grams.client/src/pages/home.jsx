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

    const openDialog = () => {
        setOpen(true);
    }
    const closeDialog = () => {
        setOpen(false);
    }

    const fetchGrams = async () => {
        try {
            const response = await axiosInstance.get('/Posts/fetch');
            setPosts(response.data.data);
        } catch (error) {
            console.error(error);
        }
    }

    const handleCreate = async (e) => {
        e.preventDefault();

        const data = new FormData();
        if (formData.photo && formData.caption) {
            data.append("photo", formData.photo);
            data.append("caption", formData.caption);
        }

        try {
            const response = await axiosInstance.post('/Posts/create', data, {
                headers: {
                    'Content-Type': 'multipart/form-data',
                },
            });
            closeDialog();

            fetchGrams();

        } catch (erro) {
            console.error(error);
        }
    }

    return (
        <h2>Home</h2>
    );

};

export default Home;