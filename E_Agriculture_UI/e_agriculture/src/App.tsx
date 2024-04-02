
import './App.css';
import { BrowserRouter, Route, Routes } from 'react-router-dom';
import HomePage from './Home';
import FarmerPage from './Farmer';
import BuyerPage from './Buyer';
import StudentPage from './Student';
import AdminPage from './Admin';


function App() {
  return (
    <div className="App">
      <BrowserRouter>
        <Routes>
          <Route path="/" Component={HomePage} />
          <Route path="/farmerpage" Component={FarmerPage} />
          <Route path="/buyerpage" Component={BuyerPage} />
          <Route path="/studentpage" Component={StudentPage} />
          <Route path="/adminpage" Component={AdminPage} />
          </Routes>
          </BrowserRouter>
    </div>
  );
}

export default App;
