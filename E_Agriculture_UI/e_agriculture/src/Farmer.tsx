import React, { useEffect, useState } from "react";
import axios from "axios";
import {
  Button,
  Card,
  DatePicker,
  Form,
  Input,
  Modal,
  Pagination,
  Table,
  message,
} from "antd";
import "./Farmer.css";
import moment from "moment";
import { FaRegUser } from "react-icons/fa6";
import { FaAngleDown } from "react-icons/fa";
import { useNavigate } from "react-router-dom";
import UserDetails from "./UserDetails";

interface UserData {
  user_Id: string;
}
const ITEMS_PER_PAGE = 5;
const FarmerPage: React.FC = () => {
  const [userProfile, setUserProfile] = useState<UserData | null>(null);
  const [buyerDetails, setBuyerDetails] = useState<any[]>([]);
  const [marketDetails, setMarketDetails] = useState<any[]>([]);
  const [govtPrograms, setGovtPrograms] = useState<any[]>([]);
  const [viewQuaries, setViewQuaries] = useState<any[]>([]);
  const [viewAnswer, setViewAnswer] = useState<any[]>([]);
  const [activeSection, setActiveSection] = useState<string | null>(null);
  const [isModalVisible, setIsModalVisible] = useState(false);
  const [currentPage, setCurrentPage] = useState(1);
  const navigate = useNavigate();
  const [form] = Form.useForm();
  const Email = sessionStorage.getItem("email");

  const showModal = () => {
    setIsModalVisible(true);
  };

  const handleCancel = () => {
    setIsModalVisible(false);
  };

  const handleAvailabilityCropsClick = () => {
    setActiveSection("availability Crops");
    form.setFieldsValue({
      user_Id: userProfile?.user_Id,
    });
    showModal();
  };
  
  const govtProgramsToDisplay = govtPrograms.slice(
    (currentPage - 1) * ITEMS_PER_PAGE,
    currentPage * ITEMS_PER_PAGE
  );
  const handlePageChange = (page: number) => {
    setCurrentPage(page);
  };

  useEffect(() => {
    if (Email) {
      axios
        .get(`https://localhost:7120/api/Login/UserProfile?mail_id=${Email}`)
        .then((response: any) => {
          setUserProfile(response.data);
        })
        .catch((error: any) => {
          console.error(error.message);
        });
    }
  }, [Email]);

  //Adding AvailabilityCrop api
  const onFinish = (values: any) => {
    axios({
      method: "post",
      headers: { "Content-Type": "application/json" },
      url: "/api/E_Agriculture/AddAvailabilityCrops",
      data: values,
    })
      .then((r: any) => {
        message.success("Your record have been added successfully");
        navigate("/farmerpage");
        window.location.reload();
      })
      .catch((error: any) => {
        message.error(error.response.data);
      });
  };
  useEffect(() => {
    // Fetch Buyer Details
    axios.get("/api/E_Agriculture/RequiredcropsForBuyer").then((response) => {
      setBuyerDetails(response.data);
    });

    // Fetch Market Details
    axios.get("/api/E_Agriculture/GetAllMarketDetails").then((response) => {
      setMarketDetails(response.data);
    });

    //Fetch GovtPrograms
    axios
      .get("/api/E_Agriculture/GetAllGovernmentPrograms")
      .then((response) => {
        setGovtPrograms(response.data);
      });
  }, []);

  useEffect(() => {
    const fetchData = async () => {
      try {
        // Fetch student questions
        const quariesResponse = await axios.get(
          "/api/E_Agriculture/StudentQuestions"
        );
        setViewQuaries(quariesResponse.data);

        // Fetch answers for student questions
        const answersResponse = await axios.get(
          "/api/E_Agriculture/GetAnswersForQuestions"
        );
        setViewAnswer(answersResponse.data);
      } catch (error) {
        // Handle errors if any
        console.error("Error fetching data:", error);
      }
    };

    fetchData();
  }, []);

  // Combine data based on Question_Id
  const combinedData = viewQuaries.map((quary) => {
    const correspondingAnswer = viewAnswer.find(
      (answer) => answer.question_Id === quary.question_Id
    );

    return {
      ...quary,
      ...correspondingAnswer,
    };
  });

  const buyerColumns = [
    { title: "BuyerName", dataIndex: "user_Name", key: "user_Name" },
    { title: "Address", dataIndex: "address", key: "address" },
    { title: "Contact", dataIndex: "contact_No", key: "contact_No" },
    { title: "RequiredCropName", dataIndex: "rCrop_Name", key: "rCrop_Name" },
    { title: "RequiredQuantity", dataIndex: "rQuantity", key: "rQuantity" },
    { title: "Location", dataIndex: "rLocation", key: "rLocation" },
    {
      title: "date",
      dataIndex: "rAdd_Date",
      key: "rAdd_Date",
      render: (text: any, record: any) =>
        moment(record.rAdd_Date).format("DD/MM/YYYY"),
    },
  ];

  const marketColumns = [
    { title: "CropName", dataIndex: "mCrop_Name", key: "mCrop_Name" },
    { title: "Amount", dataIndex: "mPrice", key: "mPrice" },
    { title: "Quantity", dataIndex: "mQuantity", key: "mQuantity" },
    {
      title: "Date",
      dataIndex: "mAdd_Date",
      key: "mAdd_Date",
      render: (text: any, record: any) =>
        moment(record.mAdd_Date).format("DD/MM/YYYY"),
    },
    {
      title: "Location",
      dataIndex: "mLocation",
      key: "mLocation",
    },
  ];

  const viewColumns = [
    { title: "Question_Id", dataIndex: "question_Id", key: "question_Id" },

    { title: "Student_Id", dataIndex: "user_Id", key: "user_Id" },
    { title: "Question", dataIndex: "question", key: "question" },
    {
      title: "Question_Date",
      dataIndex: "question_Date",
      key: "question_Date",
      render: (text: any, record: any) =>
        moment(record.question_Date).format("DD/MM/YYYY"),
    },
    {
      title: "Admin_Answer",
      dataIndex: "answersFor",
      key: "answersFor",
    },
  ];

  return (
    <div className="pagecontainer">
      <h2 className="heading">E-Agriculture Service System</h2>
      <div>
        <div className="section-links">
          <h3
            style={{
              backgroundColor: "white",
              display: "flex",
              flexDirection: "column",
              justifyContent: "center",
              alignItems: "center",
              height: "25%",
              padding: "10px",
              borderRadius: "6px",
            }}
          >
            Agriculture
          </h3>
          <span
            style={{
              backgroundColor:
                activeSection === "buyerDetails" ? "rgb(17 144 186)" : "",
              display: "flex",
              flexDirection: "column",
              justifyContent: "center",
              alignItems: "center",
              height: "81%",
              padding: "10px",
            }}
            onClick={() => setActiveSection("buyerDetails")}
          >
            <div>
              <FaRegUser />
              Buyer Details
              <FaAngleDown />
            </div>
          </span>

          <span
            style={{
              backgroundColor:
                activeSection === "marketDetails" ? "rgb(17 144 186)" : "",
              display: "flex",
              flexDirection: "column",
              justifyContent: "center",
              alignItems: "center",
              height: "81%",
              padding: "10px",
            }}
            onClick={() => setActiveSection("marketDetails")}
          >
            <div>
              <FaRegUser />
              Market Details
              <FaAngleDown />
            </div>
          </span>

          <span
            style={{
              backgroundColor:
                activeSection === "availability Crops" ? "rgb(17 144 186)" : "",
              display: "flex",
              flexDirection: "column",
              justifyContent: "center",
              alignItems: "center",
              height: "81%",
              padding: "10px",
            }}
            onClick={() => handleAvailabilityCropsClick()}
          >
            <div>
              <FaRegUser />
              Availability Crops
              <FaAngleDown />
            </div>
          </span>
          <span
            style={{
              backgroundColor:
                activeSection === "govtPrograms" ? "rgb(17 144 186)" : "",
              display: "flex",
              flexDirection: "column",
              justifyContent: "center",
              alignItems: "center",
              height: "81%",
              padding: "10px",
            }}
            onClick={() => setActiveSection("govtPrograms")}
          >
            <div>
              <FaRegUser />
              GovtPrograms
              <FaAngleDown />
            </div>
          </span>
          <span
            style={{
              backgroundColor: activeSection === "ViewQuaries" ? "rgb(17 144 186)" : "",
              display: "flex",
              flexDirection: "column",
              justifyContent: "center",
              alignItems: "center",
              height: "81%",
              padding: "10px",
            }}
            onClick={() => setActiveSection("ViewQuaries")}
          >
            <div>
              <FaRegUser />
              View_Quaries
              <FaAngleDown />
            </div>
          </span>
          <span
            style={{
              backgroundColor: activeSection === "home" ? "rgb(17 144 186)" : "",
              display: "flex",
              flexDirection: "column",
              justifyContent: "center",
              alignItems: "center",
              height: "81%",
              padding: "10px",
            }}
            onClick={() => setActiveSection("home")}
          >
            <div>
              <FaRegUser />
              Home
              <FaAngleDown />
            </div>
          </span>
        </div>
      </div>
      {activeSection === null && (
        <img
          className="images"
          src="../Images/P5.jpg"
          alt="imageasd"
          style={{
            width: "100%",
            height: "480px",
            marginTop: "-0.1%",
            marginLeft: "-0%",
          }}
        />
      )}
      <div>
        {activeSection === "buyerDetails" && (
          <div className="tablebackgroundimage">
            <Table
              dataSource={buyerDetails}
              columns={buyerColumns}
             
            />
          </div>
        )}

        {activeSection === "marketDetails" && (
          <div
          className="tablebackgroundimage"
          >
            <Table
              dataSource={marketDetails}
              columns={marketColumns}
             
            />
          </div>
        )}
        {activeSection === "availability Crops" && (
          <div
          className="tablebackgroundimage"
          >
            <Modal
              title={
                <span style={{ fontSize: "20px", color: "blue" }}>
                  Availability Crops
                </span>
              }
              visible={isModalVisible}
              onCancel={handleCancel}
              footer={null}
              style={{ marginTop: "6%" }}
            >
              <Form
                initialValues={{
                  remember: true,
                  user_Id: userProfile?.user_Id,
                }}
                autoComplete="off"
                onFinish={onFinish}
                form={form}
              >
                <Form.Item
                  name="user_Id"
                  label="Farmer_Id"
                  rules={[
                    { required: true, message: "Please enter the Farmer_Id!" },
                  ]}
                  labelCol={{ span: 24 }}
                  wrapperCol={{ span: 24 }}
                >
                  <Input disabled />
                </Form.Item>
                <Form.Item
                  name="aCrop_Name"
                  label="Crop Name"
                  rules={[
                    { required: true, message: "Please enter the crop name!" },
                  ]}
                  labelCol={{ span: 24 }}
                  wrapperCol={{ span: 24 }}
                >
                  <Input />
                </Form.Item>
                <Form.Item
                  name="aQuantity"
                  label="Quantity"
                  rules={[
                    { required: true, message: "Please enter the quantity!" },
                  ]}
                  labelCol={{ span: 24 }}
                  wrapperCol={{ span: 24 }}
                >
                  <Input />
                </Form.Item>
                <Form.Item
                  name="price"
                  label="Amount"
                  rules={[
                    { required: true, message: "Please enter the amount!" },
                  ]}
                  labelCol={{ span: 24 }}
                  wrapperCol={{ span: 24 }}
                >
                  <Input />
                </Form.Item>
                <Form.Item
                  name="aLocation"
                  label="Location"
                  rules={[
                    { required: true, message: "Please enter the location!" },
                  ]}
                  labelCol={{ span: 24 }}
                  wrapperCol={{ span: 24 }}
                >
                  <Input />
                </Form.Item>
                <Form.Item
                  name="add_Date"
                  label="Date"
                  rules={[
                    { required: true, message: "Please select the date!" },
                  ]}
                  labelCol={{ span: 24 }}
                  wrapperCol={{ span: 24 }}
                >
                  <DatePicker style={{ width: "100%" }} />
                </Form.Item>
                <Form.Item
                  name="image"
                  label="Add Photo"
                  rules={[{ required: true, message: "Please add an image!" }]}
                  labelCol={{ span: 24 }}
                  wrapperCol={{ span: 24 }}
                >
                  <Input type="file" />
                </Form.Item>
                <Form.Item
                  style={{
                    display: "flex",
                    justifyContent: "center",
                    marginTop: "-2%",
                  }}
                >
                  <Button
                    style={{ backgroundColor: "#101e8a", color: "white" }}
                    htmlType="submit"
                  >
                    Continue
                  </Button>
                </Form.Item>
              </Form>
            </Modal>
          </div>
        )}

        {activeSection === "govtPrograms" && (
          <div
          className="tablebackgroundimage"
          >
            {govtProgramsToDisplay.map((program, index) => (
              <Card
                type="inner"
                key={index}
                title={
                  <span style={{ fontSize: "18px", color: "white" }}>
                    {program.program_Name}
                  </span>
                }
                style={{ width: "1370px",  borderRadius: "0%" }}
              >
                <p>
                  <b>Description:</b> {program.program_Description}
                </p>
                <p>
                  <b>Start Date:</b>{" "}
                  {moment(program.programStart_Date).format("DD/MM/YYYY")}
                </p>
                <p>
                  <b>End Date:</b>{" "}
                  {moment(program.programEnd_Date).format("DD/MM/YYYY")}
                </p>
              </Card>
            ))}
            <Pagination
              current={currentPage}
              pageSize={ITEMS_PER_PAGE}
              total={govtPrograms.length}
              onChange={handlePageChange}
              style={{ marginTop: "10px", marginLeft: "90%" }}
            />
          </div>
        )}

        {activeSection === "ViewQuaries" && (
          <div
          className="tablebackgroundimage"
          >
            <Table
              dataSource={combinedData}
              columns={viewColumns}
            
            />
          </div>
        )}
        {activeSection === "home" && (
          <div  className="tablebackgroundimage">
            <UserDetails />
          </div>
        )}
      </div>
    </div>
  );
};

export default FarmerPage;
