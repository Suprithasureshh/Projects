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
import { FaEdit } from "react-icons/fa";
import { RiDeleteBin5Fill } from "react-icons/ri";


const ITEMS_PER_PAGE = 5;

interface MarketData {
  mCrop_Id:string;
  mCrop_Name: string;
  mQuantity: string;
  mPrice: string;
  mLocation: string;
  mAdd_Date: string;
}
interface GovernmentProgram{
  program_Id: string;
  program_Name: string;
  program_Description:string;
  programStart_Date:string;
  programEnd_Date: string;
}
const AdminPage: React.FC = () => {
  const [selectedRowData, setSelectedRowData] = useState<any | null>(null);

  const [farmerDetails, setFarmerDetails] = useState<any[]>([]);
  const [buyerDetails, setBuyerDetails] = useState<any[]>([]);
  const [marketDetails, setMarketDetails] = useState<any[]>([]);
  const [govtPrograms, setGovtPrograms] = useState<any[]>([]);
  const [viewQuaries, setViewQuaries] = useState<any[]>([]);
  const [answersData, setAnswersData] = useState<any[]>([]);

  const [activeSection, setActiveSection] = useState<string | null>(null);
  const [isModalVisible, setIsModalVisible] = useState(false);
  const [isProgramModalVisible, setIsProgramModalVisible] = useState(false);
  const [isAnswerModalVisible, setIsAnswerModalVisible] = useState(false);
  const [currentPage, setCurrentPage] = useState(1);
  const navigate = useNavigate();
  const [form] = Form.useForm();
 
  const [selectedProgram, setSelectedProgram] = useState<GovernmentProgram | null>(null);
  const [updateModalVisible, setUpdateModalVisible] = useState(false);
  const [updatemarketDetails, setUpdateMarketDetails] =
    useState<MarketData | null>(null);
  const [isMarketModalOpen, setIsMarketModalOpen] = useState(false);

  const showupdateModal = () => {
    setIsMarketModalOpen(true);
  };

  const updatehandleCancel = () => {
    setIsMarketModalOpen(false);
  };
  const showModal = () => {
    setIsModalVisible(true);
  };

  const handleCancel = () => {
    setIsModalVisible(false);
  };
  const showProgramModal = () => {
    setIsProgramModalVisible(true);
  };

  const handleProgramCancel = () => {
    setIsProgramModalVisible(false);
  };
  const showAnswerModal = () => {
    setIsAnswerModalVisible(true);
  };

  const handleCancelAnswermodel = () => {
    setIsAnswerModalVisible(false);
  };

  const handleNewItemClick = () => {
    setActiveSection("newitem");
    showModal();
  };
  const handleNewProgramClick = () => {
    setActiveSection("newprogram");
    showProgramModal();
  };
  const handleUpdateMarket = (record: any) => {
    setUpdateMarketDetails(record); 
    showupdateModal();
  };

  const handleDeleteMarket = (record: any) => {
    setUpdateMarketDetails(record);
    showDeleteConfirm(record);
  };
  const showDeleteConfirm = (record:any) => {
    Modal.confirm({
      title: 'Are you sure you want to delete this record?',
      
      onOk: () => {
        DeleteMarketData(record);
      },
      onCancel: () => {
       
      },
    });
  };
  const handleUpdateClick = (program:any) => {
    setSelectedProgram(program);
    setUpdateModalVisible(true);
    form.setFieldsValue({
      program_Id: program.program_Id,
      program_Name: program.program_Name,
      program_Description: program.program_Description,
      programStart_Date: moment(program.programStart_Date),
      programEnd_Date: moment(program.programEnd_Date),
    });
  };
  
  const UpdateGovernmentProgram = (values: any) => {
    if (!govtPrograms) {
      console.error("govtPrograms is null");
      return;
    }
  
    axios
      .put("/api/E_Agriculture/UpdateGovernmentPrograms", values)
      .then((response) => {
        // Log the entire response for debugging
        console.log("Update Response:", response);
  
        // Assuming the server returns the updated program after a successful update
        const updatedProgram = response.data;
  
        // Log the updated program for debugging
        console.log("Updated Program:", updatedProgram);
  
        message.success("Government program updated successfully");
  
        // Update the state with the updated program
        const updatedGovernmentPrograms = govtPrograms.map((program: GovernmentProgram) => {
          return program.program_Id === updatedProgram.program_Id ? updatedProgram : program;
        });
  
        // Log the updated state for debugging
        console.log("Updated State:", updatedGovernmentPrograms);
  
        setGovtPrograms(updatedGovernmentPrograms);
        setUpdateModalVisible(false);
      })
      .catch((error) => {
        message.error(error.response?.data || "Failed to update government program");
      });
  };
  
  
  
  
  
  const handleUpdateCancel = () => {
    setUpdateModalVisible(false);
  };
  const handleAnswerClick = (record: any) => {
    setActiveSection("newanswer");
    setSelectedRowData(record);
    showAnswerModal();
    form.setFieldsValue({
      user_Id: record.user_Id,
      user_Name: record.user_Name,
      question_Id: record.question_Id,
    });
  };
  const initialValues = {
    user_Id: form.getFieldValue("user_Id") || selectedRowData?.user_Id,
    user_Name: form.getFieldValue("user_Name") || selectedRowData?.user_Name,
    question_Id:
      form.getFieldValue("question_Id") || selectedRowData?.question_Id,
  };

  const govtProgramsToDisplay = govtPrograms.slice(
    (currentPage - 1) * ITEMS_PER_PAGE,
    currentPage * ITEMS_PER_PAGE
  );
  const handlePageChange = (page: number) => {
    setCurrentPage(page);
  };
  //Adding AvailabilityCrop api
  const onFinish = (values: any) => {
    axios({
      method: "post",
      headers: { "Content-Type": "application/json" },
      url: "/api/E_Agriculture/AddMarketDeatils",
      data: values,
    })
      .then((r: any) => {
        message.success("Your record have been added successfully");
        navigate("/adminpage");
      
      })
      .catch((error: any) => {
        message.error(error.response.data);
      });
  };

  const AddAnswer = (values: any) => {
    axios({
      method: "post",
      headers: { "Content-Type": "application/json" },
      url: "/api/E_Agriculture/AddAnswer",
      data: values,
    })
      .then((r: any) => {
        message.success("Your record have been added successfully");
        navigate("/adminpage");
       
      })
      .catch((error: any) => {
        message.error(error.response.data);
      });
  };

  const AddProgram = (values: any) => {
    axios({
      method: "post",
      headers: { "Content-Type": "application/json" },
      url: "/api/E_Agriculture/AddGovernmentPrograms",
      data: values,
    })
      .then((r: any) => {
        message.success("Your record have been added successfully");
        navigate("/adminpage");
       
      })
      .catch((error: any) => {
        message.error(error.response.data);
      });
  };

  useEffect(() => {
    const fetchData = async () => {
      try {
        const response = await axios.get("/api/E_Agriculture/GetAllMarketDetails");
        const marketData: MarketData[] = response.data;
        setMarketDetails(marketData);
      } catch (error) {
        console.error("Error fetching market details:", error);
      }
    };

    fetchData();
  }, []); 

  const UpdateMarketData = (values: any) => {
    const updatedMarketDetails = marketDetails.map((marketItem: MarketData) => {
      if (marketItem.mCrop_Id === updatemarketDetails?.mCrop_Id) {
       
        return { ...marketItem, ...values };
      }
      return marketItem;
    });

    axios
      .put("/api/E_Agriculture/UpdateMarketDeatils", values)
      .then((r: any) => {
        message.success("Your record has been updated successfully");
        setMarketDetails(updatedMarketDetails);
        updatehandleCancel(); 
      })
      .catch((error: any) => {
        message.error(error.response.data);
      });
  };
  const DeleteMarketData = (values: any) => {
    axios
      .delete(`/api/E_Agriculture/DeleteMarketDetailsById?Id=${updatemarketDetails?.mCrop_Id}`)
      .then((r: any) => {
        message.success("Your record has been deleted successfully");
        const updatedMarketDetails = marketDetails.filter((marketItem: MarketData) => marketItem.mCrop_Id !== updatemarketDetails?.mCrop_Id);
        setMarketDetails(updatedMarketDetails);
       
      })
      .catch((error: any) => {
        message.error(error.response.data);
      });
  };
  

  useEffect(() => {
    // Fetch Farmer Details
    axios.get("/api/E_Agriculture/FarmeSellingDetails").then((response) => {
      setFarmerDetails(response.data);
    });

    // Fetch Buyer Details
    axios.get("/api/E_Agriculture/RequiredcropsForBuyer").then((response) => {
      setBuyerDetails(response.data);
    });

    // Fetch Market Details
    axios.get("/api/E_Agriculture/GetAllMarketDetails").then((response) => {
      setMarketDetails(response.data);
    });

    //Fetch GovtPrograms
   
      axios.get("/api/E_Agriculture/GetAllGovernmentPrograms")
        .then((response) => {
          setGovtPrograms(response.data);
        })
        .catch((error) => {
          console.error("Failed to fetch government programs", error);
        });
   
    
    //Fetch ViewQuaries

    axios.get("/api/E_Agriculture/StudentQuestions").then((response) => {
      setViewQuaries(response.data);
    });

    //Fetch Answers
    axios.get("/api/E_Agriculture/GetAllAnswer").then((response) => {
      setAnswersData(response.data);
      console.log(answersData);
    });
  }, []);

  const farmerColumns = [
    { title: "FarmerName", dataIndex: "user_Name", key: "user_Name" },
    { title: "Address", dataIndex: "address", key: "address" },
    { title: "Contact", dataIndex: "contact_No", key: "contact_No" },
    { title: "AvailableCropName", dataIndex: "aCrop_Name", key: "aCrop_Name" },
    { title: "AvailableQuantity", dataIndex: "aQuantity", key: "aQuantity" },
    { title: "Location", dataIndex: "price", key: "price" },
    { title: "Price", dataIndex: "aLocation", key: "aLocation" },
    {
      title: "date",
      dataIndex: "add_Date",
      key: "add_Date",
      render: (text: any, record: any) =>
        moment(record.add_Date).format("DD/MM/YYYY"),
    },
  ];

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
    {
      title: "Action",
      dataIndex: "action",
      key: "action",
      render: (text: any, record: any) => (
        <>
          <span
            style={{
              fontSize: "30px",
              color: "white",
              marginLeft: "3%",
            
            }}
            onClick={() => handleUpdateMarket(record)}
          >
            <FaEdit />
          </span>
        
          <span
            style={{
              fontSize: "30px",
              color: "red",
              marginLeft: "3%",
            
            }}
            onClick={() => handleDeleteMarket(record)}
          >
            <RiDeleteBin5Fill />
          </span>
        </>
      ),
    },
  ];
  const viewQuariesColumns = [
    { title: "QuestionId", dataIndex: "question_Id", key: "question_Id" },
    { title: "StudentId", dataIndex: "user_Id", key: "user_Id" },
    { title: "StudentName", dataIndex: "user_Name", key: "user_Name" },
    { title: "Question", dataIndex: "question", key: "question" },
    {
      title: "Date",
      dataIndex: "question_Date",
      key: "question_Date",
      render: (text: any, record: any) =>
        moment(record.question_Date).format("DD/MM/YYYY"),
    },
    {
      title: "Response",
      dataIndex: "answerFor",
      key: "answerFor",
      render: (text: any, record: any) => (
        <>
          {answersData.filter((e: any) => e.question_Id === record.question_Id)
            .length === 0 ? (
            <Button
              onClick={() => handleAnswerClick(record)}
              style={{ backgroundColor: "#101e8a", color: "white" }}
            >
              Send Answer
            </Button>
          ) : (
            <Button
              onClick={() => handleAnswerClick(record)}
              style={{ backgroundColor: "rgb(116 176 224)", color: "white" }}
              disabled
            >
              Send Answer
            </Button>
          )}
        </>
      ),
    },
  ];
  return (
    <div className="pagecontainer">
      <h2 className="heading">E-Agriculture Service System</h2>
      <div>
        <div className="section-links">
          <span
            style={{
              backgroundColor:
                activeSection === "farmerDetails" ? "rgb(17 144 186)" : "",
              display: "flex",
              flexDirection: "column",
              justifyContent: "center",
              alignItems: "center",
              height: "81%",
              padding: "10px",
            }}
            onClick={() => setActiveSection("farmerDetails")}
          >
            <div>
              <FaRegUser />
              Farmer Details
              <FaAngleDown />
            </div>
          </span>
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
                activeSection === "newitem" ? "rgb(17 144 186)" : "",
              display: "flex",
              flexDirection: "column",
              justifyContent: "center",
              alignItems: "center",
              height: "81%",
              padding: "10px",
            }}
            onClick={() => handleNewItemClick()}
          >
            <div>
              <FaRegUser />
              AddNewItem to_Market
              <FaAngleDown />
            </div>
          </span>
          <span
            style={{
              backgroundColor:
                activeSection === "newprogram" ? "rgb(17 144 186)" : "",
              display: "flex",
              flexDirection: "column",
              justifyContent: "center",
              alignItems: "center",
              height: "81%",
              padding: "10px",
            }}
            onClick={() => handleNewProgramClick()}
          >
            <div>
              <FaRegUser />
              Add New Program
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
              backgroundColor:
                activeSection === "ViewQuaries" ? "rgb(17 144 186)" : "",
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
              backgroundColor:
                activeSection === "home" ? "rgb(17 144 186)" : "",
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
        {activeSection === "farmerDetails" && (
          <div className="tablebackgroundimage">
            <Table dataSource={farmerDetails} columns={farmerColumns} />
          </div>
        )}
        {activeSection === "buyerDetails" && (
          <div className="tablebackgroundimage">
            <Table dataSource={buyerDetails} columns={buyerColumns} />
          </div>
        )}
        {activeSection === "marketDetails" && (
          <div className="tablebackgroundimage">
            <Table dataSource={marketDetails} columns={marketColumns} />
          </div>
        )}
        {activeSection === "newitem" && (
          <div className="tablebackgroundimage">
            <Modal
              title={
                <span style={{ fontSize: "20px", color: "blue" }}>
                  Adding New Item
                </span>
              }
              visible={isModalVisible}
              onCancel={handleCancel}
              footer={null}
              style={{ marginTop: "6%" }}
            >
              <Form
                initialValues={{ remember: true }}
                autoComplete="off"
                onFinish={onFinish}
                form={form}
              >
                <Form.Item
                  name="mCrop_Name"
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
                  name="mQuantity"
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
                  name="mPrice"
                  label="Price"
                  rules={[
                    { required: true, message: "Please enter the amount!" },
                  ]}
                  labelCol={{ span: 24 }}
                  wrapperCol={{ span: 24 }}
                >
                  <Input />
                </Form.Item>
                <Form.Item
                  name="mLocation"
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
                  name="mAdd_Date"
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
        {activeSection === "newprogram" && (
          <div className="tablebackgroundimage">
            <Modal
              title={
                <span style={{ fontSize: "20px", color: "blue" }}>
                  Adding New Program
                </span>
              }
              visible={isProgramModalVisible}
              onCancel={handleProgramCancel}
              footer={null}
              style={{ marginTop: "6%" }}
            >
              <Form
                initialValues={{ remember: true }}
                autoComplete="off"
                onFinish={AddProgram}
                form={form}
              >
                <Form.Item
                  name="program_Name"
                  label="Program Name"
                  rules={[
                    {
                      required: true,
                      message: "Please enter the program name!",
                    },
                  ]}
                  labelCol={{ span: 24 }}
                  wrapperCol={{ span: 24 }}
                >
                  <Input />
                </Form.Item>
                <Form.Item
                  name="program_Description"
                  label="Program Description"
                  rules={[
                    { required: true, message: "Please fill the Description!" },
                  ]}
                  labelCol={{ span: 24 }}
                  wrapperCol={{ span: 24 }}
                >
                  <Input.TextArea />
                </Form.Item>

                <Form.Item
                  name="programStart_Date"
                  label="Start Date"
                  rules={[
                    { required: true, message: "Please select the date!" },
                  ]}
                  labelCol={{ span: 24 }}
                  wrapperCol={{ span: 24 }}
                >
                  <DatePicker style={{ width: "100%" }} />
                </Form.Item>

                <Form.Item
                  name="programEnd_Date"
                  label="End Date"
                  rules={[
                    { required: true, message: "Please select the date!" },
                  ]}
                  labelCol={{ span: 24 }}
                  wrapperCol={{ span: 24 }}
                >
                  <DatePicker style={{ width: "100%" }} />
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
        {activeSection === "newanswer" && (
          <div className="tablebackgroundimage">
            <Modal
              title={
                <span style={{ fontSize: "20px", color: "blue" }}>
                  Send Answer
                </span>
              }
              visible={
                isAnswerModalVisible &&
                selectedRowData &&
                selectedRowData.answerFor === undefined
              }
              onCancel={handleCancelAnswermodel}
              footer={null}
              style={{ marginTop: "6%" }}
            >
              <Form
                initialValues={initialValues}
                autoComplete="off"
                onFinish={AddAnswer}
                form={form}
              >
                <div style={{ display: "flex" }}>
                  <Form.Item
                    name="user_Id"
                    label="Student_Id"
                    labelCol={{ span: 24 }}
                    wrapperCol={{ span: 24 }}
                  >
                    <Input />
                  </Form.Item>

                  <Form.Item
                    name="user_Name"
                    label="Student Name"
                    labelCol={{ span: 24 }}
                    wrapperCol={{ span: 24 }}
                    style={{ marginLeft: "2%" }}
                  >
                    <Input />
                  </Form.Item>
                </div>
                <Form.Item
                  name="question_Id"
                  label="Question_Id "
                  labelCol={{ span: 24 }}
                  wrapperCol={{ span: 24 }}
                  style={{ marginLeft: "2%" }}
                >
                  <Input />
                </Form.Item>
                <Form.Item
                  name="answerFor"
                  label="Write Answer"
                  rules={[{ required: true, message: "Write your answer!" }]}
                  labelCol={{ span: 24 }}
                  wrapperCol={{ span: 24 }}
                >
                  <Input.TextArea />
                </Form.Item>

                <Form.Item
                  name="answer_Date"
                  label="Date"
                  labelCol={{ span: 24 }}
                  wrapperCol={{ span: 24 }}
                >
                  <DatePicker style={{ width: "100%" }} />
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
          <div className="tablebackgroundimage">
            {govtProgramsToDisplay.map((program, index) => (
              <Card
                type="inner"
                key={index}
                title={
                  <div>
                    <span style={{ fontSize: "18px", color: "white" }}>
                      {program.program_Name}
                    </span>
                    <span>
                      
                    </span>
                    <span
                      style={{
                        fontSize: "30px",
                        color: "white",
                        marginLeft: "75%",
                      }}
                      onClick={() => handleUpdateClick(program)}
                    >
                      <FaEdit />
                    </span>
                    <span
                      style={{
                        fontSize: "30px",
                        color: "red",
                        marginLeft: "0.5%",
                      }}
                      onClick={() => handleUpdateClick(program)}
                    >
                      <RiDeleteBin5Fill />
                    </span>
                  </div>
                }
                style={{ width: "1370px", borderRadius: "0%" }}
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
          <div className="tablebackgroundimage">
            <Table dataSource={viewQuaries} columns={viewQuariesColumns} />
          </div>
        )}
        {activeSection === "home" && (
          <div className="tablebackgroundimage">
            <UserDetails />
          </div>
        )}
      </div>
      <Modal
        className="custom-modal"
        title="Update MarketDetails"
        open={isMarketModalOpen}
        footer={[]}
        onCancel={updatehandleCancel}
        width={300}
        style={{ marginTop: "6%", marginRight: "17%" }}
      >
        <Form
          form={form}
          onFinish={UpdateMarketData}
          initialValues={{
            mCrop_Name: updatemarketDetails?.mCrop_Name,
            mQuantity: updatemarketDetails?.mQuantity,
            mPrice: updatemarketDetails?.mPrice,
            mLocation: updatemarketDetails?.mLocation,
            mAdd_Date: updatemarketDetails?.mAdd_Date
              ? moment(updatemarketDetails.mAdd_Date, "YYYY-MM-DD")
              : undefined,
          }}
        >
          <div style={{ display: "flex" }}>
            <Form.Item
              name="mCrop_Name"
              label="Crop_Name"
              labelCol={{ span: 24 }}
              wrapperCol={{ span: 24 }}
            >
              <Input />
            </Form.Item>
            <Form.Item
              name="mQuantity"
              label="Quantity "
              labelCol={{ span: 24 }}
              wrapperCol={{ span: 24 }}
              style={{ marginLeft: "2%" }}
            >
              <Input />
            </Form.Item>
          </div>
          <Form.Item
            name="mPrice"
            label="Price"
            labelCol={{ span: 24 }}
            wrapperCol={{ span: 24 }}
          >
            <Input />
          </Form.Item>

          <Form.Item
            name="mLocation"
            label="Location"
            labelCol={{ span: 24 }}
            wrapperCol={{ span: 24 }}
          >
            <Input />
          </Form.Item>
          <Form.Item
            name="mAdd_Date"
            label="Add_Date"
            labelCol={{ span: 24 }}
            wrapperCol={{ span: 24 }}
          >
            <DatePicker style={{ width: "100%" }} />
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
              Update
            </Button>
          </Form.Item>
        </Form>
      </Modal>
      <Modal
        title="Update Government Program"
        visible={updateModalVisible}
        onCancel={handleUpdateCancel}
        footer={[]}
      >
        <Form form={form} layout="vertical"
        
        onFinish={UpdateGovernmentProgram}
        >
          <Form.Item
            name="program_Name"
            label="Program Name"
            rules={[{ required: true, message: 'Please enter Program Name' }]}
          >
            <Input />
          </Form.Item>
          <Form.Item
            name="program_Description"
            label="Description"
            rules={[{ required: true, message: 'Please enter Description' }]}
          >
            <Input.TextArea />
          </Form.Item>
          <Form.Item
            name="programStart_Date"
            label="Start Date"
            rules={[{ required: true, message: 'Please select Start Date' }]}
          >
            <DatePicker />
          </Form.Item>
          <Form.Item
            name="programEnd_Date"
            label="End Date"
            rules={[{ required: true, message: 'Please select End Date' }]}
          >
             <DatePicker  />
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
              Update
            </Button>
          </Form.Item>
        </Form>
      </Modal>
    </div>
  );
};

export default AdminPage;
