import { Button,  DatePicker, Form, Input, message, Modal, Pagination, Popconfirm, Select,  Table} from 'antd';
import axios, { all } from 'axios';
import { useEffect, useState } from 'react';
import { SearchOutlined, UndoOutlined } from '@ant-design/icons';
import Postforms from './Postform';
import form from 'antd/es/form';
import moment from 'moment';
import { useForm } from 'antd/es/form/Form';

const Tab: React.FC = () => {
  const [tableData, setData] = useState<Array<any>>([]);
  const [searchQuery, setSearchQuery] = useState<string>("");
  const [selectedRowKeys, setSelectedRowKeys] = useState([]);
  const [selectedRows, setSelectedRows] = useState([]);
  const [currentPage, setCurrentPage] = useState<number>(1);
  const [setpageSize, setPageSize] = useState<number>();
  const setIsFormVisible = (visible: boolean) => {}
  const [isModalOpen, setIsModalOpen] = useState(false);
  const { Option } = Select;
  const [form] = useForm(); 
  const [pageSize, PageSize] = useState(3);
  useEffect(() => {
    getDetails();
  }, [searchQuery]);
  

  const getDetails = (status?:Boolean) => { //To get active and deactive ecords
    const isActives=status==undefined?true:status
    axios({
      method: "get",
      headers: { "Content-Type": "application/json" },
      url: `/api/Todo/Pending and Complete?Status=${isActives}`,
    })
    .then((response: any) => {
      console.log(response.data);
      const filteredData = response.data.filter((item: any) =>
      item.title.toLowerCase().includes(searchQuery.toLowerCase()) ||
      item.description.toLowerCase().includes(searchQuery.toLowerCase()) ||
      item.due_Date.toString().slice(0,10).toLowerCase().includes(searchQuery.toLowerCase()) ||
      item.status.toString().toLowerCase().includes(searchQuery.toLowerCase())
    );
      setData(filteredData);
      message.success("Process Done");
    })
    .catch((error: any) => {
      console.log(error.data)
      message.error(error.response.data);
    });
  }; 
  const rowSelection = {
    selectedRowKeys,
    onChange: (selectedKeys: any, selectedRows: any) => {
      setSelectedRowKeys(selectedKeys);
      setSelectedRows(selectedRows);
      console.log(selectedKeys, selectedRows);
      // Update the checked property of the selected rows
      const updatedTableData = tableData.map((row) =>
        selectedRows.includes(row) ? { ...row, checked: true } : row
      );
      setData(updatedTableData);
      
    },
  };
 
 const  PendingandCompleted = (status:boolean) => {
    const selectedRecord = tableData.filter((row) => row.checked);
    if (selectedRecord.length === 0) {
      message.warning("Please select at least one row");
      return;
    }
    Promise.all(
      selectedRecord.map((row) =>
        axios.put(`/api/Todo/Update`, {
          ...row,
          status: status,
        })
      )
    )
      .then((response) => {
        console.log(response);
        message.success("Status list");
       
        setData([...tableData]);
      })
      .catch((error) => {
        console.log(error);
        message.error(error.message);
      });
  };
  const isRowSelectedAndStatus = () => {
    const selectedRows = tableData.filter((row) => row.checked);
    if (selectedRows.length === 0) {
      return false;
    }
    if (selectedRows.some((row) => row.status)) {
      return true; // At least one selected row is active
    }
    return false; // All selected rows are inactive
  };

  const DeleteData = (id: any) => {    //To delete the record 
    axios
    .delete(`/api/Todo/${id}`)
    .then(response => {
      getDetails();
      console.log('Data deleted successfully');
    })
    .catch(error => {
      console.error('Error deleting data:', error);
    });
  };
  
  const UpdateRow = () => { //for open update model
    setIsModalOpen(false);
    ///UpdateRecord();
  };
  
  const UpdateRecord = (values: any) => { //For update the records
    console.log(values);
    axios
    ({
      method: "put",
      headers: {
        "Content-Type": "application/json"
      }, 
      url: `/api/Todo/Update`,
      data: values,
    }) 
    .then(response => {
      console.log('Data Updated Successfully.');
    })
    .catch((error) => {
      message.error(error.response.data);
    });
  };
  
  function handleRowClick(record: { id: any; title: any; description: any; due_date: any; status: any; }) {
    // Set the form fields to the values of the clicked row
    form.setFieldsValue({
      id: record.id,
      title: record.title,
      description: record.description,
      due_date: moment(record.due_date),
      status: record.status,
    });
    setIsModalOpen(true);
  }
 
  const columns: any = [   
    {
      title: 'SLNo',
      dataIndex: 'slno',
      key: 'slno',
      render: (text:any, record:any, index:number) => index + 1
    },
    {
      title: 'Title',
      dataIndex: 'title',
      key: 'title', 
      render: (text: any, record: any, index: number) => (
        <Input
          value={record.title}
            
        />
      ),
    },
    {
      title: 'Description',
      dataIndex: 'description',
      key: 'description',
      render: (text: any, record: any, index: number) => (
        <Input
        value={record.description}
        />
      ),
    },
    {
      title: 'Due_Date',
      dataIndex: 'due_Date',
      key: 'due_Date',
      render: (text: any, record: any, index: number) => (
        <Input
          type="date"
          value={record.due_Date.toString().slice(0,10)}
          
        />
      ),
    },
    {
      title: "Status",
      dataIndex: "status",
      key: "status",
      render: (text: any, record: any, index: number) => (
        <Select value={record.status}>
          <Option value={true}>Pending</Option>
          <Option value={false}>Completed</Option>
        </Select>
      ),
      //   render: (record: any) => (record.status ? "completed" : "pending"),
    },
    {
      title: 'Undo',
      key: 'undo',
      render: (record: any) => (
        <div hidden={selectedRowKeys.length === 0}>
          <Button style={{ marginRight: "10px" }} type="primary" disabled={isRowSelectedAndStatus()}
            onClick={() => PendingandCompleted(true)}><UndoOutlined /> 
          </Button>
        </div>
      ),
    },
    {
      title: 'Action',
      key: 'action',
      render: (record: any) => (
        <div style={{ visibility: (selectedRowKeys.length === 1 && record.title === selectedRowKeys[0]) ? 'visible' : 'hidden', display: 'flex' }}>
         <Button type="primary" onClick={() => { setIsFormVisible(true); UpdateRow(); }}>Edit</Button>
         <Popconfirm title="Are you sure you want to delete this record?" onConfirm={() => DeleteData(record.id)} okText="Yes" cancelText="No">
            <Button type="primary" danger onClick={() => setIsFormVisible(true)}>Delete</Button>
          </Popconfirm>
        </div>
      ),
    }
  
  ];
  return (
    <div >
      <br></br> 
      <div style={{ display: "flex", flexDirection: "row" }}>
        <Input.Search style={{ width: '200px', borderColor: '#333', color: 'black', height: '30px',marginLeft:'2%' }}
        placeholder="Search" value={searchQuery} onChange={(e) => setSearchQuery(e.target.value)} enterButton={<SearchOutlined />}
        />
        <label style={{ marginLeft: "550px" }}>Active-Status:</label>
        <Select onChange={(value) => {
            if (value === "pending") {
              getDetails(true);
            } else if (value === "completed") {
              getDetails(false);
            }
          }}style={{ marginRight: "10px", width: "100px" }}>
          <Option value="--">---</Option>
          <Option value="pending">Pending</Option>
          <Option value="completed">Completed</Option>
        </Select>
        <div hidden={selectedRowKeys.length === 0}>
          <Button style={{marginRight: "10px", backgroundColor: "green", borderColor: "green", color:'black' }}
            type="primary" disabled={!isRowSelectedAndStatus()}onClick={() => PendingandCompleted(false)}>
            Mark Completed
          </Button>
        </div>
        <Postforms />
      </div>
      <br></br>
      <Table columns={columns} dataSource={tableData.slice((currentPage - 1) * 5, currentPage * 5)} 
        pagination={false} rowKey={(record) => record.title} rowSelection={rowSelection}  onRow={(record) => ({
          onClick: () => handleRowClick(record),
        })}
      />
      <Modal title="Update Modal" open={isModalOpen} onCancel={()=>setIsModalOpen(false)} footer={null}>
        <Form labelCol={{ span: 9 }} wrapperCol={{ span: 10 }}style={{ maxWidth: 1200 }}
          initialValues={{ remember: true }}onFinish={UpdateRecord}form={form} >
          <br></br>
          <Form.Item name="id" label="Id"
            rules={[
              {
                required: true,
                message:"Please enter your Id",
              },
            ]}
            hasFeedback
            >
            <Input placeholder="Enter your Id"/>
          </Form.Item>
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
              { required: true,
                message:"Field should not be empty"
              }
            ]}>
            <Select  disabled> 
              <Option >--Select--</Option> {/* @ts-ignore */}
              <Option value={true}>Pending</Option>
            </Select>
          </Form.Item> 
          <div className='buttons'>
            <Form.Item>
              <Button id="b1" type="primary" htmlType="submit" className='btn' style={{marginLeft:'95%'}}>Submit</Button>
            </Form.Item>
          </div>
        </Form>
      </Modal>
      <Pagination
        style={{ textAlign: "right" }}
        current={currentPage}
        total={tableData.length}
        pageSize={pageSize}
        showSizeChanger
        pageSizeOptions={['3', '5', '10', '20']}
        onShowSizeChange={(current, size) => {
          setCurrentPage(1);
          setPageSize(size);
        }}
        onChange={(page: number) => setCurrentPage(page)}
      />
     
    </div>
      
  );
  
};
      
export default Tab;