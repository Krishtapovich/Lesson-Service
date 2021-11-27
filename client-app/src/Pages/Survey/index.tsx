import SurveyCard from "@Components/Cards/Survey";
import SurveySendingModal from "@Components/Modals/SurveySending";
import { Box, Button, Card, CardContent, Typography } from "@mui/material";
import useStore from "@Stores";
import { observer } from "mobx-react-lite";
import { useEffect, useState } from "react";

import { card, option, pageWrapper, previewText, question, questionsView, surveyBlock, surveyPreview } from "./style";

function SurveyPage() {
  const { surveyStore } = useStore();
  const { surveys, surveyQuestions } = surveyStore;

  const [id, setId] = useState<string>();

  useEffect(() => {
    surveyStore.init();
    return () => surveyStore.dispose();
  }, [surveyStore]);

  const [isModalOpen, setIsModalOpen] = useState(false);
  const toggleModal = () => setIsModalOpen(!isModalOpen);

  const detailsCallback = (surveyId: string) => {
    setId(surveyId);
    surveyStore.getSurveyQuestions(surveyId);
  };

  const sendSurvey = (groups: Array<string>, openPeriod?: number) => {
    id && surveyStore.sendSurvey({ id, groups, openPeriod });
  };

  return (
    <Box sx={pageWrapper}>
      <Box sx={surveyBlock}>
        {surveys.map((s) => (
          <SurveyCard
            key={s.id}
            sx={card}
            survey={s}
            detailsCallback={() => detailsCallback(s.id)}
            closeCallback={() => surveyStore.closeSruvey(s.id)}
            deleteCallback={() => surveyStore.deleteSurvey(s.id)}
          />
        ))}
      </Box>
      {surveyQuestions.length ? (
        <Card sx={questionsView}>
          <CardContent>
            {surveyQuestions.map((q, i) => (
              <Box key={q.id}>
                <Typography sx={question}>{`${i + 1}. ${q.text}`}</Typography>
                {q.options?.map((o) => (
                  <Typography key={o.id} sx={option(o.isCorrect)}>
                    {o.text}
                  </Typography>
                ))}
              </Box>
            ))}
            <Button onClick={toggleModal}>Send</Button>
          </CardContent>
        </Card>
      ) : (
        <Box sx={surveyPreview}>
          <Typography sx={previewText}>Click Details to see survey</Typography>
        </Box>
      )}
      <SurveySendingModal
        isOpen={isModalOpen}
        handleClose={toggleModal}
        sendCallback={sendSurvey}
      />
    </Box>
  );
}

export default observer(SurveyPage);
