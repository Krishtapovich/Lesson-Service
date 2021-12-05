import "./index.css";

import Layout from "@Components/Layout";
import AllSurveysPage from "@Pages/AllSurveys";
import SurveyCreationPage from "@Pages/SurveyCreation";
import SurveyResultsPage from "@Pages/SurveyResults";
import { Route, Routes } from "react-router-dom";
import StudentsPage from "@Pages/Students";

function App() {
  return (
    <Layout>
      <Routes>
        <Route path="/" element={<AllSurveysPage />} />
        <Route path="/survey-creation" element={<SurveyCreationPage />} />
        <Route path="/survey-results/:surveyId" element={<SurveyResultsPage />} />
        <Route path="/students" element={<StudentsPage />} />
      </Routes>
    </Layout>
  );
}

export default App;
