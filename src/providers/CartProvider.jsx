import React, { createContext, useState } from 'react';

const CartContext = createContext();

const CartProvider = ({ children }) => {
    const [cartItems, setCartItems] = useState([]);
    const [cartItemsListCount, setCartItemsCount] = useState(0);

    const addCartItem = (cartItem) => {
        setCartItems([...cartItems, cartItem]);
        setCartItemsCount(cartItems.length);
    };

    const removeCartItem = (cartItem) => {
        setCartItems([...cartItems.filter(item => item !== cartItem.id)]);
        setCartItemsCount(cartItems.length);
    };

    const assignCart = (cartItems) => {
        setCartItems(cartItems);
        setCartItemsCount(cartItems.length);
    };

    const assignCartCount = (count) => {
        setCartItemsCount(count)
    };

    const contextValue = {
        cartItems,
        cartItemsListCount,
        addCartItem,
        removeCartItem,
        setCartItems,
        setCartItemsCount,
        assignCart,
        assignCartCount
    };

    return <CartContext.Provider value={contextValue}>{children}</CartContext.Provider>;
};

export { CartContext, CartProvider };
