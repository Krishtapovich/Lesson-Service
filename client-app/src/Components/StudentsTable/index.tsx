import StudentModel from "@Models/Student";
import * as MUI from "@mui/material";

import { button, tableBodyCell, tableHeaderCell } from "./style";

interface Props {
  students: Array<StudentModel>;
  resultsCallback: (student: StudentModel) => void;
}

function StudentsTable(props: Props) {
  const { students, resultsCallback } = props;

  return (
    <MUI.Table stickyHeader>
      <MUI.TableHead>
        <MUI.TableRow>
          <MUI.TableCell sx={tableHeaderCell}>
            <MUI.Typography>Group</MUI.Typography>
          </MUI.TableCell>
          <MUI.TableCell align="center" sx={tableHeaderCell}>
            <MUI.Typography>Last Name</MUI.Typography>
          </MUI.TableCell>
          <MUI.TableCell align="center" sx={tableHeaderCell}>
            <MUI.Typography>First Name</MUI.Typography>
          </MUI.TableCell>
          <MUI.TableCell sx={tableHeaderCell}></MUI.TableCell>
        </MUI.TableRow>
      </MUI.TableHead>
      <MUI.TableBody>
        {students.map((s) => (
          <MUI.TableRow key={s.id}>
            <MUI.TableCell sx={tableBodyCell}>
              <MUI.Typography>{s.groupNumber}</MUI.Typography>
            </MUI.TableCell>
            <MUI.TableCell align="center" sx={tableBodyCell}>
              <MUI.Typography>{s.lastName}</MUI.Typography>
            </MUI.TableCell>
            <MUI.TableCell align="center" sx={tableBodyCell}>
              <MUI.Typography>{s.firstName}</MUI.Typography>
            </MUI.TableCell>
            <MUI.TableCell align="center" sx={tableBodyCell}>
              <MUI.Button size="small" onClick={() => resultsCallback(s)} sx={button}>
                View Results
              </MUI.Button>
            </MUI.TableCell>
          </MUI.TableRow>
        ))}
      </MUI.TableBody>
    </MUI.Table>
  );
}

export default StudentsTable;
