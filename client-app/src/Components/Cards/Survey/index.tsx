import { SurveyListModel } from "@Models/Survey";
import { Button, Card, CardActions, CardContent, Typography } from "@mui/material";
import { SxProps, Theme } from "@mui/system";

import { card, date, id, status, close, details } from "./style";

interface Props {
  survey: SurveyListModel;
  closeCallback: () => void;
  detailsCallback: () => void;
  deleteCallback: () => void;
  sx?: SxProps<Theme>;
}

function SurveyCard(props: Props) {
  const { survey, detailsCallback, closeCallback, deleteCallback, sx } = props;
  const style = { ...card, ...sx };

  const creationDate = new Date(survey.creationTime);
  creationDate.setTime(creationDate.getTime() - creationDate.getTimezoneOffset() * 60 * 1000);

  return (
    <Card sx={style}>
      <CardContent>
        <Typography sx={id}>{survey.id}</Typography>
        <Typography sx={status}>Status: {survey.isClosed ? "Closed" : "Open"}</Typography>
        <Typography sx={date}>Creation Date: {creationDate.toLocaleString("ru-Ru")}</Typography>
      </CardContent>
      <CardActions>
        <Button size="small" sx={close} disabled={survey.isClosed} onClick={closeCallback}>
          Close
        </Button>
        <Button size="small" sx={details} onClick={detailsCallback}>
          Details
        </Button>
        <Button size="small" color="error" onClick={deleteCallback}>
          Delete
        </Button>
      </CardActions>
    </Card>
  );
}

export default SurveyCard;
