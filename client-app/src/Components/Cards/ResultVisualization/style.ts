import { SxProps } from "@mui/system";
import { colors } from "@Utils/Theme";
import React from "react";

export const card: SxProps = {
  background: "linear-gradient(90deg, rgba(29, 187, 221, 0.46), rgba(51, 241, 111, 0.69))",
  boxShadow: 5,
  flexGrow: 1,
  borderRadius: 3,
  padding: 2
};

export const contentWrapper: SxProps = {
  maxHeight: "100%",
  overflowY: "auto",

  "&::-webkit-scrollbar-thumb": {
    backgroundColor: "#39aebd",
  },
};

export const chartWrapper: SxProps = {
  maxWidth: "97%",
  overflowX: "auto",
  "&::-webkit-scrollbar": {
    height: 4,
    width: "auto",
  },
  "&::-webkit-scrollbar-thumb": {
    backgroundColor: "#39aebd",
  }
};

export const text: SxProps = {
  fontSize: 23,
  fontWeight: 600,
  color: "white",
  marginBottom: 1
};

export const tooltip: React.CSSProperties = {
  borderRadius: 10,
  backgroundColor: colors.drawer
};

export const tooltipText: React.CSSProperties = {
  color: "white",
  fontSize: 18,
  fontWeight: 500
};
