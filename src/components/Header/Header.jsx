import React, {useContext, useState} from 'react';

import styles from './Header.module.scss';
import {IconButton, Badge, Button, List, Drawer, ListItem, ListItemText} from '@mui/material';
import { ShoppingCart } from '@mui/icons-material';
import {UserContext} from "../../providers/UserProvider";
import {CartContext} from "../../providers/CartProvider";
import uuid from "react-uuid";

function Header(props) {
    const [open, setOpen] = useState(false);
    const { token, user } = useContext(UserContext);
    const { cartItemsListCount, cartItems } = useContext(CartContext);

    const toggleDrawer = () => {
        setOpen(!open);
    };

    const handleClose = () => {
        setOpen(false);
    };


    return (
        <>
            <header className={styles.header}>
                <div className="d-flex align-items-center justify-content-center">
                    <a className={styles.logo}>MAKE-UP</a>
                </div>
                {
                    token ?   <IconButton aria-label="Cart" onClick={toggleDrawer}>
                        <Badge badgeContent={cartItemsListCount} color="error">
                            <ShoppingCart />
                        </Badge>
                    </IconButton> : <></>
                }
            </header>
            <Drawer anchor="bottom" open={open} onClose={handleClose}>
                <div
                    style={{width: '100%'}}
                    role="presentation"
                    onClick={handleClose}
                    onKeyDown={handleClose}
                >
                    <List>
                        { cartItems.length ? cartItems.map((product) => {
                            return <ListItem button key={uuid()} style={{width: '100%'}}>
                                <img src={product.item.photoUrl}
                                     style={{width: '100px', height: '100px', marginRight: '10px'}}
                                     alt={product.item.name}/>
                                <ListItemText primary={product.item.name} />
                            </ListItem>
                        }) : 'No cart items'}
                    </List>
                </div>
            </Drawer>
        </>

    );
}

export default Header;


