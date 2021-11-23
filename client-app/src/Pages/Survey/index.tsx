import SurveyCard from "@Components/Cards/Survey";
import useStore from "@Stores";
import { observer } from "mobx-react-lite";
import { useEffect } from "react";

function SurveyPage() {
  const { surveyStore } = useStore();
  const { surveys } = surveyStore;

  useEffect(() => {
    surveyStore.init();
    return () => surveyStore.dispose();
  }, []);

  return (
    <div style={{ width: "90%", margin: "auto" }}>
      {surveys.map((survey) => (
        <SurveyCard survey={survey} deleteCallback={() => surveyStore.deleteSurvey(survey.id)} />
      ))}
    </div>
  );
}

export default observer(SurveyPage);
