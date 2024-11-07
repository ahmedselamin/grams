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

    const handleMenueOpen = (e) => {
        setAnchorEl(e.currentTarget);
    };

    const handleMenueClose = () => {
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

                {/ *  desktop*/}
                <Box sx={{ display: { xs: 'none', md: 'block' } }}>
                    <Button component={Link} to="/" sx={{ color: 'white', fontWeight: 'bold' }}>
                        Home
                    </Button>
                    {isAuthenticated ?
                        (
                            <>
                                <Button component={Link} to="/notifications" sx={{ color: 'white', fontWeight: 'bold' }}>
                                    Notifications
                                </Button>
                                <Button onClick={handleLogout} sx={{ color: 'white', fontWeight: 'bold' }}>
                                    Logout
                                </Button>
                            </>
                            
                        ) : (
                            <>
                                <Button component={Link} to="/register" sx={{ color: 'white', fontWeight: 'bold' }}>
                                    Register
                                </Button>
                                <Button component={Link} to="/login" sx={{ color: 'white', fontWeight: 'bold' }}>
                                    Login
                                </Button>
                            </>
                        )
                   }
                </Box>

                {/* mobile menu icon*/}
                <IconButton edge="end" color="inherit" aria-label="menu"
                    onClick={handleMenuOpen}
                    sx={{ display: { xs: 'block', md: 'none' } }} >

                        <MenuIcon />
                </IconButton>

            </Toolbar>
        </AppBar>
    );
};

export default Navbar;
