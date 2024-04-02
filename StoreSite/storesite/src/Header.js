import React, { useState } from "react";
import { BsCart3 } from "react-icons/bs";
import {GiPlainCircle} from "react-icons/gi";
import { Link } from "react-router-dom";
import './Header.css';

export function HeaderPage({ cart }) {
  const [currentPage, setCurrentPage] = useState("home");

  const cartCount = cart.length;

  return (
    <>
      <div className='header'>
        <div><h3 className='home1'><Link className='home1' to="/">Home</Link></h3></div>
        <h3 className='title'>Uttara Shop</h3>
        <div className='contact' onClick={() => setCurrentPage("contact")}>
          <h3><Link className='contact' to="/contact">Contact</Link></h3>
        </div>
        <div className='icon' onClick={() => setCurrentPage("cart")}>
          <h1><Link className='icon' to="/cart">
          {cartCount > 0 && <div className='red-circle'>{cartCount}</div>}
            <BsCart3 style={{position:"absolute", top:18}}/><GiPlainCircle className="circle"/>
            
          </Link></h1>
        </div>
      </div>
    </>
  );
}
