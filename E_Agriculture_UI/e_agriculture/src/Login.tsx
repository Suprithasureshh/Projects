import {
  Button,
  Checkbox,
  DatePicker,
  Form,
  Input,
  Modal,
  Select,
  message,
} from "antd";
import axios from "axios";
import { useState } from "react";
import { Link, useNavigate } from "react-router-dom";
import "./Login.css";
import { PiUserBold, PiUserFill } from "react-icons/pi";
import { BiLock } from "react-icons/bi";
import { ForgotPassword } from "./ForgetPassword";
import { jwtDecode } from "jwt-decode";
import { CaretDownOutlined, CaretUpOutlined } from "@ant-design/icons";

const LoginPage: React.FC = () => {
  //for login
  const [loginUser, setLoginUser] = useState("");
  const [loginPassword, setLoginPassword] = useState("");
  const [loginUserAs, setLoginUserAs] = useState<string | undefined>(undefined);
  const [passwordError, setPasswordError] = useState("");
  const [emailError, setEmailError] = useState("");
  const [isModalOpen, setIsModalOpen] = useState(false);
  const [isSignupModalOpen, setIsSignupModalOpen] = useState(false);
  const [isButtonClicked, setIsButtonClicked] = useState(false);
  const [isSignupButtonClicked, setIsSignupButtonClicked] = useState(false);
  const navigate = useNavigate();

  const handleLoginUserChange = (e: any) => {
    setLoginUser(e.target.value);
    console.log(e.target.value);
  };
  const handleLoginPasswordChange = (e: any) => {
    setLoginPassword(e.target.value);
  };
  const validateInputs = () => {
    let isValid = true;

    // Validate email
    if (loginUser === "") {
      setEmailError("*Email is required");
      isValid = false;
    } else {
      setEmailError("");
    }

    // Validate password
    if (loginPassword === "") {
      setPasswordError("*Set a password");
      isValid = false;
    } else {
      setPasswordError("");
    }
    return isValid;
  };

  const handleSignInClick = (e: any) => {
    e.preventDefault();
    validateInputs();
    sessionStorage.setItem("email", loginUser);

    if (validateInputs()) {
      const userData = {
        email: loginUser,
        password: loginPassword,
        userAs: loginUserAs,
      };
      console.log(userData);
      axios
        .post("/api/Login/Login", userData)
        .then((response) => {
          const token = response.data;
          const decodedToken: any = jwtDecode(token);
          console.log(decodedToken);
          message.success("Login Successful");

          if (userData.userAs === "Farmer") {
            navigate("/farmerpage");
          } else if (userData.userAs === "Buyer") {
            navigate("/buyerpage");
          } else if (userData.userAs === "Student") {
            navigate("/studentpage");
          } else if (userData.userAs === "Admin") {
            navigate("/adminpage");
          } else {
            navigate("/home");
          }
        })
        .catch((error: any) => {
          message.error(error.response.data);
        });
    }
  };

  const onFinish = (values: any) => {
    axios({
      method: "post",
      headers: { "Content-Type": "application/json" },
      url: "/api/E_Agriculture/AddUserDetails",
      data: values,
    })
      .then((r: any) => {
        message.success("Your record have been added successfully");
        navigate("/home");
      })
      .catch((error: any) => {
        message.error(error.response.data);
      });
  };

  const showsignupmodal = () => {
    setIsSignupModalOpen(true);
    setIsModalOpen(false);
    setIsSignupButtonClicked(true);
  };
  const signupmodalcancle = () => {
    setIsSignupModalOpen(false);
    setIsSignupButtonClicked(false);
  };

  const showModal = () => {
    setIsModalOpen(true);
    setIsSignupModalOpen(false);
    setIsButtonClicked(true);
  };
  const handleCancel = () => {
    setIsModalOpen(false);
    setIsButtonClicked(false);
  };
  return (
    <div className="form-container">
      <Button
        type="link"
        className={`signup-button ${
          isSignupButtonClicked ? "button-clicked" : ""
        }`}
        onClick={isSignupModalOpen ? signupmodalcancle : showsignupmodal}
      >
        Register{""}
        {isSignupModalOpen ? <CaretUpOutlined /> : <CaretDownOutlined />}
      </Button>
      <Button
        type="link"
        className={`signin-button ${isButtonClicked ? "button-clicked" : ""}`}
        onClick={isModalOpen ? handleCancel : showModal}
      >
        Sign In{""}
        {isModalOpen ? <CaretUpOutlined /> : <CaretDownOutlined />}
      </Button>
      <Modal
        className="login-modal"
        open={isModalOpen}
        onCancel={handleCancel}
        footer={[]}
        closable={false}
        width={270}
        bodyStyle={{ border: "none" }}
      >
        <form action="#">
          <Input
            prefix={<PiUserFill size={20} />}
            className="mailto"
            type="text"
            placeholder="Email"
            value={loginUser}
            onChange={handleLoginUserChange}
          />
          {emailError === "" ? (
            <br></br>
          ) : (
            <span style={{ color: "red" }} className="mailerr">
              {emailError}
            </span>
          )}{" "}
          <br></br>
          <Input.Password
            prefix={<BiLock size={20} />}
            className="pass"
            type="password"
            placeholder="Password"
            value={loginPassword}
            onChange={handleLoginPasswordChange}
          />
          {passwordError === " " ? (
            <br></br>
          ) : (
            <span className="passerr" style={{ color: "red" }}>
              {passwordError}
            </span>
          )}
          <br></br>
          <Select
            placeholder="UserAs"
            value={loginUserAs}
            style={{ width: 222, marginTop: "10%", zIndex: 999 }}
            onChange={(value) => setLoginUserAs(value)}
          >
            <Select.Option value="Admin">Admin</Select.Option>
            <Select.Option value="Farmer">Farmer</Select.Option>
            <Select.Option value="Buyer">Buyer</Select.Option>
            <Select.Option value="Student">Student</Select.Option>
          </Select>
          <Checkbox className="check">Remember me</Checkbox>
          <h3 className="fpass">
            <ForgotPassword />
          </h3>
          <button className="signin" onClick={handleSignInClick}>
            Sign In
          </button>
          <br></br>
          <p style={{ color: "rgb(24, 23, 48)", marginLeft: "24%" }}>
            New here?{" "}
            <Button
              type="link"
              style={{ textDecoration: "none", color: "#0352bf" }}
              onClick={showsignupmodal}
            >
              Join Us
            </Button>
          </p>
        </form>
      </Modal>
      <div style={{ zIndex: "1" }}>
        <Modal
          className="signup-modal"
          open={isSignupModalOpen}
          onCancel={signupmodalcancle}
          footer={[]}
          closable={false}
          width={370}
          bodyStyle={{ border: "none" }}
        >
          {" "}
          <h3 style={{ color: "#101e8a" }}>
            Welcome to Agriculture Service System
          </h3>
          <Form
            name="basic"
            initialValues={{ remember: true }}
            autoComplete="off"
            onFinish={onFinish}
            style={{ width: "79%", marginLeft: "7%" }}
            // form={form}
          >
            <Form.Item
              label="UserName"
              name="user_Name"
              rules={[
                { required: true, message: "Please input your UserName!" },
              ]}
              className="uname"
            >
              <Input />
            </Form.Item>
            <Form.Item
              label="Address"
              name="address"
              rules={[
                { required: true, message: "Please input your Address!" },
              ]}
              className="addressselect"
            >
              <Input.TextArea style={{ marginLeft: "5%" }} />
            </Form.Item>
            <Form.Item
              label="Email"
              name="email"
              rules={[
                { required: true, message: "Please input your Mail!" },
                {
                  type: "email",
                  message: "Please enter a valid email address!",
                },
              ]}
              className="mailselect"
            >
              <Input style={{ marginLeft: "12%" }} />
            </Form.Item>

            <Form.Item
              label="PhoneNumber"
              name="phoneNumber"
              rules={[
                { required: true, message: "Please input your PhoneNumber!" },
                {
                  pattern: /^\d{10}$/,
                  message: "Please enter a valid PhoneNumber!",
                },
                // Add the custom validation rule
              ]}
              className="phone"
            >
              <Input />
            </Form.Item>

            <Form.Item
              label="Date"
              name="joining_Date"
              rules={[
                { required: true, message: "Provide your Date_Of_Birth!" },
              ]}
            >
              <DatePicker
                placeholder="Select Date"
                style={{ marginLeft: "15%" }}
              />
            </Form.Item>
            <Form.Item
              name="password"
              label="Password"
              hasFeedback
              rules={[
                { required: true, message: "Please enter your password!" },
              ]}
              className="passselect"
            >
              <Input.Password />
            </Form.Item>

            <Form.Item
              label="User As"
              name="userAs"
              rules={[{ required: true, message: "Field is mandatory!" }]}
              className="userselect"
            >
              <Select style={{ width: 155, marginLeft: "5%" }}>
                <Select.Option value="Admin">Admin</Select.Option>
                <Select.Option value="Farmer">Farmer</Select.Option>
                <Select.Option value="Buyer">Buyer</Select.Option>
                <Select.Option value="Student">Student</Select.Option>
              </Select>
            </Form.Item>
            <Form.Item>
              <Button className="signup" htmlType="submit">
                Register
              </Button>
            </Form.Item>
          </Form>
        </Modal>
      </div>
    </div>
  );
};
export default LoginPage;
