import React, { useState } from 'react';
import { Layout, Menu, theme } from 'antd';
import Tab from './TodoTable';
import { UserOutlined } from '@ant-design/icons';
const { Header, Content, Footer, Sider } = Layout;



const NavHead = () => {
  const {
    token: { colorBgContainer },
  } = theme.useToken();

  const [isTableVisible, setIsTableVisible] = useState(false);

  const handleMenuClick = () => {
    setIsTableVisible(true);
  };

  return (
    <Layout>
      <Sider
        breakpoint="lg"
        collapsedWidth="0"
        onBreakpoint={(broken) => {
          console.log(broken);
        }}
        onCollapse={(collapsed, type) => {
          console.log(collapsed, type);
        }}
      >
        <div className="logo" />
        <Menu
          theme="dark"
          mode="inline"
          defaultSelectedKeys={['1']}
          items={[
            {
              key: '1',
              icon: <UserOutlined />,
              label: 'TODO List ',
              onClick: handleMenuClick, 
            },
           
          ]}
        />
      </Sider>
      <Layout>
        <Header style={{ textAlign:'center', backgroundColor:'darkgrey', color:'black', background: colorBgContainer, fontSize: '24px', fontWeight: 'bold' }} >TODO List</Header>
        <Content style={{ margin: '24px 16px 0' }}>
          <div style={{ padding: 24, minHeight: 360, background: colorBgContainer }}>
            {isTableVisible && <Tab />} {/* Render the Tab only if isTableVisible is true */}
          </div>
        </Content>
        <Footer style={{ textAlign: 'center' }}>ToDo List of ©2023 Created by JOYIT..</Footer>
      </Layout>
    </Layout>
  );
};

export default NavHead;