import { SxProps } from "@mui/system";
import { colors } from "@Utils/Theme";

export const modal: SxProps = {
  width: "30%",
  position: "absolute",
  top: "50%",
  left: "50%",
  transform: "translate(-50%, -50%)",
  borderRadius: 5,
  padding: 3,
  backgroundColor: colors.drawer
};

export const form: SxProps = {
  height: "100%",
  display: "flex",
  flexDirection: "column",
  "& .MuiFormControl-root": {
    marginBottom: 3
  }
};

export const button: SxProps = {
  width: 100,
  marginTop: 2,
  color: "#39aebd",
  borderRadius: 2,
  border: "2px solid #39aebd",
  transition: "0.3s",
  "&:hover": {
    borderRadius: 2,
    border: "2px solid #39aebd",
    backgroundColor: "#39aebd",
    color: "#111827"
  },
  "&.Mui-disabled": {
    borderColor: "unset"
  }
};
