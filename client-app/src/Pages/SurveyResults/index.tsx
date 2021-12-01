import LoadingWrapper from "@Components/LoadingWrapper";
import StudentsTable from "@Components/StudentsTable";
import { Box } from "@mui/material";
import useStore from "@Stores";
import { observer } from "mobx-react-lite";
import { useEffect } from "react";
import { useParams } from "react-router-dom";

import { loader, tableWrapper } from "./style";

function SurveyResultsPage() {
  const { surveyStore } = useStore();
  const { surveyId } = useParams();

  const { surveyStudents, isAllAnswersLoading } = surveyStore;

  useEffect(() => {
    surveyStore.getSurveyStudents(surveyId!);
    surveyStore.getSurveyAnswers(surveyId!);
    return () => surveyStore.disposeResults();
  }, [surveyStore, surveyId]);

  return (
    <Box>
      <LoadingWrapper size="10%" sx={loader} isLoading={isAllAnswersLoading}>
        <Box sx={tableWrapper}>
          <StudentsTable
            students={surveyStudents}
            resultsCallback={(studentId) => surveyStore.getStudentAnswers(surveyId!, studentId)}
          />
        </Box>
      </LoadingWrapper>
    </Box>
  );
}

export default observer(SurveyResultsPage);
