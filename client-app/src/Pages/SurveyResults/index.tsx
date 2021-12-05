import ResultVisualizationCard from "@Components/Cards/ResultVisualization";
import StudentAnswersCard from "@Components/Cards/StudentAnswers";
import LoadingWrapper from "@Components/LoadingWrapper";
import StudentsTable from "@Components/StudentsTable";
import StudentModel from "@Models/Student";
import { Box } from "@mui/material";
import useStore from "@Stores";
import { observer } from "mobx-react-lite";
import { useEffect, useState } from "react";
import { useParams } from "react-router-dom";

import { card, content, loader, tableWrapper } from "./style";

function SurveyResultsPage() {
  const { surveyStore } = useStore();
  const { surveyId } = useParams();

  const { surveyStudents, visualization, isAllAnswersLoading, csvAnswers } = surveyStore;
  
  useEffect(() => {
    surveyStore.getSurveyStudents(surveyId!);
    surveyStore.getSurveyAnswers(surveyId!);
    surveyStore.getSurveyCsvAnswers(surveyId!);
    return () => surveyStore.disposeResults();
  }, [surveyStore, surveyId]);

  const [student, setStudent] = useState<StudentModel>();

  return (
    <LoadingWrapper size="10%" sx={loader} isLoading={isAllAnswersLoading}>
      <Box sx={content}>
        <Box sx={tableWrapper}>
          <StudentsTable
            students={surveyStudents}
            csvAnswers={csvAnswers}
            resultsCallback={(student) => setStudent(student)}
          />
        </Box>
        {student ? (
          <StudentAnswersCard surveyId={surveyId!} student={student} sx={card} />
        ) : (
          <ResultVisualizationCard answers={visualization} sx={card} />
        )}
      </Box>
    </LoadingWrapper>
  );
}

export default observer(SurveyResultsPage);
