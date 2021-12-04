import LoadingWrapper from "@Components/LoadingWrapper";
import StudentModel from "@Models/Student";
import FiberManualRecordIcon from "@mui/icons-material/FiberManualRecord";
import { Box, Card, CardMedia, Typography } from "@mui/material";
import { SxProps } from "@mui/system";
import useStore from "@Stores";
import { observer } from "mobx-react-lite";
import { useEffect } from "react";

import { card, loader, header, option, question, title, textAnswer } from "./style";

interface Props {
  student: StudentModel;
  surveyId: string;
  sx?: SxProps;
}

function StudentAnswersCard(props: Props) {
  const { student, surveyId, sx } = props;

  const { surveyStore } = useStore();
  const { isStudentAnswersLoading, answers } = surveyStore;

  const cardStyle = { ...card, ...sx };

  useEffect(() => {
    surveyStore.getStudentAnswers(surveyId, student.id);
  }, [surveyStore, student, surveyId]);

  return (
    <Card sx={cardStyle}>
      <Box sx={header}>
        <Typography
          sx={title}
        >{`${student.lastName} ${student.firstName}, ${student.groupNumber}`}</Typography>
      </Box>
      <LoadingWrapper size="15%" sx={loader} isLoading={isStudentAnswersLoading}>
        {answers.map((a, i) => (
          <Box key={i}>
            <Typography sx={question}>{`${i + 1}. ${a.question.text}`}</Typography>
            {a.question.options?.map((o, j) => (
              <Box sx={option(o.isCorrect, a.optionId === o.id)} key={j}>
                <FiberManualRecordIcon fontSize="small" />
                <Typography>{o.text}</Typography>
              </Box>
            ))}
            <Typography sx={textAnswer}>{a.text}</Typography>
            <CardMedia image={a.imageUrl} />
          </Box>
        ))}
      </LoadingWrapper>
    </Card>
  );
}

export default observer(StudentAnswersCard);
