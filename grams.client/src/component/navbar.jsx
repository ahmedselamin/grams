import React, { useState } from 'react';
import { AppBar, Toolbar, IconButton, Typography, Menu, MenuItem, Button, Box } from '@mui/material';
import MenuIcon from '@mui/icons-material/Menu';

const Navbar = () => {
    const [anchor, setAnchor] = useState(null);

    const handleMenuOpen = (event) => {
        setAnchor(event.currentTarget);
    };

    const handleMenuClose = () => {
        setAnchor(null);
    };

    const menuItems = ['Home', 'Profile', 'Settings'];

    return (
        
    );
};

export default Navbar;
