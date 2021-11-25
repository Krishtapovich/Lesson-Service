import SurveyCard from "@Components/Cards/Survey";
import { SurveyModel } from "@Models/Survey";
import { Box, Card, CardContent, Typography } from "@mui/material";
import useStore from "@Stores";
import { observer } from "mobx-react-lite";
import { useEffect, useState } from "react";

import {
  card,
  option,
  pageWrapper,
  previewText,
  question,
  surveyBlock,
  surveyPreview,
  surveyQuestions
} from "./style";

function SurveyPage() {
  const { surveyStore } = useStore();
  const { surveys } = surveyStore;

  const [survey, setSurvey] = useState<SurveyModel>();

  useEffect(() => {
    surveyStore.init();
    return () => surveyStore.dispose();
  }, [surveyStore]);

  const questionsView = { ...surveyPreview, ...surveyQuestions };

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
        <Card sx={questionsView}>
          <CardContent>
            {survey.questions.map((q, i) => (
              <Box key={q.id}>
                <Typography sx={question}>{`${i + 1}. ${q.text}`}</Typography>
                {q.options?.map((o) => (
                  <Typography key={o.id} sx={option(o.isCorrect)}>
                    {o.text}
                  </Typography>
                ))}
              </Box>
            ))}
          </CardContent>
        </Card>
      ) : (
        <Box sx={surveyPreview}>
          <Typography sx={previewText}>Click Details to see survey questions</Typography>
        </Box>
      )}
    </Box>
  );
}

export default observer(SurveyPage);
