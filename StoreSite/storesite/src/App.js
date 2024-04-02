import React from "react";
import { BrowserRouter, Routes, Route, Link } from "react-router-dom";
import { HomePage } from "./Home";
import { ContactPage } from "./Contact";
import { CartPage } from "./Cart";
import { PaymentPage } from "./Payment";

export function App() {
  return (
    <>
      <BrowserRouter>
        <Routes>
          <Route path="/" element={<HomePage />} />
        </Routes>
        <Routes>
          <Route path="/contact" element={<ContactPage />} />
        </Routes>
        <Routes>
          <Route path="/cart" element={<CartPage />} />
        </Routes>
        <Routes>
          <Route path="/payment" element={<PaymentPage />} />
        </Routes>
      </BrowserRouter>
    </>
  );
}
