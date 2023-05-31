import './App.css';
import Header from "./components/Header/Header";
import Login from "./components/Login/Login";
import {UserProvider} from "./providers/UserProvider";
import {Route, Routes} from "react-router-dom";
import Products from "./components/Products/Products";
import PrivateRoute from "./helpers/PrivateRoute";
import {ProductProvider} from "./providers/ProductProvider";
import ProductsDetails from "./components/Products/ProductsDetail/ProductsDetail";
import {CartProvider} from "./providers/CartProvider";

function App() {
  return (
      <UserProvider>
          <ProductProvider>
              <CartProvider>
                  <Header/>
                  <Routes>
                      <Route
                          path="/"
                          element={
                              <PrivateRoute>
                                  <Products />
                              </PrivateRoute>
                          }
                      />
                      <Route path="/login" element={<Login />}/>
                      <Route path="/products/:productId" element={<ProductsDetails />}/>
                  </Routes>
              </CartProvider>
          </ProductProvider>
      </UserProvider>
  );
}

export default App;
