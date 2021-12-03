import { SxProps } from "@mui/system";

export const title: SxProps = {
  width: "35%"
};

export const questionsContainer: SxProps = {
  maxHeight: "90%"
};

export const questionWrapper: SxProps = {
  display: "flex",
  justifyContent: "space-between",
  alignItems: "center",
  width: "70%",
  marginTop: 2.5
};

export const question: SxProps = {
  width: "90%"
};

export const optionsContainer: SxProps = {
  width: "60%",
  maxHeight: 220,
  overflowY: "auto",
  marginY: 2,

  "&::-webkit-scrollbar-thumb": {
    backgroundColor: "#1c7483",
  }
};

export const optionWrapper: SxProps = {
  display: "flex",
  alignItems: "center",
  marginLeft: 5,
  marginY: 2
};

export const option: SxProps = {
  marginX: "5%",
  width: "60%"
};

export const deleteIcon: SxProps = {
  fontSize: 30,
  cursor: "pointer",
  color: "red",
  transition: "0.2s",
  "&:hover": {
    transform: "scale(1.3)"
  }
};

const addButton: SxProps = {
  borderRadius: 5,
  backgroundColor: "#4dce94",
  transition: "0.2s",
  "&:hover": {
    backgroundColor: "#4dce94",
    transform: "scale(1.1)"
  }
};

export const addOption: SxProps = {
  marginLeft: "60%",
  ...addButton
};

export const addQuestion: SxProps = {
  marginY: 2,
  ...addButton
};

export const save: SxProps = {
  marginTop: 3,
  width: 100,
  borderRadius: 5,
  backgroundColor: "#19d24b",
  transition: "0.2s",
  "&:hover": {
    backgroundColor: "#19d24b",
    transform: "scale(1.1)"
  }
};
