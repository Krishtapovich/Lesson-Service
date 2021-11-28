import { SxProps } from "@mui/system";
import { colors } from "@Utils/Theme";
import React from "react";

export const globalWrapper: SxProps = {
  height: "100%"
};

export const drawer: SxProps = {
  width: "18%",
  backgroundColor: colors.drawer
};

export const listItem = (isActive: boolean): SxProps => {
  return {
    backgroundColor: isActive ? "rgba(255,255,255, 0.08)" : "none",
    color: isActive ? colors.primary : "white",
    borderRadius: 2,
    width: "90%",
    margin: "auto",
    marginBottom: 1,
    "&:hover": {
      backgroundColor: "rgba(255,255,255, 0.08)"
    },
    "& svg": {
      color: isActive ? colors.primary : "white"
    }
  };
};

export const navLink: React.CSSProperties = {
  textDecoration: "none"
};

export const content: SxProps = {
  padding: 0,
  height: "100%",
  width: "82%",
  marginLeft: "18%",
  background:
    "linear-gradient(90deg, rgba(34, 72, 110, 1) 3%, rgba(57, 177, 191, 1) 49%, rgba(51, 181, 153, 1) 91%)",
  "& > div": {
    height: "92%",
    overflowY: "auto",
    "&::-webkit-scrollbar": {
      width: 8
    },
    "&::-webkit-scrollbar-thumb": {
      backgroundColor: "#8266c4",
      borderRadius: 3
    }
  }
};
