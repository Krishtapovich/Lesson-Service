import { SxProps } from "@mui/system";

export const loader: SxProps = {
  margin: "22% 45%"
};

export const content: SxProps = {
  display: "flex",
  flexDirection: "row",
  justifyContent: "space-between"
};

export const tableWrapper: SxProps = {
  width: "fit-content",
  height: "fit-content",
  maxHeight: "100%",
  flexGrow: 1,
  overflowY: "auto",
  marginRight: 7,
  borderRadius: 3,
  "&::-webkit-scrollbar": {
    width: 0
  }
};

export const charts: SxProps = {
  maxWidth: "50%"
};
