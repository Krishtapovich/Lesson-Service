import { SxProps } from "@mui/system";
import { colors } from "@Utils/Theme";

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

export const button: SxProps = {
  color: "turquoise",
  transition: "0.4s",
  "&:hover": {
    background: "rgba(64, 224, 208, 0.1)"
  }
};
