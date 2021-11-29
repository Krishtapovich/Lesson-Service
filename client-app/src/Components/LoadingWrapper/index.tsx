import { CircularProgress, CircularProgressProps } from "@mui/material";
import React from "react";

import { loader } from "./style";

interface Props {
  isLoading: boolean;
}

function LoadingWrapper(props: React.PropsWithChildren<Props> & CircularProgressProps) {
  const { isLoading, sx, children, ...rest } = props;
  const style = { ...loader, ...sx };
  return isLoading ? <CircularProgress sx={style} {...rest} /> : (children as JSX.Element);
}

export default LoadingWrapper;
