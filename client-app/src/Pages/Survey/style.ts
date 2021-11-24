import { SxProps, Theme } from "@mui/system";

export const pageWrapper: SxProps<Theme> = {
  padding: 3,
  width: "95%",
  margin: "auto",
  display: "flex",
  flexDirection: "row",
  maxHeight: "91vh",
  justifyContent: "space-between"
};

export const surveyBlock: SxProps<Theme> = {
  width: "40%",
  marginRight: 3,
  paddingY: 1.2,
  overflow: "auto",
  "&::-webkit-scrollbar": {
    width: 4,
    borderRadius: 3
  },
  "&::-webkit-scrollbar-track": {
    borderRadius: 3
  },
  "&::-webkit-scrollbar-thumb": {
    backgroundColor: "#10B981",
    borderRadius: 3
  }
};

export const card: SxProps<Theme> = {
  width: "90%",
  margin: "3% auto",
  background: "linear-gradient(90deg, rgba(38,99,142,1) 0%, rgba(57,177,191,1) 100%)"
};

export const surveyPreview: SxProps<Theme> = {
  width: "60%",
  height: "92vh",
  border: "2px dashed white",
  borderRadius: 3,
  overflow: "auto",
  display: "flex"
};

export const previewText: SxProps<Theme> = {
  fontSize: 30,
  color: "white",
  margin: "auto"
};

export const surveyQuestions: SxProps<Theme> = {
  background: "linear-gradient(90deg, rgba(29, 187, 221, 0.46), rgba(51, 241, 111, 0.69))",
  boxShadow: 5,
  border: "0"
};

export const question: SxProps<Theme> = {
  fontSize: 23,
  fontWeight: 600,
  color: "white"
};

export const option = (isCorrect: boolean): SxProps<Theme> => {
  return {
    fontSize: 18,
    fontWeight: 500,
    color: isCorrect ? "#3dff00" : "white",
    marginLeft: 5
  };
};
