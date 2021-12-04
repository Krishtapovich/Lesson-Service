import { SxProps } from "@mui/system";

export const card: SxProps = {
  background: "linear-gradient(90deg, rgba(29, 187, 221, 0.46), rgba(51, 241, 111, 0.69))",
  boxShadow: 5,
  flexGrow: 1,
  borderRadius: 3,
  padding: 2,
  position: "relative"
};

export const header: SxProps = {
  position: "sticky",
  top: 0,
  display: "flex",
  justifyContent: "space-between",
  alignItems: "center",
  marginBottom: 2
};

export const loader: SxProps = {
  position: "absolute",
  top: "45%",
  left: "45%"
};

export const title: SxProps = {
  fontSize: 30,
  fontWeight: 700,
  color: "white"
};

export const question: SxProps = {
  fontSize: 23,
  fontWeight: 600,
  color: "white"
};

export const textAnswer: SxProps = {
  marginLeft: 5,
  color: "white",
  fontSize: 18,
  fontWeight: 500
};

export const option = (isCorrect: boolean, isWrong: boolean): SxProps => {
  return {
    marginLeft: 5,
    display: "flex",
    alignItems: "center",
    color: isCorrect ? "#3dff00" : isWrong ? "red" : "white",
    "& svg": {
      marginRight: 1,
      paddingTop: "3px"
    },
    "& p": {
      fontSize: 18,
      fontWeight: 500
    }
  };
};
