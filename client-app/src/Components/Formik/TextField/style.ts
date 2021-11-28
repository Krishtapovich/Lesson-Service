import styled from "@emotion/styled";
import { TextField } from "@mui/material";

export const TextInput = styled(TextField)`
  & label {
    color: white;
    &.Mui-focused {
      color: #39aebd;
    }
    &.Mui-error {
      color: #d32f2f;
    }
  }

  & .MuiOutlinedInput-root {
    border-radius: 12px;
    color: white;
    outline: none;
    background-color: #111827;

    input:-webkit-autofill,
    input:-webkit-autofill:hover,
    input:-webkit-autofill:focus,
    input:-webkit-autofill:active {
      -webkit-box-shadow: 0 0 0 30px #111827 inset !important;
      -webkit-text-fill-color: white !important;
      caret-color: white;
    }

    fieldset {
      color: white;
      background: none;
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

  & .Mui-error {
    &.Mui-focused fieldset,
    &:hover fieldset {
      border-color: #d32f2f;
    }
  }
`;
