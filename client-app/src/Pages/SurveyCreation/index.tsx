import Checkbox from "@Components/Formik/Checkbox";
import TextField from "@Components/Formik/TextField";
import { SurveyFormModel } from "@Models/Survey";
import DeleteIcon from "@mui/icons-material/Delete";
import { Box, Button } from "@mui/material";
import useStore from "@Stores";
import { FieldArray, Form, Formik, FormikHelpers } from "formik";
import { observer } from "mobx-react-lite";
import * as Yup from "yup";

import * as style from "./style";

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
    surveyStore.addSurvey(survey);
    formikHelpers.resetForm();
  };

  return (
    <Box>
      <Formik initialValues={initialSurvey} validationSchema={schema} onSubmit={handleSubmit}>
        {({ values, isValid, dirty }) => (
          <Form>
            <TextField name="title" label="Title" sx={style.title} />
            <FieldArray
              name="questions"
              render={(questions) => (
                <Box>
                  {values.questions.map((_, i) => (
                    <Box key={i}>
                      <Box sx={style.questionWrapper}>
                        <TextField
                          name={`questions[${i}].text`}
                          multiline
                          label="Question"
                          sx={style.question}
                        />
                        <DeleteIcon sx={style.deleteIcon} onClick={() => questions.remove(i)} />
                      </Box>
                      <FieldArray
                        name={`questions[${i}].options`}
                        render={(options) => (
                          <>
                            <Box sx={style.optionsContainer}>
                              {values.questions[i].options?.map((_, j) => (
                                <Box key={j} sx={style.optionWrapper}>
                                  <Checkbox
                                    name={`questions[${i}].options[${j}].isCorrect`}
                                    label="Is Correct"
                                  />
                                  <TextField
                                    name={`questions[${i}].options[${j}].text`}
                                    label="Option"
                                    multiline
                                    sx={style.option}
                                  />
                                  <DeleteIcon
                                    sx={style.deleteIcon}
                                    onClick={() => options.remove(j)}
                                  />
                                </Box>
                              ))}
                            </Box>
                            <Button
                              variant="contained"
                              sx={style.addOption}
                              onClick={() => options.push({})}
                            >
                              Add Option
                            </Button>
                          </>
                        )}
                      />
                    </Box>
                  ))}
                  <Button
                    variant="contained"
                    sx={style.addQuestion}
                    onClick={() => questions.push({})}
                  >
                    Add Question
                  </Button>
                </Box>
              )}
            />
            <Button sx={style.save} type="submit" variant="contained" disabled={!isValid || !dirty}>
              Save
            </Button>
          </Form>
        )}
      </Formik>
    </Box>
  );
}

export default observer(SurveyCreationPage);
