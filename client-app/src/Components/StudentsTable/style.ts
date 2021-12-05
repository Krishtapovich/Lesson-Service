import { SxProps } from "@mui/system";
import { colors } from "@Utils/Theme";
import React from "react";

export const tableHeaderCell: SxProps = {
  color: "white",
  backgroundColor: "#172034",
  "&:not(:last-of-type)": {
    borderRight: "1px solid white"
  }
};

export const link: React.CSSProperties = {
  textDecoration: "none"
};

export const tableBodyCell: SxProps = {
  color: "white",
  backgroundColor: colors.drawer,
  borderRight: "none"
};

export const button: SxProps = {
  color: "turquoise",
  flexShrink: 0,
  fontSize: 13,
  transition: "0.4s",
  "&:hover": {
    background: "rgba(64, 224, 208, 0.1)"
  }
};
