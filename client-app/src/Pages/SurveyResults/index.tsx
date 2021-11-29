import useStore from "@Stores";
import { observer } from "mobx-react-lite";
import { useEffect } from "react";
import { useLocation } from "react-router-dom";

function SurveyResultsPage() {
  const { surveyStore } = useStore();
  const { state } = useLocation();

  const { surveyStudents } = surveyStore;

  useEffect(() => {
    surveyStore.getSurveyStudents(state.surveyId);
    return () => surveyStore.dispose();
  }, [surveyStore, state.surveyId]);

  return <></>;
}

export default observer(SurveyResultsPage);
