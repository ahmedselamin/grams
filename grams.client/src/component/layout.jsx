import React from 'react';
import { CssBaseline, Container, Box } from '@mui/material';
import { Outlet } from 'react-router-dom'; // Import Outlet
import Navbar from './Navbar'; // Assuming the Navbar is in the same folder

const Layout = () => {
    return (
        <Box sx={{ display: 'flex', flexDirection: 'column', minHeight: '100vh' }}>
            {/* Navbar */}
            <Navbar />

            {/* Main content */}
            <Container component="main" sx={{ flexGrow: 1, mt: 2, mb: 2 }}>
                <Outlet /> {/* Use Outlet to render child routes */}
            </Container>
        </Box>
    );
};

export default Layout;
