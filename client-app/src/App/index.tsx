import "./index.css";

import Layout from "@Components/Layout";
import SurveyPage from "@Pages/Survey";
import { Route, Routes } from "react-router-dom";

function App() {
  return (
    <Layout>
      <Routes>
        <Route path="/" element={<SurveyPage />} />
        <Route path="/students" />
      </Routes>
    </Layout>
  );
}

export default App;
