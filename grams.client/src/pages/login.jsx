import { useState, useContext } from "react";
import { useNavigate } from 'react-router-dom';
import { Box, Container, Typography, Button, TextField } from "@mui/material";
import { AuthContext } from '../authContext';

const LoginPage = () => {
    const navigate = useNavigate();
    const { login } = useContext(AuthContext);
    const [formData, setFormData] = useState({ username: '', password: '' });

    const handleInputChange = (e) => {
        setFormData({
            ...formData,
            [e.target.name]: e.target.value,
        });
    };

    const handleLogin = async (e) => {
        e.preventDefault();

        const success = await login(formData); 

        if (success) {
            navigate('/');
        } else {
            console.error('Login failed');
        }
    };

    return (
        <Container maxWidth="xs">
            <Box
                sx={{
                    mt: 2,
                    display: 'flex',
                    flexDirection: 'column',
                    justifyContent: 'center',
                    alignItems: 'left',
                }}
            >
                <Typography
                    variant="h3"
                    sx={{
                        fontWeight: 'bold',
                        mb: 2,
                        color: '#005477',
                    }}
                >
                    Welcome Back!
                </Typography>
                <Typography
                    variant="body1"
                    sx={{
                        mb: 4,
                        textAlign: 'center',
                    }}
                >
                    Please log in to your account
                </Typography>

                <Box component="form" onSubmit={handleLogin} noValidate autoComplete="off" sx={{ width: '100%' }}>
                    <TextField
                        margin="dense"
                        name="username"
                        label="Username"
                        type="text"
                        fullWidth
                        required
                        value={formData.username}
                        onChange={handleInputChange}
                        sx={{ mb: 2 }}
                    />
                    <TextField
                        margin="dense"
                        name="password"
                        label="Password"
                        type="password"
                        fullWidth
                        required
                        value={formData.password}
                        onChange={handleInputChange}
                        sx={{ mb: 3 }}
                    />
                    <Button
                        variant="contained"
                        fullWidth
                        type="submit"
                        sx={{
                            backgroundColor: '#005477',
                            borderRadius: '20px',
                            py: 1.5,
                            fontSize: '16px',
                            textTransform: 'none',
                            '&:hover': {
                                backgroundColor: '#000',
                            },
                        }}
                    >
                        Login
                    </Button>
                </Box>
                <Typography
                    variant="body2"
                    sx={{ mt: 2, color: 'text.secondary', textAlign: 'center' }}>
                    Don't have an account? <a href="/register" style={{ color: '#005477' }}>Sign Up</a>
                </Typography>
            </Box>
        </Container>
    );
};

export default LoginPage;