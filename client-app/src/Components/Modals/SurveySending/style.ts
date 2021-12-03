import styled from "@emotion/styled";
import { TextField } from "@mui/material";
import { SxProps } from "@mui/system";
import { colors } from "@Utils/Theme";

export const modal: SxProps = {
  width: 400,
  position: "absolute",
  display: "flex",
  flexDirection: "column",
  top: "50%",
  left: "50%",
  transform: "translate(-50%, -50%)",
  borderRadius: 5,
  padding: 3,
  backgroundColor: colors.drawer,
  color: "white"
};

export const title: SxProps = {
  fontSize: 22,
  fontWeight: 500
};

export const loader: SxProps = {
  marginY: 3,
  color: "#39aebd"
};

export const tableWrapper: SxProps = {
  marginTop: 1,
  marginBottom: 2,
  maxHeight: 250,
  borderRadius: 3,
  overflowY: "auto",

  "&::-webkit-scrollbar-thumb": {
    backgroundColor: "#39aebd",
  }
};

export const checkbox: SxProps = {
  color: "white",
  "&.Mui-checked": {
    color: "#32db82"
  }
};

export const tableHeaderCell: SxProps = {
  color: "white",
  backgroundColor: "#172034",
  "&:first-of-type": {
    borderRight: "1px solid white"
  }
};

export const tableBodyCell: SxProps = {
  color: "white",
  fontSize: 16,
  borderTop: "1px solid white",
  borderBottom: "none",
  "&:first-of-type": {
    borderRight: "1px solid white"
  }
};

export const sendButton: SxProps = {
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
  }
};

export const TextInput = styled(TextField)`
  & label {
    color: white;
    &.Mui-focused {
      color: #39aebd;
    }
  }

  & .MuiOutlinedInput-root {
    height: 60px;
    color: white;
    border-radius: 12px;

    fieldset {
      border: 2px solid white;
    }

    &:hover fieldset {
      border-color: white;
    }

    &.Mui-focused {
      border: none;
      fieldset {
        border-color: #39aebd;
      }
    }
  }
`;
