import "./index.css";

import Layout from "@Components/Layout";
import AllSurveysPage from "@Pages/AllSurveys";
import SurveyCreationPage from "@Pages/SurveyCreation";
import { Route, Routes } from "react-router-dom";

function App() {
  return (
    <Layout>
      <Routes>
        <Route path="/" element={<AllSurveysPage />} />
        <Route path="/survey-creation" element={<SurveyCreationPage />} />
        <Route path="/students" />
      </Routes>
    </Layout>
  );
}

export default App;
