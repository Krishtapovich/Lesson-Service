import { SurveyListModel } from "@Models/Survey";
import { Button, Card, CardActions, CardContent, Typography } from "@mui/material";
import { SxProps, Theme } from "@mui/system";

import { card, close, details, id, remove, results, status, title } from "./style";

interface Props {
  survey: SurveyListModel;
  closeCallback: () => void;
  detailsCallback: () => void;
  deleteCallback: () => void;
  resultsCallback: () => void;
  sx?: SxProps<Theme>;
}

function SurveyCard(props: Props) {
  const { survey, detailsCallback, closeCallback, deleteCallback, resultsCallback, sx } = props;
  const style = { ...card, ...sx };

  return (
    <Card sx={style}>
      <CardContent>
        <Typography sx={id}>{survey.id}</Typography>
        <Typography sx={status}>Status: {survey.isClosed ? "Closed" : "Open"}</Typography>
        <Typography sx={title}>Title: {survey.title}</Typography>
      </CardContent>
      <CardActions>
        <Button size="small" sx={close} disabled={survey.isClosed} onClick={closeCallback}>
          Close
        </Button>
        <Button size="small" sx={details} onClick={detailsCallback}>
          Details
        </Button>
        <Button size="small" sx={results} onClick={resultsCallback}>
          Results
        </Button>
        <Button size="small" sx={remove} onClick={deleteCallback}>
          Delete
        </Button>
      </CardActions>
    </Card>
  );
}

export default SurveyCard;
