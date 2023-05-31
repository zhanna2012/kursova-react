import React, {useContext, useEffect, useState} from 'react';
import {useParams} from "react-router-dom";
import {ProductContext} from "../../../providers/ProductProvider";
import {
    Avatar,
    Card,
    CardActionArea,
    CardContent,
    CardMedia, Divider,
    Grid,
    IconButton,
    ImageListItem,
    ImageListItemBar, List, ListItem, ListItemAvatar, ListItemText, TextField, Typography
} from "@mui/material";
import axios from "axios";
import {UserContext} from "../../../providers/UserProvider";
import uuid from "react-uuid";



const ProductsDetails = (props) => {
    const { productId } = useParams();
    const [comments, setComments] = useState([]);
    const [commentChanged, setCommentChanged] = useState([]);
    const [commentValue, setCommentValue] = useState('');
    const {  products } = useContext(ProductContext);
    const { token, user } = useContext(UserContext);
    const product = products.find((item) => item.id.toString() === productId);


    useEffect(() => {
        const url = 'https://localhost:7207/Comments/' + productId;
        const headers = {
            'accept': '*/*',
            'Authorization': 'Bearer ' + token
        };
        axios.get(url, {headers})
            .then(response => {
                setComments(response.data);
            })
            .catch(error => {
                console.error('Loading failed', error);
            });
    }, []);


    useEffect(() => {
        const url = 'https://localhost:7207/Comments/' + productId;
        const headers = {
            'accept': '*/*',
            'Authorization': 'Bearer ' + token
        };
        axios.get(url, {headers})
            .then(response => {
                setComments(response.data);
            })
            .catch(error => {
                console.error('Loading failed', error);
            });
    }, [commentChanged]);



    const postComment = () => {
        if(commentValue) {
            const url = 'https://localhost:7207/Comments';
            const headers = {
                'accept': '*/*',
                'Content-Type': 'application/json-patch+json',
                'Authorization': 'Bearer ' + token
            };
            const userId = user.email.includes('john') ? 2 : 1;
            const rating = 5;
            const productIdRes = parseInt(productId);
            axios.post(url, {
                text: commentValue,
                userId: userId,
                productId: productIdRes,
                rating: rating,
            }, {headers})
                .then(response => {
                    setCommentChanged(response.data);
                    setCommentValue('');
                })
                .catch(error => {
                    console.error('Authorization failed', error);
                });
        }
    }



    return (
        <>
            <Grid container rowSpacing={1} columnSpacing={{ xs: 1, sm: 2, md: 3 }} style={{padding: '100px'}}>
                <Grid item xs={5}>
                    <ImageListItem>
                        <img
                            src={product.item.photoUrl}
                            alt={product.item.title}
                            loading="lazy"
                        />
                    </ImageListItem>
                </Grid>
                <Grid item xs={7}>
                    <Typography gutterBottom variant="h3" component="div">
                        {product.item.name}
                    </Typography>
                    <Card >
                        <CardActionArea>
                            <CardMedia
                                component="img"
                                height="140"
                                image={product.brand.photoUrl}
                                alt={product.brand.title}
                            />
                            <CardContent>
                                <Typography gutterBottom variant="h5" component="div">
                                    Brand:  {product.brand.title}
                                </Typography>
                            </CardContent>
                        </CardActionArea>
                    </Card>
                    <Typography gutterBottom variant="h7" component="div">
                        Type:  {product.productType.title}
                    </Typography>
                    <Typography gutterBottom variant="h7" component="div">
                        Weight:  {product.item.weight ? product.item.weight : 'No Information'}
                    </Typography>
                    <Typography gutterBottom variant="h7" component="div">
                        Price:  {product.item.price ? product.item.price : 'No Information'}
                    </Typography>
                    <Typography gutterBottom variant="h7" component="div">
                        Description:  {product.item.description ? product.item.description : 'No Information'}
                    </Typography>
                    <Typography gutterBottom variant="h7" component="div">
                        Usage:  {product.item.productUsage ? product.item.productUsage : 'No Information'}
                    </Typography>
                    <Typography gutterBottom variant="h7" component="div">
                        Color:  {product.item.color ? product.item.color : 'No Information'}
                    </Typography>
                    <Typography gutterBottom variant="h7" component="div">
                        Country:  {product.item.country ? product.item.country : 'No Information'}
                    </Typography>
                    <Typography gutterBottom variant="h7" component="div">
                        Ingredients:  {product.item.ingredients ? product.item.ingredients : 'No Information'}
                    </Typography>
                </Grid>
            </Grid>
            <div style={{padding: '100px', width: '100%'}}>
                <TextField
                    style={{width: '70%'}}
                    id="outlined-multiline-static"
                    label="Write a comment"
                    multiline
                    rows={4}
                    onChange={(e) => {
                        e.preventDefault();
                        setCommentValue(e.target.value);
                    }
                    }
                    onKeyDown={(e) => {
                        if (e.key === 'Enter') {
                            postComment();
                        }
                    }
                    }
                />
                <List sx={{ width: '100%', maxWidth: 360, bgcolor: 'background.paper' }}>
                    {
                        comments.map((item) => {
                            return <>
                                <ListItem alignItems="flex-start" key={uuid()}>
                                    <ListItemAvatar>
                                        <Avatar alt={item.user.firstName} src={item.user.photoUrl} />
                                    </ListItemAvatar>
                                    <ListItemText
                                        primary={item.user.firstName + ' ' + item.user.lastName}
                                        secondary={
                                            <React.Fragment>
                                                {item.text}
                                            </React.Fragment>
                                        }
                                    />
                                </ListItem>
                                <Divider variant="inset" component="li" />
                            </>
                        })
                    }

                </List>
            </div>

        </>
    );
};

export default ProductsDetails;
