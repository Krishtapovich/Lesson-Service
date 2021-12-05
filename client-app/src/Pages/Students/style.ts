import { SxProps } from "@mui/system";
import { colors } from "@Utils/Theme";

export const loader: SxProps = {
  margin: "22% 45%"
};

export const wrapper: SxProps = {
  borderRadius: 3,
  overflowY: "auto",
  maxHeight: "100%",
  "&::-webkit-scrollbar": {
    width: 0
  }
};

export const tableHeaderCell: SxProps = {
  color: "white",
  backgroundColor: "#172034",
  "&:not(:last-of-type)": {
    borderRight: "1px solid white"
  }
};

export const tableBodyCell: SxProps = {
  color: "white",
  backgroundColor: colors.drawer,
  borderRight: "none"
};

export const buttonCell: SxProps = {
  backgroundColor: colors.drawer,
  display: "flex",
  justifyContent: "space-around"
};

export const updateButton: SxProps = {
  color: "orange"
};

export const deleteButton: SxProps = {
  transition: "0.4s",
  color: "red",
  "&:hover": {
    background: "rgba(255, 0, 0, 0.1)"
  }
};
