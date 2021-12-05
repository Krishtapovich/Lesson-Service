import TextField from "@Components/Formik/TextField";
import StudentModel from "@Models/Student";
import { Box, Button, Modal } from "@mui/material";
import { Form, Formik } from "formik";
import * as Yup from "yup";

import { button, form, modal } from "./style";

interface Props {
  isOpen: boolean;
  student: StudentModel;
  handleClose: () => void;
  updateCallback: (student: StudentModel) => void;
}

function UpdateStudentModal(props: Props) {
  const { isOpen, student, handleClose, updateCallback } = props;

  const schema = Yup.object().shape({
    firstName: Yup.string().required("Student first name is required"),
    lastName: Yup.string().required("Student last name is required"),
    groupNumber: Yup.string().required("Student group number is required")
  });

  const onSubmit = (student: StudentModel) => {
    updateCallback(student);
    handleClose();
  };

  return (
    <Modal open={isOpen} onClose={handleClose}>
      <Box sx={modal}>
        <Formik initialValues={student} onSubmit={onSubmit} validationSchema={schema}>
          {({ isValid }) => (
            <Form>
              <Box sx={form}>
                <TextField name="firstName" label="First name" multiline />
                <TextField name="lastName" label="Last name" multiline />
                <TextField name="groupNumber" label="Group number" />
                <Button type="submit" sx={button} disabled={!isValid}>
                  Update
                </Button>
              </Box>
            </Form>
          )}
        </Formik>
      </Box>
    </Modal>
  );
}

export default UpdateStudentModal;
