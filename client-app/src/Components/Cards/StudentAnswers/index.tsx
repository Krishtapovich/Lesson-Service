import LoadingWrapper from "@Components/LoadingWrapper";
import { StudentCsvAnswerModel } from "@Models/Answer";
import StudentModel from "@Models/Student";
import FiberManualRecordIcon from "@mui/icons-material/FiberManualRecord";
import { Box, Button, Card, CardMedia, Checkbox, Typography } from "@mui/material";
import { SxProps } from "@mui/system";
import useStore from "@Stores";
import { observer } from "mobx-react-lite";
import { useEffect, useState } from "react";
import { CSVLink } from "react-csv";

import * as style from "./style";

interface Props {
  student: StudentModel;
  surveyId: string;
  sx?: SxProps;
}

function StudentAnswersCard(props: Props) {
  const { student, surveyId, sx } = props;
  const { groupNumber, firstName, lastName } = student;

  const { surveyStore } = useStore();
  const { isStudentAnswersLoading, answers } = surveyStore;
  const [csvAnswers, setCsvAnswers] = useState<Array<StudentCsvAnswerModel>>([]);

  useEffect(() => {
    surveyStore.getStudentAnswers(surveyId, student.id).then((res) => setCsvAnswers(res));
  }, [surveyStore, student, surveyId]);

  const check = (i: number) =>
    setCsvAnswers(csvAnswers.map((a, j) => (j === i ? { ...a, isCorrect: !a.isCorrect } : a)));

  const cardStyle = { ...style.card, ...sx };

  return (
    <Card sx={cardStyle}>
      <Box sx={style.header}>
        <Typography sx={style.title}>{`${lastName} ${firstName}, ${groupNumber}`}</Typography>
      </Box>
      <LoadingWrapper size="15%" sx={style.loader} isLoading={isStudentAnswersLoading}>
        <Box sx={style.content}>
          {answers.map((a, i) => (
            <Box key={i} sx={style.answer}>
              <Box sx={style.answerHeader}>
                <Typography sx={style.question}>{`${i + 1}. ${a.question.text}`}</Typography>
                {!a.option?.id && <Checkbox onClick={() => check(i)} sx={style.checkbox} />}
              </Box>
              {a.question.options?.map((o, j) => (
                <Box sx={style.option(o.isCorrect, a.option?.id === o.id)} key={j}>
                  <FiberManualRecordIcon fontSize="small" />
                  <Typography>{o.text}</Typography>
                </Box>
              ))}
              <Typography sx={style.textAnswer}>{a.text}</Typography>
              <CardMedia component="img" image={a.imageUrl} sx={style.imageAnswer} />
            </Box>
          ))}
          <CSVLink
            style={style.link}
            filename={`${groupNumber}.${lastName}-${firstName}.csv`}
            data={csvAnswers}
          >
            <Button variant="contained" sx={style.button}>
              Download Answers
            </Button>
          </CSVLink>
        </Box>
      </LoadingWrapper>
    </Card>
  );
}

export default observer(StudentAnswersCard);
