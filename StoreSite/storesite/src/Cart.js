import React from "react";
import { useCart } from "./CartContext";
import { HeaderPage } from "./Header";
import { Link } from "react-router-dom";
export function CartPage() {
  const { cart, incrementQuantity, decrementQuantity, cartTotal } = useCart();

  return (
    <div>
      <HeaderPage cart={cart} />
      {cart.length === 0 ? (
        <h3 style={{ marginTop: "8%", textAlign: "center", color: "red" }}>
          Your Shopping Cart is empty.
        </h3>
      ) : (
        <div>
          <h3 style={{ marginTop: "8%", marginLeft: "23%" }}>
            Your Cart Items
          </h3>
          <ul style={{ listStyle: "none", padding: 0 }}>
            {cart.map((item) => (
              <li key={item.id} style={{ marginBottom: "20px" }}>
                <div
                  style={{
                    display: "flex",
                    justifyContent: "space-between",
                    alignItems: "center",
                    marginLeft: "23%",
                    marginTop: "2%",
                    width: "50%",
                    height: "80px",
                    boxShadow: "0px 4px 8px rgba(0, 0, 0, 0.3)",
                    padding: "20px",
                  }}
                >
                  <img
                    style={{
                      width: "100px",
                      height: "70px",
                      marginRight: "10px",
                    }}
                    src={item.image}
                    alt={item.title}
                  />
                  <div>
                    <h4>{item.title}</h4>
                    <b>
                      {" "}
                      <p style={{ marginLeft: "38%" }}>${item.price}</p>
                    </b>
                  </div>
                  <div style={{ marginLeft: "20px" }}>
                    <b>
                      <Link
                        className="increment"
                        style={{
                          fontSize: "35px",
                          textDecoration: "none",
                          color: "black",
                        }}
                        onClick={() => decrementQuantity(item)}
                      >
                        -
                      </Link>
                    </b>
                    <b>
                      <span style={{ margin: "0 10px" }}>{item.quantity}</span>
                    </b>
                    <b>
                      <Link
                        className="decrement"
                        style={{
                          fontSize: "19px",
                          textDecoration: "none",
                          color: "black",
                        }}
                        onClick={() => incrementQuantity(item)}
                      >
                        +
                      </Link>
                    </b>
                  </div>
                </div>
              </li>
            ))}
          </ul>
          <b>
            <p style={{ color: "green", marginLeft: "60%" }}>
              Total Amount: ${cartTotal}
            </p>{" "}
          </b>
          <button
            style={{
              backgroundColor: "#111827",
              marginLeft: "43%",
              height: "35px",
              borderRadius: "5px",
            }}
          >
            <Link
              style={{
                textDecoration: "none",
                color: "white",
                fontSize: "14px",
              }}
              to="/"
            >
              Continue Shopping
            </Link>
          </button>
          <button
            style={{
              backgroundColor: "#111827",
              marginLeft: "2%",
              height: "35px",
              borderRadius: "5px",
            }}
          >
            <Link
              style={{
                textDecoration: "none",
                color: "white",
                fontSize: "14px",
              }}
              to="/payment"
            >
              Checkout
            </Link>
          </button>
        </div>
      )}
    </div>
  );
}
