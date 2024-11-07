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
    const { isAuthenticated } = useContext(AuthContext);
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

    useEffect(() => {
        fetchGrams();
    }, []);

    return (
        <Box sx={{ padding: 20 }}>
            {isAuthenticated ? (
                <Container>
                    <Typography>
                        Welcome back!
                    </Typography>

                    <Box sx={{ display: 'flex', justifyContent: 'flex-end', marginBottom: '20px', maxWidth: '600px', marginX: 'auto' }}>
                        <Button
                            variant="contained"
                            onClick={openDialog}
                            sx={{
                                backgroundColor: '#005477',
                                color: 'white',
                                borderRadius: '20px',
                                paddingX: '25px',
                                paddingY: '10px',
                                transition: 'transform 0.3s ease',
                                '&:hover': {
                                    transform: 'scale(1.1)',
                                },
                            }}>
                            Create 
                        </Button>
                    </Box>
                </Container>
            ): null}
        </Box>
    );

};

export default Home;