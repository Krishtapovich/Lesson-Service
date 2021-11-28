import SurveySendingModal from "@Components/Modals/SurveySending";
import { SurveyListModel } from "@Models/Survey";
import FiberManualRecordIcon from "@mui/icons-material/FiberManualRecord";
import { Box, Button, Card, Typography } from "@mui/material";
import useStore from "@Stores";
import { observer } from "mobx-react-lite";
import { useEffect, useState } from "react";

import { card, content, header, option, question, sendButton, title } from "./style";

interface Props {
  survey: SurveyListModel;
}

function SurveyQuestionsCard({ survey }: Props) {
  const { surveyStore } = useStore();
  const { surveyQuestions } = surveyStore;

  useEffect(() => {
    surveyStore.getSurveyQuestions(survey.id);
  }, [surveyStore, survey.id]);

  const [isModalOpen, setIsModalOpen] = useState(false);
  const toggleModal = () => setIsModalOpen(!isModalOpen);

  const sendSurvey = (groups: Array<string>, openPeriod?: number) => {
    surveyStore.sendSurvey({ id: survey.id, groups, openPeriod });
  };

  return (
    <>
      <Card sx={card}>
        <Box sx={header}>
          <Typography sx={title}>{survey.title}</Typography>
          <Button variant="contained" onClick={toggleModal} sx={sendButton}>
            Send
          </Button>
        </Box>
        <Box sx={content}>
          {surveyQuestions.map((q, i) => (
            <Box key={q.id}>
              <Typography sx={question}>{`${i + 1}. ${q.text}`}</Typography>
              {q.options?.map((o) => (
                <Box sx={option(o.isCorrect)} key={o.id}>
                  <FiberManualRecordIcon fontSize="small" />
                  <Typography>{o.text}</Typography>
                </Box>
              ))}
            </Box>
          ))}
        </Box>
      </Card>
      <SurveySendingModal
        isOpen={isModalOpen}
        handleClose={toggleModal}
        sendCallback={sendSurvey}
      />
    </>
  );
}

export default observer(SurveyQuestionsCard);
