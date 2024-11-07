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
    const [posts, setPosts] = useState([]);
    const [open, setOpen] = useState(false);
    const [formData, setFormData] = useState({
        caption: "",
        file: null,
    });

    const handleInputChange = (e) => {
        setFormData({
            ...formData,
            [e.target.name]: e.target.value
        });
    }

    const handleImageChange = (e) => {
        setFormData({
            ...formData,
            file: e.target.files[0],
        });
    };

    const clearForm = () => {
        setFormData({
            caption: "",
            file: null,
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
        if (formData.file) {
            data.append("file", formData.file);
        }
        if (formData.caption) {
            data.append("caption", formData.caption);
        }

        try {
            const response = await axiosInstance.post('/Posts/create', data, {
                headers: {
                    'Content-Type': 'multipart/form-data',
                },
            });

            if (response.status === 200) {
                closeDialog();
                fetchGrams();
                clearForm();
            }

        } catch (error) {
            console.error(error);
        }
    }

    useEffect(() => {
        fetchGrams();
    }, []);

    return (
        <Box sx={{ padding: '20px' }}>
            {isAuthenticated ? (
                <Container>
                    <Typography variant="h6" align="left">
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
            ) : null}
            <Stack spacing={3} sx={{ maxWidth: '450px', margin: '0 auto' }}>
                {posts.length === 0 ? (
                    <Typography variant="h6" align="center">
                        Nothing to see here.
                    </Typography>
                ) : (
                    <Typography variant="h6" align="center">
                        something is here
                    </Typography>
                )}
            </Stack>

            <Dialog maxWidth="xs" fullWidth open={open} close={closeDialog}>
                <DialogTitle sx={{ textAlign: "center" }}>Gram</DialogTitle>
                <DialogContent>
                    <Box component="form" onSubmit={handleCreate}>                        
                        <input
                            accept="image/*"
                            type="file"
                            onChange={handleImageChange}
                            style={{ marginTop: '10px' }}
                        />
                        <TextField
                            margin="dense"
                            name="caption"
                            label="Caption"
                            type="text"
                            value={formData.caption}
                            onChange={handleInputChange}
                        />
                        <DialogActions sx={{ display: 'flex', flexDirection: 'row', alignItems: 'center', gap: 2 }}>
                            <Button variant="outlined" onClick={closeDialog} sx={{ color: 'black', borderRadius: '20px' }}>
                                Discard
                            </Button>
                            <Button variant="contained" type="submit" sx={{ backgroundColor: '#005477', borderRadius: '20px' }}>
                                Confirm
                            </Button>
                        </DialogActions>
                    </Box>
                </DialogContent>
            </Dialog>
        </Box>
    );

};

export default Home;