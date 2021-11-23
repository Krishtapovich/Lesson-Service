import { SurveyModel } from "@Models/Survey";
import { Button, Card, CardActions, CardContent, Typography } from "@mui/material";

interface Props {
  survey: SurveyModel;
  deleteCallback: () => void;
}

function SurveyCard(props: Props) {
  const { survey, deleteCallback } = props;

  return (
    <Card>
      <CardContent>
        <Typography sx={{ fontSize: 14 }} color="text.secondary" gutterBottom>
          {survey.id}
        </Typography>
        <Typography>{survey.isClosed}</Typography>
        <CardActions>
          <Button onClick={deleteCallback} color="error" variant="contained">
            Delete
          </Button>
        </CardActions>
      </CardContent>
    </Card>
  );
}

export default SurveyCard;
