import Checkbox from "@Components/Formik/Checkbox";
import TextField from "@Components/Formik/TextField";
import { QuestionCreateModel } from "@Models/Question";
import { SurveyFormModel } from "@Models/Survey";
import DeleteIcon from "@mui/icons-material/Delete";
import { Box, Button, Typography } from "@mui/material";
import useStore from "@Stores";
import { FieldArray, Form, Formik, FormikErrors, FormikHelpers } from "formik";
import { observer } from "mobx-react-lite";
import { useState } from "react";
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
            .test(
              "test-min-amount",
              "Questions with options should have at least 2 options",
              async (value: any) => !value || !value.length || value.length > 1
            )
            .test(
              "test-max-amount",
              "Questions with options can have 10 options",
              async (value: any) => !value || !value.length || value.length < 11
            )
            .test(
              "test-isCorrect",
              "One option should be marked as correct",
              async (value: any) =>
                !value || !value.length || value.filter((o: any) => o.isCorrect).length === 1
            )
        })
      )
      .min(1, "Survey should have at least one question")
  });

  const handleSubmit = async (survey: SurveyFormModel, helpers: FormikHelpers<SurveyFormModel>) => {
    surveyStore.addSurvey(survey);
    helpers.resetForm();
  };

  const [errors, setErrors] = useState<FormikErrors<SurveyFormModel>>();

  const showQuestionsErrors = () => {
    const questions = errors?.questions;
    return (
      typeof questions === "string" && <Typography sx={style.errorText}>{questions}</Typography>
    );
  };

  const showOptionsErrors = (i: number) => {
    const questions = errors?.questions as Array<FormikErrors<QuestionCreateModel>>;
    if (questions) {
      const optionErrors = questions[i]?.options;
      return (
        typeof optionErrors === "string" && (
          <Typography sx={style.errorText}>{optionErrors}</Typography>
        )
      );
    }
  };

  return (
    <Box>
      <Formik
        initialValues={initialSurvey}
        validationSchema={schema}
        validateOnChange={false}
        onSubmit={handleSubmit}
      >
        {({ values, validateForm }) => (
          <Form noValidate>
            <TextField name="title" label="Title" sx={style.title} />
            <FieldArray name="questions">
              {({ push, remove }) => (
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
                        <DeleteIcon sx={style.deleteIcon} onClick={() => remove(i)} />
                      </Box>
                      <FieldArray name={`questions[${i}].options`}>
                        {({ push, remove }) => (
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
                                  <DeleteIcon sx={style.deleteIcon} onClick={() => remove(j)} />
                                </Box>
                              ))}
                            </Box>
                            {showOptionsErrors(i)}
                            <Button
                              variant="contained"
                              sx={style.addOption}
                              onClick={() => push({ text: "" })}
                            >
                              Add Option
                            </Button>
                          </>
                        )}
                      </FieldArray>
                    </Box>
                  ))}
                  <Button
                    variant="contained"
                    sx={style.addQuestion}
                    onClick={() => push({ text: "" })}
                  >
                    Add Question
                  </Button>
                </Box>
              )}
            </FieldArray>
            {showQuestionsErrors()}
            <Button
              sx={style.save}
              type="submit"
              onClick={() => validateForm(values).then((errors) => errors && setErrors(errors))}
              variant="contained"
            >
              Save
            </Button>
          </Form>
        )}
      </Formik>
    </Box>
  );
}

export default observer(SurveyCreationPage);
