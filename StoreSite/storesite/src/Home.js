import React, { useState, useEffect } from "react";
import { useCart } from "./CartContext"; // Import the useCart hook
import { HeaderPage } from "./Header";
import { Card, List } from "antd";

export function HomePage() {
  const { cart, addToCart } = useCart(); // Use the useCart hook to access cart and addToCart
  const [products, setProducts] = useState([]);

  useEffect(() => {
    fetch("https://fakestoreapi.com/products")
      .then((response) => response.json())
      .then((data) => setProducts(data));
  }, []);

  return (
    <>
      <div>
        <HeaderPage cart={cart} />
        <List
          style={{ marginLeft: 120, marginTop: "10%" }}
          grid={{ gutter: 14, column: 3 }}
          dataSource={products}
          renderItem={(product) => (
            <List.Item>
              <Card style={{ width: "300px", height: "420px" }}>
                <div key={product.id}>
                  <img
                    style={{ width: "150px", height: "170px" }}
                    src={product.image}
                    alt={product.title}
                  />
                  <h3>{product.title}</h3>
                  <p>${product.price}</p>
                  <button
                    className="buttons"
                    onClick={() => addToCart(product)}
                  >
                    Add To Cart
                  </button>
                </div>
              </Card>
            </List.Item>
          )}
        />
      </div>
    </>
  );
}
