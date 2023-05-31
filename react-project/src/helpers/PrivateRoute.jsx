import { Navigate } from 'react-router-dom';
import {useContext} from "react";
import {UserContext} from "../providers/UserProvider";

export { PrivateRoute };

function PrivateRoute({ children }) {
    const { token } = useContext(UserContext);

    if (!token) {
        return <Navigate to="/login"  />
    }
    return children;
}

export default PrivateRoute;
