import "./Home.css";
import LoginPage from "./Login";

const HomePage: React.FC = () => {
  return (
    <div className="container">
      <h2 className="title">Agriculture Service System</h2>
      <div className="header">
        <LoginPage />
      </div>
      <div className="scrollcontainer">
        <div className="scrolling-image">
          <img className="images" src="../Images/P1.jpg" alt="imageasd" />
          <img className="images" src="../Images/P2.jpg" alt="imageasd" />
          <img className="images" src="../Images/P3.jpg" alt="imageasd" />
          <img className="images" src="../Images/P4.jpg" alt="imageasd" />
        </div>
        <hr style={{ marginTop: "4%" }}></hr>
        <div className="scrolling-text">
          <h3 className="text">Agriculture Service System</h3>
        </div>
        <hr></hr>
      </div>
    </div>
  );
};

export default HomePage;
