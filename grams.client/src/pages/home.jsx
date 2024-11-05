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
        file: null,
        caption: ""
    });

    const handleInputChange = (e) => {
        setFormData({
            ...formData,

        })
    }

};

export default Home;