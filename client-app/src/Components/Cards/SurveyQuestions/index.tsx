import LoadingWrapper from "@Components/LoadingWrapper";
import SurveySendingModal from "@Components/Modals/SurveySending";
import FiberManualRecordIcon from "@mui/icons-material/FiberManualRecord";
import { Box, Button, Card, Typography } from "@mui/material";
import useStore from "@Stores";
import { observer } from "mobx-react-lite";
import { useEffect, useState } from "react";

import { card, content, header, loader, option, question, sendButton, title } from "./style";

interface Props {
  surveyId: string;
}

function SurveyQuestionsCard({ surveyId }: Props) {
  const { surveyStore } = useStore();
  const { surveyQuestions, isQuestionsLoading, currentSurvey } = surveyStore;

  useEffect(() => {
    surveyStore.setSurvey(surveyId);
    surveyStore.getSurveyQuestions(surveyId);
  }, [surveyStore, surveyId]);

  const [isModalOpen, setIsModalOpen] = useState(false);
  const toggleModal = () => setIsModalOpen(!isModalOpen);

  const sendSurvey = (groups: Array<string>, openPeriod?: number) => {
    surveyStore.sendSurvey({ id: surveyId, groups, openPeriod });
  };

  return (
    <>
      <Card sx={card}>
        <Box sx={header}>
          {currentSurvey && (
            <>
              <Typography sx={title}>{currentSurvey.title}</Typography>
              <Button
                variant="contained"
                onClick={toggleModal}
                sx={sendButton}
                disabled={!currentSurvey.isClosed}
              >
                Send
              </Button>
            </>
          )}
        </Box>
        <LoadingWrapper isLoading={isQuestionsLoading} sx={loader} size="15%">
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
        </LoadingWrapper>
      </Card>
      {isModalOpen && (
        <SurveySendingModal
          isOpen={isModalOpen}
          handleClose={toggleModal}
          sendCallback={sendSurvey}
        />
      )}
    </>
  );
}

export default observer(SurveyQuestionsCard);
