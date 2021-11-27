import { SxProps } from "@mui/system";

export const pageWrapper: SxProps = {
  padding: 3,
  width: "95%",
  margin: "auto",
  display: "flex",
  flexDirection: "row",
  maxHeight: "91vh",
  justifyContent: "space-between"
};

export const surveyBlock: SxProps = {
  width: "40%",
  marginRight: 3,
  paddingY: 1.2,
  overflow: "auto",
  "&::-webkit-scrollbar": {
    width: 4
  },
  "&::-webkit-scrollbar-thumb": {
    backgroundColor: "#10B981",
    borderRadius: 3
  }
};

export const card: SxProps = {
  width: "90%",
  margin: "3% auto",
  background: "linear-gradient(90deg, rgba(38,99,142,1) 0%, rgba(57,177,191,1) 100%)"
};

export const surveyPreview: SxProps = {
  width: "60%",
  height: "92vh",
  border: "2px dashed white",
  borderRadius: 3,
  overflow: "auto",
  display: "flex"
};

export const previewText: SxProps = {
  fontSize: 30,
  color: "white",
  margin: "auto",
  textAlign: "center"
};

export const surveyQuestions: SxProps = {
  background: "linear-gradient(90deg, rgba(29, 187, 221, 0.46), rgba(51, 241, 111, 0.69))",
  boxShadow: 5,
  border: "0"
};

export const questionsView: SxProps = { ...surveyPreview, ...surveyQuestions };

export const question: SxProps = {
  fontSize: 23,
  fontWeight: 600,
  color: "white"
};

export const option = (isCorrect: boolean): SxProps => {
  return {
    fontSize: 18,
    fontWeight: 500,
    color: isCorrect ? "#3dff00" : "white",
    marginLeft: 5
  };
};
