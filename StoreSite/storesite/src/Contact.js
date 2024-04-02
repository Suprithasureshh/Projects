import React from "react";
import { HeaderPage } from "./Header";
import './Home.css';
import { useCart } from "./CartContext"; 
export function ContactPage() {
    const { cart } = useCart(); 
  return (
    <div>
     <HeaderPage cart={cart} />
      <div className="contactlist">
        <p><b>1st Block, Rajaji Nagar....</b></p>
        <p><b>Phone Number:....</b></p>
      </div>
    </div>
  );
}


