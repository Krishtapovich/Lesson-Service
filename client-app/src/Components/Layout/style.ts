import { SxProps, Theme } from "@mui/system";
import { colors } from "@Utils/Theme";
import React from "react";

export const drawer: SxProps<Theme> = {
  width: "18%",
  backgroundColor: colors.drawer
};

export const listItem = (isActive: boolean): SxProps<Theme> => {
  return {
    backgroundColor: isActive ? "rgba(255,255,255, 0.08)" : "none",
    color: isActive ? colors.primary : "white",
    borderRadius: 2,
    width: "90%",
    margin: "auto",
    marginBottom: 1,
    "&:hover": {
      backgroundColor: "rgba(255,255,255, 0.08)"
    }
  };
};

export const color = (isActive: boolean): SxProps<Theme> => {
  return {
    color: isActive ? colors.primary : "white"
  };
};

export const navLink: React.CSSProperties = {
  textDecoration: "none"
};

export const content: SxProps<Theme> = {
  width: "82%",
  marginLeft: "18%",
  minHeight: "100vh",
  background:
    "linear-gradient(90deg, rgba(34, 72, 110, 1) 3%, rgba(57, 177, 191, 1) 49%, rgba(51, 181, 153, 1) 91%)"
};
