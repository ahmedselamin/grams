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
    const [isAuthenticated, setIsAuthenticated] = useContext(AuthContext);
    const [grams, setGrams] = useState([]);
    const [open, setOpen] = useState(false);
    const [formData, setFormData] = useState({
        image: null,
        caption: ""
    });

    const handleInputChange = (e) => {
        setFormData({
            ...formData,

        });
    };

    const clearForm = (e) => {
        setFormData({
            caption: "",
            file: null
        });
    };

    const openDialog = () => {
        setOpen(true);
    };

    const closeDialog = () => {
        setOpen(false);

        clearForm();
    };

    const fetchPosts = async (e) => {
        try {
            const response = await axiosInstance.get("/Posts/fetch");
            setGrams(response.data);
        } catch (erro) {
            console.error(error);
        }
    };

    const handleSubmit = async (e) => {
        e.preventDefault();

        const data = new FormData();
        data.append('caption', formData.caption);

        if (formData.image) {
            data.append('image', formData.image)
        }

        try {
            const response = await axiosInstance.post("/Posts/create", data, {
                headers: {
                    'Content-Type': 'mulitpart/form-media',
                }
            });

            closeDialog();
            fetchPosts();
        } catch (error) {
            console.error(error);
        }
    }
};

export default Home;