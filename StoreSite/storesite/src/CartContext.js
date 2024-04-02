import React, {createContext, useContext, useReducer} from "react";
import {toast} from "react-toastify";
import "react-toastify/dist/ReactToastify.css";

const CartContext = createContext();
const cartReducer = (state, action) => {
  const existingItemIndex = state.findIndex(item => item.id === action.payload.id);

  switch (action.type) {
    case "ADD_TO_CART":
      if (existingItemIndex !== -1) {
        
        toast.info("Item already added to the cart!", {
          position: toast.POSITION.TOP_RIGHT
        });
      } else {
        return [...state, { ...action.payload, quantity: 1 }];
      }
    case "INCREMENT_QUANTITY":
      return state.map((item) =>
        item.id === action.payload.id ? { ...item, quantity: item.quantity + 1 } : item
      );
      case "DECREMENT_QUANTITY":
        if (state[existingItemIndex].quantity === 1) {
          return state.filter(item => item.id !== action.payload.id);
        } else {
          return state.map(item =>
            item.id === action.payload.id ? { ...item, quantity: item.quantity - 1 } : item
          );
        }
        
    default:
      return state;
  }
};

const showToastError = () => {
  toast.info("Item already added to the cart!", {
    position: toast.POSITION.BOTTOM_RIGHT
  });
};
const showToast = () => {
  toast.info("Item Sucessfully added to cart!", {
    position: toast.POSITION.BOTTOM_RIGHT
  });
};

export const CartProvider = ({ children }) => {
  const [cart, dispatch] = useReducer(cartReducer, []);

  const addToCart = (product) => {
    const existingItemIndex = cart.findIndex(item => item.id === product.id);
    if (existingItemIndex !== -1) {
      showToastError();
    } else {
      dispatch({ type: "ADD_TO_CART", payload: product });
      showToast();
    }
  };

  const incrementQuantity = (product) => {
    dispatch({ type: "INCREMENT_QUANTITY", payload: { id: product.id } });
  };
 
  const decrementQuantity = (product) => {
    dispatch({ type: "DECREMENT_QUANTITY", payload: { id: product.id } });
  };
  const cartTotal = parseFloat(cart.reduce((total, item) => total + item.price * item.quantity, 0).toFixed(2));

  return (
    <CartContext.Provider
      value={{
        cart,
        addToCart,
        incrementQuantity,
        decrementQuantity,
        cartTotal,
      }}
    >
      {children}
    </CartContext.Provider>
  );
};

export const useCart = () => {
  return useContext(CartContext);
};


