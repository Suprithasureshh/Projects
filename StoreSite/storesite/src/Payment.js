import React from "react";
import { HeaderPage } from "./Header";
import './Home.css';
import { useCart } from "./CartContext"; 

export function PaymentPage() {
    const { cart } = useCart(); 
  return (
    <div>
     <HeaderPage cart={cart} />
      <div className="paymentlist">
        <p><b>Connects to Payment Gateway</b></p>
        
      </div>
    </div>
  );
}

