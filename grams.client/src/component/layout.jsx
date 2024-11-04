import React from 'react';
import { CssBaseline, Container, Box } from '@mui/material';
import Navbar from './Navbar'; // Assuming the Navbar is in the same folder

const Layout = ({ children }) => {
    return (
        <Box sx={{ display: 'flex', flexDirection: 'column', minHeight: '100vh' }}>
            {/* Navbar */}
            <Navbar />

            {/* Main content */}
            <Container component="main" sx={{ flexGrow: 1, mt: 2, mb: 2 }}>
                {children}
            </Container>
        </Box>
    );
};

export default Layout;
