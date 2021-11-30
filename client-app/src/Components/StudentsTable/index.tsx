import StudentModel from "@Models/Student";
import {
  Button,
  Table,
  TableBody,
  TableCell,
  TableHead,
  TableRow,
  Typography
} from "@mui/material";

import { button, tableBodyCell, tableHeaderCell } from "./style";

interface Props {
  students: Array<StudentModel>;
  resultsCallback: (studentId: number) => void;
}

function StudentsTable(props: Props) {
  const { students, resultsCallback } = props;

  return (
    <Table stickyHeader>
      <TableHead>
        <TableRow>
          <TableCell sx={tableHeaderCell}>
            <Typography>Group</Typography>
          </TableCell>
          <TableCell align="center" sx={tableHeaderCell}>
            <Typography>First Name</Typography>
          </TableCell>
          <TableCell align="center" sx={tableHeaderCell}>
            <Typography>Last Name</Typography>
          </TableCell>
          <TableCell sx={tableHeaderCell}></TableCell>
        </TableRow>
      </TableHead>
      <TableBody>
        {students.map((s) => (
          <TableRow key={s.id}>
            <TableCell sx={tableBodyCell}>
              <Typography>{s.groupNumber}</Typography>
            </TableCell>
            <TableCell align="center" sx={tableBodyCell}>
              <Typography>{s.firstName}</Typography>
            </TableCell>
            <TableCell align="center" sx={tableBodyCell}>
              <Typography>{s.lastName}</Typography>
            </TableCell>
            <TableCell sx={tableBodyCell}>
              <Button size="small" onClick={() => resultsCallback(s.id)} sx={button}>
                View Results
              </Button>
            </TableCell>
          </TableRow>
        ))}
      </TableBody>
    </Table>
  );
}

export default StudentsTable;
