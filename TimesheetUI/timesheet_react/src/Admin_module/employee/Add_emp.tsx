import { useEffect, useState } from "react";
import { Button, DatePicker, Form, Input, message, Modal, Select } from "antd";
import axios from "axios";

export function AddEmployee() {
  const [isModalOpen, setIsModalOpen] = useState(false);
  const showModal = () => {
    setIsModalOpen(true);
  };
  const onFinish = (values: any) => {
    axios({
      method: "post",
      headers: { "Content-Type": "application/json" },
      url: "/api/Admin/AddEmployee",
      data: values,
    })
      .then((r: any) => {
        message.success("Your record have been added successfully");
        setIsModalOpen(false);
        window.location.reload();
      })
      .catch((error: any) => {
        message.error(error.response.data);
      });
  };
  const [designations, setDesignations] = useState<Designation[]>([]);
  interface Designation {
    designation_Id: number;
    designation: string;
    is_Active: boolean;
    create_Date: string;
    modified_Date: string | null;
  }
  useEffect(() => {
    const fetchDesignations = async () => {
      const response = await axios.get("/api/Admin/GetAllDesignations");
      setDesignations(response.data);
    };
    fetchDesignations();
  }, []);
  interface EmployeeType {
    employee_Type_Id: number;
    employee_Type: string;
    is_Active: boolean;
    create_Date: string;
    modified_Date: string | null;
  }
  useEffect(() => {
    const fetchEmployeeTypes = async () => {
      const response = await axios.get<EmployeeType[]>(
        "/api/Admin/GetAllEmplyoeeTypes"
      );
      setEmployeeTypes(response.data);
    };
    fetchEmployeeTypes();
  }, []);

  const [employeeTypes, setEmployeeTypes] = useState<EmployeeType[]>([]);
  return (
    <>
      <Button
        id="idp"
        type="primary"
        onClick={showModal}
        style={{
          display: "flex",
          float: "right",
          background:
            "-webkit-linear-gradient(45deg, rgba(9, 0, 159, 0.3), rgba(0, 255, 149, 0.3) 95%)",
          color: "black",
          fontWeight: "bold",
        }}
      >
        Add Employee
      </Button>
      <Modal
        title="Add Employee"
        open={isModalOpen}
        onCancel={() => setIsModalOpen(false)}
        footer={[]}
        style={{
          fontWeight: 600,
        }}
        width={1000}
      >
        <Form
          name="basic"
          labelCol={{ span: 50 }}
          wrapperCol={{ span: 25 }}
          initialValues={{ remember: true }}
          autoComplete="off"
          onFinish={onFinish}
          style={{ marginTop: 30 }}
        >
          <div
            style={{
              display: "flex",
              flexDirection: "row",
              justifyContent: "space-between",
              // marginRight: 300,
            }}
          >
            <Form.Item
              label="Employee Code"
              name="employee_code"
              rules={[
                { required: true, message: "Please input your Employee Code!" },
              ]}
            >
              <Input />
            </Form.Item>
            <Form.Item
              label="First Name"
              name="first_Name"
              rules={[
                { required: true, message: "Please input your First Name!" },
              ]}
            >
              <Input />
            </Form.Item>
            <Form.Item
              label="Last Name"
              name="last_Name"
              rules={[
                { required: true, message: "Please input your Last Name!" },
              ]}
            >
              <Input />
            </Form.Item>
          </div>
          <div
            style={{
              display: "flex",
              flexDirection: "row",
              justifyContent: "space-between",
              //marginRight: 45,
            }}
          >
            <Form.Item
              label="Employee Type"
              name="employee_Type_Id"
              rules={[
                {
                  required: true,
                  message: "Please select an Employee Type!",
                },
              ]}
            >
              <Select style={{ width: 135 }}>
                {employeeTypes.map((employeeType) => (
                  <Select.Option
                    key={employeeType.employee_Type_Id}
                    value={employeeType.employee_Type_Id}
                  >
                    {employeeType.employee_Type}
                  </Select.Option>
                ))}
              </Select>
            </Form.Item>
            <Form.Item
              label="Designation"
              name="designation_Id"
              rules={[
                { required: true, message: "Please input your Designation !" },
              ]}
            >
              <Select style={{ width: 240 }}>
                {designations.map((designation) => (
                  <Select.Option
                    key={designation.designation_Id}
                    value={designation.designation_Id}
                  >
                    {designation.designation}
                  </Select.Option>
                ))}
              </Select>
            </Form.Item>
            <Form.Item
              label="Reporting Manager"
              name="reporting_Manager1"
              rules={[
                {
                  required: true,
                  message: "Please input your Reporting Manager1!",
                },
              ]}
            >
              <Select style={{ width: 155 }}>
                <Select.Option value="Manuj Kumar B">
                  Manuj Kumar B
                </Select.Option>
                <Select.Option value="Appusamy S">Appusamy S</Select.Option>
                <Select.Option value="Rabik S">Rabik S</Select.Option>
              </Select>
            </Form.Item>
          </div>

          <div
            style={{
              display: "flex",
              flexDirection: "row",
              justifyContent: "space-between",
              marginRight: 450,
            }}
          >
            <Form.Item
              name="joining_Date"
              label="Joining Date"
              rules={[
                { required: true, message: "Please provide Joining Date!" },
              ]}
              hasFeedback
            >
              <DatePicker placeholder="Select Date" />
            </Form.Item>
          </div>
          <h4>Contact Info</h4>
          <div
            style={{
              display: "flex",
              flexDirection: "row",
              justifyContent: "space-between",
            }}
          >
            <Form.Item
              label="Mail ID"
              name="official_Email"
              rules={[{ required: true, message: "Please input your Mail!" }]}
            >
              <Input />
            </Form.Item>
            <Form.Item
              label="Alternate Mail ID"
              name="alternate_Email"
              rules={[
                {
                  required: false,
                  message: "Please input your Alternate  Mail!",
                },
              ]}
            >
              <Input />
            </Form.Item>
            <Form.Item
              label="Contact No"
              name="contact_No"
              rules={[
                { required: true, message: "Please input your Contact No!" },
              ]}
            >
              <Input />
            </Form.Item>
          </div>
          <Form.Item
            style={{ display: "flex", justifyContent: "center", marginTop: 50 }}
          >
            <Button type="primary" htmlType="submit">
              Confirm
            </Button>
          </Form.Item>
        </Form>
      </Modal>
    </>
  );
}
