import React, {useContext, useEffect, useState} from 'react';
import styles from './Products.module.scss'
import {Box, Button, ButtonGroup, Grid} from "@mui/material";
import {UserContext} from "../../providers/UserProvider";
import {ProductContext} from "../../providers/ProductProvider";
import axios from "axios";
import uuid from "react-uuid";
import ProductCard from "./ProductCard/ProductCard";



const Products = () => {
    const {  products, assignProducts } = useContext(ProductContext);
    const { token } = useContext(UserContext);
    const [productsList, setProductsList] = useState([]);

    useEffect(() => {
        if(!products.length) {
            const url = 'https://localhost:7207/Products';
            const headers = {
                'accept': '*/*',
                'Authorization': 'Bearer ' + token
            };
            axios.get(url, {headers})
                .then(response => {
                    setProductsList(response.data);
                    assignProducts(response.data);
                })
                .catch(error => {
                    console.error('Authorization failed', error);
                });
        } else {
            setProductsList(products);
        }

    }, []);

    const filterByType = (title) => {
        if(title === 'All') {
            setProductsList(products);
        } else {
            setProductsList(products.filter(item => item.productType.title === title));
        }
    }

    const filterByBrand = (title) => {
        setProductsList(products.filter(item => item.brand.title === title));
    }


    const typesButtons = [
        <Button key="All" onClick={(e) => {
            e.preventDefault();
            filterByType('All');
        }
        }>All</Button>,
        <Button key="Cosmetics" onClick={(e) => {
            e.preventDefault();
            filterByType('Cosmetics');
        }}>Cosmetics</Button>,
        <Button key="Accessories"  onClick={(e) => {
            e.preventDefault();
            filterByType('Accessories');
        }}>Accessories</Button>,
    ];

    const brandsButtons = [
        <Button key="NYX Professional Makeup" onClick={(e) => {
            e.preventDefault();
            filterByBrand('NYX Professional Makeup');
        }
        }>NYX Professional Makeup</Button>,
        <Button key="L`Oreal Paris" onClick={(e) => {
            e.preventDefault();
            filterByBrand('L`Oreal Paris');
        }
        }>L`Oreal Paris</Button>,
        <Button key="Maybelline New York" onClick={(e) => {
            e.preventDefault();
            filterByBrand('Maybelline New York');
        }
        }>Maybelline New York</Button>,
    ];

    return (
        <div>
            <Box
                sx={{
                    display: 'flex',
                    flexDirection: 'column',
                    alignItems: 'center',
                    '& > *': {
                        m: 1,
                    },
                }}
            >
                <ButtonGroup color="secondary" aria-label="medium secondary button group">
                    {brandsButtons}
                </ButtonGroup>
                <ButtonGroup size="large" aria-label="large button group">
                    {typesButtons}
                </ButtonGroup>
            </Box>
            <Box sx={{ flexGrow: 1 }} style={{margin: '70px'}}>
                <Grid container spacing={{ xs: 2, md: 3 }} columns={{ xs: 4, sm: 8, md: 12 }}>
                    {productsList.map((product) => (
                        <Grid item xs={2} sm={4} md={4} key={product.id}>
                            <ProductCard data={product}/>
                        </Grid>
                    ))}
                </Grid>
            </Box>
        </div>
    );
};

export default Products;
