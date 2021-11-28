import { Checkbox as MuiCheckbox, FormControlLabel } from "@mui/material";
import { FieldHookConfig, useField } from "formik";
import { useState } from "react";

import { checkbox, control } from "./style";

interface Props {
  label: string;
}

function Checkbox(props: FieldHookConfig<string> & Props) {
  const [field] = useField(props);
  const [checked, setChecked] = useState(false);
  return (
    <FormControlLabel
      control={
        <MuiCheckbox
          inputProps={field}
          checked={checked}
          onClick={() => setChecked(!checked)}
          sx={checkbox}
        />
      }
      label={props.label}
      sx={control}
    />
  );
}

export default Checkbox;
