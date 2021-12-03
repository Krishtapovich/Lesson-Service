import ResultVisualizationCard from "@Components/Cards/ResultVisualization";
import LoadingWrapper from "@Components/LoadingWrapper";
import StudentsTable from "@Components/StudentsTable";
import { Box } from "@mui/material";
import useStore from "@Stores";
import { observer } from "mobx-react-lite";
import { useEffect } from "react";
import { useParams } from "react-router-dom";

import { content, loader, tableWrapper, charts } from "./style";

function SurveyResultsPage() {
  const { surveyStore } = useStore();
  const { surveyId } = useParams();

  const { surveyStudents, visualization, isAllAnswersLoading } = surveyStore;

  useEffect(() => {
    surveyStore.getSurveyStudents(surveyId!);
    surveyStore.getSurveyAnswers(surveyId!);
    return () => surveyStore.disposeResults();
  }, [surveyStore, surveyId]);

  return (
    <LoadingWrapper size="10%" sx={loader} isLoading={isAllAnswersLoading}>
      <Box sx={content}>
        <Box sx={tableWrapper}>
          <StudentsTable
            students={surveyStudents}
            resultsCallback={(studentId) => surveyStore.getStudentAnswers(surveyId!, studentId)}
          />
        </Box>
        <ResultVisualizationCard answers={visualization} sx={charts} />
      </Box>
    </LoadingWrapper>
  );
}

export default observer(SurveyResultsPage);
