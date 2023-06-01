import React, { createContext, useState } from 'react';

const ProductContext = createContext();

const ProductProvider = ({ children }) => {
    const [products, setAllProducts] = useState([]);


    const assignProducts = (data) => {
        setAllProducts(data);
    };


    const contextValue = {
        products, assignProducts
    };

    return <ProductContext.Provider value={contextValue}>{children}</ProductContext.Provider>;
};

export { ProductProvider, ProductContext };
