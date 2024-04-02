import React, { useState } from 'react';
import { Form, Button,  Input, DatePicker,  message, Select,Modal } from 'antd';
import { Option } from 'antd/lib/mentions';
import axios from "axios";


const Postforms: React.FC = () => {
  const [isModalOpen, setIsModalOpen] = useState(false);

  const TodoPost = (values: any) => {
    console.log(values);
    axios({
      method: 'post',
      headers: {'Content-Type': 'application/json',},
      url: "/api/todo",
      data:  values,   
    })
    .then((r : any) => {
    message.success('Data Successfully Stored..');
    })
    .catch((error: any) => {
      console.log(error.data)
      message.error(error.response.data);
    });
  };


  return (
    <>
      <Button  id="add" type="primary" onClick={()=>setIsModalOpen(true)}>Add ToDo List</Button> 
      
      <Modal title="ToDo Modal" open={isModalOpen} footer={null} 
      onCancel={()=>setIsModalOpen(false)}>
        <Form labelCol={{span:12}} wrapperCol={{span:20 }} style={{ maxWidth: 400 }} onFinish={TodoPost}  >
          <Form.Item name="title" label="Title"
            rules={[
              {
                required: true,
                message:"Please enter your title",
              },
             
            ]}
            hasFeedback
            >
            <Input placeholder="Type your title"/>
          </Form.Item>
          <Form.Item name="description" label="Description"
           rules={[
              {
                required: true,
                message:"Please enter your title",
              },
             
            ]}
            hasFeedback
            >
            <Input.TextArea placeholder="Type your description" autoSize={{ minRows: 2, maxRows: 4 }} />
          </Form.Item>
          <Form.Item name="due_date" label="Due_Date"
            rules={[
              { required: true, 
                message:"Please provide Due_Date"
              },
            ]}hasFeedback>
            <DatePicker
              placeholder="Select Due_Date"
              style={{ width: "100%" }}
            />
          </Form.Item>
          <Form.Item label="Status" name="status"
            rules={[
              {required: true,message:"Field should not be empty"}
            ]}>
            <Select  > 
              <Option >--Select--</Option> {/* @ts-ignore */}
             <Option value={true}>Pending</Option>
            </Select>
          </Form.Item>  
          <div className='buttons'>
            <Form.Item>
              <Button style={{marginLeft:'55%'}}id="b1" type="primary" htmlType="submit" className='btn'>Submit</Button>
              <Button style={{marginLeft:'2%'}} id="b2" type="primary" htmlType="reset" >Reset</Button>
            </Form.Item>
          </div>
                
        </Form>
           
      </Modal>
   </>
  )
}
            

export default Postforms;
