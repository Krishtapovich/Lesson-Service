import Checkbox from "@Components/Formik/Checkbox";
import TextField from "@Components/Formik/TextField";
import { SurveyFormModel } from "@Models/Survey";
import DeleteIcon from "@mui/icons-material/Delete";
import { Box, Button } from "@mui/material";
import useStore from "@Stores";
import { FieldArray, Form, Formik, FormikHelpers } from "formik";
import { observer } from "mobx-react-lite";
import * as Yup from "yup";

import {
  addOption,
  addQuestion,
  creationPage,
  deleteIcon,
  option,
  optionsContainer,
  optionWrapper,
  question,
  questionWrapper,
  save,
  title,
} from "./style";

function SurveyCreationPage() {
  const { surveyStore } = useStore();

  const initialSurvey: SurveyFormModel = {
    title: "",
    questions: [
      {
        text: ""
      }
    ]
  };

  const schema = Yup.object({
    title: Yup.string().required("Title is required"),
    questions: Yup.array()
      .of(
        Yup.object().shape({
          text: Yup.string().required("Question text is required"),
          options: Yup.array()
            .of(
              Yup.object().shape({
                text: Yup.string().required("Option text is required"),
                isCorrect: Yup.boolean().notRequired()
              })
            )
            .test("", "", (value: any) => !value || !value.length || value.length > 1)
        })
      )
      .min(1)
  });

  const handleSubmit = (survey: SurveyFormModel, formikHelpers: FormikHelpers<SurveyFormModel>) => {
    surveyStore.addSurvey({ ...survey });
    formikHelpers.resetForm();
  };

  return (
    <Box sx={creationPage}>
      <Formik initialValues={initialSurvey} validationSchema={schema} onSubmit={handleSubmit}>
        {({ values, isValid, dirty }) => (
          <Form>
            <TextField name="title" label="Title" sx={title} />
            <FieldArray
              name="questions"
              render={(questions) => (
                <Box>
                  {values.questions.map((_, i) => (
                    <Box key={i}>
                      <Box sx={questionWrapper}>
                        <TextField
                          name={`questions[${i}].text`}
                          multiline
                          label="Question"
                          sx={question}
                        />
                        <DeleteIcon sx={deleteIcon} onClick={() => questions.remove(i)} />
                      </Box>
                      <FieldArray
                        name={`questions[${i}].options`}
                        render={(options) => (
                          <>
                            <Box sx={optionsContainer}>
                              {values.questions[i].options?.map((_, j) => (
                                <Box key={j} sx={optionWrapper}>
                                  <Checkbox
                                    name={`questions[${i}].options[${j}].isCorrect`}
                                    label="Is Correct"
                                  />
                                  <TextField
                                    name={`questions[${i}].options[${j}].text`}
                                    label="Option"
                                    multiline
                                    sx={option}
                                  />
                                  <DeleteIcon sx={deleteIcon} onClick={() => options.remove(j)} />
                                </Box>
                              ))}
                            </Box>
                            <Button
                              variant="contained"
                              sx={addOption}
                              onClick={() => options.push({})}
                            >
                              Add Option
                            </Button>
                          </>
                        )}
                      />
                    </Box>
                  ))}
                  <Button variant="contained" sx={addQuestion} onClick={() => questions.push({})}>
                    Add Question
                  </Button>
                </Box>
              )}
            />
            <Button sx={save} type="submit" variant="contained" disabled={!isValid || !dirty}>
              Save
            </Button>
          </Form>
        )}
      </Formik>
    </Box>
  );
}

export default observer(SurveyCreationPage);
