import React from "react";
import ReactDOM from "react-dom";
import {App} from "./App"; 
import { CartProvider } from "./CartContext";
import { ToastContainer } from "react-toastify";
import "react-toastify/dist/ReactToastify.css";

ReactDOM.render(
  <React.StrictMode>
    <CartProvider>
      <App />
    </CartProvider>
    <ToastContainer />
  </React.StrictMode>,
  document.getElementById("root")
);
