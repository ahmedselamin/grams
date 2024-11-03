import { createTheme } from '@mui/material/styles';

const theme = createTheme({
    palette: {
        primary: {
            main: "#005477"  // oceanic blue color 
        },
        secondary: {
            main: "#f7fcfc"  // off white color
        },
        third: {
            main: "#d32f2f"  // red for errors
        },
        background: {
            default: '#fff', // White background
        },
        text: {
            primary: '#000', // Black text
            secondary: '#729c8c', // Greyish secondary text
        },
    },
});