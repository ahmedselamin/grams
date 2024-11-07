import { useState, useContext } from 'react';
import { Link, useNavigate } from 'react-router-dom';
import { AppBar, Toolbar, IconButton, Typography, Button, Menu, MenuItem, Box } from '@mui/material';
import MenuIcon from '@mui/icons-material/Menu';
import { AuthContext } from '../authContext';

const Navbar = () => {
    const [anchorEl, setAnchorEl] = useState(null);
    const { isAuthenticated, logout } = useContext(AuthContext);
    const navigate = useNavigate();

    const handleLogout = () => {
        logout();
        navigate('/');
    };


    const handleMenuOpen = (event) => {
        setAnchorEl(event.currentTarget);
    };


    const handleMenuClose = () => {
        setAnchorEl(null);
    };

    return (
        <>
            <AppBar position="sticky" sx={{
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

                    {/* Desktop */}
                    <Box sx={{ display: { xs: 'none', md: 'block' } }}>
                        <Button component={Link} to="/" sx={{ color: 'white', fontWeight: "bold" }}>
                            Home
                        </Button>
                        {isAuthenticated ? (
                            <>
                                <Button component={Link} to="/notifications" sx={{ color: 'white', fontWeight: "bold" }}>
                                    Notifications
                                </Button>
                                <Button onClick={handleLogout} sx={{ color: 'white', fontWeight: "bold" }}>
                                    Logout
                                </Button>
                            </>
                        ) : (
                            <>
                                <Button component={Link} to="/register" sx={{ color: 'white', fontWeight: "bold" }}>
                                    Register
                                </Button>
                                <Button component={Link} to="/login" sx={{ color: 'white', fontWeight: "bold" }}>
                                    Login
                                </Button>
                            </>
                        )}
                    </Box>

                    {/* Mobile Menu Icon */}
                    <IconButton
                        edge="end"
                        color="inherit"
                        aria-label="menu"
                        onClick={handleMenuOpen}
                        sx={{ display: { xs: 'block', md: 'none' } }}>

                        <MenuIcon />
                    </IconButton>

                    {/* Mobile Dropdown Menu */}
                    <Menu
                        anchorEl={anchorEl}
                        open={Boolean(anchorEl)}
                        onClose={handleMenuClose}
                        sx={{
                            display: { xs: 'block', md: 'none' },
                            '& .MuiPaper-root': {
                                minWidth: '250px', 
                                padding: '10px',
                            },
                        }}
                    >
                        <MenuItem onClick={handleMenuClose} component={Link} to="/">
                            Home
                        </MenuItem>
                        {isAuthenticated ? (
                            <>
                                <MenuItem onClick={handleLogout}>
                                    Logout
                                </MenuItem>
                                <MenuItem onClick={handleMenuClose} component={Link} to="/notifications">
                                    Notifications
                                </MenuItem>
                            </>
                        ) : (
                            <>
                                <MenuItem onClick={handleMenuClose} component={Link} to="/login">
                                    Login
                                </MenuItem>
                                <MenuItem onClick={handleMenuClose} component={Link} to="/register">
                                    Register
                                </MenuItem>
                            </>
                        )}
                    </Menu>
                </Toolbar>
            </AppBar>
        </>
    );
};

export default Navbar;