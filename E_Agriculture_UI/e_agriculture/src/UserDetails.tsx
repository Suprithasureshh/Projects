import { useEffect, useState } from "react";
import { Card, Modal, message } from "antd";
import axios from "axios";
import { BiArrowBack, BiUserCircle } from "react-icons/bi";
import { RiLogoutCircleLine } from "react-icons/ri";
import { useNavigate } from "react-router-dom";
interface UserData {
  user_Id: string;
  user_Name: string;
  joining_Date: string;
  contact_No: string;
  address: string;
  userAs: string;
}

const UserDetails = () => {
  const [userProfile, setUserProfile] = useState<UserData | null>(null);
  const [isModalOpen, setIsModalOpen] = useState(false);

  const Email = sessionStorage.getItem("email");
  const navigate = useNavigate();

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

  const showModal = () => {
    setIsModalOpen(true);
  };

  const handleCancel = () => {
    setIsModalOpen(false);
  };

  const handleLogoutClick = () => {
    navigate("/");
  };
  return (
    <div>
      <div
        style={{
          cursor: "pointer",
          marginLeft: "86%",
          
        }}
        onClick={showModal}
      >
        <Card style={{ backgroundColor: "#3140b7", width: 190, height: 40 }}>
          <div
            style={{ display: "flex", alignItems: "center", marginTop: "-14%" }}
          >
            <BiUserCircle
              style={{
                width: 30,
                height: 30,
                color: "black",
                marginLeft: "2%",
                marginTop: "2%",
              }}
            />
            <span
              style={{ fontSize: "20px", marginLeft: "8px", marginTop: "2%" }}
            >
              UserProfile
            </span>
          </div>
        </Card>
      </div>
      <div
        style={{
          cursor: "pointer",
          marginLeft: "86%",
        }}
        onClick={handleLogoutClick}
      >
        <Card
          style={{
            backgroundColor: "#3140b7",
            width: 190,
            height: 40,
            marginTop: "-0.5%",
          }}
        >
          <div
            style={{ display: "flex", alignItems: "center", marginTop: "-14%" }}
          >
            <RiLogoutCircleLine
              style={{
                width: 25,
                height: 30,
                color: "red",
                marginLeft: "4%",
                marginTop: "2%",
              }}
            />
            <span
              style={{ fontSize: "20px", marginLeft: "8px", marginTop: "2%" }}
            >
              Logout
            </span>
          </div>
        </Card>
      </div>
      <div style={{ backgroundColor: "#3140b7" }}>
        <Modal
          className="custom-modal"
          title=""
          open={isModalOpen}
          footer={[]}
          closable={false}
          width={300}
          style={{ marginTop: "6%", marginRight: "17%" }}
        >
          <h3 style={{ marginLeft: "95%", marginTop: "-4%" }}>
            <BiArrowBack
              style={{ width: 30, height: 30 }}
              onClick={handleCancel}
            />
          </h3>

          {userProfile && (
            <div className="profile-details" style={{ marginTop: "-22%" }}>
              <p style={{ textAlign: "center", paddingTop: "15%" }}>
                <strong>UserId:</strong> {userProfile.user_Id}
              </p>
              <p style={{ textAlign: "center" }}>
                <strong>UserName:</strong> {userProfile.user_Name}
              </p>
              <p style={{ textAlign: "center" }}>
                <strong>JoiningDate:</strong>{" "}
                {userProfile.joining_Date &&
                  new Date(userProfile.joining_Date).toLocaleDateString(
                    "en-GB"
                  )}
              </p>
              <p style={{ textAlign: "center" }}>
                <strong>Address:</strong> {userProfile.address}
              </p>
              <p style={{ textAlign: "center" }}>
                <strong>Contact:</strong> {userProfile.contact_No}
              </p>
              <p style={{ textAlign: "center" }}>
                <strong>UserAs:</strong> {userProfile.userAs}
              </p>
            </div>
          )}
        </Modal>
      </div>
    </div>
  );
};

export default UserDetails;
