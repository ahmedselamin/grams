import { useState, useContext } from 'react';
import { Link, useNavigate } from 'react-router-dom';
import { AppBar, Toolbar, IconButton, Typography, Button, Menu, MenuItem, Box } from '@mui/material';
import MenuIcon from '@mui/icons-material/Menu';
import { AuthContext } from '../AuthContext';

const Navbar = () => {
    const [anchorEl, setAnchorEl] = useState(null);
    const [isAuthenticated, logout] = useContext(AuthContext);
    const navigate = useNavigate();

    const handleLogout = () => {
        logout();
        navigate('/');
    };

    const handleMenuOpen = (e) => {
        setAnchorEl(e.currentTarget);
    };

    const handleMenuClose = () => {
        setAnchorEl(null);
    };

    return (
        <AppBar position="sticky"
            sx={{
                backgroundColor: '#005477',
                color: 'white',
                borderRadius: "40px",
                margin: '0 auto',
                maxWidth: '1500px',
                padding: '10px 20px'
            }}>
            <Toolbar>
                <Typography
                    variant="h4"
                    component={Link}
                    to="/"
                    sx={{
                        flexGrow: 1,
                        textDecoration: 'none',
                        color: 'white',
                        fontWeight: 'bold',
                        display: 'flex',
                        alignItems: 'center',
                    }}>
                    Grams
                </Typography>
            </Toolbar>
        </AppBar>
    );
};

export default Navbar;
