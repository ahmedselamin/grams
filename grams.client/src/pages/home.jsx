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

    useEffect(() => {
        fetchPosts();

    }, []);

    return (
        <Box sx={{ padding: '20px' }}>
            {isAuthenticated ? (
                <Container>
                    <Typography variant="h4" sx={{ textAlign: 'left', flexGrow: 1 }}>
                        Welcome, {username}!
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
                            Add Post
                        </Button>
                    </Box>
                </Container>
            ) : null}

            <Stack spacing={3} sx={{ maxWidth: '450px', margin: '0 auto' }}>
                {posts.length === 0 ? (
                    <Typography variant="h6" align="center" color="text.secondary">
                        No posts available
                    </Typography>
                ) : (
                    posts.map((post) => (
                        <Card key={post._id} sx={{ backgroundColor: '#ffffff', borderRadius: '20px', boxShadow: 2, padding: '15px' }}>
                            <CardContent>
                                <Typography variant="h5" component="div" sx={{ fontWeight: 'bold' }}>
                                    {post.caption || 'No Caption'}
                                </Typography>

                                {post.image && (
                                    <Box sx={{ display: 'flex', justifyContent: 'center', marginY: '10px' }}>
                                        <img
                                            src={`http://localhost:3030/uploads/${post.image.split('\\').pop()}`}
                                            alt={post.caption}
                                            style={{ maxHeight: '300px', width: '300px', objectFit: 'cover', borderRadius: '8px' }}
                                        />
                                    </Box>
                                )}

                                <Typography variant="caption" display="block" sx={{ marginY: '5px' }}>
                                    <strong>Author:</strong> {post.author.username || 'Unknown'}
                                </Typography>
                            </CardContent>
                        </Card>
                    ))
                )}
            </Stack>

            <Dialog maxWidth="xs" fullWidth open={open} onClose={closeDialog}>
                <DialogTitle sx={{ textAlign: "center" }}>Share A Post</DialogTitle>
                <DialogContent>
                    <Box component="form" onSubmit={handleFormSubmit}>
                        <TextField
                            margin="dense"
                            name="caption"
                            label="Caption"
                            type="text"
                            fullWidth
                            required
                            value={formData.caption}
                            onChange={handleInputChange}
                        />
                        <input
                            accept="image/*"
                            type="file"
                            onChange={handleImageChange}
                            style={{ marginTop: '10px' }}
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