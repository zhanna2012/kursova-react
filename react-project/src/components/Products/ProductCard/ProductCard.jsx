import React, {useContext} from 'react';
import {
    Button,
    Card,
    CardActionArea,
    CardActions,
    CardContent,
    CardMedia,
    Typography
} from "@mui/material";
import {useNavigate} from "react-router-dom";
import {ProductContext} from "../../../providers/ProductProvider";
import {CartContext} from "../../../providers/CartProvider";



const ProductCard = (props) => {
    const navigate = useNavigate();
    const {  products, assignProducts } = useContext(ProductContext);
    const { cartItems, assignCart } = useContext(CartContext);

    return (
        <Card sx={{ maxWidth: 345 }}>
            <CardActionArea>
                <CardMedia
                    component="img"
                    height="140"
                    image={props.data.item.photoUrl}
                    alt={props.data.item.name}
                />
                <CardContent>
                    <Typography gutterBottom variant="h5" component="div">
                        {props.data.item.name}
                    </Typography>
                    <Typography variant="body2" color="text.secondary">
                        {props.data.item.description}
                    </Typography>
                </CardContent>
            </CardActionArea>
            <CardActions>
                <Button size="small" color="primary" onClick={(e) => {
                    e.preventDefault();
                    console.log(products)
                    navigate(`/products/${props.data.id}`)
                }
                }>
                    More info
                </Button>
                <Button size="small" color="primary" onClick={(e) => {
                    e.preventDefault();
                    console.log(cartItems);
                    console.log(props.data);
                    if(cartItems.length) {
                        assignCart([...cartItems, props.data]);
                    } else {
                        assignCart([props.data]);
                    }

                    console.log(cartItems);
                }
                }>
                    Add To Cart
                </Button>
            </CardActions>
        </Card>
    );
};

export default ProductCard;
