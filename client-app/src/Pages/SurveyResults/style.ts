import { SxProps } from "@mui/system";

export const loader:SxProps = {
  margin: "22% 45%"
}

export const tableWrapper: SxProps = {
  width: "fit-content",
  maxHeight: "100%",
  overflowY: "auto",
  borderRadius: 3,
  "&::-webkit-scrollbar": {
    width: 0
  }
};
