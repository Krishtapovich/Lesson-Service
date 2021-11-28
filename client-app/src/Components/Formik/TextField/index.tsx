import { TextFieldProps } from "@mui/material";
import { FieldHookConfig, useField } from "formik";
import { TextInput } from "./style";

function TextField(props: FieldHookConfig<string> & TextFieldProps) {
  const [field, meta] = useField(props);
  const error = meta.touched ? meta.error : undefined;
  return <TextInput error={!!error} helperText={error} InputProps={field} {...props} />;
}

export default TextField;
