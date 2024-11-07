import { StrictMode } from 'react'
import { createRoot } from 'react-dom/client'
import { createBrowserRouter, RouterProvider } from "react-router-dom";
import { ThemeProvider } from '@mui/material/styles';
import theme from './theme';
import Layout from "./component/layout";
import Home from "./pages/home";
import Login from "./pages/login";
import Register from "./pages/register";
import Notifications from "./pages/notifications";

const router = createBrowserRouter([
    {
        path: "/",
        element: <Layout />,
        children: [
            {
                path: "/",
                element: <Home />
            },
            {
                path: "/login",
                element: <Login />
            },
            {
                path: "/register",
                element: <Register />
            },
            {
                path: "/notifications",
                element: <Notifications />
            }            
        ]
    }
])

createRoot(document.getElementById('root')).render(
    <StrictMode>
        <ThemeProvider theme={theme}>
           <RouterProvider router={router} />
        </ThemeProvider>
    </StrictMode>,
)
