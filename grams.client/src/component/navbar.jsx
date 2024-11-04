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
        <AppBar position="sticky" color="primary">
            <Toolbar>
                <Typography variant="h4" component="header" sx={{ flexGrow: 1 }}>
                    Grams
                </Typography>

                {/* desktop menu */}
                <Box sx={{ display: { xs: 'none', md: 'flex' } }}>
                    {menuItems.map((item) => (
                        <Button key={item} color="inherit">
                            {item}
                        </Button>
                    ))}
                </Box>

                {/* Hamburger icon for smaller screens */}
                <Box sx={{ display: { xs: 'flex', md: 'none' } }}>
                    <IconButton edge="start" color="inherit" onClick={handleMenuOpen}>
                        <MenuIcon />
                    </IconButton>
                </Box>
            </Toolbar>

            {/* Full-width dropdown menu for smaller screens */}
            <Menu
                anchorEl={anchor}
                open={Boolean(anchor)}
                onClose={handleMenuClose}
                anchorOrigin={{ vertical: 'bottom', horizontal: 'center' }}
                transformOrigin={{ vertical: 'top', horizontal: 'center' }}
                PaperProps={{
                    sx: {
                        mt: 2,
                        width: '100%',
                        maxWidth: '97%',
                    },
                }}>
            
                {menuItems.map((item) => (
                    <MenuItem key={item} onClick={handleMenuClose} sx={{ justifyContent: 'center' }}>
                        {item}
                    </MenuItem>
                ))}
            </Menu>
        </AppBar>
    );
};

export default Navbar;
