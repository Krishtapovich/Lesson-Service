import { SxProps } from "@mui/system";

export const card: SxProps = {
  background: "linear-gradient(90deg, rgba(29, 187, 221, 0.46), rgba(51, 241, 111, 0.69))",
  boxShadow: 5,
  flexGrow: 1,
  borderRadius: 3,
  paddingY: 2,
  position: "relative"
};

export const content: SxProps = {
  height: "88%",
  overflowY: "auto",
  paddingX: 3,
  "&::-webkit-scrollbar-thumb": {
    backgroundColor: "#39aebd"
  }
};

export const header: SxProps = {
  position: "sticky",
  top: 0,
  display: "flex",
  justifyContent: "space-between",
  alignItems: "center",
  paddingX: 2
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

export const answer: SxProps = {
  "&:not(:first-of-type)": {
    marginTop: 2
  }
};
export const answerHeader: SxProps = {
  display: "flex",
  flexDirection: "row",
  justifyContent: "space-between",
  alignItems: "center"
};

export const checkbox: SxProps = {
  color: "white",
  "&.Mui-checked": {
    color: "#349bae"
  }
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

export const imageAnswer: SxProps = {
  marginTop: 1,
  borderRadius: 3,
  width: "75%"
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

export const link: React.CSSProperties = {
  textDecoration: "none"
};

export const button: SxProps = {
  marginY: 2,
  borderRadius: 5,
  backgroundColor: "#39aebd",
  transition: "0.2s",
  "&:hover": {
    backgroundColor: "#39aebd",
    transform: "scale(1.1)"
  }
};
