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

    return (
        
    );
};

export default Navbar;
