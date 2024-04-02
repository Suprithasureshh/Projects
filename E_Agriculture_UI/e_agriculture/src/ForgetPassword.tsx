import { Button, Form, Input, Modal, message } from "antd"
import { useEffect, useState } from "react"
//import './ForgetPasword.css'
import axios from "axios"
import { useNavigate } from "react-router-dom"
import { GiCancel } from "react-icons/gi"
import { IoMdArrowBack } from "react-icons/io"

export function ForgotPassword(){




    const [isEmailModalOpen,setIsEmailModalOpen] = useState(false)
    const [isOtpModalOpen,setIsOtpModalOpen] = useState(false)
    const [isPasswordModalOpen,setIsPasswordModalOpen] = useState(false)

    const navigate = useNavigate();
    //to reset forms
    const [emailForm] = Form.useForm();
    const [otpForm] = Form.useForm();
    const [passwordForm] = Form.useForm();
    const onFinishEmail = (values: any) => {
      axios
        .post("/api/Login/VerifyEmail", values)
        .then(() => {
          setIsEmailModalOpen(false);
          emailForm.resetFields();
          setIsOtpModalOpen(true);
          console.log(values.email);
          sessionStorage.setItem("fPEmail", values.email);
          axios.post("/api/Login/GenerateOTP", values);
          message.success("OTP sent to your email address.");
        })
        .catch((error) => {
          message.error(error.response.data);
        });
    };
    const onFinishOtp = (values:any) => {
        axios.post("/api/Login/VerifyOTP",values)
        .then(()=>{
            sessionStorage.setItem("fPOtp",values.otp);
            setIsOtpModalOpen(false);
            otpForm.resetFields();
            setIsPasswordModalOpen(true);
        })
        .catch((error)=>{
            message.error(error.response.data);
        })
    }  
      

    const onFinishPassword = (values:any) => {
        axios.post("/api/Login/SetNewPassword",values)
        .then(()=>{
            setIsPasswordModalOpen(false);
            passwordForm.resetFields();
            navigate('/');
        })
        .catch((error)=>{
            message.error(error.response.data);
        })
    }

    const handleCancel = () => {
        setIsEmailModalOpen(false);
        setIsOtpModalOpen(false);
        setIsPasswordModalOpen(false);
        sessionStorage.setItem("fPEmail","")
        sessionStorage.setItem("fPOtp","")
    }
    const handleOtpBack = () => {
        setIsOtpModalOpen(false);
        setIsEmailModalOpen(true);
        sessionStorage.setItem("fPEmail","")
    }



    return (
    <>
     {/* Modal to get email field for forgotPassword to get otp */}
    <Button type="link" onClick={()=>{setIsEmailModalOpen(true)}}>Forgot Password?</Button>
        <Modal className="" open={isEmailModalOpen}  footer=""  closable={false} >
            <div className="forgetPasswordH3">
            <h3 style={{marginLeft:"97%",marginTop:"-2%",fontSize:"25px"}}><GiCancel onClick={handleCancel} /></h3>
                <h3 style={{color:"#101e8a",marginTop:"-3%"}}>Forgot Password? Reset here! </h3>
                
                
            </div>
            <Form className="ForgetPasswordForm"
             onFinish={onFinishEmail} form={emailForm} >
                <Form.Item name="email" rules={[{required:true,message:"Please enter a valid email"}]}>
                    <Input placeholder="Enter your registered email"/>
                </Form.Item>
                <Form.Item>
                    
                    <button type="submit" style={{width:"20%",height:"112%",borderRadius:"5px",color:"white",backgroundColor:"#101e8a"}}>Get OTP</button>
                </Form.Item>
            </Form>
        </Modal>

    <Modal className="" open={isOtpModalOpen}  footer=""  closable={false}>
    <div className="forgetPasswordH3">
    <h3 style={{marginTop:"-2%",fontSize:"25px"}} ><IoMdArrowBack onClick={handleOtpBack}/></h3>
        <h3 style={{marginLeft:"97%",marginTop:"-14%",fontSize:"25px"}} ><GiCancel onClick={handleCancel} /></h3>
    <h3 style={{color:"#101e8a",marginTop:"-3%"}}>Enter your OTP</h3>
       
    </div>
        <Form className="ForgetPasswordForm"
             onFinish={onFinishOtp} form={otpForm}>
                <Form.Item name="user" initialValue={sessionStorage.getItem("fPEmail")} hidden >
                    <Input placeholder="Enter the recieved otp "/>
                </Form.Item>
                <Form.Item name="otp" rules={[{required:true,message:"Please enter OTP"}]}>
                    <Input placeholder="Enter OTP"/>
                </Form.Item>
                <Form.Item>
                    <button type="submit" style={{width:"20%",height:"112%",borderRadius:"5px",color:"white",backgroundColor:"#101e8a"}}>Verify OTP</button>
                </Form.Item>
            </Form>
    </Modal>

    <Modal className="" open={isPasswordModalOpen}  footer=""  closable={false}>
    <div className="forgetPasswordH3">
    <h3 style={{marginLeft:"97%",marginTop:"-2%",fontSize:"25px"}}><GiCancel onClick={handleCancel} /></h3>
    <h3 style={{color:"#101e8a",marginTop:"-6%"}}>Set New Password  </h3>
        
        
    </div>
        <Form className="ForgetPasswordForm"
             onFinish={onFinishPassword} form={passwordForm}>
                <Form.Item name="user" initialValue={sessionStorage.getItem("fPEmail")} hidden >
                    <Input />
                </Form.Item>
                <Form.Item name="otp" initialValue={sessionStorage.getItem("fPOtp")} hidden >
                    <Input />
                </Form.Item>
                <Form.Item name="newPassword" rules={[{required:true,message:"Please set a new-password"},
                    
                ]}>
                    <Input placeholder="New Password"/>
                </Form.Item>
                <Form.Item name="confirmPassword" rules={[{required:true,message:"Please re-enter new-password "}]}>
                    <Input placeholder="Confirm Password"/>
                </Form.Item>
                <Form.Item>
                    <button type="submit" style={{width:"25%",borderRadius:"5px",color:"white",backgroundColor:"#101e8a"}}>Set Password</button>
                </Form.Item>
            </Form>
    </Modal>

    </>
)}
