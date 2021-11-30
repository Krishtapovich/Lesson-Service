import { SxProps } from "@mui/system";

export const pageWrapper: SxProps = {
  margin: "auto",
  display: "flex",
  flexDirection: "row",
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
  width: "57%",
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
