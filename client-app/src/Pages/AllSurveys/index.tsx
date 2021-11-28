import SurveyCard from "@Components/Cards/Survey";
import SurveyQuestionsCard from "@Components/Cards/SurveyQuestions";
import { SurveyListModel } from "@Models/Survey";
import { Box, Typography } from "@mui/material";
import useStore from "@Stores";
import { observer } from "mobx-react-lite";
import { useEffect, useState } from "react";

import { card, pageWrapper, previewText, surveyBlock, surveyPreview } from "./style";

function AllSurveysPage() {
  const { surveyStore } = useStore();
  const { surveys } = surveyStore;

  const [survey, setSurvey] = useState<SurveyListModel>();

  useEffect(() => {
    surveyStore.init();
    return () => surveyStore.dispose();
  }, [surveyStore]);

  return (
    <Box sx={pageWrapper}>
      <Box sx={surveyBlock}>
        {surveys.map((s) => (
          <SurveyCard
            key={s.id}
            sx={card}
            survey={s}
            detailsCallback={() => setSurvey(s)}
            closeCallback={() => surveyStore.closeSruvey(s.id)}
            deleteCallback={() => surveyStore.deleteSurvey(s.id)}
          />
        ))}
      </Box>
      {survey ? (
        <SurveyQuestionsCard survey={survey} />
      ) : (
        <Box sx={surveyPreview}>
          <Typography sx={previewText}>Click Details to see survey</Typography>
        </Box>
      )}
    </Box>
  );
}

export default observer(AllSurveysPage);
