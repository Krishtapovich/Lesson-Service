import { SxProps } from "@mui/system";

export const card: SxProps = {
  borderRadius: 3,
  background: "linear-gradient(90deg, rgba(29, 187, 221, 0.46), rgba(51, 241, 111, 0.69))",
  boxShadow: 5,
  position: "relative"
};

export const header: SxProps = {
  position: "sticky",
  top: 16,
  display: "flex",
  justifyContent: "space-between",
  alignItems: "center",
  paddingX: 2,
  marginBottom: 2
};

export const loader: SxProps = {
  position: "absolute",
  top: "45%",
  left: "45%"
};

export const content: SxProps = {
  height: "85%",
  paddingX: 2,
  overflow: "auto",

  "&::-webkit-scrollbar-thumb": {
    backgroundColor: "#39aebd"
  }
};

export const sendButton: SxProps = {
  position: "sticky",
  top: 16,
  width: 100,
  height: 40,
  borderRadius: 5,
  backgroundColor: "#39aebd",
  transition: "0.2s",
  "&:hover": {
    backgroundColor: "#39aebd",
    transform: "scale(1.1)"
  }
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

export const option = (isCorrect: boolean): SxProps => {
  return {
    marginLeft: 5,
    display: "flex",
    alignItems: "center",
    color: isCorrect ? "#3dff00" : "white",
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
