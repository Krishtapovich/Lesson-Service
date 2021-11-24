import { SurveyModel } from "@Models/Survey";
import { Button, Card, CardActions, CardContent, Typography } from "@mui/material";
import { SxProps, Theme } from "@mui/system";

import { card, id, status, date } from "./style";

interface Props {
  survey: SurveyModel;
  closeCallback: () => void;
  detailsCallback: () => void;
  deleteCallback: () => void;
  sx?: SxProps<Theme>;
}

function SurveyCard(props: Props) {
  const { survey, detailsCallback, deleteCallback, sx } = props;
  const style = { ...card, ...sx };

  return (
    <Card sx={style}>
      <CardContent>
        <Typography sx={id}>{survey.id}</Typography>
        <Typography sx={status}>Status: {survey.isClosed ? "Closed" : "Open"}</Typography>
        <Typography sx={date}>Creation Date: {survey.creationTime}</Typography>
      </CardContent>
      <CardActions>
        <Button size="small" color="warning" disabled={!survey.isClosed}>
          Close
        </Button>
        <Button size="small" sx={{ color: "white" }} onClick={detailsCallback}>
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
