import { useState } from "react";
import {
  Avatar,
  Button,
  Dropdown,
  Form,
  Input,
  Layout,
  Menu,
  Modal,
  message,
} from "antd";
import {
  DesktopOutlined,
  FieldTimeOutlined,
  UserOutlined,
  ProfileOutlined,
  PhoneOutlined,
  LockOutlined,
} from "@ant-design/icons";
import {
  BrowserRouter as Router,
  Route,
  Link,
  Routes,
  useNavigate,
} from "react-router-dom";
import joy from "../Main_module/joy.png";
import AddTimesheet from "./Emp_Timesheet/ETimesheet";
import axios from "axios";

const { Header, Content, Sider } = Layout;

export function EmpTimesheet() {
  const [collapsed, setCollapsed] = useState(false);
  const [selectedKeys, setSelectedKeys] = useState<Array<any>>([]);
  const onCollapse = (collapsed: any) => {
    setCollapsed(collapsed);
  };

  const handleMenuClick = (e: any) => {
    setSelectedKeys([e.key]);
  };

  const navigate = useNavigate();
  function handleLogout() {
    window.history.replaceState(null, "", "/");
    navigate("/", { replace: true });
    localStorage.clear();
  }

  const [showChangePasswordModal, setShowChangePasswordModal] = useState(false);
  const handleChangePasswordClick = () => {
    setShowChangePasswordModal(true);
  };

  const handleCPCancel = () => {
    setShowChangePasswordModal(false);
  };
  const mailId = localStorage.getItem("mailId");
  function ChangePassword() {
    const onFinish = (values: any) => {
      axios({
        method: "post",
        headers: {
          "Content-Type": "application/json",
          "Access-Control-Allow-Origin": "*",
          "Access-Control-Allow-Methods": "GET,PUT,POST,DELETE,PATCH,OPTIONS",
        },
        url: "/api/Login/Change-password",
        data: values,
      })
        .then((response) => {
          message.success("Password updated successfully");
        })
        .catch((error) => {
          message.error(error.response.data);
        });
    };

    return (
      <>
        <Form onFinish={onFinish} initialValues={{ email: mailId || "" }}>
          <Form.Item
            name="email"
            rules={[
              {
                required: true,
                message: "Please input your email!",
              },
              {
                type: "email",
                message: "Please enter a valid email address!",
              },
            ]}
          >
            <Input prefix={<UserOutlined />} placeholder="Email" />
          </Form.Item>
          <Form.Item
            name="password"
            rules={[
              {
                required: true,
                message: "Please input old password!",
              },
            ]}
          >
            <Input.Password
              prefix={<LockOutlined />}
              placeholder="Old password"
            />
          </Form.Item>

          <Form.Item
            name="newPassword"
            rules={[
              {
                required: true,
                message: "Please enter a new password",
              },
              {
                min: 8,
                message: "Password must be at least 8 characters long",
              },
              {
                pattern:
                  /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[!@#$%^&*()_+\-=[\]{};':"\\|,.<>/?])[A-Za-z\d!@#$%^&*()_+\-=[\]{};':"\\|,.<>/?]{8,}$/,
                message:
                  "Password must contain at least one uppercase letter, one lowercase letter, one number, and one special character",
              },
            ]}
          >
            <Input.Password
              prefix={<LockOutlined />}
              placeholder="New password"
            />
          </Form.Item>
          <Form.Item
            name="confrimNewPassword"
            dependencies={["newPassword"]}
            rules={[
              { required: true, message: "Please re-enter your new password!" },
              ({ getFieldValue }) => ({
                validator(_, value) {
                  if (!value || getFieldValue("newPassword") === value) {
                    return Promise.resolve();
                  }
                  return Promise.reject(
                    new Error(
                      "The two passwords that you entered do not match."
                    )
                  );
                },
              }),
            ]}
          >
            <Input.Password
              prefix={<LockOutlined />}
              placeholder="Confirm new password"
            />
          </Form.Item>

          <Form.Item>
            <Button type="primary" htmlType="submit">
              Update password
            </Button>
          </Form.Item>
        </Form>
      </>
    );
  }
  function UserDetails() {
    const userMenu = (
      <Menu>
        <Menu.Item key="logout" onClick={handleLogout}>
          Logout
        </Menu.Item>
        <Menu.Item key="logout" onClick={handleChangePasswordClick}>
          Change Password
        </Menu.Item>
      </Menu>
    );

    return (
      <div style={{ display: "flex", alignItems: "right", marginLeft: 400 }}>
        <Dropdown overlay={userMenu} placement="bottomRight">
          <Avatar
            style={{
              backgroundColor: "navy",
              cursor: "pointer",
              marginRight: 50,
            }}
            icon={<UserOutlined />}
          />
        </Dropdown>
      </div>
    );
  }
  return (
    <>
      <Layout style={{ minHeight: "100vh", position: "fixed" }}>
        <Header
          style={{
            display: "flex",
            alignItems: "center",
            background: "rgba(19, 19, 109, 0.3)",
            padding: 0,
          }}
        >
          <img
            style={{
              width: 68,
              display: "inline-block",
              marginLeft: 20,
              marginRight: 100,
              color: "white",
              filter: "saturate(100%)",
            }}
            src={joy}
            alt="JOY"
          />
          <h1
            style={{
              color: "black",
              fontSize: 24,
              textAlign: "center",
              marginBottom: 10,
              marginTop: 2,
              fontFamily: "Roboto",
              marginLeft: 15,
              marginRight: 390, // Add margin to separate the image and text
            }}
          >
            𝐉𝐨𝐲 𝐈𝐓 𝐒𝐨𝐥𝐮𝐭𝐢𝐨𝐧𝐬
          </h1>
          <UserDetails />
        </Header>
        <Layout style={{ minHeight: "100vh" }}>
          <Sider
            collapsible
            collapsed={collapsed}
            onCollapse={onCollapse}
            style={{ marginTop: 0 }}
          >
            <div className="logo" />

            <Menu
              theme="dark"
              onClick={handleMenuClick}
              mode="inline"
              style={{ marginTop: 5 }}
              selectedKeys={selectedKeys}
            >
              <Menu.Item key="" icon={<DesktopOutlined />}>
                <Link to="/employee/dashboard">Dashboard</Link>
              </Menu.Item>
              <Menu.Item key="timesheetsummary" icon={<FieldTimeOutlined />}>
                <Link to="/employee/timesheetsummary">Timesheet Summary</Link>
              </Menu.Item>
              <Menu.Item key="timesheet" icon={<ProfileOutlined />}>
                <Link to="/employee/timesheet">Timesheet </Link>
              </Menu.Item>
              <Menu.Item key="hrcontactinfo" icon={<PhoneOutlined />}>
                <Link to="/employee/hrcontact">HR Contact</Link>
              </Menu.Item>
              <Menu.Item key="userprofile" icon={<UserOutlined />}>
                <Link to="/employee/userprofile">User Profile</Link>
              </Menu.Item>
            </Menu>
          </Sider>

          <Header className="site-layout-background" style={{ padding: 0 }} />
          <Content
            style={{
              height: "calc(100vh )",
              overflow: "scroll",
              padding: "0% 3% 30% 2% ",
              paddingLeft: 50,
              background:
                "-webkit-linear-gradient(45deg,rgba(255, 192, 203, 0.7), rgba(135, 206, 235, 0.4) 100%)",
            }}
          >
            <AddTimesheet />
          </Content>
          <Modal
            title="Change Password"
            open={showChangePasswordModal}
            onCancel={handleCPCancel}
            footer={null}
          >
            <ChangePassword />
          </Modal>
        </Layout>
      </Layout>
    </>
  );
}
